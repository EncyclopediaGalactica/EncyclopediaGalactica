using Microsoft.EntityFrameworkCore;

namespace DataGraph.DatabaseContext;

public class DataGraphContext : DbContext
{
    public DataGraphContext(DbContextOptions options) : base(options)
    {
    }

    protected DataGraphContext()
    {
    }

}
