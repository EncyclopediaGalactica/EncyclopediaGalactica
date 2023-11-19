namespace EncyclopediaGalactica.Services.Document.Specifications.Endpoints.Graphql;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Controllers.Document;
using Controllers.ExceptionFilters;
using Controllers.SourceFormatNode;
using Ctx;
using FluentValidation.AspNetCore;
using Mappers;
using Mappers.Document;
using Mappers.Interfaces;
using Mappers.SourceFormatNode;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Document;
using Repository.Interfaces;
using Repository.SourceFormatNode;
using Service;
using Service.Document;
using Service.Interfaces;
using Service.Interfaces.Document;
using Service.Interfaces.SourceFormatNode;
using Service.SourceFormatNodeService;
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
                services.SingleOrDefault(d => d.ServiceType == typeof(DocumentDbContext));
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
            services.AddDbContext<DocumentDbContext>(options =>
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
                DocumentDbContext db = scopedServices.GetRequiredService<DocumentDbContext>();
                ILogger<SourceFormatWebApplicationFactory<TStartup>> logger = scopedServices
                    .GetRequiredService<ILogger<SourceFormatWebApplicationFactory<TStartup>>>();

                db.Database.EnsureCreated();
            }
        });
    }
}