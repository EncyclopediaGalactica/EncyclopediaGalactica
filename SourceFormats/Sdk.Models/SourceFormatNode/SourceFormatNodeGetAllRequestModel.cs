namespace EncyclopediaGalactica.SourceFormats.Sdk.Models.SourceFormatNode;

using System.Net.Http.Headers;
using Dtos;
using EncyclopediaGalactica.Sdk.Core.Model.Interfaces;

public class SourceFormatNodeGetAllRequestModel : IRequestModel<List<SourceFormatNodeDto>>
{
    public List<SourceFormatNodeDto>? Payload { get; }
    public List<MediaTypeWithQualityHeaderValue> AcceptHeaders { get; }
}