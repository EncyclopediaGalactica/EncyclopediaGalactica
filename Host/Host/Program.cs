using System.Diagnostics;
using EncyclopediaGalactica.SourceFormats.Ctx;
using EncyclopediaGalactica.SourceFormats.ExceptionFilters;
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
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<SourceFormatNodeServiceInputValidationExceptionFilter>();
    options.Filters.Add<SourceFormatNodeServiceExceptionFilter>();
});
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

SqliteConnection connection = new SqliteConnection("Filename=:memory");
connection.Open();
builder.Services.AddDbContext<SourceFormatsDbContext>(options =>
{
    options.UseSqlite(connection);
    options.LogTo(m => Debug.WriteLine(m)).EnableSensitiveDataLogging().EnableDetailedErrors();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

namespace Host
{
    public partial class Program
    {
    }
}