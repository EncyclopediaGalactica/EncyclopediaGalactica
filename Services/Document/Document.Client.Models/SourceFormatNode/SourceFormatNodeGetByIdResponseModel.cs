namespace EncyclopediaGalactica.Services.Document.Sdk.Client.Models.SourceFormatNode;

using System.Net;
using Contracts.Input;
using EncyclopediaGalactica.Client.Core.Model.Interfaces;

public class SourceFormatNodeGetByIdResponseModel : IHttpResponseModel<SourceFormatNodeInputContract>
{
    public SourceFormatNodeInputContract? Result { get; set; }
    public bool IsOperationSuccessful { get; set; }
    public string Message { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }
}