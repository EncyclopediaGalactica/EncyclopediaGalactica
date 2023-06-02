namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.ConfigurationValuesManager;

using Microsoft.Extensions.Logging;

/// <inheritdoc />
public partial class ConfigurationValuesManager : IConfigurationValuesManager
{
    private readonly Logger<ConfigurationValuesManager> _logger = new Logger<ConfigurationValuesManager>(
        LoggerFactory.Create(options => options.AddConsole()));
}