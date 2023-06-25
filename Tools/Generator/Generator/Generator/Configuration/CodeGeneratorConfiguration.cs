namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Configuration;

using Newtonsoft.Json;

public class CodeGeneratorConfiguration
{
    /// <summary>
    ///     Gets or sets the generated programming language name
    /// </summary>
    [JsonProperty("lang")]
    public string Lang { get; set; }

    [JsonProperty("openapi_specification_path")]
    public string OpenApiSpecificationPath { get; set; }

    /// <summary>
    ///     Gets or sets the solution file type value
    /// </summary>
    [JsonProperty("solution_file_type")]
    public string SolutionFileType { get; set; }

    /// <summary>
    ///     Gets or sets the solution project file type
    ///     <remarks>
    ///         In case of c-sharp this value is <b>csproj</b>
    ///     </remarks>
    /// </summary>
    [JsonProperty("solution_project_file_type")]
    public string SolutionProjectFileType { get; set; }

    /// <summary>
    /// Gets or sets the target directory
    /// <remarks>
    /// The target directory is where the solution file and the whole solution is placed
    /// </remarks>
    /// </summary>
    [JsonProperty("target_directory")]
    public string TargetDirectory { get; set; }

    /// <summary>
    ///     Sets or gets the solution name
    ///     <remarks>
    ///         This is the solution name and will be used to generate projects within the solution
    ///     </remarks>
    /// </summary>
    [JsonProperty("solution_name")]
    public string? SolutionName { get; set; }

    /// <summary>
    ///     Gets or sets the solution base name space.
    ///     <remarks>
    ///         During generation this value is the base.
    ///         If any other subproject namespace is given they will be concatenated
    ///     </remarks>
    /// </summary>
    [JsonProperty("solution_base_namespace")]
    public string? SolutionBaseNamespace { get; set; }

    /// <summary>
    ///     Gets or sets skip dto preprocess value
    ///     <remarks>
    ///         This value controls if DTO preprocess phase is skipped or not
    ///     </remarks>
    /// </summary>
    [JsonProperty("skip_dto_preprocess")]
    public bool SkipDtoPreProcess { get; set; }

    /// <summary>
    ///     Gets or sets skip dto generating value
    ///     <remarks>
    ///         This value controls if DTO generating phase will be skipped or not
    ///     </remarks>
    /// </summary>
    [JsonProperty("skip_dto_generating")]
    public bool SkipDtoGenerating { get; set; }

    /// <summary>
    ///     Gets or sets skip dto test preprocess value
    ///     <remarks>
    ///         This value controls if DTO test preprocess will be skipped or not
    ///     </remarks>
    /// </summary>
    [JsonProperty("skip_dto_test_preprocess")]
    public bool SkipDtoTestPreProcess { get; set; }

    /// <summary>
    ///     Gets or sets skip dto test generating value
    ///     <remarks>
    ///         This value controls if Dto test generating phase will be skipped or not
    ///     </remarks>
    /// </summary>
    [JsonProperty("skip_dto_test_generating")]
    public bool SkipDtoTestGenerating { get; set; }
}