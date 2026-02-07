using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MvcDemo.Models;
using Pmad.Milsymbol.AspNetCore.Orbat;

namespace MvcDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View(new HomeViewModel()
            {
                Markdown = System.IO.File.ReadAllText(
                    Path.Combine(_hostEnvironment.WebRootPath, "..", "..", "..", "Pmad.Milsymbol.Markdig", "EXAMPLES.md")) +
                    System.IO.File.ReadAllText(
                    Path.Combine(_hostEnvironment.WebRootPath, "..", "..", "..", "Pmad.Milsymbol.Markdig", "QUICKREF.md")),
                RootUnit = new OrbatUnitViewModel()
                {
                    Sdic = "10031000150000000000",
                    CommonIdentifier = "Root",
                    SubUnits = [
                        new OrbatUnitViewModel()
                        {
                            Sdic = "10031000140000000000",
                            CommonIdentifier = "1",
                            SubUnits = [
                                new OrbatUnitViewModel() { Sdic = "10031000130000000000", CommonIdentifier = "11" },
                                new OrbatUnitViewModel() { Sdic = "10031000130000000000", CommonIdentifier = "12" },
                                new OrbatUnitViewModel() { Sdic = "10031000130000000000", CommonIdentifier = "13" },
                                new OrbatUnitViewModel() { Sdic = "10031000130000000000", CommonIdentifier = "14" }
                            ]
                        },
                        new OrbatUnitViewModel()
                        {
                            Sdic = "10031000140000000000",
                            CommonIdentifier = "2",
                            SubUnits = [
                                new OrbatUnitViewModel() { Sdic = "10031000130000000000", CommonIdentifier = "21" },
                                new OrbatUnitViewModel() { Sdic = "10031000130000000000", CommonIdentifier = "22" },
                                new OrbatUnitViewModel() { Sdic = "10031000130000000000", CommonIdentifier = "23" },
                                new OrbatUnitViewModel() { Sdic = "10031000130000000000", CommonIdentifier = "24" }
                            ]
                        },
                        new OrbatUnitViewModel()
                        {
                            Sdic = "10031000140000000000",
                            CommonIdentifier = "3",
                            SubUnits = [
                                new OrbatUnitViewModel() { Sdic = "10031000130000000000", CommonIdentifier = "31" },
                                new OrbatUnitViewModel() { Sdic = "10031000130000000000", CommonIdentifier = "32" },
                                new OrbatUnitViewModel() { Sdic = "10031000130000000000", CommonIdentifier = "33" },
                                new OrbatUnitViewModel() { Sdic = "10031000130000000000", CommonIdentifier = "34" }
                            ]
                        },
                        new OrbatUnitViewModel()
                        {
                            Sdic = "10031000140000000000",
                            CommonIdentifier = "4",
                            SubUnits = [
                                new OrbatUnitViewModel() { Sdic = "10031000130000000000", CommonIdentifier = "41" },
                                new OrbatUnitViewModel() { Sdic = "10031000130000000000", CommonIdentifier = "42" },
                                new OrbatUnitViewModel() { Sdic = "10031000130000000000", CommonIdentifier = "43" },
                                new OrbatUnitViewModel() { Sdic = "10031000130000000000", CommonIdentifier = "44" }
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
