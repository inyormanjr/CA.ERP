using CA.ERP.WebApp.Dto;
using CA.ERP.WebApp.Test.Integration.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
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
            getBranchResponse.Branches.Should().HaveCount(c => c > 1);
        }

    }
}
