namespace EncyclopediaGalactica.SourceFormats.Sdk.Models.SourceFormatNode;

using Dtos;

/// <summary>
///     This model is used in creating new SourceFormatNode entity in the system.
///     It provides a Builder to collect all necessary data to do so. However, the builder does not represent
///     validation for the collected data.
/// </summary>
public class SourceFormatNodeAddRequestModel : IRequestModel<SourceFormatNodeDto>
{
    /// <summary>
    ///     The payload object which contains details of the SourceFormatNode object
    ///     we wish to create.
    /// </summary>
    public SourceFormatNodeDto Payload { get; private init; }

    public class Builder
    {
        protected long? Id { get; private set; }
        protected string? Name { get; private set; }

        public Builder SetId(long id)
        {
            Id = id;
            return this;
        }

        public Builder SetName(string name)
        {
            Name = name;
            return this;
        }

        public SourceFormatNodeAddRequestModel Build()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
                throw new SdkModelsException($"Add operation expects {nameof(Name)} being set up.");

            SourceFormatNodeDto dto = new SourceFormatNodeDto
            {
                Name = Name
            };

            SourceFormatNodeAddRequestModel requestModel = new SourceFormatNodeAddRequestModel
            {
                Payload = dto
            };
            return requestModel;
        }
    }
}