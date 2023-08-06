namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.TestBase;

using System.Diagnostics.CodeAnalysis;
using Generator.Managers.OpenApiDocumentManager;

[ExcludeFromCodeCoverage]
public class TestBase
{
    protected readonly ISchemasTestData SchemasTestData;
    protected readonly OpenApiDocumentManager Sut;

    public TestBase()
    {
        SchemasTestData = new SchemasTestData();
        Sut = new OpenApiDocumentManager();
    }
}