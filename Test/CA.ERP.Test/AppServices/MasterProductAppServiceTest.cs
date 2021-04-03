using CA.ERP.Application.Services;
using CA.ERP.Domain.MasterProductAgg;
using CA.ERP.Domain.UnitOfWorkAgg;
using FakeItEasy;
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
            await sut.CreateMasterProduct("m160", "no idea", Guid.NewGuid(),default);

            //assert
            A.CallTo(() => unitOfWork.CommitAsync(default)).MustHaveHappened();
        }
    }
}
