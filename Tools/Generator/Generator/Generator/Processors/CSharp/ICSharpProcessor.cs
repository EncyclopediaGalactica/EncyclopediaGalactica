namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Managers.SolutionManager;
using Managers.SolutionProjectManager;
using Models;

/// <summary>
///     ICSharpProcessor Interface
///     <remarks>
///         It provides methods for processing all c-sharp related metadata
///     </remarks>
/// </summary>
public interface ICSharpProcessor
{
    /// <summary>
    ///     Transforms the type names from the provided <see cref="TypeInfo" /> objects to a valid
    ///     c-sharp type names. The postfix parameter makes possible to create type names like
    ///     "SomethingDto".
    ///     <remarks>
    ///         The type names in OpenApi regime means schemas under the components - schemas section.
    ///     </remarks>
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="typeNamePostfix">Type name postfix</param>
    void ProcessTypeName(List<TypeInfo> typeInfos, string? typeNamePostfix);

    /// <summary>
    ///     Transforms the file name from the provided <see cref="TypeInfo" /> objects to a valid
    ///     c-sharp type names. The postfix parameter makes possible to create file names like
    ///     "SomethingDto.cs". The file type parameter defines the file type.
    /// </summary>
    /// <param name="typeInfos">List of <see cref="FileInfo" /></param>
    /// <param name="fileNamePostFix">The file name postfix</param>
    /// <param name="fileType">The file type</param>
    void ProcessFileName(List<TypeInfo> typeInfos, string fileNamePostFix, string fileType);

    /// <summary>
    ///     Creates an absolute path for Dto generation.
    /// </summary>
    /// <param name="solutionInfo">The <see cref="SolutionInfo" /> instance</param>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    void ProcessDtoTargetPath(SolutionInfo solutionInfo, List<TypeInfo> typeInfos);

    /// <summary>
    ///     Creates the target path with file name value using the previously provided and processed
    ///     filename and path values.
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    void ProcessPathWithFileName(List<TypeInfo> typeInfos);

    /// <summary>
    ///     Adds the template path to every <see cref="TypeInfo" /> objects in the provided list
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="templatePath">The path to the template</param>
    void ProcessTemplatePath(List<TypeInfo> typeInfos, string templatePath);

    /// <summary>
    ///     Creates a c-sharp valid namespace token from the available namespace information.
    ///     <remarks>
    ///         The namespace string is constructed by values from configuration.
    ///     </remarks>
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    void ProcessDtoNamespace(List<TypeInfo> typeInfos);

    /// <summary>
    ///     Checks if variables in a reference type (c-sharp class) have name which is a reserved word.
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="reservedWords">List of reserved words</param>
    /// <exception cref="GeneratorException">
    ///     When the variable name is a reserved word
    /// </exception>
    void ReservedWordsCheckForOriginalVariableNamesOfAType(List<TypeInfo> typeInfos, List<string> reservedWords);

    /// <summary>
    ///     Checks if variable names in a type are c-sharp reserved words or they are already a type name
    ///     <remarks>
    ///         <list type="bullet">
    ///             <item>
    ///                 When it comes to variables does not matter if the variable is property or instance variable
    ///             </item>
    ///             <item>Type is a c-sharp reference type or class</item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="reservedWords">List of reserved words</param>
    void ReservedWordCheckForVariableNames(
        List<TypeInfo> typeInfos,
        List<string> reservedWords);

    /// <summary>
    ///     Iterates through a reference type variables and checks which one is nullable and marks them as nullable.
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    void ProcessNullableVariableTypes(List<TypeInfo> typeInfos);

    /// <summary>
    ///     Based on OpenApi types the method determines what is the c-sharp type
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="openApiCsharpTypeMap">OpenApi type to c-sharp type map</param>
    void ProcessOpenApiTypesToCsharpTypes(List<TypeInfo> typeInfos, Dictionary<string, string> openApiCsharpTypeMap);

    /// <summary>
    ///     Checks if original names of type, acquired from OpenApi file, are reserved words are not.
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="reservedWords">List of reserved words</param>
    void ReservedWordCheckForOriginalTypeNames(List<TypeInfo> typeInfos, List<string> reservedWords);

    /// <summary>
    ///     Checks if the original base namespace token is reserved word or not
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="reservedWords">List of reserved words</param>
    void ReservedWordCheckForOriginalBaseNamespaceToken(List<TypeInfo> typeInfos, List<string> reservedWords);

    /// <summary>
    ///     Checks if the original Dto namespace token is a reserved word or not
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="reservedWords">List of reserved words</param>
    void ReservedWordCheckForOriginalDtoNamespaceToken(List<TypeInfo> typeInfos, List<string> reservedWords);

