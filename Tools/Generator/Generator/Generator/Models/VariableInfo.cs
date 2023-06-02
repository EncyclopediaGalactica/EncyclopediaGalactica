namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Models;

/// <summary>
///     VariableInfo class describes a variable within a type (c-sharp class)
///     <remarks>
///         A variable can be the following:
///         <list type="bullet">
///             <item>simple variable used in for example tests</item>
///             <item>property</item>
///         </list>
///     </remarks>
/// </summary>
public class VariableInfo
{
    /// <summary>
    ///     Gets or sets the original variable name token value
    ///     <remarks>
    ///         This value comes from the OpenApi file
    ///     </remarks>
    /// </summary>
    public string? OriginalVariableNameToken { get; set; }

    /// <summary>
    ///     Gets or sets the variable name
    ///     <remarks>
    ///         This variable name is result of processing <see cref="OriginalVariableNameToken" />
    ///     </remarks>
    /// </summary>
    public string? VariableName { get; set; }

    /// <summary>
    ///     Gets or sets the value of original variable type name token
    ///     <remarks>
    ///         This value comes from directly the provided OpenApi file
    ///     </remarks>
    /// </summary>
    public string OriginalVariableTypeNameToken { get; set; }

    /// <summary>
    ///     Gets or sets the variable type name value
    ///     <remarks>
    ///         This value is result of processing <see cref="OriginalVariableTypeNameToken" />
    ///     </remarks>
    /// </summary>
    public string VariableTypeName { get; set; }

    /// <summary>
    ///     Gets or sets the value of is nullable
    ///     <remarks>
    ///         This value comes from the provided OpenApi file
    ///     </remarks>
    /// </summary>
    public bool IsNullable { get; set; }

    public string? OriginalVariableTypeFormat { get; set; }

    /// <summary>
    ///     Gets or sets value of is property
    ///     <remarks>
    ///         Every variable created by OpenApi file is property
    ///     </remarks>
    /// </summary>
    public bool IsProperty { get; set; }

    /// <summary>
    ///     Gets or set IsString value
    /// </summary>
    public bool IsString { get; set; }

    /// <summary>
    ///     Gets or set IsInt value
    /// </summary>
    public bool IsInt { get; set; }

    /// <summary>
    ///     Gets or sets IsLong value
    /// </summary>
    public bool IsLong { get; set; }

    /// <summary>
    ///     Gets or sets IsFloat value
    /// </summary>
    public bool IsFloat { get; set; }

    /// <summary>
    ///     Gets or sets IsDouble value
    /// </summary>
    public bool IsDouble { get; set; }

    /// <summary>
    ///     Gets or set IsBool value
    /// </summary>
    public bool IsBool { get; set; }
}