using System.Diagnostics;
using EncyclopediaGalactica.Services.Document.CacheService;
using EncyclopediaGalactica.Services.Document.CacheService.Interfaces;
using EncyclopediaGalactica.Services.Document.CacheService.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.Controllers.ExceptionFilters;
using EncyclopediaGalactica.Services.Document.Controllers.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.Ctx;
using EncyclopediaGalactica.Services.Document.Mappers;
using EncyclopediaGalactica.Services.Document.Mappers.Interfaces;
using EncyclopediaGalactica.Services.Document.Mappers.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.Repository;
using EncyclopediaGalactica.Services.Document.Repository.Interfaces;
using EncyclopediaGalactica.Services.Document.Repository.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.Service;
using EncyclopediaGalactica.Services.Document.Service.Interfaces;
using EncyclopediaGalactica.Services.Document.Service.Interfaces.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.Service.SourceFormatNodeService;
using EncyclopediaGalactica.Services.Document.ValidatorService;
using EncyclopediaGalactica.Utils.GuardsService;
using EncyclopediaGalactica.Utils.GuardsService.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<ValidationExceptionsFilter>();
        options.Filters.Add<InternalServerErrorExceptionsFilter>();
        options.Filters.Add<NoSuchEntityExceptionsFilter>();
    })
    .AddNewtonsoftJson()
    .AddApplicationPart(typeof(SourceFormatNodeController).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<ISourceFormatMappers, SourceFormatMappers>();
builder.Services.AddScoped<ISourceFormatNodeMappers, SourceFormatNodeMappers>();
builder.Services.AddScoped<ISourceFormatNodeRepository, SourceFormatNodeRepository>();
builder.Services.AddScoped<ISourceFormatsRepository, SourceFormatsRepository>();
builder.Services.AddScoped<ISourceFormatsCacheService, SourceFormatsCacheService>();
builder.Services.AddScoped<ISourceFormatNodeCacheService, SourceFormatNodeCacheService>();
builder.Services.AddScoped<IGuardsService, GuardsService>();
builder.Services.AddScoped<ISourceFormatNodeService, SourceFormatNodeService>();
builder.Services.AddScoped<ISourceFormatsService, SourceFormatsService>();
builder.Services.AddMvc()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SourceFormatNodeValidator>());
builder.Services.AddLogging(log =>
{
    log.ClearProviders();
    log.AddConsole();
    log.AddDebug();
});

SqliteConnection connection = new("Filename=:memory:");
connection.Open();
builder.Services.AddDbContext<DocumentDbContext>(options =>
{
    options.UseSqlite(connection);
    options.LogTo(m => Debug.WriteLine(m))
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});
ServiceProvider? sp = builder.Services.BuildServiceProvider();
if (sp is null)
    throw new ArgumentNullException(nameof(sp));

using (IServiceScope scope = sp.CreateScope())
{
    IServiceProvider scopedServices = scope.ServiceProvider;
    DocumentDbContext db = scopedServices.GetRequiredService<DocumentDbContext>();
    db.Database.EnsureCreated();
}

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}
else
{
    app.UseExceptionHandler("/error-development");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

namespace EncyclopediaGalactica.Host.RestApi
{
    public class Program
    {
    }
}