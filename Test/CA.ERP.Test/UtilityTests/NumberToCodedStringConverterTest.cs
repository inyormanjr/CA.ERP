using CA.ERP.Domain.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.Test.UtilityTests
{
    public class NumberToCodedStringConverterTest
    {
        [Fact]
        public void ShouldConvertCorrect()
        {
            //arrange
            var sut = new NumberToCodedStringConverter();

            //act
            var code = sut.Covert(1);

            //assert
            code.Should().Be(sut.AvailableStrings[1].ToString());
        }
    }
}
