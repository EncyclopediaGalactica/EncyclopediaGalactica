// See https://aka.ms/new-console-template for more information

namespace ConsoleApp1;

using Microsoft.Build.Construction;

class ConsoleApp
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello");

        string dirPath = "/Users/andrascsanyi/tmp";
        string solutionFilePath = $"{dirPath}/solution.sln";
        string classlib1 = $"{dirPath}/classlib/classlib.csproj";
        string classlib2 = $"{dirPath}/classlib2/classlib2.csproj";

        SolutionFile solutionFile = SolutionFile.Parse(solutionFilePath);
        ProjectRootElement? project1 = ProjectRootElement.Open(classlib1);
        ProjectRootElement? project2 = ProjectRootElement.Open(classlib2);

        ProjectItemGroupElement? item = project2.ItemGroups.First();
    }
}