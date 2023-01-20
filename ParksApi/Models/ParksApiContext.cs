using Microsoft.EntityFrameworkCore;

namespace ParksApi.Models;

public class ParksApiContext : DbContext
{
  public DbSet<Park> Parks { get; set; }

  public ParksApiContext(DbContextOptions<ParksApiContext> options) : base(options)
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
        new Park { ParkId = 1, Name = "Grand Canyon", State = "AZ", City = "Grand Canyon National Park", ParkTypeId = 1},
        new Park { ParkId = 2, Name = "Yosemite", State = "CA", City = "Yosemite National Park", ParkTypeId = 1},
        new Park { ParkId = 3, Name = "Acadia", State = "ME", City = "Acadia National Park", ParkTypeId = 1},
        // State Parks
        new Park { ParkId = 4, Name = "Pfieffer Big Sur", State = "CA", City = "Big Sur", ParkTypeId = 2},
        new Park { ParkId = 5, Name = "Julia Pfieffer Burns", State = "CA", City = "Big Sur", ParkTypeId = 2},
        new Park { ParkId = 6, Name = "Andrew Molera", State = "CA", City = "Big Sur", ParkTypeId = 2}
      );
  }
}