using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.EFCore.Mappings
{
    public class InventoryMapping : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventory");
            builder.HasKey(x => x.Id);

            //hasmany 2 table joda ke bayad jpin bokhore || owns many bakhshi as inventory
            builder.OwnsMany(x => x.Operations, modelbuilder =>
            {
                modelbuilder.HasKey(x => x.Id);
                modelbuilder.ToTable("InventoryOperations");
                modelbuilder.Property(x => x.Description).HasMaxLength(1000);
                modelbuilder.WithOwner(x => x.Inventory).HasForeignKey(x => x.InventoryId);

            });
        }
    }
}
