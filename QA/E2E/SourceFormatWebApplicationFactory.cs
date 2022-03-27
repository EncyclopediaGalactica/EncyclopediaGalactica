namespace E2E;

using System;
using System.Diagnostics;
using System.Linq;
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
using FluentValidation.AspNetCore;
using Guards;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class SourceFormatWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            ServiceDescriptor? descriptor =
                services.SingleOrDefault(d => d.ServiceType == typeof(SourceFormatsDbContext));
            services.Remove(descriptor!);

            SqliteConnection connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            services.AddDbContext<SourceFormatsDbContext>(options =>
            {
                options.UseSqlite(connection);
                options.LogTo(m => Debug.WriteLine(m)).EnableSensitiveDataLogging().EnableDetailedErrors();
            });
            services.AddScoped<ISourceFormatMappers, SourceFormatMappers>();
            services.AddScoped<ISourceFormatNodeMappers, SourceFormatNodeMappers>();
            services.AddScoped<ISourceFormatNodeRepository, SourceFormatNodeRepository>();
            services.AddScoped<ISourceFormatsRepository, SourceFormatsRepository>();
            services.AddScoped<ISourceFormatsCacheService, SourceFormatsCacheService>();
            services.AddScoped<ISourceFormatNodeCacheService, SourceFormatNodeCacheService>();
            services.AddScoped<IGuardService, GuardService>();
            services.AddScoped<ISourceFormatNodeService, SourceFormatNodeService>();
            services.AddScoped<ISourceFormatsService, SourceFormatsService>();
            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SourceFormatNodeValidator>());

            ServiceProvider? sp = services.BuildServiceProvider();
            if (sp is null)
                throw new ArgumentNullException(nameof(sp));

            using (var scope = sp.CreateScope())
            {
                IServiceProvider scopedServices = scope.ServiceProvider;
                SourceFormatsDbContext db = scopedServices.GetRequiredService<SourceFormatsDbContext>();
                ILogger<SourceFormatWebApplicationFactory<TStartup>> logger = scopedServices
                    .GetRequiredService<ILogger<SourceFormatWebApplicationFactory<TStartup>>>();

                db.Database.EnsureCreated();
            }
        });
    }
}