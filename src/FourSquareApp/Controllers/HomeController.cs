using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FourSquareApp.Models;

namespace FourSquareApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetVenues()
        {
            var allVenues = Venue.GetVenues();
            return View(allVenues);
        }
        public IActionResult SendVenue()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendVenue(Venue newVenue)
        {
            newVenue.Send();
            return RedirectToAction("Index");
        }
    }
}
