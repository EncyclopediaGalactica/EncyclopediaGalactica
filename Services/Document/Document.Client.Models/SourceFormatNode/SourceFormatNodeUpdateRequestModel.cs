namespace EncyclopediaGalactica.Services.Document.Sdk.Client.Models.SourceFormatNode;

using System.Net.Http.Headers;
using Contracts.Input;
using EncyclopediaGalactica.Client.Core.Model.Interfaces;
using FluentValidation;
using ValidatorService;

public class SourceFormatNodeUpdateRequestModel : IRequestModel<SourceFormatNodeInputContract>
{
    public SourceFormatNodeInputContract? Payload { get; private init; }

    public List<MediaTypeWithQualityHeaderValue> AcceptHeaders { get; private init; } = new();

    public class Builder
    {
        private readonly List<MediaTypeWithQualityHeaderValue> _acceptHeaders = new();
        private long _id;
        private string _name;

        public Builder SetId(long id)
        {
            _id = id;
            return this;
        }

        public Builder SetName(string name)
        {
            _name = name;
            return this;
        }

        public Builder AddAcceptHeader(MediaTypeWithQualityHeaderValue header)
        {
            _acceptHeaders.Add(header);
            return this;
        }

        public SourceFormatNodeUpdateRequestModel Build()
        {
            SourceFormatNodeInputContract inputContract = new()
            {
                Id = _id,
                Name = _name
            };

            SourceFormatNodeDtoValidator validator = new();
            validator.Validate(inputContract, options =>
            {
                options.IncludeRuleSets(SourceFormatNodeDtoValidator.Update);
                options.ThrowOnFailures();
            });

            SourceFormatNodeUpdateRequestModel requestModel = new()
            {
                Payload = inputContract,
                AcceptHeaders = _acceptHeaders
            };

            return requestModel;
        }
    }
}