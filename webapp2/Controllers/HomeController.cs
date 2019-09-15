using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using irid.Models;

namespace irid.Controllers
{
    public class HomeController : Controller
    {
        private readonly IridDbContext _dbContext;

        public HomeController(IridDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            _dbContext.Devices.Add(new Device
                {Name = "test_device", CreatedAt = DateTime.Now, Data = new byte[10], Phi = 0, Theta = 0});
            _dbContext.SaveChanges();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}