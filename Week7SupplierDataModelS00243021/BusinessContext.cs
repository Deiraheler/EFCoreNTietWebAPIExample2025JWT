using Microsoft.EntityFrameworkCore;
using ProductModel;

namespace Week7SupplierDataModelS00243021
{
    public class BusinessContext : DbContext
    {
        public BusinessContext(DbContextOptions<BusinessContext> options) : base(options)
        {
        }

        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            Supplier[] products = DBHelper.Get<Supplier>(@"..\Week7SupplierDataModelS00243021\Supplier.csv").ToArray();
            modelBuilder.Entity<Supplier>().HasData(products);

            base.OnModelCreating(modelBuilder);
        }
    }
}
