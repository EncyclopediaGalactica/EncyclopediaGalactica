namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.StringManager;

using Microsoft.Extensions.Logging;

/// <inheritdoc />
public partial class StringManagerImpl : IStringManager
{
    private readonly Logger<StringManager.StringManagerImpl> _logger = new(LoggerFactory.Create(c => c.AddConsole()));
}