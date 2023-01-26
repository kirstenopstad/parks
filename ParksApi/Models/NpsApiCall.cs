using System.Threading.Tasks;
using RestSharp;
using ParksApi.Models;

namespace ParksApi.Models;

public class NpsApiCall
{
  
  public static async Task<string> GetAll()
  {
    // establish client
    RestClient client = new RestClient("https://developer.nps.gov/");
    // create request 
    // ${process.env.NPS_API_KEY}
    RestRequest request = new RestRequest($"api/v1/parks?api_key=9UT1S9llOHvPs4vppfG7VcFTrZYJJSiE5FXvq7T7");
    // submit request, await response
    RestResponse response = await client.GetAsync(request);
    // return content
    return response.Content;
  }
}