    /// <summary>
    ///     Creates valid property names from original names where the variable marked as property
    ///     <remarks>
    ///         Property names are PascalCase
    ///     </remarks>
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    void ProcessPropertiesByType(List<TypeInfo> typeInfos);

    /// <summary>
    ///     Checks if generated reference types (generated c-shar classes) based-on OpenApi schema are already in use
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="typesInGenerationScope">List of type names already in use</param>
    void TypeCheckInGenerationScope(List<TypeInfo> typeInfos, List<string> typesInGenerationScope);

    /// <summary>
    ///     Adds generated reference type names (generated c-sharp classes) based on OpenApi schema to the list of
    ///     reference types used in generation scope
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="typesInGenerationScope">List of reference types</param>
    void AddTypeNamesToGenerationScope(List<TypeInfo> typeInfos, List<string> typesInGenerationScope);

    /// <summary>
    ///     Creates c-sharp reference type names (c-sharp classes) from original type names from OpenApi schema
    ///     <remarks>
    ///         The expected result will be TypeName_Should
    ///     </remarks>
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="testTypeNamePostfix">Postfix value</param>
    void ProcessTestTypeName(List<TypeInfo> typeInfos, string? testTypeNamePostfix = default);

    /// <summary>
    ///     Creates the path for the Dto test files.
    ///     <remarks>
    ///         The following parameters are considered when the path is created:
    ///         <list type="bullet">
    ///             <item>provided base path</item>
    ///             <item>provided test project base path</item>
    ///             <item>provided test project additional path</item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    void ProcessDtoTestsTargetPath(List<TypeInfo> typeInfos);

    /// <summary>
    ///     Checks if the provided Dto test namespace token is:
    ///     <list type="bullet">
    ///         <item>reserved word</item>
    ///         <item>contains a reserved words within dots</item>
    ///     </list>
    /// </summary>
    /// <param name="typeInfos">List of <see cref="TypeInfo" /></param>
    /// <param name="reservedWords">List of reserved words</param>
    void ReservedWordCheckForOriginalDtoTestNamespaceToken(List<TypeInfo> typeInfos, List<string> reservedWords);

    /// <summary>
    ///     Creates the full namespace for Dto Test classes.
    ///     <remarks>
    ///         The namespace is created from:
    ///         <list type="bullet">
    ///             <item>base namespace token</item>
    ///             <item>dto test namespace token</item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    /// <param name="typeInfos"></param>
    void ProcessDtoTestNamespace(List<TypeInfo> typeInfos);

    /// <summary>
    ///     Sets up the name of the tested type
    ///     <remarks>
    ///         It takes also the Dto TypeName postfix in order to have the same type name in the tests as the Dto got in the
    ///         Dto generation phase
    ///     </remarks>
    /// </summary>
    /// <param name="dtoTestTypeInfos">List of <see cref="TypeInfo" />s</param>
    /// <param name="dtoTypeNamePostfix">The name postfix value used in Dto file generation</param>
    void ProcessTypeNameUnderTest(List<TypeInfo> dtoTestTypeInfos, string dtoTypeNamePostfix);

    /// <summary>
    ///     Adds the Dto dependencies
    /// </summary>
    /// <param name="dtoTestTypeInfos">List of Dto Test <see cref="TypeInfo" />s</param>
    /// <param name="dtoTypeInfos">List of Dto <see cref="TypeInfo" />s</param>
    void AddImportsToTestTypes(List<TypeInfo> dtoTestTypeInfos, List<TypeInfo> dtoTypeInfos);

    /// <summary>
    ///     Creates the list of project names based on the provided patter and initial solution name
    /// </summary>
    /// <param name="solutionInfo">the <see cref="SolutionInfo" /> instance</param>
    /// <exception cref="GeneratorException">
    ///     When there is no naming convention to a given slot
    /// </exception>
    void ProcessSolutionProjectNames(SolutionInfo solutionInfo);

    /// <summary>
    ///     Processes the solution name using the value from configuration
    /// </summary>
    /// <param name="solutionInfo">Solution info instance</param>
    void ProcessSolutionName(SolutionInfo solutionInfo);

    /// <summary>
    ///     Processes the solution base path.
    ///     <remarks>
    ///         <p>The original solution base path token should be provided prior to operation.</p>
    ///         <p>The processing includes making the path absolute path</p>
    ///     </remarks>
    /// </summary>
    /// <param name="solutionInfo">The <see cref="SolutionInfo" /> instance</param>
    void ProcessSolutionBasePath(SolutionInfo solutionInfo);

