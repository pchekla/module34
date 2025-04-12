using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Module34.Models;
using Module34.Models.Home;

namespace Module34.Controllers;

public class MvcHomeController : Controller
{
    private readonly ILogger<MvcHomeController> _logger;
    private readonly IOptions<HomeOptions> _options;

    public MvcHomeController(ILogger<MvcHomeController> logger, IOptions<HomeOptions> options)
    {
        _logger = logger;
        _options = options;
    }

    public IActionResult Index()
    {
        ViewBag.HomeOptions = _options.Value;
        return View();
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