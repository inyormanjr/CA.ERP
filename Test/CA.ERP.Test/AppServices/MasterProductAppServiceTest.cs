using CA.ERP.Application.Services;
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

namespace CA.ERP.Test.AppServices
{
    public class MasterProductAppServiceTest
    {
        [Fact]
        public async Task Should_Create_MasterProduct_Success()
        {
            //arrage
            var unitOfWork = A.Fake<IUnitOfWork>();
            IMasterProductRepository masterProductRepository = A.Fake<IMasterProductRepository>();

            MasterProductAppService sut = new MasterProductAppService(unitOfWork, masterProductRepository);

            //act
            var result = await sut.CreateMasterProduct("m160", "no idea", Guid.NewGuid(),default);

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

            MasterProductAppService sut = new MasterProductAppService(unitOfWork, masterProductRepository);

            //act
            var result = await sut.CreateMasterProduct("", "no idea", Guid.NewGuid(), default);

            //assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();

            A.CallTo(() => unitOfWork.CommitAsync(default)).MustNotHaveHappened();
        }
    }
}
