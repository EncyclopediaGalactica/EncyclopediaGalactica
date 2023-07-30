namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator;

using Configuration;
using FluentValidation;
using Generators;
using Managers.ConfigurationValuesManager;
using Managers.FileManager;
using Managers.OpenApiToTypeInfoManager;
using Managers.PathManager;
using Managers.StringManager;
using Managers.TemplateManager;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Newtonsoft.Json;

public class CodeGenerator
{
    private readonly IConfigurationValuesManager _configurationValuesManager;

    // private readonly string _dtoTestTemplatePath = "Templates/dto_tests.handlebars";
    private readonly IFileManager _fileManager;
    private readonly CodeGeneratorConfiguration _generatorConfiguration;
    private readonly ILogger<CodeGenerator> _logger;
    private readonly OpenApiDocument _openApiDocument;
    private readonly IOpenApiToTypeInfoManager _openApiToTypeInfoManager;
    private readonly IPathManager _pathManager;
    private readonly IStringManager _stringManager;
    private readonly ITemplateManager _templateManager;
    private List<string> _availableCodeGenerators = new List<string> { "csharp" };

    private CodeGenerator(OpenApiDocument openApiDocument, CodeGeneratorConfiguration generatorConfiguration)
    {
        ArgumentNullException.ThrowIfNull(openApiDocument);
        ArgumentNullException.ThrowIfNull(generatorConfiguration);

        _openApiDocument = openApiDocument;
        _generatorConfiguration = generatorConfiguration;

        _logger = new Logger<CodeGenerator>(LoggerFactory.Create(c => c.AddConsole()));
        _fileManager = new FileManagerImpl();
        _pathManager = new PathManagerImpl();
        _templateManager = new TemplateManagerImpl();
        _stringManager = new Managers.StringManager.StringManagerImpl();
        _openApiToTypeInfoManager = new OpenApiToTypeInfoManager();
        _configurationValuesManager = new ConfigurationValuesManager();
        CreateGenerator(generatorConfiguration);
        SpecificCodeGenerator!.Generate();
        // Generate();
    }

    public ICodeGenerator SpecificCodeGenerator { get; private set; }

    private void CreateGenerator(CodeGeneratorConfiguration generatorConfiguration)
    {
        if (!_availableCodeGenerators.Contains(generatorConfiguration.Lang.ToLower()))
        {
            throw new GeneratorException($"Language, {generatorConfiguration.Lang}, is not available.");
        }

        switch (generatorConfiguration.Lang)
        {
            case "csharp":
                SpecificCodeGenerator = new CSharpGenerator()
                    .SetGeneratorConfiguration(generatorConfiguration)
                    .SetOpenApiYamlSchema(_openApiDocument)
                    .SetFileManager(_fileManager)
                    .SetPathManager(_pathManager)
                    .SetTemplateManager(_templateManager)
                    .SetStringManager(_stringManager)
                    .SetOpenApiToFileInfoManager(_openApiToTypeInfoManager)
                    .SetConfigurationToTypeInfoManager(_configurationValuesManager)
                    .Build();
                break;
        }
    }

    public class Builder
    {
        private readonly ILogger<CodeGenerator.Builder> _logger;
        private string _actualPath = Directory.GetCurrentDirectory();
        private string _generatorConfigurationPath;


        public Builder()
        {
            _logger = new Logger<Builder>(LoggerFactory.Create(c => c.AddConsole()));
        }

        /// <summary>
        ///     Set path to configuration file.
        /// </summary>
        /// <param name="path">The path to configuration file</param>
        /// <returns>Returns a Builder</returns>
        public Builder SetPath(string path)
        {
            _generatorConfigurationPath = path;
            return this;
        }

        public CodeGenerator? Generate()
        {
            try
            {
                if (string.IsNullOrEmpty(_generatorConfigurationPath))
                {
                    string msg = "Path to configuration file is not defined";
                    _logger.LogError(msg);
                    throw new GeneratorException(msg);
                }

                if (!File.Exists(_generatorConfigurationPath))
                {
                    string msg = $"Config file path is invalid: {_generatorConfigurationPath}!";
                    _logger.LogError(msg);
                    throw new GeneratorException(msg);
                }

                _logger.LogInformation(
                    "=== Encyclopedia Galactica Rest Api Sdk Generator ==="
                );
                _logger.LogInformation("Generator config file path: {Path}", _generatorConfigurationPath);

                string configFileContent = File.ReadAllText(_generatorConfigurationPath);
                CodeGeneratorConfiguration generatorConfiguration =
                    JsonConvert.DeserializeObject<CodeGeneratorConfiguration>(configFileContent);

                if (generatorConfiguration is null)
                {
                    string msg = "Configuration file deserialization is unsuccessful.";
                    _logger.LogError(msg);
                    throw new GeneratorException(msg);
                }

                _logger.LogInformation("Generator configuration has been parsed");

                CodeGeneratorConfigurationValidator configFileValidator = new CodeGeneratorConfigurationValidator();
                configFileValidator.Validate(generatorConfiguration, o => { o.ThrowOnFailures(); });

                string yamlFileFullPath = $"{_actualPath}/{generatorConfiguration.OpenApiSpecificationPath}";
                _logger.LogInformation("OpenApi yaml file location: {Path}", yamlFileFullPath);

                using FileStream yamlString = new FileStream(
                    yamlFileFullPath,
                    FileMode.Open);
                OpenApiDocument? openApiSpecification = new OpenApiStreamReader().Read(
                    yamlString,
                    out OpenApiDiagnostic? openApiDiagnostic);
                return new CodeGenerator(openApiSpecification, generatorConfiguration);
            }
            catch (Exception e)
            {
                _logger.LogError(
                    "Unsuccessful code generation. For further information see inner exception. " +
                    "Inner exception message: {Message} ", e.Message
                );
                throw new GeneratorException(
                    "Unsuccessful code generation. For further information see inner exception. " +
                    $"Inner exception message: {e.Message} " +
                    $"Stack trace: {e.StackTrace}",
                    e);
            }
        }
    }
}