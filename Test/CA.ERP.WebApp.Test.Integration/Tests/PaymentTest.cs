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
    public class PaymentTest : IClassFixture<CustomWebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;


        public PaymentTest(CustomWebApplicationFactory<Startup> factory)
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
        public async Task ShouldCreateCashSuccess_Ok()
        {
            //Guid branchId;
            //using (var scope = _factory.Services.CreateScope())
            //{
            //    var dbContext = scope.ServiceProvider.GetService<CADataContext>();
            //    branchId = dbContext.Users.Where(u => u.Username == "ExistingUser").SelectMany(u => u.UserBranches).FirstOrDefault()?.BranchId ?? Guid.Empty;
            //}

            //var data = new Dto.Payment.PaymentCreate()
            //{
            //    BranchId = branchId,
            //    OfficialReceiptNumber = Guid.NewGuid().ToString(),
            //    PaymentType = Domain.PaymentAgg.PaymentType.Installment,
            //    PaymentMethod = Domain.PaymentAgg.PaymentMethod.Cash,
            //    PaymentDate = DateTime.Now,
            //    GrossAmount = 1000,
            //    Rebate = 100,
            //    Interest = 200,
            //    Discount = 50,
            //    Remarks = "Test Cash Payment",
            //    TenderAmount = 1500
            //};

            //var request = new Dto.Payment.CreatePaymentRequest()
            //{
            //    Data = data
            //};


            //var response = await _client.PostAsJsonAsync("api/Payment/", request);

            //response.StatusCode.Should().Be(HttpStatusCode.OK);

            //var createContent = await response.Content.ReadAsAsync<CreateResponse>();

            //createContent.Should().NotBeNull();
            //createContent.Id.Should().NotBe(Guid.Empty);
            //using (var scope = _factory.Services.CreateScope())
            //{
            //    var dbContext = scope.ServiceProvider.GetService<CADataContext>();

            //    var dalPayment = dbContext.Payments.Include(p => p.CashPaymentDetail).FirstOrDefault(p => p.Id == createContent.Id);
            //    dalPayment.Should().NotBeNull();
            //    dalPayment.NetAmount.Should().Be(1050);
            //    dalPayment.CashPaymentDetail.Should().NotBeNull();
            //    dalPayment.CashPaymentDetail.Change.Should().Be(450);
            //}
        }

    }
}
