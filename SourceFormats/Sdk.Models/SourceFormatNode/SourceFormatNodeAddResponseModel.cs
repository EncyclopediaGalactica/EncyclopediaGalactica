namespace EncyclopediaGalactica.SourceFormats.Sdk.Models.SourceFormatNode;

using System.Net;
using Dtos;
using EncyclopediaGalactica.Sdk.Core.Model.Interfaces;

public class SourceFormatNodeAddResponseModel : IResponseModel<SourceFormatNodeDto>
{
    public SourceFormatNodeDto? Result { get; set; }
    public bool IsOperationSuccessful { get; set; }
    public int HttpStatusCode { get; set; }
    public string? Message { get; set; }

    public class Builder
    {
        private SourceFormatNodeDto? _result;
        private bool _isOperationSuccessful;
        private HttpStatusCode? _httpStatusCode;
        private string? _message;

        public Builder SetResult(SourceFormatNodeDto dto)
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

        public SourceFormatNodeAddResponseModel Build()
        {
            if (_httpStatusCode is null)
                throw new ArgumentException("Http status code must be set.");

            SourceFormatNodeAddResponseModel responseModel = new SourceFormatNodeAddResponseModel
            {
                Result = _result,
                IsOperationSuccessful = _isOperationSuccessful,
                Message = _message,
                HttpStatusCode = (int)_httpStatusCode
            };

            return responseModel;
        }
    }
}