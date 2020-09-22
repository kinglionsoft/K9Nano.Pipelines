using System.Threading;
using System.Threading.Tasks;

namespace K9Nano.Pipelines
{
    public interface IPipelineInvoker
    {
        Task InvokeAsync(CancellationToken cancellation);
    }

    public interface IPipelineInvoker<in TInput>
    {
        Task InvokeAsync(TInput input, CancellationToken cancellation);
    }

    public interface IPipelineInvoker<in TInput, TOutput>
    {
        Task<TOutput> InvokeAsync(TInput input, CancellationToken cancellation);
    }
}