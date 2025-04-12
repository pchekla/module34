using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using HomeApi.Configuration;
using HomeApi.Contracts.Validation;
using HomeApi.Data;
using HomeApi.Data.Repos;
using HomeApi.Models.DbFirst;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HomeApi
{
    public class Startup
    {
        /// <summary>
        /// Загрузка конфигурации из файла Json
        /// </summary>
        private IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .AddJsonFile("HomeOptions.json")
            .Build();

        public void ConfigureServices(IServiceCollection services)
        {
            // Подключаем автомаппинг
            var assembly = Assembly.GetAssembly(typeof(MappingProfile));
            services.AddAutoMapper(assembly);
            
            // регистрация сервиса репозитория для взаимодействия с базой данных
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<HomeApiContext>(options => options.UseSqlServer(connection), ServiceLifetime.Scoped);
            
            // Регистрация MasterContext для DbFirst модели
            string masterConnection = Configuration.GetConnectionString("MasterConnection");
            if (string.IsNullOrEmpty(masterConnection))
                masterConnection = "Server=PC-WIN\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";
            services.AddDbContext<MasterContext>(options => options.UseSqlServer(masterConnection), ServiceLifetime.Scoped);
            
            // Подключаем валидацию (современный способ)
            services.AddControllers();
            services.AddFluentValidationAutoValidation()
                   .AddFluentValidationClientsideAdapters()
                   .AddValidatorsFromAssemblyContaining<AddDeviceRequestValidator>();
            
            // Добавляем новый сервис
            services.Configure<HomeOptions>(Configuration);
            
            // Загружаем только адресс (вложенный Json-объект))
            services.Configure<Address>(Configuration.GetSection("Address"));
            
            // поддерживает автоматическую генерацию документации WebApi с использованием Swagger
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "HomeApi", Version = "v1"}); });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Проставляем специфичные для запуска при разработке свойства
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            // Сопоставляем маршруты с контроллерами
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}