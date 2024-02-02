namespace EncyclopediaGalactica.Services.Document.Sdk.Client.Models.Document;

using System.Net;
using Contracts.Input;
using EncyclopediaGalactica.Client.Core.Model.Interfaces;

public class DocumentAddResponseModel : IHttpResponseModel<DocumentInput>
{
    public DocumentInput? Result { get; set; }
    public bool IsOperationSuccessful { get; set; }
    public string Message { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }
}