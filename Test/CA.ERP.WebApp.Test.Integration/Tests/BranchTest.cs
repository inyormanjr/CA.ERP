using Bogus;
using CA.ERP.WebApp.Dto;
using CA.ERP.WebApp.Test.Integration.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
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
    public class BranchTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public BranchTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        }

        [Fact]
        public async Task ShouldGetAtleastOneBranch()
        {
            var response = await _client.GetAsync("api/Branch/");

            var getBranchResponse = await response.Content.ReadAsAsync<GetBranchResponse>();

            response.IsSuccessStatusCode.Should().BeTrue();
            getBranchResponse.Should().NotBeNull();
            getBranchResponse.Branches.Should().HaveCount(c => c >= 1);
        }

        [Fact]
        public async Task ShouldCreateBranchSuccessfully()
        {
            var fakeBranchGenerator = new Faker<Branch>()
                .CustomInstantiator(f => new Branch() { Id = null })
                .RuleFor(f => f.Name, f => f.Address.City())
                .RuleFor(f => f.BranchNo, f => f.PickRandom<int>(1, 2, 3, 4, 5))
                .RuleFor(f => f.Code, f => f.PickRandom<int>(1, 2, 3, 4, 5).ToString("00000"))
                .RuleFor(f => f.Address, f => f.Address.StreetAddress())
                .RuleFor(f => f.Contact, f => f.Name.FullName());

            //add branch for testing
            var branch = fakeBranchGenerator.Generate();

            var request = new CreateBranchRequest() {
                Branch = branch
            };
            

            var response = await _client.PostAsJsonAsync<CreateBranchRequest>("api/Branch/", request);

            response.IsSuccessStatusCode.Should().BeTrue($"Status code was {response.StatusCode}");

            var createBranchResponse = await response.Content.ReadAsAsync<CreateBranchResponse>();

            createBranchResponse.Should().NotBeNull();
            createBranchResponse.Branch.Should().NotBeNull();
            createBranchResponse.Branch.Name.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task ShouldUpdateBranch_NoContent()
        {
            var fakeBranchGenerator = new Faker<Branch>()
                .CustomInstantiator(f => new Branch() { Id = null })
                .RuleFor(f => f.Name, f => f.Address.City())
                .RuleFor(f => f.BranchNo, f => f.PickRandom<int>(1, 2, 3, 4, 5))
                .RuleFor(f => f.Code, f => f.PickRandom<int>(1, 2, 3, 4, 5).ToString("00000"))
                .RuleFor(f => f.Address, f => f.Address.StreetAddress())
                .RuleFor(f => f.Contact, f => f.Name.FullName());

            //add branch for testing
            var branch = fakeBranchGenerator.Generate();

            //id is static from utilities
            branch.Id = "56e5e4fc-c583-4186-a288-55392a6946d4";
            var request = new UpdateBranchRequest()
            {
                Branch = branch
            };


            var response = await _client.PutAsJsonAsync<UpdateBranchRequest>($"api/Branch/{branch.Id}", request);

            response.IsSuccessStatusCode.Should().BeTrue($"Status code was {response.StatusCode}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldUpdateBranch_NotFound()
        {
            var fakeBranchGenerator = new Faker<Branch>()
                .CustomInstantiator(f => new Branch() { Id = null })
                .RuleFor(f => f.Name, f => f.Address.City())
                .RuleFor(f => f.BranchNo, f => f.PickRandom<int>(1, 2, 3, 4, 5))
                .RuleFor(f => f.Code, f => f.PickRandom<int>(1, 2, 3, 4, 5).ToString("00000"))
                .RuleFor(f => f.Address, f => f.Address.StreetAddress())
                .RuleFor(f => f.Contact, f => f.Name.FullName());

            //add branch for testing
            var branch = fakeBranchGenerator.Generate();

            //id is static from utilities
            branch.Id = "56e5e4fc-c583-4186-a288";
            var request = new UpdateBranchRequest()
            {
                Branch = branch
            };


            var response = await _client.PutAsJsonAsync<UpdateBranchRequest>($"api/Branch/{branch.Id}", request);

            response.IsSuccessStatusCode.Should().BeFalse($"Status code was {response.StatusCode}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound, "Id should not exist");

        }

        [Fact]
        public async Task ShouldDeleteBranch_NoContent()
        {

            //id is static from utilities
            var branchId = "56e5e4fc-c583-4186-a288-55392a6946d4";


            var response = await _client.DeleteAsync($"api/Branch/{branchId}");

            response.IsSuccessStatusCode.Should().BeTrue($"Status code was {response.StatusCode}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ShouldDeleteBranch_NotFound()
        {

            //id is static from utilities
            var branchId = "56e5e4fc-c583-4186-a288";


            var response = await _client.DeleteAsync($"api/Branch/{branchId}");

            response.IsSuccessStatusCode.Should().BeFalse($"Status code was {response.StatusCode}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
