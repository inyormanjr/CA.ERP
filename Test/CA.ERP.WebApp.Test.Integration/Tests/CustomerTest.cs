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

    }
}
