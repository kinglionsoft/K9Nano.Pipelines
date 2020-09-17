using System.Threading.Tasks;

namespace K9Nano.Pipelines
{
    public interface IPipelineMiddleware<in TContext>
    {
        ValueTask InvokeAsync(TContext context);
    }
}