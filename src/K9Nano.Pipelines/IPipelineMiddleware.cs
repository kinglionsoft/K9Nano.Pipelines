using System.Threading.Tasks;

namespace K9Nano.Pipelines
{
    public interface IPipelineMiddleware<in TContext>
    {
        Task InvokeAsync(TContext context);
    }
}