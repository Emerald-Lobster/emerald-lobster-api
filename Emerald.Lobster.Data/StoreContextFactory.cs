using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Emerald.Lobster.Data
{
    public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
    {
        public StoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();

            optionsBuilder.UseSqlite("Data Source=../Emerald.Lobster.sqlite",
                b => b.MigrationsAssembly("Emerald.Lobster.Api"));

            return new StoreContext(optionsBuilder.Options);
        }
    }
}
