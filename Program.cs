using IOT.Data;
using IOT.Interfaces;
using IOT.Services;
using Microsoft.OpenApi.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;
using Serilog.Events;
using Microsoft.Extensions.Logging;
using System;
using Serilog.Formatting.Json;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IOT.Loggings;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //============================
        // Cấu hình Serilog từ appsettings.json
        //============================
        builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration));
        //============================

        //============================
        // Cấu hình DbContext với kết nối SQL
        //============================
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<IoTHealthBackendContext>(options =>
            options.UseSqlServer(connectionString));
        //============================

        //============================
        //============================
        // Thêm dịch vụ logging (Logging là mặc định được tích hợp trong ASP.NET Core)
        //============================
        builder.Services.AddLogging(config =>
        {
            config.AddConsole() // Ghi log ra Console
                  .AddDebug(); // Ghi log ra Debug
                               //.AddFile("Logs/myapp-{Date}.log"); // Thêm ghi log vào file (nếu sử dụng thư viện thêm như Serilog, NLog...)
        });

        //============================
        // A, B. Cấu hình Serilog: File & Console.
        //============================
        Log.Logger = new LoggerConfiguration()
            //.Enrich.WithMachineName()  // Thêm tên máy vào mỗi log
            //.Enrich.WithThreadId()     // Thêm ID của thread vào mỗi log
            .WriteTo.Console(new JsonFormatter())  // Ghi log trực tiếp vào console khi chạy ứng dụng:
            .WriteTo.File("Logs/myapp-{Date}.log", rollingInterval: RollingInterval.Day)    //Ghi log vào một file, ví dụ log.txt, và ghi các log mới vào cuối file:
                                                                                            //.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)  // Giới hạn lưu trữ log trong 7 ngày
            .CreateLogger();

        //============================
        //C. Cấu hình Serilog: Log vào SQL Server
        //============================
        Log.Logger = new LoggerConfiguration()
            .WriteTo.MSSqlServer(
                connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
                tableName: "Logs",  // Tên bảng mà bạn muốn tạo hoặc ghi logs vào
                autoCreateSqlTable: true)   // Tự động tạo bảng nếu chưa có
            .CreateLogger();

        //============================
        //AddLogging 
        //============================
        builder.Services.AddLogging(configure => configure.AddSerilog());

        //============================
        //D. Cấu hình Serilog: Log vào MongoDB
        //============================
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()  // Đặt mức độ log tối thiểu
            .WriteTo.MongoDB("mongodb://localhost:27017/logDB", collectionName: "logs")  // Kết nối tới MongoDB và chỉ định collection
            .Enrich.FromLogContext()  // Làm giàu log với thông tin context
            .CreateLogger();

        //============================
        //E. Tích Hợp với ASP.NET Core
        //============================
        builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .WriteTo.Console()
            .WriteTo.File("log.txt")
            .MinimumLevel.Debug());

        //============================
        //*** Để sử dụng Serilog thay vì ILogger mặc định của ASP.NET Core
        //============================
        builder.Host.UseSerilog();


        #region F. Cấu hình API Versioning
        // Cấu hình API versioning
        builder.Services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true; // Cho phép báo cáo phiên bản API.
            options.AssumeDefaultVersionWhenUnspecified = true; // Giả sử phiên bản mặc định nếu không có trong yêu cầu.
            options.DefaultApiVersion = new ApiVersion(1, 0); // Phiên bản mặc định.
        });

        // Cấu hình Swagger để hỗ trợ API versioning
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "IoTHealthBackend API", Version = "v1" });
            options.SwaggerDoc("v2", new OpenApiInfo { Title = "IoTHealthBackend API", Version = "v2" });

            // Cấu hình Swagger để hỗ trợ API versioning
            options.DocInclusionPredicate((version, apiDescription) =>
            {
                var versions = apiDescription.ActionDescriptor
                    .EndpointMetadata
                    .OfType<ApiVersionAttribute>()
                    .SelectMany(attr => attr.Versions);

                return versions.Any(v => $"v{v}" == version);
            });
        });
        #endregion F. Cấu hình API Versioning

        #region G. Cấu hình JWT Bearer Authentication

        // Cấu hình JWT
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://your-identity-provider.com"; // URL của Identity Provider (Issuer)
                options.Audience = "your-audience"; // Audience mà bạn dự định nhận token từ
                options.RequireHttpsMetadata = false; // Chỉ để phát triển, không nên dùng trong sản phẩm

                // Cấu hình các tham số xác thực token
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = "https://your-identity-provider.com",
                    ValidAudience = "your-audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"))
                };
            });
        #endregion G. Cấu hình JWT Bearer Authentication

        //============================
        // Add services to the container.
        //============================
        builder.Services.AddControllers();

        //============================
        // Thêm các dịch vụ ứng dụng vào DI container
        //============================

        builder.Services.AddScoped<IDeviceService, DeviceService>();
        builder.Services.AddScoped<IBloodPressureMonitorService, BloodPressureMonitorService>();
        builder.Services.AddScoped<IDeviceDataService, DeviceDataService>();
        builder.Services.AddScoped<IPatientService, PatientService>();

        //============================
        // Cấu hình GlobalExeption Ghi log ra file
        //============================
        builder.Services.AddControllers(services => services.Filters.Add(new GlobalExeption()));

        //============================
        // Cấu hình Swagger
        //============================

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "IoTHealthBackend", Version = "v1" });
        });

        #region Tạo Middleware để Ghi Nhận Request và Response
        //============================
        // Cấu hình Serilog
        //============================
        builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day));
        //============================

        var app = builder.Build();

        //============================
        // Cấu hình HTTP Request vs Response pipeline vào Logs
        //============================
        app.UseMiddleware<RequestResponseLoggingMiddleware>(); // Thêm middleware log request/response
        #endregion Tạo Middleware để Ghi Nhận Request và Response

        //============================
        // Cấu hình Swagger UI
        //============================
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IoTHealthBackend API v1.0");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "IoTHealthBackend API v2.0");
            });
        }
        //============================

        app.UseHttpsRedirection();

        // Cấu hình Authentication và Authorization Middleware
        app.UseAuthentication(); // Xử lý xác thực
        app.UseAuthorization();  // Xử lý phân quyền

        // Map controllers
        app.MapControllers();

        app.Run();
    }
}