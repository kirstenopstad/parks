using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ParksClient.Models;

namespace ParksClient.Models;

public class Park
{
  public int ParkId { get;set; }
  public string Name { get;set; }
  public string State { get;set; }
  public string City { get;set; }
  // reference
  public int ParkTypeId { get; set; }
  public ParkType Type { get; set; }

  public static List<Park> GetParks()
    {
      var apiCallTask = ApiHelper.GetAll();
      var result = apiCallTask.Result;

      JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(result);
      List<Park> parkList = JsonConvert.DeserializeObject<List<Park>>(jsonResponse.ToString());

      return parkList;
    }
}