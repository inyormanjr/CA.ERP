using CA.ERP.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CA.ERP.DataAccess.EFMapping
{
  public class CustomerMapping : IEntityTypeConfiguration<Customer>
  {
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => t.FirstName);
        builder.HasIndex(t => t.LastName);

        builder.Property(t => t.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(t => t.MiddleName).HasMaxLength(50);
        builder.Property(t => t.LastName).IsRequired().HasMaxLength(50);
        builder.Property(t => t.Address).HasMaxLength(150);
        builder.Property(t => t.Employer).HasMaxLength(50);
        builder.Property(t => t.EmployerAddress).HasMaxLength(50);
        builder.Property(t => t.CoMaker).HasMaxLength(50);
        builder.Property(t => t.CoMakerAddress).HasMaxLength(50);
    }
  }
}