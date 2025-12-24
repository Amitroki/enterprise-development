using Microsoft.EntityFrameworkCore;
using CarRental.Domain.DataModels;
using CarRental.Domain.InternalData.ComponentClasses;
using MongoDB.EntityFrameworkCore.Extensions;

namespace CarRental.Infrastructure;

public class CarRentalDbContext : DbContext
{
    public DbSet<Car> Cars { get; init; }
    public DbSet<Client> Clients { get; init; }
    public DbSet<Rent> Rents { get; init; }
    public DbSet<CarModel> CarModels { get; init; }
    public DbSet<CarModelGeneration> ModelGenerations { get; init; }

    public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>().ToCollection("clients");
        modelBuilder.Entity<Car>().ToCollection("cars");
        modelBuilder.Entity<Rent>().ToCollection("rents");
        modelBuilder.Entity<CarModel>().ToCollection("car_models");
        modelBuilder.Entity<CarModelGeneration>().ToCollection("model_generations");

        modelBuilder.Entity<Client>().HasKey(c => c.Id);
        modelBuilder.Entity<Car>().HasKey(c => c.Id);
        modelBuilder.Entity<Rent>().HasKey(r => r.Id);
        modelBuilder.Entity<CarModel>().HasKey(m => m.Id);
        modelBuilder.Entity<CarModelGeneration>().HasKey(g => g.Id);

        modelBuilder.Entity<CarModel>(entity =>
        {
            entity.Property(c => c.BodyType).HasConversion<string>();
            entity.Property(c => c.DriveType).HasConversion<string>();
            entity.Property(c => c.ClassType).HasConversion<string>();
        });

        modelBuilder.Entity<CarModelGeneration>(entity =>
        {
            entity.Property(c => c.TransmissionType).HasConversion<string>();
        });
        modelBuilder.Entity<Rent>(entity =>
        {
            entity.Property(r => r.Duration).HasElementName("duration_hours");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(c => c.BirthDate).HasElementName("birth_date");
        });
    }
}
