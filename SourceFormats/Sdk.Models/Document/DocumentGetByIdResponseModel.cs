namespace EncyclopediaGalactica.Services.Document.Sdk.Models.Document;

using System.Net;
using Dtos;
using EncyclopediaGalactica.Sdk.Core.Model.Interfaces;

public class DocumentGetByIdResponseModel : IHttpResponseModel<DocumentDto>
{
    public DocumentDto? Result { get; set; }
    public bool IsOperationSuccessful { get; set; }
    public string Message { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }
}