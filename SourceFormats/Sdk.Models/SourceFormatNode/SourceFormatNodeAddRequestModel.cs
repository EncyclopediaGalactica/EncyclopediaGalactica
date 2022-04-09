namespace EncyclopediaGalactica.SourceFormats.Sdk.Models.SourceFormatNode;

using Dtos;
using EncyclopediaGalactica.Sdk.Core.Model.Interfaces;
using FluentValidation;
using ValidatorService;

/// <summary>
///     This model is used in creating new SourceFormatNode entity in the system.
///     It provides a Builder to collect all necessary data to do so. However, the builder does not represent
///     validation for the collected data.
/// </summary>
public class SourceFormatNodeAddRequestModel : IRequestModel<SourceFormatNodeDto>
{
    private SourceFormatNodeAddRequestModel()
    {
    }

    /// <summary>
    ///     The payload object which contains details of the SourceFormatNode object
    ///     we wish to create.
    /// </summary>
    public SourceFormatNodeDto? Payload { get; private init; }

    public class Builder
    {
        protected string? Name { get; private set; }

        public Builder SetName(string name)
        {
            Name = name;
            return this;
        }

        public SourceFormatNodeAddRequestModel Build()
        {
            try
            {
                SourceFormatNodeDto dto = new SourceFormatNodeDto
                {
                    Name = Name
                };

                SourceFormatNodeDtoValidator validator = new SourceFormatNodeDtoValidator();
                validator.Validate(dto, options =>
                {
                    options.ThrowOnFailures();
                    options.IncludeRuleSets(SourceFormatNodeDtoValidator.Add);
                });

                SourceFormatNodeAddRequestModel requestModel = new SourceFormatNodeAddRequestModel
                {
                    Payload = dto
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