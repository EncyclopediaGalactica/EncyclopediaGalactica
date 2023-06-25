namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.ConfigurationValuesManager;

using Configuration;
using Models;

/// <summary>
///     Interface providing method to extract configuration values
/// </summary>
public interface IConfigurationValuesManager
{

    /// <summary>
    ///     Takes the <b>target_directory</b> from the provided generator configuration and adds to the
    ///     type information objects used by the code generator.
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="generatorConfiguration">Instance of <see cref="CodeGeneratorConfiguration" /></param>
    void GetOriginalTargetDirectoryTokenFromConfiguration(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration);

    /// <summary>
    ///     Takes the <b>target_directory</b> from the provided generator configuration and adds to solution configuration.
    /// </summary>
    /// <param name="solutionInfo">Solution configuration</param>
    /// <param name="generatorConfiguration">Instance of <see cref="CodeGeneratorConfiguration" /></param>
    void GetOriginalTargetDirectoryTokenFromConfiguration(
        SolutionInfo solutionInfo,
        CodeGeneratorConfiguration? generatorConfiguration);

    /// <summary>
    ///     Takes the <b>solution_name</b> value from the provided generator configuration and adds it to the
    ///     solution configuration
    /// </summary>
    /// <param name="solutionInfo">Solution configuration</param>
    /// <param name="generatorConfiguration">Generator configuration</param>
    void GetOriginalSolutionNameTokenFromConfiguration(
        SolutionInfo solutionInfo,
        CodeGeneratorConfiguration? generatorConfiguration);

    /// <summary>
    ///     Takes the <b>solution_base_namespace</b> from the provided generator configuration and adds to the
    ///     type information objects used by the code generator.
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="generatorConfiguration">Instance of <see cref="CodeGeneratorConfiguration" /></param>
    void GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration);

    /// <summary>
    ///     Takes the <b>solution_file_type</b> value from the provided configuration and adds to the solution level
    ///     configuration
    /// </summary>
    /// <param name="solutionInfo">the instance of <see cref="SolutionInfo" /></param>
    /// <param name="generatorConfiguration">the instance of <see cref="CodeGeneratorConfiguration" /></param>
    void GetOriginalSolutionFileTypeTokenFromConfigurationAndAddToSolutionInfo(
        SolutionInfo solutionInfo,
        CodeGeneratorConfiguration generatorConfiguration);

    /// <summary>
    ///     Takes the <b>solution_project_file_type</b> value from the provided configuration and adds to the solution
    ///     level configuration.
    /// </summary>
    /// <param name="solutionInfo">the instance of <see cref="SolutionInfo" /></param>
    /// <param name="generatorConfiguration">the instance of <see cref="CodeGeneratorConfiguration" /></param>
    void GetOriginalSolutionProjectFileTypeTokenFromConfigurationAndAddToSolutionInfo(
        SolutionInfo solutionInfo,
        CodeGeneratorConfiguration generatorConfiguration);
}