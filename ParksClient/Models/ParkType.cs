using System.Collections.Generic;
namespace ParksClient.Models;

public class ParkType
{
  public int ParkTypeId { get;set; }
  public string Type { get;set; }
  public List<Park> Parks { get; set; }
}