    /// <summary>
    ///     Processes the solution project base path values
    ///     <remarks>
    ///         <p>It uses the solution base path value</p>
    ///         <p>It also uses the project name as directory name</p>
    ///     </remarks>
    /// </summary>
    /// <param name="solutionInfo"></param>
    void ProcessSolutionProjectBasePaths(SolutionInfo solutionInfo);

    /// <summary>
    ///     Checks if the given solution exist.
    /// </summary>
    /// <param name="solutionInfo">the <see cref="SolutionInfo" /> instance</param>
    void CheckIfSolutionFileExists(SolutionInfo solutionInfo);

    /// <summary>
    ///     It takes the solution project names and their base path and creates a full path to the project files
    /// </summary>
    /// <param name="solutionInfo">The <see cref="SolutionInfo" /> instance</param>
    void ProcessSolutionProjectFilesWithAbsolutePath(SolutionInfo solutionInfo);

    /// <summary>
    ///     Takes the solution name, file type and the provided path and creates the full path to the solution file
    /// </summary>
    /// <param name="solutionInfo">The instance of <see cref="SolutionInfo" /></param>
    void ProcessSolutionNameWithFullPath(SolutionInfo solutionInfo);

    /// <summary>
    ///     Checks if the provided path for the solution exists.
    /// </summary>
    /// <param name="solutionInfo">the <see cref="SolutionInfo" /> instance</param>
    /// <exception cref="GeneratorException">
    ///     When solution path does not exist
    /// </exception>
    void CheckIfSolutionPathExists(SolutionInfo solutionInfo);

    /// <summary>
    ///     Checks if the directory path for solution projects exist
    /// </summary>
    /// <param name="solutionInfo">the <see cref="SolutionInfo" /> instance</param>
    /// <exception cref="GeneratorException">
    ///     When project's, member of the solution, path does not exist.
    /// </exception>
    void CheckIfSolutionProjectsPathExist(SolutionInfo solutionInfo);

    /// <summary>
    ///     Checks if solution project files, absolute path and project file name, are available on the disk.
    /// </summary>
    /// <param name="solutionInfo">the <see cref="SolutionInfo" /> instance</param>
    /// <exception cref="GeneratorException">
    ///     When a project file is not available on the disk
    /// </exception>
    void CheckIfSolutionProjectFilesExist(SolutionInfo solutionInfo);

    /// <summary>
    ///     Based on the available structure descriptor it creates the slots for the projects
    /// </summary>
    /// <param name="solutionInfo">The <see cref="SolutionInfo" /> instance</param>
    void ProcessProjectStructureSlots(SolutionInfo solutionInfo);

    /// <summary>
    ///     Initiates a <see cref="SolutionManager" /> instance creation for the solution
    /// </summary>
    /// <param name="solutionInfo">the <see cref="SolutionInfo" /> instance</param>
    void SetupSolutionManager(SolutionInfo solutionInfo);

    /// <summary>
    ///     Initiates a <see cref="SolutionProjectManager" /> instance for every project
    /// </summary>
    /// <param name="solutionInfo">the <see cref="SolutionInfo" /> instance</param>
    void SetupSolutionProjectManagers(SolutionInfo solutionInfo);

    /// <summary>
    ///     Initiates checking the dependencies between solution projects.
    /// </summary>
    /// <param name="solutionInfo">the <see cref="SolutionInfo" /> instance</param>
    void CheckSolutionProjectDependencies(SolutionInfo solutionInfo);

    /// <summary>
    ///     It creates the path string for referencing the project in c-sharp solutions
    /// </summary>
    /// <param name="solutionInfo">the <see cref="SolutionInfo" /> instance</param>
    void ProcessSolutionProjectPathFromSolutionBasePath(SolutionInfo solutionInfo);

    /// <summary>
    ///     It creates the solution file with file type string
    /// </summary>
    /// <param name="solutionInfo">the instance of <see cref="SolutionInfo" /></param>
    void ProcessSolutionNameWithWithFileType(SolutionInfo solutionInfo);

    /// <summary>
    ///     It creates the file name with file type extension for the projects in the solution
    /// </summary>
    /// <param name="solutionInfo">the <see cref="SolutionInfo" /> instance</param>
    void ProcessSolutionProjectNamesWithFileType(SolutionInfo solutionInfo);

    /// <summary>
    ///     Checks if solution project file types is set up. If not it will default to the one defined in the
    ///     structure
    /// </summary>
    /// <param name="solutionInfo">The <see cref="SolutionInfo" /> instance</param>
    void ProcessSolutionProjectFileTypes(SolutionInfo solutionInfo);
}