using CRUD_NET9.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_NET9.Data
{
    // AppDbContext é um meio de campo entre a API e o banco de dados (SQL Server)
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ProductModel> Products { get; set; }
    }
}
