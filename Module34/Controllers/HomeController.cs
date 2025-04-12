using System.Diagnostics;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Module34.Contracts;
using Module34.Models;
using Module34.Models.Home;

namespace Module34.Controllers;

public class HomeController : Controller
{
    private readonly IOptions<HomeOptions> _options;
    private readonly ILogger<HomeController>? _logger;
    private readonly IMapper _mapper;

    public HomeController(IOptions<HomeOptions> options, IMapper mapper, ILogger<HomeController>? logger = null)
    {
        _options = options;
        _mapper = mapper;
        _logger = logger;
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

    /// <summary>
    /// Метод для получения информации о доме через API
    /// </summary>
    [HttpGet]
    [Route("api/home/info")]
    public IActionResult Info()
    {
        // Получим запрос, "смапив" конфигурацию на модель запроса
        var infoResponse = _mapper.Map<HomeOptions, InfoResponse>(_options.Value);
        // Вернём ответ
        return StatusCode(200, infoResponse);
    }
}
