namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.OpenApiToTypeInfoManager;

using Microsoft.OpenApi.Models;
using Models;

public interface IOpenApiToTypeInfoManager
{
    /// <summary>
    ///     Takes type names from the provided OpenApi document and puts into the provided file info list.
    /// </summary>
    /// <param name="typeInfos">The file infos</param>
    /// <param name="openApiDocument">The Open Api document</param>
    /// <exception cref="Exception">In case of any error</exception>
    void GetTypeNamesFromOpenApiAndAddToTypeInfo(List<TypeInfo> typeInfos, OpenApiDocument openApiDocument);

    /// <summary>
    ///     Takes the required properties for every type in the OpenApi document and puts into file infos.
    ///     <remarks>
    ///         As a result every type will have a list of required properties
    ///     </remarks>
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="openApiDocument">The <see cref="OpenApiDocument" /></param>
    void GetRequiredPropertiesByTypeAndAddToTypeInfo(List<TypeInfo> typeInfos, OpenApiDocument openApiDocument);

    /// <summary>
    ///     Takes the property names from the provided <see cref="OpenApiDocument" /> and adds them to the
    ///     provided <see cref="TypeInfo" />s
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="openApiDocument">
    ///     <see cref="OpenApiDocument" />
    /// </param>
    void GetPropertyNamesByTypeAndAddToTypeInfo(List<TypeInfo> typeInfos, OpenApiDocument openApiDocument);

    /// <summary>
    ///     Takes the property types from the provided <see cref="OpenApiDocument" /> and adds them to the provided
    ///     <see cref="TypeInfo" />s
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" />s</param>
    /// <param name="openApiDocument">
    ///     <see cref="OpenApiDocument" />
    /// </param>
    void GetPropertyTypesByTypeAndAddTypeInfo(List<TypeInfo> typeInfos, OpenApiDocument openApiDocument);

    /// <summary>
    ///     Marks variables as property in variable infos object based on OpenApi schema.
    ///     <remarks>
    ///         The schema variables described in the OpenApi schema are will be always properties
    ///     </remarks>
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="openApiYamlSchema">
    ///     <see cref="OpenApiDocument" />
    /// </param>
    void MarkVariablesAsPropertyBasedOnOpenApiSchema(List<TypeInfo> typeInfos, OpenApiDocument openApiYamlSchema);
}