using CA.ERP.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.EFMapping
{
    public class StockMoveMapping : IEntityTypeConfiguration<StockMove>
    {
        public void Configure(EntityTypeBuilder<StockMove> builder)
        {
            builder.HasKey(t => new { t.BranchId, t.MasterProductId });

            builder.Property(t => t.PreviousQuantity)
                .HasPrecision(18, 3);
            builder.Property(t => t.ChangeQuantity)
                .HasPrecision(18, 3);
            builder.Property(t => t.CurrentQuantity)
                .HasPrecision(18, 3);

            builder.HasOne(t => t.StockInventory)
                .WithMany(t => t.StockMoves)
                .HasForeignKey(t => new { t.BranchId, t.MasterProductId })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.StockReceive)
                .WithMany(t => t.StockMoves)
                .HasForeignKey(t => t.StockReceiveId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
