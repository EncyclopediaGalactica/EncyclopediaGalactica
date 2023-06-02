namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.OpenApiDocumentManager;

using Microsoft.OpenApi.Models;

public interface ISchemas
{
    /// <summary>
    ///     Returns the list of schema names from the OpenApi schema.
    ///     <remarks>
    ///         Important to note that OpenApi calls schema what C# calls as class name or type.
    ///         <a href="https://swagger.io/specification/#schema-object">Schema object</a>
    ///     </remarks>
    /// </summary>
    /// <param name="openApiDocument">
    ///     <see cref="OpenApiDocument" />
    /// </param>
    /// <returns>List of type names</returns>
    List<string> GetSchemaNames(OpenApiDocument openApiDocument);

    /// <summary>
    ///     Returns list of schemas from the provided OpenApi file.
    ///     <remarks>
    ///         When the <see cref="OpenApiDocument" /> does not have schemas (type in C# regime) the method returns an empty
    ///         list.
    ///         Important to note that what OpenApi calls schema C# calls class name or type name.
    ///         <a href="https://swagger.io/specification/#schema-object">Schema object</a>
    ///     </remarks>
    /// </summary>
    /// <param name="openApiDocument">the <see cref="OpenApiDocument" /></param>
    /// <returns>List of <see cref="IDictionary{TKey,TValue}" /></returns>
    IDictionary<string, OpenApiSchema> GetSchemas(OpenApiDocument openApiDocument);

    /// <summary>
    ///     Returns the list of required properties of a schema
    ///     <remarks>
    ///         Important to note that what OpenApi calls schema C# calls it class name or type name.
    ///         If the type does not have required properties an empty list will be returned.
    ///         <a href="https://swagger.io/specification/#schema-object">Schema object</a> and look for "required"
    ///         section.
    ///     </remarks>
    /// </summary>
    /// <param name="schemaName">Name of the type</param>
    /// <param name="openApiDocument">
    ///     <see cref="OpenApiDocument" />
    /// </param>
    /// <returns>List of required properties</returns>
    List<string> GetRequiredPropertiesBySchema(string schemaName, OpenApiDocument openApiDocument);

    /// <summary>
    ///     Returns the property names of the given schema
    ///     <remarks>
    ///         Important to note that what OpenApi calls schema in the C# regime it is called type
    ///         If the schema does not have properties empty list will be returned
    ///     </remarks>
    /// </summary>
    /// <param name="schemaName">Name of the schema / type name</param>
    /// <param name="openApiDocument">
    ///     <see cref="OpenApiDocument" />
    /// </param>
    /// <returns>List of the property names in the indicated schema</returns>
    List<string> GetPropertyNamesBySchema(string schemaName, OpenApiDocument openApiDocument);

    /// <summary>
    ///     Returns the property types of the indicated schema
    ///     <remarks>
    ///         Important to note that what OpenApi regime considers as schema, C# regime considers as type
    ///     </remarks>
    /// </summary>
    /// <param name="schemaName">The schema name</param>
    /// <param name="openApiDocument">
    ///     <see cref="OpenApiDocument" />
    /// </param>
    /// <returns>Dictionary of property types where the key is the property name, the value is the property type</returns>
    IDictionary<string, Dictionary<string, string>> GetPropertyTypesBySchema(
        string schemaName,
        OpenApiDocument openApiDocument);
}