namespace EncyclopediaGalactica.SourceFormats.QA.Datasets;

public static class SourceFormatNodeDatasets
{
    public static IEnumerable<object?[]> ValidationDataSet => new List<object?[]>
    {
        new object?[] { null },
        new object?[] { string.Empty },
        new object?[] { " " },
        new object?[] { "as" },
        new object?[] { "   " },
    };
}