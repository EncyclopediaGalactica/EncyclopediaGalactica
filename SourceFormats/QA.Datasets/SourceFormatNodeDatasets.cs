namespace EncyclopediaGalactica.SourceFormats.QA.Datasets;

public static class SourceFormatNodeDatasets
{
    public static IEnumerable<object?[]> AddValidationDataSet => new List<object?[]>
    {
        new object?[] { null },
        new object?[] { string.Empty },
        new object?[] { " " },
        new object?[] { "as" },
        new object?[] { "   " }
    };

    public static IEnumerable<object?[]> UpdateValidationDataSet => new List<object?[]>
    {
        new object?[] { 0, "asd" },
        new object?[] { 1, null },
        new object?[] { 1, string.Empty },
        new object?[] { 1, "  " },
        new object?[] { 1, "as" },
        new object?[] { 1, "as " },
        new object?[] { 1, "   " }
    };
}