namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.OpenApiDocumentManager;

public class Components : IComponents
{
    public Components()
    {
        Schemas = new Schemas();
    }

    public ISchemas Schemas { get; }
}