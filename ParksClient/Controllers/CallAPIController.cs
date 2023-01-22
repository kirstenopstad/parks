using JWTAPI.Models;
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

public IActionResult Index(string message)
{
    ViewBag.Message = message;
    return View();
}
 
[HttpPost]
public IActionResult Index(string username, string password)
{
    // replace this code with something that checks the username and password of the user from the database
    if ((username != "secret") || (password != "secret"))
        return View((object)"Login Failed");
 
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
 
    List<Reservation> reservationList = new List<Reservation>();
 
    using (var httpClient = new HttpClient())
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        using (var response = await httpClient.GetAsync("https://localhost:44360/Reservation")) // change API URL to yours 
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
            }
 
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Index", new { message = "Please Login again" });
            }
        }
    }
 
    return View(reservationList);
}