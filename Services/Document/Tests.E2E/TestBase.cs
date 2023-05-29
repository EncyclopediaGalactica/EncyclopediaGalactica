namespace EncyclopediaGalactica.Services.Document.Tests.E2E;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using Client.Core;
using Client.Core.Interfaces;
using EncyclopediaGalactica.Host.RestApi;
using EncyclopediaGalactica.Services.Document.Sdk;
using EncyclopediaGalactica.Services.Document.Sdk.Document;
using EncyclopediaGalactica.Services.Document.Sdk.Interfaces;
using EncyclopediaGalactica.Services.Document.Sdk.SourceFormatNode;
using Microsoft.AspNetCore.Mvc.Testing;

public class TestBase : SourceFormatWebApplicationFactory<Program>
{
    protected readonly HttpClient _httpClient;
    protected readonly ISourceFormatsSdk SourceFormatsSdk;
    protected readonly SourceFormatWebApplicationFactory<Program> WebApplicationFactory;

    public TestBase()
    {
        WebApplicationFactory = new SourceFormatWebApplicationFactory<Program>();
        _httpClient = WebApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        _httpClient.BaseAddress = new Uri("http://localhost");
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        ISdkCore sdkCore = new SdkCore(_httpClient);
        ISourceFormatNodeSdk sourceFormatNodeSdk = new SourceFormatNodeSdk(sdkCore);
        IDocumentsSdk documentsSdk = new DocumentSdk(sdkCore);
        SourceFormatsSdk = new SourceFormatsSdk(sourceFormatNodeSdk, documentsSdk);
    }
}