using EncyclopediaGalactica.BusinessLogic.Contracts;

namespace UIWasm.Services;

public interface IFileFormatService
{
    Task<ICollection<FileFormatResult>> GetAllAsync();
}