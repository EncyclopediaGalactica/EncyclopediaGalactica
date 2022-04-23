using System.Diagnostics;
using EncyclopediaGalactica.SourceFormats.Controllers.SourceFormatNode;
using EncyclopediaGalactica.SourceFormats.Ctx;
using EncyclopediaGalactica.SourceFormats.Mappers;
using EncyclopediaGalactica.SourceFormats.Mappers.Interfaces;
using EncyclopediaGalactica.SourceFormats.Mappers.SourceFormatNode;
using EncyclopediaGalactica.SourceFormats.Repository;
using EncyclopediaGalactica.SourceFormats.Repository.Interfaces;
using EncyclopediaGalactica.SourceFormats.Repository.SourceFormatNode;
using EncyclopediaGalactica.SourceFormats.SourceFormatsCacheService;
using EncyclopediaGalactica.SourceFormats.SourceFormatsCacheService.Interfaces;
using EncyclopediaGalactica.SourceFormats.SourceFormatsCacheService.SourceFormatNode;
using EncyclopediaGalactica.SourceFormats.SourceFormatsService;
using EncyclopediaGalactica.SourceFormats.SourceFormatsService.Interfaces;
using EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;
using EncyclopediaGalactica.SourceFormats.ValidatorService;
using EncyclopediaGalactica.Utils.GuardsService;
using FluentValidation.AspNetCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddApplicationPart(typeof(SourceFormatNodeController).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

SqliteConnection connection = new SqliteConnection("Filename=:memory:");
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}