using Microsoft.AspNetCore.Mvc;
using ParksClient.Models;

namespace ParksClient.Controllers;

public class ParksController : Controller
{
  public IActionResult Index()
  {
    List<Park> parks = Park.GetParks();
    return View(parks);
  }
}
