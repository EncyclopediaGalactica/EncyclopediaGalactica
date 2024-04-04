namespace UIWasm.Services;

using EncyclopediaGalactica.BusinessLogic.Contracts;

public interface IUserService
{
    Task<ICollection<UserResult>> GetAllAsync();
}