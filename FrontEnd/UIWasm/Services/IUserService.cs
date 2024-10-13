namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public interface IUserService
{
    Task<ICollection<UserResult>> GetAllAsync();
}