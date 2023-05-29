namespace EncyclopediaGalactica.Services.Document.Sdk.Models.SourceFormatNode;

using System.Net;
using Client.Core.Model.Interfaces;
using Dtos;

public class SourceFormatNodeGetByIdResponseModel : IHttpResponseModel<SourceFormatNodeDto>
{
    public SourceFormatNodeDto? Result { get; set; }
    public bool IsOperationSuccessful { get; set; }
    public string Message { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }
}