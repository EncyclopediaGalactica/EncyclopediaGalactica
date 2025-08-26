namespace EncyclopediaGalactica.CommandLineInterface.Cli;

using Common;
using Exercises;
using Exercises.Generate;
using Exercises.Generate.Book;
using Exercises.List;
using Infrastructure;
using Spectre.Console;
using Spectre.Console.Cli;
using Storage;
using Storage.Edge;
using Storage.EdgeType;
using Storage.Vertex;
using Storage.VertexType;

public static class EgCli
{
    public static void RenderError(EgError error)
    {
        Table table = new();
        table.AddColumn("Message").AddColumn("Trace");
        table.AddRow(
            error.Message == null ? "No message" : error.Message,
            error.Trace == null ? "Notrace" : error.Trace
        );
        AnsiConsole.Write(table);
    }

    public static CommandApp CreateCommandApp(TypeRegistrar registrar)
    {
        CommandApp app = new(registrar);
        app.Configure(cliConfig =>
            {
                cliConfig.AddBranch<ExercisesSettings>(
                    "exercise",
                    exercise =>
                    {
                        exercise.AddBranch<GenerateSettings>(
                            "generate",
                            generateBranch =>
                            {
                                generateBranch.AddCommand<BooksCommand>("books");
                            }
                        );
                        exercise.AddBranch<ListSettings>(
                            "list",
                            listBranch =>
                            {
                                listBranch.AddCommand<ExercisesListBooksCommand>("books");
                                listBranch.AddCommand<ExercisesListTopicsCommand>("topics");
                                listBranch.AddCommand<ExercisesListChaptersCommand>("chapters");
                            }
                        );
                    }
                );
                cliConfig.AddBranch<StorageSettings>(
                    "storage",
                    storage =>
                    {
                        storage.AddBranch<EdgeTypeSettings>(
                            "edge-type",
                            edgeType =>
                            {
                                edgeType.AddCommand<AddEdgeTypeCommand>("add");
                                edgeType.AddCommand<UpdateEdgeTypeCommand>("update");
                                edgeType.AddCommand<ListEdgeTypeCommand>("list");
                                edgeType.AddCommand<DeleteEdgeTypeCommand>("delete");
                            }
                        );
                        storage.AddBranch<EdgeSettings>(
                            "edge",
                            edge =>
                            {
                                edge.AddCommand<AddEdgeCommand>("add");
                                edge.AddCommand<UpdateEdgeCommand>("update");
                                edge.AddCommand<ListEdgeCommand>("list");
                                edge.AddCommand<DeleteEdgeCommand>("delete");
                            }
                        );
                        storage.AddBranch<VertexSettings>(
                            "vertex",
                            vertex =>
                            {
                                vertex.AddCommand<AddVertexCommand>("add");
                                vertex.AddCommand<UpdateVertexCommand>("update");
                                vertex.AddCommand<ListVertexCommand>("list");
                                vertex.AddCommand<DeleteVertexCommand>("delete");
                            }
                        );
                        storage.AddBranch<VertexTypeSettings>(
                            "vertex-type",
                            vertexType =>
                            {
                                vertexType.AddCommand<AddVertexTypeCommand>("add");
                                vertexType.AddCommand<UpdateVertexTypeCommand>("update");
                                vertexType.AddCommand<ListVertexTypeCommand>("list");
                                vertexType.AddCommand<DeleteVertexTypeCommand>("delete");
                            }
                        );
                    }
                );
            }
        );
        return app;
    }
}