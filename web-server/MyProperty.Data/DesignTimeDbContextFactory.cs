using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyProperty.Data;

namespace DemoApi.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyPropertyContext>
    {
        public MyPropertyContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MyPropertyContext>();

            var connectionString = "User ID=postgres;Password=formless;Host=localhost;Port=5432;Database=Property-Management;Pooling=true;";

            builder.UseNpgsql(connectionString);

            return new MyPropertyContext(builder.Options);
        }
    }
}
