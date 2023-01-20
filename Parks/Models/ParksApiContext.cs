using Microsoft.EntityFrameworkCore;

namespace Parks.Models;

public class ParksContext : DbContext
{
  public DbSet<Park> Parks { get; set; }

  public ParksContext(DbContextOptions<ParksContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.Entity<ParkType>()
      .HasData(
        new ParkType { ParkTypeId = 1, Type = "National"},
        new ParkType { ParkTypeId = 2, Type = "State"}
      );
    
    builder.Entity<Park>()
      .HasData(
        // National Parks
        new ParkType { Name = "Grand Canyon", State = "AZ", City = "Grand Canyon Village", TypeId = 1},
        // State Parks
        new ParkType { Name = "Pfieffer Big Sur", State = "CA", City = "Big Sur", TypeId = 2},
      );
  }
}