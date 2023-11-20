namespace EncyclopediaGalactica.Services.Document.Sdk.Client.Models.SourceFormatNode;

using System.Net;
using Contracts.Input;
using EncyclopediaGalactica.Client.Core.Model.Interfaces;

public class SourceFormatNodeGetAllResponseModel : IHttpResponseModel<List<SourceFormatNodeInput>>
{
    public List<SourceFormatNodeInput>? Result { get; set; }
    public bool IsOperationSuccessful { get; set; }
    public string? Message { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public class Builder
    {
        private HttpStatusCode? _httpStatusCode;
        private bool _isOperationSuccessful;
        private string? _message;
        private List<SourceFormatNodeInput>? _result;

        public Builder SetResult(List<SourceFormatNodeInput> dto)
        {
            _result = dto;
            return this;
        }

        public Builder SetOperationSuccessful()
        {
            _isOperationSuccessful = true;
            return this;
        }

        public Builder SetOperationFailed()
        {
            _isOperationSuccessful = false;
            return this;
        }

        public Builder SetHttpStatusCode(HttpStatusCode statusCode)
        {
            _httpStatusCode = statusCode;
            return this;
        }

        public Builder SetMessage(string message)
        {
            _message = message;
            return this;
        }

        public SourceFormatNodeGetAllResponseModel Build()
        {
            ArgumentNullException.ThrowIfNull(_httpStatusCode);
            ArgumentNullException.ThrowIfNull(_message);

            SourceFormatNodeGetAllResponseModel responseModel = new()
            {
                Result = _result,
                IsOperationSuccessful = _isOperationSuccessful,
                Message = _message,
                HttpStatusCode = (System.Net.HttpStatusCode)_httpStatusCode
            };

            return responseModel;
        }
    }
}