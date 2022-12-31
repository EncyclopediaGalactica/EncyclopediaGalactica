﻿namespace EncyclopediaGalactica.Sdk.Core.Interfaces;

using Model.Interfaces;

/// <summary>
/// The Sdk Core Interface
/// </summary>
public interface ISdkCore
{
    Task<TResponseModel> SendAsync<TResponseModel, TResponseModelPayload>(
        HttpRequestMessage httpRequestMessage,
        CancellationToken cancellationToken = default)
        where TResponseModel : IHttpResponseModel<TResponseModelPayload>, new();
}