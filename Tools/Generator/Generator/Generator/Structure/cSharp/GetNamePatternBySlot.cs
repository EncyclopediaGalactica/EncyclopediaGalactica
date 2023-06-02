namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Structure.cSharp;

using Microsoft.Extensions.Logging;

public partial class CSharpStructureDescriptor
{
    /// <inheritdoc />
    public string GetNamingPatternBySlot(string slotName)
    {
        if (!_structure.ContainsKey(slotName))
        {
            _logger.LogError("No naming rule for {Slot}", slotName);
            throw new GeneratorException($"No naming rule for {slotName}");
        }

        return _structure[slotName].NamingPattern;
    }
}