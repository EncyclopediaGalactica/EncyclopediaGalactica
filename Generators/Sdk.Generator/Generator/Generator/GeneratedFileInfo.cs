namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator;

public class GeneratedFileInfo
{
    public string FileName { get; set; }
    
    public string Namespace { get; set; }
    
    public ICollection<PropertyInfo> PropertyInfos { get; set; } = new List<PropertyInfo>();
}