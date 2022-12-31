namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator;

using Newtonsoft.Json;

public class CodeGeneratorConfiguration
{
    public CodeGeneratorConfiguration()
    {
    }

    [JsonProperty("openapi_specification_path")]
    public string OpenApiSpecificationPath { get; set; }

    [JsonProperty("solution_directory")]
    public string SolutionDirectory { get; set; }

    [JsonProperty("solution_name")]
    public string SolutionName { get; set; }

    [JsonProperty("solution_base_namespace")]
    public string SolutionBaseNamespace { get; set; }

    [JsonProperty("dto_project_name")]
    public string DtoProjectName { get; set; }

    /// <summary>
    ///     Sets or gets the DtoProjectBasePath property
    ///     <remarks>
    ///         This directory is the base directory for the whole Dto project
    ///     </remarks>
    /// </summary>
    [JsonProperty("dto_project_base_path")]
    public string DtoProjectBasePath { get; set; }

    /// <summary>
    ///     Sets or gets the DtoProjectAdditionalPath property
    ///     <remarks>
    ///         This path segment will be added to the <see cref="DtoProjectBasePath" />, so that
    ///         for this exact generation a separate path can be defined.
    ///     </remarks>
    /// </summary>
    [JsonProperty("dto_project_additional_path")]
    public string DtoProjectAdditionalPath { get; set; }

    [JsonProperty("dto_project_namespace")]
    public string DtoProjectNameSpace { get; set; }

    [JsonProperty("dto_test_project_name")]
    public string DtoTestProjectName { get; set; }

    [JsonProperty("dto_test_project_base_path")]
    public string DtoTestProjectBasePath { get; set; }

    [JsonProperty("dto_test_project_namespace")]
    public string DtoTestProjectNameSpace { get; set; }

    [JsonProperty("skip_dto_generating")]
    public bool SkipDtoGenerating { get; set; }

    [JsonProperty("skip_dto_tests_generating")]
    public bool SkipDtoTestsGenerating { get; set; }

    [JsonProperty("skip_request_model_generating")]
    public bool SkipRequestModelGenerating { get; set; }

    [JsonProperty("skip_request_model_tests_generating")]
    public bool SkipRequestModelTestsGenerating { get; set; }

    [JsonProperty("test_mode")]
    public bool TestMode { get; set; }
}