using Microsoft.EntityFrameworkCore;
using CarRental.Domain.DataModels;
using CarRental.Domain.InternalData.ComponentClasses;
using MongoDB.EntityFrameworkCore.Extensions;

namespace CarRental.Infrastructure;

public class CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : DbContext(options)
{
    public DbSet<Car> Cars { get; init; }
    public DbSet<Client> Clients { get; init; }
    public DbSet<Rent> Rents { get; init; }
    public DbSet<CarModel> CarModels { get; init; }
    public DbSet<CarModelGeneration> ModelGenerations { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

        modelBuilder.Entity<Car>(builder =>
        {
            builder.ToCollection("cars");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasElementName("_id");
        });

        modelBuilder.Entity<Client>(builder =>
        {
            builder.ToCollection("clients");
            builder.HasKey(cl => cl.Id);
            builder.Property(cl => cl.Id).HasElementName("_id");
        });

        modelBuilder.Entity<Rent>(builder =>
        {
            builder.ToCollection("rents");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasElementName("_id");
            builder.Property(r => r.CarId).HasElementName("car_id");
            builder.Property(r => r.ClientId).HasElementName("client_id");
        });

        modelBuilder.Entity<CarModel>(builder =>
        {
            builder.ToCollection("car_models");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasElementName("_id");
        });

        modelBuilder.Entity<CarModelGeneration>(builder =>
        {
            builder.ToCollection("model_generations");
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).HasElementName("_id");
        });
    }
}
