namespace EncyclopediaGalactica.Services.Document.Sdk.Client.Models.Document;

using System.Net.Http.Headers;
using Contracts.Input;
using EncyclopediaGalactica.Client.Core.Model.Interfaces;
using FluentValidation;
using ValidatorService;

public class DocumentAddRequestModel : IRequestModel<DocumentInput>
{
    /// <inheritdoc />
    public DocumentInput? Payload { get; private init; }

    /// <inheritdoc />
    public List<MediaTypeWithQualityHeaderValue> AcceptHeaders { get; private init; }

    public class Builder
    {
        private List<MediaTypeWithQualityHeaderValue> _acceptHeaders = new List<MediaTypeWithQualityHeaderValue>();
        private string _desc;
        private string _name;
        private Uri? _uri;

        public Builder SetName(string name)
        {
            _name = name;
            return this;
        }

        public Builder SetDescription(string desc)
        {
            _desc = desc;
            return this;
        }

        public Builder AddAcceptHeader(string header)
        {
            _acceptHeaders.Add(new MediaTypeWithQualityHeaderValue(header));
            return this;
        }

        public Builder SetUri(Uri uri)
        {
            this._uri = uri;
            return this;
        }

        public DocumentAddRequestModel Build()
        {
            DocumentInput input = new DocumentInput
            {
                Name = _name,
                Description = _desc,
                Uri = _uri
            };

            IValidator<DocumentInput> validator = new InlineValidator<DocumentInput>();
            validator.Validate(input, o =>
            {
                o.IncludeRuleSets(Operations.Add);
                o.ThrowOnFailures();
            });

            return new DocumentAddRequestModel
            {
                Payload = input,
                AcceptHeaders = _acceptHeaders
            };
        }
    }
}