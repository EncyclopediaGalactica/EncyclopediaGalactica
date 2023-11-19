namespace EncyclopediaGalactica.Services.Document.Sdk.Client.Models.SourceFormatNode;

using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Contracts.Input;
using EncyclopediaGalactica.Client.Core.Model.Interfaces;
using FluentValidation;
using ValidatorService;

/// <summary>
///     This model is used in creating new SourceFormatNode entity in the system.
///     It provides a Builder to collect all necessary data to do so. However, the builder does not represent
///     validation for the collected data.
/// </summary>
public class SourceFormatNodeAddRequestModel : IRequestModel<SourceFormatNodeInputContract>
{
    /// <summary>
    ///     Creates a new instance.
    /// </summary>
    public SourceFormatNodeAddRequestModel()
    {
    }

    /// <summary>
    ///     The payload object which contains details of the SourceFormatNode object
    ///     we wish to create.
    /// </summary>
    [JsonPropertyName("payload")]
    public SourceFormatNodeInputContract? Payload { get; private init; }

    public List<MediaTypeWithQualityHeaderValue> AcceptHeaders { get; private init; } =
        new List<MediaTypeWithQualityHeaderValue>();

    public class Builder
    {
        private string? Name { get; set; }

        private List<MediaTypeWithQualityHeaderValue> AcceptHeaders { get; set; } =
            new List<MediaTypeWithQualityHeaderValue>();

        public Builder SetName(string name)
        {
            Name = name;
            return this;
        }

        public Builder AddAcceptHeader(string mediaType)
        {
            MediaTypeWithQualityHeaderValue value = new MediaTypeWithQualityHeaderValue(mediaType);
            AcceptHeaders.Add(value);
            return this;
        }

        public SourceFormatNodeAddRequestModel Build()
        {
            try
            {
                SourceFormatNodeInputContract inputContract = new SourceFormatNodeInputContract
                {
                    Name = Name
                };

                SourceFormatNodeDtoValidator validator = new SourceFormatNodeDtoValidator();
                validator.Validate(inputContract, options =>
                {
                    options.ThrowOnFailures();
                    options.IncludeRuleSets(SourceFormatNodeDtoValidator.Add);
                });

                SourceFormatNodeAddRequestModel requestModel = new SourceFormatNodeAddRequestModel
                {
                    Payload = inputContract,
                    AcceptHeaders = AcceptHeaders
                };
                return requestModel;
            }
            catch (Exception e)
            {
                const string msg = $"Error happened while building {nameof(SourceFormatNodeAddRequestModel)}.";
                throw new SdkModelsException(msg, e);
            }
        }
    }
}