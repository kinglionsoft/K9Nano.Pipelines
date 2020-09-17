using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace K9Nano.Pipelines
{
    public interface IPipelineInvoker<in TInput, TOutput>
    {
        ValueTask<TOutput> InvokeAsync(TInput input, CancellationToken cancellation);
    }

}