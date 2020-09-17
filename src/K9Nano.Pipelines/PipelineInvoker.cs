using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace K9Nano.Pipelines
{
    public class PipelineInvoker<TInput, TOutput, TContext, TBuilder> : IPipelineInvoker<TInput, TOutput>
        where TContext : IPipelineContext<TInput, TOutput>, new()
        where TBuilder : IPipelineBuilder<TContext>, new()
    {
        protected readonly IServiceScopeFactory ServiceScopeFactory;

        protected readonly PipelineDelegate<TContext> Delegate;

        public PipelineInvoker(IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
            Delegate = new TBuilder().Build();
        }

        public async ValueTask<TOutput> InvokeAsync(TInput input, CancellationToken cancellation)
        {
            using var scope = ServiceScopeFactory.CreateScope();

            var context = new TContext
            {
                Input = input,
                ServiceProvider = scope.ServiceProvider,
                CancellationToken = cancellation
            };

            await Delegate.Invoke(context);

            return context.Output;
        }
    }

    public class PipelineInvoker<TInput, TOutput, TBuilder> : PipelineInvoker<TInput, TOutput, PipelineContext<TInput, TOutput>, TBuilder>
        where TBuilder : IPipelineBuilder<PipelineContext<TInput, TOutput>>, new()
    {
        public PipelineInvoker(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }
    }
}