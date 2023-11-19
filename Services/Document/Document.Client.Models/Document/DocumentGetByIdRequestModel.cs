namespace EncyclopediaGalactica.Services.Document.Sdk.Client.Models.Document;

using System.Net.Http.Headers;
using Contracts.Input;
using EncyclopediaGalactica.Client.Core.Model.Interfaces;
using FluentValidation;

public class DocumentGetByIdRequestModel : IRequestModel<DocumentGraphqlInput>
{
    public DocumentGraphqlInput? Payload { get; private init; }
    public List<MediaTypeWithQualityHeaderValue> AcceptHeaders { get; private init; }

    public class Builder
    {
        private List<MediaTypeWithQualityHeaderValue> _acceptHeaders = new List<MediaTypeWithQualityHeaderValue>();
        private long Id;

        public Builder AddAcceptHeader(MediaTypeWithQualityHeaderValue acceptHeader)
        {
            _acceptHeaders.Add(acceptHeader);
            return this;
        }

        public Builder SetId(long id)
        {
            Id = id;
            return this;
        }

        public DocumentGetByIdRequestModel Build()
        {
            if (Id == 0)
            {
                throw new ValidationException("Id cannot be zero");
            }

            return new DocumentGetByIdRequestModel
            {
                Payload = new DocumentGraphqlInput { Id = Id },
                AcceptHeaders = _acceptHeaders
            };
        }
    }
}