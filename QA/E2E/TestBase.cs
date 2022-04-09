using Program = Host.Program;

namespace EncyclopediaGalactica.SourceFormats.QA.E2E;

using System.Net.Http;
using EncyclopediaGalactica.Sdk.Core;
using EncyclopediaGalactica.Sdk.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Sdk;
using Sdk.Interfaces;
using Sdk.SourceFormatNode;
using Xunit;

public class TestBase : IClassFixture<SourceFormatWebApplicationFactory<Program>>
{
    protected readonly HttpClient _httpClient;
    protected readonly ISourceFormatsSdk SourceFormatsSdk;
    protected readonly SourceFormatWebApplicationFactory<Program> WebApplicationFactory;

    public TestBase(SourceFormatWebApplicationFactory<Program> webApplicationFactory)
    {
        WebApplicationFactory = webApplicationFactory;
        _httpClient = WebApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        ISdkCore sdkCore = new SdkCore(_httpClient);
        ISourceFormatNodeSdk sourceFormatNodeSdk = new SourceFormatNodeSdk(sdkCore);
        SourceFormatsSdk = new SourceFormatsSdk(sourceFormatNodeSdk);
    }
}