namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.OpenApiDocumentManager;

public class OpenApiDocumentManager : IOpenApiDocumentManager
{
    public OpenApiDocumentManager()
    {
        Components = new Components();
    }

    public IComponents Components { get; }
}