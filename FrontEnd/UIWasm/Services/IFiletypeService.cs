namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public interface IFiletypeService
{
    Task<ICollection<FiletypeResult>> GetAllAsync();
}