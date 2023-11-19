namespace EncyclopediaGalactica.Services.Document.Sdk.Client.Models.SourceFormatNode;

using System.Net.Http.Headers;
using Contracts.Input;
using EncyclopediaGalactica.Client.Core.Model.Interfaces;

public class SourceFormatNodeGetAllRequestModel : IRequestModel<List<SourceFormatNodeInputContract>>
{
    public List<SourceFormatNodeInputContract>? Payload { get; private init; }
    public List<MediaTypeWithQualityHeaderValue> AcceptHeaders { get; private init; }

    public class Builder
    {
        private List<MediaTypeWithQualityHeaderValue> AcceptHeaders { get; set; } =
            new List<MediaTypeWithQualityHeaderValue>();

        public Builder AddAcceptHeader(string mediaType)
        {
            MediaTypeWithQualityHeaderValue value = new MediaTypeWithQualityHeaderValue(mediaType);
            AcceptHeaders.Add(value);
            return this;
        }

        public SourceFormatNodeGetAllRequestModel Build()
        {
            SourceFormatNodeGetAllRequestModel requestModel = new SourceFormatNodeGetAllRequestModel
            {
                AcceptHeaders = AcceptHeaders,
                Payload = null
            };
            return requestModel;
        }
    }
}