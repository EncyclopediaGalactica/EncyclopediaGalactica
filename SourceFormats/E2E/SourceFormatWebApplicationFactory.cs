namespace EncyclopediaGalactica.SourceFormats.E2E;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Controllers.SourceFormatNode;
using Ctx;
using FluentValidation.AspNetCore;
using Mappers;
using Mappers.Interfaces;
using Mappers.SourceFormatNode;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SourceFormatsCacheService;
using SourceFormatsCacheService.Interfaces;
using SourceFormatsCacheService.SourceFormatNode;
using SourceFormatsRepository;
using SourceFormatsRepository.Interfaces;
using SourceFormatsRepository.SourceFormatNode;
using SourceFormatsService;
using SourceFormatsService.Interfaces;
using SourceFormatsService.Interfaces.SourceFormatNode;
using SourceFormatsService.SourceFormatNodeService;
using Utils.GuardsService;
using Utils.GuardsService.Interfaces;
using ValidatorService;

[ExcludeFromCodeCoverage]
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

            SqliteConnection connection = new("Filename=:memory:");
            connection.Open();
            services.AddControllers()
                .AddApplicationPart(typeof(SourceFormatNodeController).Assembly);
            services.AddDbContext<SourceFormatsDbContext>(options =>
            {
                options.UseSqlite(connection);
                options.LogTo(m => Debug.WriteLine(m))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            });
            services.AddScoped<ISourceFormatMappers, SourceFormatMappers>();
            services.AddScoped<ISourceFormatNodeMappers, SourceFormatNodeMappers>();
            services.AddScoped<ISourceFormatNodeRepository, SourceFormatNodeRepository>();
            services.AddScoped<ISourceFormatsRepository, SourceFormatsRepository>();
            services.AddScoped<ISourceFormatsCacheService, SourceFormatsCacheService>();
            services.AddScoped<ISourceFormatNodeCacheService, SourceFormatNodeCacheService>();
            services.AddScoped<IGuardsService, GuardsService>();
            services.AddScoped<ISourceFormatNodeService, SourceFormatNodeService>();
            services.AddScoped<ISourceFormatsService, SourceFormatsService>();
            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SourceFormatNodeValidator>());
            services.AddLogging(log =>
            {
                log.ClearProviders();
                log.AddConsole();
                log.AddDebug();
            });

            ServiceProvider? sp = services.BuildServiceProvider();
            if (sp is null)
                throw new ArgumentNullException(nameof(sp));

            using (IServiceScope scope = sp.CreateScope())
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