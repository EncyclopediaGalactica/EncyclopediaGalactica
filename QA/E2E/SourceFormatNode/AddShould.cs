namespace E2E.SourceFormatNode;

using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

[ExcludeFromCodeCoverage]
public class AddShould : IClassFixture<SourceFormatWebApplicationFactory<Program>>
{
    private HttpClient _httpClient;
    private readonly SourceFormatWebApplicationFactory<Program> _webApplicationFactory;

    public AddShould(SourceFormatWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
        _httpClient = _webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task Add()
    {
    }
}