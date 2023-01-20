namespace ParksApi.Models;

public class Park
{
  public int ParkId { get;set; }
  public string Name { get;set; }
  public string State { get;set; }
  public string City { get;set; }
  // reference
  public int ParkTypeId { get; set; }
  public ParkType Type { get; set; }
}