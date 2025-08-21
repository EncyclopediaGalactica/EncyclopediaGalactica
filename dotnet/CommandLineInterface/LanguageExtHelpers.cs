namespace EncyclopediaGalactica.CommandLineInterface;

using System.CommandLine;
using Common;

public static class LanguageExtHelpers
{
    public static Either<EgError, T?> GetValueFromParseResult<T>(string key, ParseResult parseResult)
    {
        try
        {
            T? result = parseResult.GetValue<T>(key);
            return Right(result);
        }
        catch (Exception e)
        {
            string typeName = typeof(T).Name;
            return Left(new EgError($"Type: {typeName} \n Message: {e.Message}", e.StackTrace));
        }
    }
}