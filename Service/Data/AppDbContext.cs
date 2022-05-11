using Microsoft.EntityFrameworkCore;
using Service.Models;

namespace Service.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Package> Packages { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<EmployeePackage> EmployeePackages { get; set; }
}