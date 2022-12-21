using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) 
    { 
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {  
        modelBuilder.Entity<Transacition>()
        .HasOne<Acount>(s => s.Acount)
        .WithMany(g => g.Transacitions)
        .HasForeignKey(s => s.AcountId);
    }
    
    public DbSet<Acount> Acounts { get; set; }
    public DbSet<Transacition> Transacitions { get; set; }
}