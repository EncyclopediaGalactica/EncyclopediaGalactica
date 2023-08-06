namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.TemplateManager;

using Models;

public interface ITemplateManager
{
    /// <summary>
    ///     Compiles Handlebar template using the provided data structure.
    /// </summary>
    /// <param name="template">the Handlebar template content</param>
    /// <param name="typeInfoRender">file information</param>
    /// <returns>the compiled template</returns>
    string CompileTemplate(string template, TypeInfoRender typeInfoRender);
}