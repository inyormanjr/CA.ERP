using CA.ERP.Domain.Helpers;
using CA.ERP.Domain.UserAgg;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace CA.ERP.Test.Unit
{
    public class UserRoleToListTest
    {
        [Fact]
        public void ShouldReturnTwoItems()
        {
            UserRole userRole = UserRole.Admin | UserRole.Marketing;
            //convert to list here
            ICollection<string> actual = new EnumFlagsHelper().ConvertToList(userRole);

            ICollection<string> expected = new List<String>() { "Admin", "Marketing" };

            actual.Should().HaveCount(2)
                .And.Contain("Admin")
                .And.Contain("Marketing")
                .And.NotContain("Cashier");
        }
    }
}
