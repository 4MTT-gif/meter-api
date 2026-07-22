using MeterApi.Services;

var builder = WebApplication.CreateBuilder(args);

// --- Render icin PORT ayari ---
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

// --- Servisler ---
builder.Services.AddControllers();

// DeviceStore tek kopya (Singleton) olarak kaydediliyor
builder.Services.AddSingleton<IDeviceStore, DeviceStore>();

// --- CORS ---
var allowedOrigins = builder.Configuration["AllowedOrigins"]?.Split(",")
    ?? new[] { "http://localhost:5173" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("FrontendPolicy");

app.MapControllers();

// Health check + Render uyandirma endpointi
app.MapGet("/health", () => Results.Ok(new { status = "ok", time = DateTime.UtcNow }));

app.Run();
