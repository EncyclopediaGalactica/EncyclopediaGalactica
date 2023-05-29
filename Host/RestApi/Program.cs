using System.Diagnostics;
using EncyclopediaGalactica.Services.Document.Controllers.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.Ctx;
using EncyclopediaGalactica.Services.Document.Mappers;
using EncyclopediaGalactica.Services.Document.Mappers.Interfaces;
using EncyclopediaGalactica.Services.Document.Mappers.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsCacheService;
using EncyclopediaGalactica.Services.Document.SourceFormatsCacheService.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsCacheService.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsService;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.ExceptionFilters;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.SourceFormatNodeService;
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
builder.Services.AddDbContext<SourceFormatsDbContext>(options =>
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
    SourceFormatsDbContext db = scopedServices.GetRequiredService<SourceFormatsDbContext>();
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