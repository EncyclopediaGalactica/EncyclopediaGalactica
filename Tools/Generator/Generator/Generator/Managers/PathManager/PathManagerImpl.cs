namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.PathManager;

using System.Text;
using Microsoft.Extensions.Logging;

/// <inheritdoc />
public partial class PathManagerImpl : IPathManager
{
    private readonly Logger<PathManagerImpl> _logger = new(LoggerFactory.Create(c => c.AddConsole()));

    private string ConcatenatePathSegments(
        string? pathSegment1 = null,
        string? pathSegment2 = null,
        string? pathSegment3 = null)
    {
        StringBuilder builder = new StringBuilder();
        bool isFirstParamNotUsable = false;
        bool isSecondParamNotUsable = false;

        if (!string.IsNullOrEmpty(pathSegment1)
            && !string.IsNullOrWhiteSpace(pathSegment1))
        {
            builder.Append(pathSegment1.Trim());
        }
        else
        {
            isFirstParamNotUsable = true;
        }

        if (!string.IsNullOrEmpty(pathSegment2)
            && !string.IsNullOrWhiteSpace(pathSegment2))
        {
            if (isFirstParamNotUsable)
            {
                builder.Append(pathSegment2.Trim());
            }
            else
            {
                builder.Append("/").Append(pathSegment2.Trim());
            }
        }
        else
        {
            isSecondParamNotUsable = true;
        }

        if (!string.IsNullOrEmpty(pathSegment3)
            && !string.IsNullOrWhiteSpace(pathSegment3))
        {
            if (isFirstParamNotUsable && isSecondParamNotUsable)
            {
                builder.Append(pathSegment3.Trim());
            }
            else
            {
                builder.Append("/").Append(pathSegment3.Trim());
            }
        }

        return builder.ToString();
    }
}