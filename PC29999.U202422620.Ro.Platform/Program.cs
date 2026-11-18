using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PC29999.U202422620.Ro.Platform.Clinical.Application;
using PC29999.U202422620.Ro.Platform.Clinical.Domain.Repositories;
using PC29999.U202422620.Ro.Platform.Clinical.Infrastructure.Persistence;
using PC29999.U202422620.Ro.Platform.Shared.Interfaces.REST;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var message = AcceptLanguageSelector.IsSpanish(context.HttpContext.Request.Headers.AcceptLanguage.ToString())
            ? "Uno o más valores de la solicitud no son válidos."
            : "One or more request values are invalid.";
        return new BadRequestObjectResult(new { message });
    };
});
builder.Services.AddOpenApi();
builder.Services.AddDbContext<PrescriptionDbContext>(options => options.UseMySQL(connectionString!));
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<CreatePrescriptionService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapOpenApi();
app.MapControllers();

app.Run();
