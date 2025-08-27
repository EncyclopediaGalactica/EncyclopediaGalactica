namespace EncyclopediaGalactica.CommandLineInterface.Infrastructure;

public class AppSettings
{
    public ConnectionStrings? ConnectionStrings { get; set; }
    public Exercises? Exercises { get; set; }
}

public class ConnectionStrings
{
    public string? DefaultConnection { get; set; }
}

public class Exercises
{
    public ConnectionStrings? ConnectionStrings { get; set; }
    public string? TextBookCatalogPath { get; set; }
    public string? GeneratedTestsPath { get; set; }
}