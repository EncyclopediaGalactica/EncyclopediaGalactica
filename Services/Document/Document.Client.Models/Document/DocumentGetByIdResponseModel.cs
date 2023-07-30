namespace EncyclopediaGalactica.Services.Document.Sdk.Models.Document;

using System.Net;
using Client.Core.Model.Interfaces;
using Dtos;

public class DocumentGetByIdResponseModel : IHttpResponseModel<DocumentDto>
{
    public DocumentDto? Result { get; set; }
    public bool IsOperationSuccessful { get; set; }
    public string Message { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }
}