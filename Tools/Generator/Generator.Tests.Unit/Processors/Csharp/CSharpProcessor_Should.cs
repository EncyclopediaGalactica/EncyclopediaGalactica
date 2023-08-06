namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using System.Diagnostics.CodeAnalysis;
using Generator.Managers.FileManager;
using Generator.Managers.PathManager;
using Generator.Processors.CSharp;
using Generator.Structure.cSharp;
using Xunit;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
public partial class CSharpProcessor_Should
{
    private readonly ICSharpProcessor _sut;

    public CSharpProcessor_Should()
    {
        _sut = new CSharpProcessor(
            new FileManagerImpl(),
            new Generator.Managers.StringManager.StringManagerImpl(),
            new PathManagerImpl(),
            new CSharpStructureDescriptor());
    }
}