using System.Threading.Tasks;

namespace K9Nano.Pipelines
{
    public delegate Task PipelineDelegate<in TContext>(TContext context);
}
