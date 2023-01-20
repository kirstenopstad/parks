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
        // TODO: Use NPS API to seed this API
        new ParkType { Name = "Grand Canyon", State = "AZ", City = "Grand Canyon National Park", TypeId = 1},
        new ParkType { Name = "Yosemite", State = "CA", City = "Yosemite National Park", TypeId = 1},
        new ParkType { Name = "Acadia", State = "ME", City = "Acadia National Park", TypeId = 1},
        // State Parks
        new ParkType { Name = "Pfieffer Big Sur", State = "CA", City = "Big Sur", TypeId = 2},
        new ParkType { Name = "Julia Pfieffer Burns", State = "CA", City = "Big Sur", TypeId = 2},
        new ParkType { Name = "Andrew Molera", State = "CA", City = "Big Sur", TypeId = 2},
      );
  }
}