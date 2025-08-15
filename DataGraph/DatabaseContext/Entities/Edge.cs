namespace DataGraph.DatabaseContext.Entities;

public class Edge
{
    public long Id { get; set; }
    public long FromId { get; set; }
    public Vertex From { get; set; }
    public long ToId { get; set; }
    public Vertex To { get; set; }
}
