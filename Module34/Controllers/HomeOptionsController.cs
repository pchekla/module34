using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Module34.Contracts.Models.Home;
using HomeOptions = Module34.Models.Home.HomeOptions;

namespace Module34.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeOptionsController : ControllerBase
{
    // Ссылка на объект конфигурации
    private readonly IOptions<HomeOptions> _options;
    private readonly IMapper _mapper;
    
    // Инициализация конфигурации при вызове конструктора
    public HomeOptionsController(IOptions<HomeOptions> options, IMapper mapper)
    {
        _options = options;
        _mapper = mapper;
    }

    /// <summary>
    /// Метод для получения информации о доме
    /// </summary>
    [HttpGet] // Для обслуживания Get-запросов
    [Route("info")] // Настройка маршрута с помощью атрибутов
    public IActionResult Info()
    {
        // Получим запрос, "смапив" конфигурацию на модель запроса
        var infoResponse = _mapper.Map<HomeOptions, InfoResponse>(_options.Value);
        // Вернём ответ
        return StatusCode(200, infoResponse);
    }
} 