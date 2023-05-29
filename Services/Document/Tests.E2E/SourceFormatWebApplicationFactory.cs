namespace EncyclopediaGalactica.Services.Document.Tests.E2E;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EncyclopediaGalactica.Services.Document.Controllers.Document;
using EncyclopediaGalactica.Services.Document.Controllers.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.Ctx;
using EncyclopediaGalactica.Services.Document.Mappers;
using EncyclopediaGalactica.Services.Document.Mappers.Document;
using EncyclopediaGalactica.Services.Document.Mappers.Interfaces;
using EncyclopediaGalactica.Services.Document.Mappers.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsCacheService;
using EncyclopediaGalactica.Services.Document.SourceFormatsCacheService.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsCacheService.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Document;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsRepository.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsService;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Document;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.ExceptionFilters;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces.Document;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces.SourceFormatNode;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.SourceFormatNodeService;
using EncyclopediaGalactica.Services.Document.ValidatorService;
using EncyclopediaGalactica.Utils.GuardsService;
using EncyclopediaGalactica.Utils.GuardsService.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            services.AddControllers(o =>
                {
                    o.Filters.Add<InternalServerErrorExceptionsFilter>();
                    o.Filters.Add<NoSuchEntityExceptionsFilter>();
                    o.Filters.Add<ValidationExceptionsFilter>();
                })
                .AddNewtonsoftJson()
                .AddApplicationPart(typeof(SourceFormatNodeController).Assembly)
                .AddApplicationPart(typeof(DocumentController).Assembly);
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
            services.AddScoped<IDocumentsRepository, DocumentRepository>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IDocumentMappers, DocumentMappers>();
            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SourceFormatNodeValidator>());
            services.AddLogging(log =>
            {
                log.ClearProviders();
                log.AddConsole();
                log.AddDebug();
            });

            ServiceProvider? sp = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(sp);

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