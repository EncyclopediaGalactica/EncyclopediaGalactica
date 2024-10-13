namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

using EncyclopediaGalactica.Common.Contracts;

public interface IGetAllApplicationsCommand
{
    Task<List<ApplicationResult>> GetAllAsync(CancellationToken cancellationToken = default);
}