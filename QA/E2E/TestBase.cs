using Program = Host.Program;

namespace EncyclopediaGalactica.SourceFormats.QA.E2E;

using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Sdk;
using Sdk.Core;
using Sdk.Core.Interfaces;
using Sdk.Interfaces;
using Sdk.SourceFormatNode;
using Xunit;

public class TestBase : IClassFixture<SourceFormatWebApplicationFactory<Program>>
{
    protected readonly SourceFormatWebApplicationFactory<Program> _webApplicationFactory;
    protected HttpClient _httpClient;
    protected ISourceFormatsSdk _sourceFormatsSdk;

    public TestBase(SourceFormatWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
        _httpClient = _webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        ISdkCore sdkCore = new SdkCore(_httpClient);
        ISourceFormatNodeSdk sourceFormatNodeSdk = new SourceFormatNodeSdk(sdkCore);
        _sourceFormatsSdk = new SourceFormatsSdk(sourceFormatNodeSdk);
    }
}