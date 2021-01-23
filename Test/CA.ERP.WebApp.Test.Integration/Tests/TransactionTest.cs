using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CA.ERP.DataAccess;
using CA.ERP.WebApp.Dto;
using CA.ERP.WebApp.Test.Integration.Fixtures;
using CA.ERP.WebApp.Test.Integration.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CA.ERP.WebApp.Test.Integration.Tests
{
    public class TransactionTest :  IClassFixture<CustomWebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;


        public TransactionTest(CustomWebApplicationFactory<Startup> factory)
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

        public async Task ShouldCreateTransactionSuccess()
        {
            Guid branchId;
            Guid salesmanId;
            Guid investigatedById;
            CA.ERP.DataAccess.Entities.Stock stock;
            using (var scope = _factory.Services.CreateScope())
            {
                var random = new Random();
                var dbContext = scope.ServiceProvider.GetService<CADataContext>();
                branchId = dbContext.Users.Where(u => u.Username == "ExistingUser").SelectMany(u => u.UserBranches).FirstOrDefault()?.BranchId ?? Guid.Empty;
                salesmanId = dbContext.Users.Where(u => u.Role == Domain.UserAgg.UserRole.Salesman).Select(u => u.Id).OrderBy(id => random.Next()).FirstOrDefault();
                investigatedById = dbContext.Users.Where(u => u.Role == Domain.UserAgg.UserRole.CreditInvestigation).Select(u => u.Id).OrderBy(id => random.Next()).FirstOrDefault();
                
                int stockCount = dbContext.Stocks.Count();
                int skip = random.Next(stockCount -1);
                stock = dbContext.Stocks.Skip(skip).FirstOrDefault();
            }

            var product = new Dto.Transaction.TransactionProductCreate() {
                StockId = stock.Id,
                DownPaymentOfficialReceiptNumber = "195025",
                SalePrice = stock.CostPrice * 1.10m, //10% mark up from cost price
                Quantity = 1,
                DownPayment = 4998,
                Remarks = "A Remark",

            };

            var data = new Dto.Transaction.TransactionCreate()
            {
               BranchId = branchId,
               TransactionType = Domain.TransactionAgg.TransactionType.DeliveryReceipt,
               InterestType = Domain.TransactionAgg.InterestType.NoInterest,
               TransactionDate = DateTime.Now,
               DeliveryDate = DateTime.Now.AddDays(1),
               TransactionNumber = "05539",
               SalesmanId = salesmanId,
               InvenstigatedById = investigatedById,
               Total = 24990,
               Balance = 19992,
               UDI = 0,
               TotalRebate = 480,
               PN = 20472,
               Terms = 12,
               GrossMonthly = 1706,
               RebateMonthly = 40,
               NetMonthly = 1666,
               Products = new List<Dto.Transaction.TransactionProductCreate>(){
                   product
               }
            };


            var request = new Dto.Transaction.CreateTransactionRequest()
            {
               Data = data
            };


            var response = await _client.PostAsJsonAsync("api/Transaction/", request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var createContent = await response.Content.ReadAsAsync<CreateResponse>();

            createContent.Should().NotBeNull();
            createContent.Id.Should().NotBe(Guid.Empty);
            using (var scope = _factory.Services.CreateScope())
            {
               var dbContext = scope.ServiceProvider.GetService<CADataContext>();

               var dalTransaction = dbContext.Transactions.Include(t => t.Products).FirstOrDefault(p => p.Id == createContent.Id);
               dalTransaction.Should().NotBeNull();
               dalTransaction.Total.Should().Be(data.Total);
               dalTransaction.PN.Should().Be(data.PN);
               dalTransaction.Products.Should().NotBeNull();
               dalTransaction.Products.Should().OnlyContain(p => p.StockId == product.StockId);
            }
        }
    }
}