namespace EncyclopediaGalactica.SourceFormats.Api;

public struct SourceFormats
{
    public const string Route = "api/sourceformats";
}

public struct SourceFormatNode
{
    public const string Route = SourceFormats.Route + "/sourceformatnode";

    public const string Add = "/add";
    public const string GetAll = "/get";
}