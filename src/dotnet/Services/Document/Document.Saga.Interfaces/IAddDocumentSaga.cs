namespace EncyclopediaGalactica.Document.Saga.Interfaces;

using Services.Document.Contracts.Input;
using Services.Document.Contracts.Output;
using Services.Document.Scenario.Interfaces.Document;
using Services.Document.Scenario.Interfaces.StructureNode;

/// <summary>
///     Add Document Saga
///     <remarks>
///         Orchestrates the whole workflow of adding new <see cref="Document" /> entity to the system.
///     </remarks>
///     <para>
///         The cases must be covered by this saga are the following:
///         <list type="table">
///             <listheader>
///                 <term>Name</term>
///                 <description>Description</description>
///             </listheader>
///             <item>
///                 <term>Adding Document without <see cref="Services.Document.Entities.StructureNode" /></term>
///                 <description>
///                     <list type="number">
///                         <item>validates input</item>
///                         <item>
///                             check if the input contains <see cref="Services.Document.Entities.StructureNode" />
///                         </item>
///                         <item>execute <see cref="IAddDocumentScenario" /> which returns the newly created document id</item>
///                         <item>execute <see cref="IGetDocumentByIdScenario" /> and pass the id from previous step</item>
///                     </list>
///                 </description>
///             </item>
///             <item>
///                 <term>Adding Document with <see cref="Services.Document.Entities.StructureNode" /></term>
///                 <description>
///                     <list type="number">
///                         <item>validates input</item>
///                         <item>
///                             check if the input contains <see cref="Services.Document.Entities.StructureNode" />
///                         </item>
///                         <item>execute <see cref="IAddDocumentScenario" /> which returns the newly created document id</item>
///                         <item>
///                             execute <see cref="IAddStructureNodesCommand" />
///                         </item>
///                         <item>execute <see cref="IGetDocumentByIdScenario" /></item>
///                     </list>
///                 </description>
///             </item>
///         </list>
///     </para>
/// </summary>
public interface IAddDocumentSaga
{
    /// <summary>
    ///     Creates a <see cref="Document" /> entity based on the provided input and returns with it.
    /// </summary>
    /// <param name="documentInput">The provided <see cref="DocumentInput" />.</param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of asynchronous operation. The result type includes
    ///     <see cref="DocumentResult" />.
    /// </returns>
    Task<DocumentResult> AddAsync(DocumentInput documentInput);
}