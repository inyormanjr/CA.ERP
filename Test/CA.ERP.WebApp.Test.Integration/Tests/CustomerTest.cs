using Bogus;
using CA.ERP.DataAccess;
using CA.ERP.WebApp.Dto;
using CA.ERP.WebApp.Test.Integration.Fixtures;
using CA.ERP.WebApp.Test.Integration.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.WebApp.Test.Integration.Tests
{
    public class CustomerTest : IClassFixture<CustomWebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;


        public CustomerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        public async Task InitializeAsync()
        {
            await _client.AddAuthorizationHeader();
        }

        public Task DisposeAsync()
        {
            return Task.FromResult(0);
        }

        [Fact]
        public async Task ShouldCreateCustomerSuccess_Ok()
        {
            var fake = new Faker();
            var data = new Dto.Customer.CustomerCreate()
            {
                FirstName = fake.Person.FirstName,
                MiddleName = fake.Person.LastName,
                LastName = fake.Person.LastName,
                Address = fake.Person.Address.City.ToString(),
                Employer = fake.Person.FullName,
                EmployerAddress = fake.Person.Address.City.ToString(),
                CoMaker = fake.Person.FullName,
                CoMakerAddress = fake.Person.Address.City.ToString(),
            };

            var request = new Dto.Customer.CreateCustomerRequest()
            {
               Data = data
            };


            var response = await _client.PostAsJsonAsync("api/Customer/", request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var createContent = await response.Content.ReadAsAsync<CreateResponse>();

            createContent.Should().NotBeNull();
            createContent.Id.Should().NotBe(Guid.Empty);
            using (var scope = _factory.Services.CreateScope())
            {
               var dbContext = scope.ServiceProvider.GetService<CADataContext>();

               var customer = dbContext.Customers.FirstOrDefault(c => c.Id == createContent.Id);
               customer.Should().NotBeNull();
               
            }
        }


        [Fact]
        public async Task ShouldUpdateCustomerSuccess_NoContent()
        {
            Guid customerId;
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CADataContext>();
                var random = new Random();
                customerId = dbContext.Customers.ToList().OrderBy(c => random.Next()).FirstOrDefault()?.Id ?? Guid.Empty;
            }

            var fake = new Faker();
            var data = new Dto.Customer.CustomerUpdate()
            {
                FirstName = fake.Person.FirstName,
                MiddleName = fake.Person.LastName,
                LastName = fake.Person.LastName,
                Address = fake.Person.Address.City.ToString(),
                Employer = fake.Person.FullName,
                EmployerAddress = fake.Person.Address.City.ToString(),
                CoMaker = fake.Person.FullName,
                CoMakerAddress = fake.Person.Address.City.ToString(),
            };

            var request = new Dto.UpdateBaseRequest<Dto.Customer.CustomerUpdate>()
            {
                Data = data
            };


            var response = await _client.PutAsJsonAsync($"api/Customer/{customerId}", request);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CADataContext>();
               var customer = dbContext.Customers.FirstOrDefault(c => c.Id == customerId);
               customer.Should().NotBeNull();

               customer.FirstName.Should().Be(data.FirstName);
               customer.MiddleName.Should().Be(data.MiddleName);
               customer.LastName.Should().Be(data.LastName);
               
            }
        }

        [Fact]
        public async Task ShouldSearchCustomerSuccess_Ok()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CADataContext>();
                var random = new Random();
                var customer = dbContext.Customers.ToList().OrderBy(c => random.Next()).FirstOrDefault();
            
                var searchForFirstname = customer.FirstName.Substring(0, 3);
                var searchForLastname = customer.LastName.Substring(0, 3);
                var response = await _client.GetAsync($"api/Customer?firstname={searchForFirstname}&lastname={searchForLastname}");

                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var content =  await response.Content.ReadAsAsync<GetManyResponse<Dto.Customer.CustomerView>>();
                content.Data.Count().Should().BeGreaterThan(0);
                content.Data.Should().OnlyContain(c => c.FirstName.StartsWith(searchForFirstname) && c.LastName.StartsWith(searchForLastname));
            }
        }

        [Fact]
        public async Task ShouldGetOneCustomerSuccess_Ok()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CADataContext>();
                var random = new Random();
                var customer = dbContext.Customers.ToList().OrderBy(c => random.Next()).FirstOrDefault();
            
                var response = await _client.GetAsync($"api/Customer/{customer.Id}");

                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var content =  await response.Content.ReadAsAsync<Dto.Customer.CustomerView>();
                content.LastName.Should().Be(customer.LastName);
                content.FirstName.Should().Be(customer.FirstName);
                content.Address.Should().Be(customer.Address);
                content.CoMaker.Should().Be(customer.CoMaker);
                content.Id.Should().Be(customer.Id);
            }
        }

        [Fact]
        public async Task ShouldDeleteCustomerSuccess_NoContent()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CADataContext>();
                var random = new Random();
                var customer = dbContext.Customers.AsNoTracking().ToList().OrderBy(c => random.Next()).FirstOrDefault();
            
            
                var response = await _client.DeleteAsync($"api/Customer/{customer.Id}");

                response.StatusCode.Should().Be(HttpStatusCode.NoContent);

                var deletedCustomer =dbContext.Customers.FirstOrDefault(c => c.Id == customer.Id);
                deletedCustomer.Should().NotBeNull();
                deletedCustomer.Status.Should().Be(Status.Inactive);
            }
            
        }

    }
}
