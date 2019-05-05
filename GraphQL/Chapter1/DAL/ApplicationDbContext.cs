using Chapter1.Models;
using Microsoft.EntityFrameworkCore;

namespace Chapter1.DAL
{
    public class ApplicationDbContext:DbContext
    {
        #region Members.
        /*!!!!此处DBSet<T>类型的属性名称，必须要和DB中的表名一致吗？*/
        public DbSet<SellableItem> SellableItems { get; set; }
        public DbSet<CustomCustomer> CustomCustomer { get; set; }
        public DbSet<CustomOrder> CustomOrder { get; set; }
        public DbSet<CustomOrderSellableItemRelation> CustomOrderSellableItemRelation { get; set; }
        #endregion

        #region Constructors.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        #endregion

        #region Methods.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SellableItem>().ToTable("SellableItems");
            modelBuilder.Entity<SellableItem>().HasKey(i => i.Barcode);

            modelBuilder.Entity<SellableItem>().HasData(
                new SellableItem { Barcode = "123", Title = "Headphone", SellingPrice = 50 });
            modelBuilder.Entity<SellableItem>().HasData(
                new SellableItem { Barcode = "456", Title = "Keyboard", SellingPrice = 40 });
            modelBuilder.Entity<SellableItem>().HasData(
                new SellableItem { Barcode = "789", Title = "Monitor", SellingPrice = 100 });

            modelBuilder.Entity<CustomCustomer>().HasKey(x => x.CustomerId);
            modelBuilder.Entity<CustomCustomer>().HasMany(x => x.Orders)
                .WithOne()
                .HasForeignKey(dependentType => dependentType.CustomerId);
            modelBuilder.Entity<CustomOrder>().HasKey(x => x.OrderId);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SellableItem>(typeBuilder =>
            {
                typeBuilder.Property(type => type.SellingPrice).HasColumnType("decimal(18,6)");
            });
        }
        #endregion
    }
}
