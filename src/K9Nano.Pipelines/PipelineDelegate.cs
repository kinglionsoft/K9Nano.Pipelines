using System.Threading.Tasks;

namespace K9Nano.Pipelines
{
    public delegate ValueTask PipelineDelegate<in TContext>(TContext context);
}
