using CA.ERP.Application.CommandQuery.MasterProductCommandQuery.CreateMasterProduct;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.MasterProductAgg;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.Test.MasterProductTests
{
    public class CreateMasterProducCommandTest
    {
        [Fact]
        public async Task Should_Create_MasterProduct_Success()
        {
            //arrage
            var unitOfWork = A.Fake<IUnitOfWork>();
            IMasterProductRepository masterProductRepository = A.Fake<IMasterProductRepository>();

            CreateMasterProductHandler sut = new CreateMasterProductHandler(unitOfWork, masterProductRepository);
            var command = new CreateMasterProductCommand("m160", "no idea", Guid.NewGuid());
            //act
            var result = await sut.Handle(command, default);

            //assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();

            A.CallTo(() => unitOfWork.CommitAsync(default)).MustHaveHappened();
        }


        [Fact]
        public async Task Should_Create_MasterProduct_Fail()
        {
            //arrage
            var unitOfWork = A.Fake<IUnitOfWork>();
            IMasterProductRepository masterProductRepository = A.Fake<IMasterProductRepository>();

            CreateMasterProductHandler sut = new CreateMasterProductHandler(unitOfWork, masterProductRepository);
            var command = new CreateMasterProductCommand("", "no idea", Guid.NewGuid());
            //act
            var result = await sut.Handle(command, default);



            //assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();

            A.CallTo(() => unitOfWork.CommitAsync(default)).MustNotHaveHappened();
        }
    }
}
