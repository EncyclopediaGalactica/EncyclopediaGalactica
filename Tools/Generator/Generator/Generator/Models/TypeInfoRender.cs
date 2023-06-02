namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Models;

/// <summary>
///     TypeInfoRender carries all the data needed for successful template compile
/// </summary>
public class TypeInfoRender
{
    /// <summary>
    ///     Gets or sets the value of TimeOfGeneration
    /// </summary>
    public string? TimeOfGeneration { get; set; }

    /// <summary>
    ///     Gets or sets the namespace value
    /// </summary>
    public string? Namespace { get; set; }

    /// <summary>
    ///     Gets or sets the VariableInfos value
    /// </summary>
    public List<VariableInfoRender> VariableInfos { get; set; } = new List<VariableInfoRender>();

    /// <summary>
    ///     Gets or sets the TypeName value
    /// </summary>
    public string? TypeName { get; set; }

    /// <summary>
    ///     Gets or sets the TypeNameUnderTest value
    /// </summary>
    public string? TypeNameUnderTest { get; set; }

    /// <summary>
    ///     Gets or sets the Imports value
    /// </summary>
    public List<string> Imports { get; set; } = new List<string>();
}