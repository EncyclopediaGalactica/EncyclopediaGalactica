namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Mappers;

using EncyclopediaGalactica.Common.Contracts;
using Entity;

public class ApplicationMapper : IApplicationMapper
{
    public List<ApplicationResult> ToApplicationResults(List<Application> applications)
    {
        List<ApplicationResult> result = new List<ApplicationResult>();

        if (applications.Any()) result.AddRange(applications.Select(ToApplicationResult));

        return result;
    }

    public ApplicationResult ToApplicationResult(Application application)
    {
        return new ApplicationResult
        {
            Id = application.Id,
            Name = application.Name,
            Description = application.Description
        };
    }

    public Application FromApplicationInput(ApplicationInput applicationInput)
    {
        return new Application
        {
            Id = applicationInput.Id,
            Name = applicationInput.Name,
            Description = applicationInput.Description
        };
    }
}

public interface IApplicationMapper
{
    List<ApplicationResult> ToApplicationResults(List<Application> applications);

    ApplicationResult ToApplicationResult(Application application);
    Application FromApplicationInput(ApplicationInput applicationInput);
}