using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

  public static List<Park> GetNPSParks()
    {
      // make API call using ApiHelper method
      var apiCallTask = NpsApiCall.GetAll();
      // store result in var
      var result = apiCallTask.Result;

      // parse result as json array
      JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(result);
      // deserialize JArray into list of Park objects 
      List<Park> ParkList = JsonConvert.DeserializeObject<List<Park>>(jsonResponse.ToString());

      return ParkList;
    }
}