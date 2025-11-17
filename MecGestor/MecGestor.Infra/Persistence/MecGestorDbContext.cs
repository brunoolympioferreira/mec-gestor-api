using MecGestor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MecGestor.Infra.Persistence;

public class MecGestorDbContext(DbContextOptions<MecGestorDbContext> options) : DbContext(options)
{
    public DbSet<Company> Companies { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}