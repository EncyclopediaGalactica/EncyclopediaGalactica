namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Models;

/// <summary>
///     <p>TypeInfo Type</p>
///     <p>The TypeInfo contains all the information related to a single type</p>
///     <p>
///         In the generator internal directory based structure the TypeInfo is the lowest level and it represents a file. A
///         file in case of c-sharp is a type.
///     </p>
/// </summary>
public class TypeInfo
{
    /// <summary>
    ///     Gets or sets the original type name property.
    ///     <remarks>
    ///         The original type name value comes directly from the yaml file and may contains postfix
    ///         like "Dto".
    ///     </remarks>
    /// </summary>
    public string? OriginalTypename { get; set; }

    /// <summary>
    ///     Gets or set the original base namespace token.
    ///     <remarks>
    ///         The original value comes from the configuration file
    ///     </remarks>
    /// </summary>
    public string? OriginalBaseNamespaceToken { get; set; }

    /// <summary>
    ///     Gets or sets the original dto namespace token
    ///     <remarks>
    ///         This value comes from configuration file
    ///     </remarks>
    /// </summary>
    public string? OriginalDtoNamespaceToken { get; set; }

    /// <summary>
    ///     Gets or sets the original type name token.
    ///     <remarks>
    ///         It comes from the yaml file's Components > Schemas > Types:name without any modification
    ///     </remarks>
    /// </summary>
    public string? OriginalTypeNameToken { get; set; }

    /// <summary>
    ///     Gets or sets the reference type name.
    /// </summary>
    public string? TypeName { get; set; }

    /// <summary>
    ///     Gets or sets the reference type name as variable name
    ///     <remarks>
    ///         <para>
    ///             In C# the variable name, which is the type name, starts with a small capital letter
    ///             while the type name starts with capital letter.
    ///             Type name: GeneratedFileInfo
    ///             Variable name: generatedFileInfo
    ///         </para>
    ///     </remarks>
    /// </summary>
    public string? TypenameAsVariableName { get; set; }

    /// <summary>
    ///     Gets or sets the filename
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    ///     Gets or sets the namespace
    /// </summary>
    public string? Namespace { get; set; }

    /// <summary>
    ///     Gets or sets the target directory
    ///     <remarks>
    ///         The provided path is absolute path.
    ///     </remarks>
    /// </summary>
    public string? AbsoluteTargetPath { get; set; }

    /// <summary>
    ///     Gets or sets the original target directory token value
    ///     <remarks>
    ///         The value comes from configuration file
    ///     </remarks>
    /// </summary>
    public string? OriginalTargetDirectoryToken { get; set; }

    /// <summary>
    ///     Gets or sets the original dto project base path token
    ///     <remarks>
    ///         This value comes from the configuration file
    ///     </remarks>
    /// </summary>
    public string? OriginalDtoProjectBasePathToken { get; set; }

    /// <summary>
    ///     Gets or sets the original dto project additional path token
    ///     <remarks>
    ///         This value comes from configuration file
    ///     </remarks>
    /// </summary>
    public string? OriginalDtoProjectAdditionalPathToken { get; set; }

    /// <summary>
    ///     Gets or sets the property info
    /// </summary>
    public ICollection<VariableInfo> VariableInfos { get; set; } = new List<VariableInfo>();

    /// <summary>
    ///     Gets or sets the target path with file name value
    /// </summary>
    public string? TargetPathWithFileName { get; set; }

    /// <summary>
    ///     Gets or sets the used template absolute path with file name
    /// </summary>
    public string? TemplateAbsolutePathWithFileName { get; set; }

    /// <summary>
    ///     Gets or sets the list of required properties
    /// </summary>
    public List<string> RequiredProperties { get; set; } = new List<string>();

    /// <summary>
    ///     Gets or sets the original dto test project base path
    /// </summary>
    public string? OriginalDtoTestProjectBasePathToken { get; set; }

    /// <summary>
    ///     Gets or set the original dto test project additional path
    /// </summary>
    public string? OriginalDtoTestProjectAdditionalPathToken { get; set; }

    /// <summary>
    ///     Gets or sets the original dto test namespace token
    /// </summary>
    public string? OriginalDtoTestProjectNamespaceToken { get; set; }

    /// <summary>
    ///     Gets or sets the type name under test value
    ///     <remarks>
    ///         This property is used during Dto test generation
    ///     </remarks>
    /// </summary>
    public string? TypeNameUnderTest { get; set; }

    /// <summary>
    ///     Gets or sets the Imports value
    /// </summary>
    public List<string> Imports { get; set; } = new List<string>();
}