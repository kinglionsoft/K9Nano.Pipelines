using System.Threading;
using System.Threading.Tasks;

namespace K9Nano.Pipelines
{
    public interface IPipelineInvoker
    {
        ValueTask InvokeAsync(CancellationToken cancellation);
    }

    public interface IPipelineInvoker<in TInput>
    {
        ValueTask InvokeAsync(TInput input, CancellationToken cancellation);
    }

    public interface IPipelineInvoker<in TInput, TOutput>
    {
        ValueTask<TOutput> InvokeAsync(TInput input, CancellationToken cancellation);
    }
}