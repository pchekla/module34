using Microsoft.OpenApi.Models;
using Module34.Models.Home;
using System.IO;

namespace Module34;

public class Startup
{
    /// <summary>
    /// Загрузка конфигурации из файла Json
    /// </summary>
    public IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("HomeOptions.json", optional: false, reloadOnChange: true)
        .Build();

    // Этот метод вызывается средой выполнения для добавления сервисов в контейнер
    public void ConfigureServices(IServiceCollection services)
    {
        // Добавляем новый сервис
        services.Configure<HomeOptions>(Configuration);

        // Добавляем поддержку MVC и API
        services.AddControllersWithViews();
        services.AddControllers();
        
        services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo {
                Title = "HomeApi", Version = "v1"
            });
        });
    }

    // Этот метод вызывается средой выполнения для настройки конвейера HTTP-запросов
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // Значение HSTS по умолчанию — 30 дней. Можно изменить для сценариев производства, см. https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        else
        {
            app.UseDeveloperExceptionPage();
            
            // Включаем Swagger только в режиме разработки
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Module34 API V1");
            });
        }

        // Убираем перенаправление на HTTPS
        // app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
