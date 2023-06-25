namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Generators;

using System.Globalization;
using Configuration;
using Managers.ConfigurationValuesManager;
using Managers.FileManager;
using Managers.OpenApiToTypeInfoManager;
using Managers.PathManager;
using Managers.StringManager;
using Managers.TemplateManager;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models;

public abstract class AbstractGenerator : ICodeGenerator
{
    private readonly Logger<AbstractGenerator> _logger = new Logger<AbstractGenerator>(LoggerFactory.Create(
        c => c.AddConsole()));

    protected IConfigurationValuesManager ConfigurationValuesManager;
    protected IFileManager FileManager;
    protected CodeGeneratorConfiguration GeneratorConfiguration;
    protected IOpenApiToTypeInfoManager OpenApiToTypeInfoManager;
    protected OpenApiDocument OpenApiYamlSchema;
    protected IPathManager PathManager;
    protected IStringManager StringManager;
    protected ITemplateManager TemplateManager;
    protected string TimeOfGeneration = DateTime.Now.ToString(CultureInfo.InvariantCulture);

    public abstract string DtoTemplatePath { get; }
    public abstract string DtoTestTemplatePath { get; }
    public SolutionInfo SolutionInfo { get; } = new SolutionInfo();

    public List<TypeInfo> DtoTypeInfos { get; } = new List<TypeInfo>();

    public List<TypeInfo> DtoTestTypeInfos { get; } = new List<TypeInfo>();

    public ICodeGenerator SetTemplateManager(ITemplateManager templateManager)
    {
        ArgumentNullException.ThrowIfNull(templateManager);
        TemplateManager = templateManager;
        return this;
    }

    public ICodeGenerator SetGeneratorConfiguration(CodeGeneratorConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        GeneratorConfiguration = configuration;
        return this;
    }

    public ICodeGenerator SetOpenApiYamlSchema(OpenApiDocument openApiDocument)
    {
        ArgumentNullException.ThrowIfNull(openApiDocument);
        OpenApiYamlSchema = openApiDocument;
        return this;
    }

    public ICodeGenerator SetFileManager(IFileManager fileManager)
    {
        ArgumentNullException.ThrowIfNull(fileManager);
        FileManager = fileManager;
        return this;
    }

    public ICodeGenerator SetPathManager(IPathManager pathManager)
    {
        ArgumentNullException.ThrowIfNull(pathManager);
        PathManager = pathManager;
        return this;
    }

    public abstract ICodeGenerator Generate();

    public ICodeGenerator SetStringManager(IStringManager stringManager)
    {
        ArgumentNullException.ThrowIfNull(stringManager);
        StringManager = stringManager;
        return this;
    }

    public abstract ICodeGenerator Build();

    public ICodeGenerator SetOpenApiToFileInfoManager(IOpenApiToTypeInfoManager openApiToTypeInfoManager)
    {
        ArgumentNullException.ThrowIfNull(openApiToTypeInfoManager);
        OpenApiToTypeInfoManager = openApiToTypeInfoManager;
        return this;
    }

    public ICodeGenerator SetConfigurationToTypeInfoManager(IConfigurationValuesManager manager)
    {
        ArgumentNullException.ThrowIfNull(manager);
        ConfigurationValuesManager = manager;
        return this;
    }

    public abstract void PreProcessDtos();

    protected bool ShouldIRunDtoGeneration()
    {
        return GeneratorConfiguration.SkipDtoGenerating;
    }

    protected bool ShouldIRunDtoPreProcessing()
    {
        return GeneratorConfiguration.SkipDtoPreProcess;
    }

    protected bool ShouldIRunDtoTestPreProcessing()
    {
        return GeneratorConfiguration.SkipDtoTestPreProcess;
    }

    protected bool ShouldIRunDtoTestGeneration()
    {
        return GeneratorConfiguration.SkipDtoTestGenerating;
    }

    /// <summary>
    ///     Checks if every library is initialised for starting code generation.
    /// </summary>
    /// <exception cref="GeneratorException">
    ///     If any of the libraries are null
    /// </exception>
    protected void Init()
    {
        if (FileManager is null
            || PathManager is null
            || StringManager is null
            || OpenApiToTypeInfoManager is null
            || ConfigurationValuesManager is null
            || GeneratorConfiguration is null
            || OpenApiYamlSchema is null)
        {
            throw new GeneratorException(
                "Generator initialization failed. One or some of the supporting libraries are not initialized.");
        }
    }

    protected void GetOriginalSolutionNameTokenFromConfiguration(SolutionInfo solutionInfo)
    {
        ConfigurationValuesManager.GetOriginalSolutionNameTokenFromConfiguration(
            solutionInfo,
            GeneratorConfiguration);
    }

    protected void GetOriginalBaseNamespaceTokenFromConfiguration(List<TypeInfo> fileInfos)
    {
        ConfigurationValuesManager.GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos(
            fileInfos,
            GeneratorConfiguration);
    }

    protected void GetOriginalTargetPathTokenFromConfiguration(List<TypeInfo> fileInfos)
    {
        ConfigurationValuesManager.GetOriginalTargetDirectoryTokenFromConfiguration(
            fileInfos,
            GeneratorConfiguration);
    }

    protected void GetOriginalTargetPathTokenFromConfiguration(SolutionInfo solutionInfo)
    {
        ConfigurationValuesManager.GetOriginalTargetDirectoryTokenFromConfiguration(
            solutionInfo,
            GeneratorConfiguration);
    }

    protected void GetOriginalSolutionFileTypeTokenFromConfiguration(SolutionInfo solutionInfo)
    {
        ConfigurationValuesManager.GetOriginalSolutionFileTypeTokenFromConfigurationAndAddToSolutionInfo(
            solutionInfo,
            GeneratorConfiguration);
    }

    protected void GetOriginalSolutionProjectFileTypeTokenFromConfiguration(SolutionInfo solutionInfo)
    {
        ConfigurationValuesManager.GetOriginalSolutionProjectFileTypeTokenFromConfigurationAndAddToSolutionInfo(
            solutionInfo,
            GeneratorConfiguration);
    }

    protected void GetOriginalTypeNameTokenFromOpenApiSchema(List<TypeInfo> typeInfos)
    {
        OpenApiToTypeInfoManager.GetTypeNamesFromOpenApiAndAddToTypeInfo(typeInfos, OpenApiYamlSchema);
    }

    protected void GetOriginalRequiredPropertiesByTypeFromOpenApiSchema(List<TypeInfo> typeInfos)
    {
        OpenApiToTypeInfoManager.GetRequiredPropertiesByTypeAndAddToTypeInfo(typeInfos, OpenApiYamlSchema);
    }

    protected void GetOriginalPropertyNamesByTypeFromOpenApiSchema(List<TypeInfo> typeInfos)
    {
        OpenApiToTypeInfoManager.GetPropertyNamesByTypeAndAddToTypeInfo(typeInfos, OpenApiYamlSchema);
    }

    protected void GetOriginalPropertyTypesByTypeFromOpenApiSchema(List<TypeInfo> typeInfos)
    {
        OpenApiToTypeInfoManager.GetPropertyTypesByTypeAndAddTypeInfo(typeInfos, OpenApiYamlSchema);
    }

    protected void MarkVariablesAsPropertiesFromOpenApiSchema(List<TypeInfo> typeInfos)
    {
        OpenApiToTypeInfoManager.MarkVariablesAsPropertyBasedOnOpenApiSchema(typeInfos, OpenApiYamlSchema);
    }
}