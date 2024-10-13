namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Mappers;

using EncyclopediaGalactica.Common.Contracts;
using Entity;

public static class FiletypeMapper
{
    public static Filetype MapToFiletypeEntity(this FiletypeInput filetypeInput)
    {
        return new Filetype
        {
            Id = filetypeInput.Id,
            Name = filetypeInput.Name,
            Description = filetypeInput.Description,
            FileExtension = filetypeInput.FileExtension
        };
    }

    public static FiletypeResult MapToFiletypeResult(this Filetype filetype)
    {
        return new FiletypeResult
        {
            Id = filetype.Id,
            Name = filetype.Name,
            Description = filetype.Description,
            FileExtension = filetype.FileExtension
        };
    }

    public static List<FiletypeResult> MapToFiletypeResultList(this List<Filetype> filetypes)
    {
        List<FiletypeResult> result = new();
        if (!filetypes.Any())
        {
            return result;
        }

        filetypes.ForEach(item => result.Add(new FiletypeResult
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            FileExtension = item.FileExtension
        }));
        return result;
    }
}