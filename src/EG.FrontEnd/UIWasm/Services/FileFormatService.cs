using EncyclopediaGalactica.BusinessLogic.Contracts;

namespace UIWasm.Services;

public class FileFormatService : IFileFormatService
{
    private ICollection<FileFormatResult> _storage = new List<FileFormatResult>
    {
        new FileFormatResult
        {
            Id = 1,
            Name = "TXT file",
            Description = "TXT file",
            FileExtension = ".txt"
        },
        new FileFormatResult
        {
            Id = 2,
            Name = "Epub file",
            Description = "Epub file",
            FileExtension = ".epub"
        },
        new FileFormatResult
        {
            Id = 3,
            Name = "HTML file",
            Description = "HTML file",
            FileExtension = ".html"
        },
        new FileFormatResult
        {
            Id = 4,
            Name = "DOCX file",
            Description = "DOCX file",
            FileExtension = ".docx"
        },
        new FileFormatResult
        {
            Id = 5,
            Name = "ODT file",
            Description = "ODT file",
            FileExtension = ".odt"
        },
    };

    public async Task<ICollection<FileFormatResult>> GetAllAsync()
    {
        return _storage;
    }
}