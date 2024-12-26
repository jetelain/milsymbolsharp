using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcDemo.Models;
using Pmad.Milsymbol.AspNetCore.Orbat;

namespace MvcDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new HomeViewModel()
            {
                RootUnit = new OrbatUnit()
                {
                    Sdic = "30031000150000000000",
                    CommonIdentifier = "Root",
                    SubUnits = [
                        new OrbatUnit()
                        {
                            Sdic = "30031000140000000000",
                            CommonIdentifier = "1",
                            SubUnits = [
                                new OrbatUnit() { Sdic = "30031000130000000000", CommonIdentifier = "11" },
                                new OrbatUnit() { Sdic = "30031000130000000000", CommonIdentifier = "12" },
                                new OrbatUnit() { Sdic = "30031000130000000000", CommonIdentifier = "13" },
                                new OrbatUnit() { Sdic = "30031000130000000000", CommonIdentifier = "14" }
                            ]
                        },
                        new OrbatUnit()
                        {
                            Sdic = "30031000140000000000",
                            CommonIdentifier = "2",
                            SubUnits = [
                                new OrbatUnit() { Sdic = "30031000130000000000", CommonIdentifier = "21" },
                                new OrbatUnit() { Sdic = "30031000130000000000", CommonIdentifier = "22" },
                                new OrbatUnit() { Sdic = "30031000130000000000", CommonIdentifier = "23" },
                                new OrbatUnit() { Sdic = "30031000130000000000", CommonIdentifier = "24" }
                            ]
                        },
                        new OrbatUnit()
                        {
                            Sdic = "30031000140000000000",
                            CommonIdentifier = "3",
                            SubUnits = [
                                new OrbatUnit() { Sdic = "30031000130000000000", CommonIdentifier = "31" },
                                new OrbatUnit() { Sdic = "30031000130000000000", CommonIdentifier = "32" },
                                new OrbatUnit() { Sdic = "30031000130000000000", CommonIdentifier = "33" },
                                new OrbatUnit() { Sdic = "30031000130000000000", CommonIdentifier = "34" }
                            ]
                        },
                        new OrbatUnit()
                        {
                            Sdic = "30031000140000000000",
                            CommonIdentifier = "4",
                            SubUnits = [
                                new OrbatUnit() { Sdic = "30031000130000000000", CommonIdentifier = "41" },
                                new OrbatUnit() { Sdic = "30031000130000000000", CommonIdentifier = "42" },
                                new OrbatUnit() { Sdic = "30031000130000000000", CommonIdentifier = "43" },
                                new OrbatUnit() { Sdic = "30031000130000000000", CommonIdentifier = "44" }
                            ]
                        }
                    ]
                }
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
