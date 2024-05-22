using System.Diagnostics;
using App.DTO.v1_0;
using Microsoft.AspNetCore.Mvc;
using TrafficReport.Models;
using TrafficReport.ViewModels;

namespace TrafficReport.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _env;

    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult ListFiles()
    {
        return View();
    }
    public IActionResult Upload()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Upload(FileUploadViewModel vm, VehicleViolation vehicleViolation)
    {
        var fileExtensions = new string[]
        {
            ".png", ".jpg", ".bmp", ".gif", ".mov", ".mp4"
        };

        if (ModelState.IsValid)
        {
            if (vm.File.Length > 0 && fileExtensions.Contains(Path.GetExtension(vm.File.FileName)))
            {
                var uploadDir = _env.WebRootPath;
                var filename = vehicleViolation.Id + "_" + Path.GetFileName(vm.File.FileName);
                var filePath = uploadDir + Path.DirectorySeparatorChar + "uploads" + Path.DirectorySeparatorChar +
                               filename;

                await using (var stream = System.IO.File.Create(filePath))
                {
                    await vm.File.CopyToAsync(stream);
                }

                return RedirectToAction(nameof(ListFiles));
            }

            ModelState.AddModelError(nameof(FileUploadViewModel.File), "This is not a valid evidence file! " + vm.File.FileName);
        }

        return View(vm);
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}