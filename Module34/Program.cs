using Module34;

var builder = WebApplication.CreateBuilder(args);

// Регистрируем Startup для конфигурации сервисов и приложения
var startup = new Startup();
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Вызываем метод Configure класса Startup
startup.Configure(app, app.Environment);

app.Run();