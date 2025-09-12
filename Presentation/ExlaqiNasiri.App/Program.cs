using ExlaqiNasiri.App.Registrations;
using ExlaqiNasiri.Application;
using ExlaqiNasiri.Infrastructure;
using ExlaqiNasiri.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterPersistenceServices(builder.Configuration);
builder.Services.RegisterApplicationServices();
builder.Services.RegisterInfrastructureServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddCustomAuthorization();
builder.Services.AddCustomSwagger();
builder.Services.AddCustomCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ExlaqiNasiri V1");
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        options.InjectStylesheet("/assets/SwaggerDark.css");

    });
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("Exlaqi-Nasiri");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
