using ParksApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParksApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
[ApiController]
public class UsersController : Controller
{
  
  [HttpPost]
  public IActionResult Index(string username, string password)
  {
      // replace this code with something that checks the username and password of the user from the database
      Users loginUser = CreateDummyUsers().Where(a => a.Username == username && a.Password == password).FirstOrDefault();
      if (loginUser == null)
        return View((object)"Login Failed");

      var claims = new[] {
        new Claim(ClaimTypes.Role, loginUser.Role)
      };

      var accessToken = GenerateJSONWebToken();
      SetJWTCookie(accessToken);
  
      return RedirectToAction("GetParks");
  }

  private string GenerateJSONWebToken()
  {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("IlanaMadonnaRihanna"));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
  
      var token = new JwtSecurityToken(
          issuer: "https://localhost:5001",
          audience: "https://localhost:5001",
          expires: DateTime.Now.AddHours(3),
          signingCredentials: credentials
        //   claims: claims
          );
  
      return new JwtSecurityTokenHandler().WriteToken(token);
  }
  
  private void SetJWTCookie(string token)
  {
      var cookieOptions = new CookieOptions
      {
          HttpOnly = true,
          Expires = DateTime.UtcNow.AddHours(3),
      };
      Response.Cookies.Append("jwtCookie", token, cookieOptions);
  }

  // FIX
  public async Task<IActionResult> GetParks()
  {
      var jwt = Request.Cookies["jwtCookie"];
  
      List<Park> parkList = new List<Park>();
  
      using (var httpClient = new HttpClient())
      {
          httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
          using (var response = await httpClient.GetAsync("https://localhost:5001/api/v2/parks")) // change API URL to yours 
          {
              if (response.StatusCode == System.Net.HttpStatusCode.OK)
              {
                  string apiResponse = await response.Content.ReadAsStringAsync();
                  parkList = JsonConvert.DeserializeObject<List<Park>>(apiResponse);
              }
  
              if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
              {
                  return RedirectToAction("Index", new { message = "Please Login again" });
              }
          }
      }
  
      return View(parkList);
  }

  public List<Users> CreateDummyUsers()
  {
      List<Users> userList = new List<Users> {
        new Users { Username = "jack", Password = "jack", Role = "Admin" },
        new Users { Username = "donald", Password = "donald", Role = "Manager" },
        new Users { Username = "thomas", Password = "thomas", Role = "Developer" }
      };
      return userList;
  }
}