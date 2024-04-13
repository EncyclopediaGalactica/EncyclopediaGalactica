namespace UIWasm.Services;

using EncyclopediaGalactica.BusinessLogic.Contracts;

public class UserService : IUserService
{
    private ICollection<UserResult> _storage = new List<UserResult>
    {
        new UserResult { Id = 1, UserName = "alpha", FirstName = "alpha first", LastName = "alpha last" },
        new UserResult { Id = 2, UserName = "beta", FirstName = "beta first", LastName = "beta last" },
        new UserResult { Id = 3, UserName = "gamma", FirstName = "gamma first", LastName = "gamma last" },
        new UserResult { Id = 4, UserName = "delta", FirstName = "delta first", LastName = "delta last" },
        new UserResult { Id = 5, UserName = "phy", FirstName = "phy first", LastName = "phy last" },
        new UserResult { Id = 6, UserName = "kappa", FirstName = "kappa first", LastName = "kappa last" },
        new UserResult { Id = 7, UserName = "rho", FirstName = "rho first", LastName = "rho last" },
        new UserResult { Id = 8, UserName = "omega", FirstName = "omega first", LastName = "omega last" },
        new UserResult
            { Id = 9, UserName = "alpha.alpha", FirstName = "alpha.alpha first", LastName = "alpha.alpha last" },
        new UserResult
            { Id = 10, UserName = "beta.beta", FirstName = "beta.beta first", LastName = "beta.beta last" },
        new UserResult
            { Id = 11, UserName = "gamma.gamma", FirstName = "gamma.gamma first", LastName = "gamma.gamma last" },
        new UserResult
            { Id = 12, UserName = "delta.delta", FirstName = "delta.delta first", LastName = "delta.delta last" },
    };

    public async Task<ICollection<UserResult>> GetAllAsync()
    {
        return _storage;
    }
}