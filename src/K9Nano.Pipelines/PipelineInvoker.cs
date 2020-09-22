using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

#if NETSTANDARD2_1
using System.Diagnostics.CodeAnalysis;
#endif

namespace K9Nano.Pipelines
{
    public class PipelineInvokerBase<TContext, TBuilder>
        where TContext : IPipelineContext, new()
        where TBuilder : IPipelineBuilder<TContext>, new()
    {
        protected readonly IServiceScopeFactory ServiceScopeFactory;

        protected readonly PipelineDelegate<TContext> Delegate;

        public PipelineInvokerBase(IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
            Delegate = new TBuilder().Build();
        }

        protected virtual async ValueTask<TContext> InvokingAsync(CancellationToken cancellation)
        {
            using var scope = ServiceScopeFactory.CreateScope();

            var context = new TContext
            {
                ServiceProvider = scope.ServiceProvider,
                CancellationToken = cancellation
            };

            await Delegate.Invoke(context);

            return context;
        }
#if NETSTANDARD2_1
        protected virtual async ValueTask<TContext> InvokingAsync(CancellationToken cancellation, [NotNull] Action<TContext> configure)
#else
        protected virtual async ValueTask<TContext> InvokingAsync(CancellationToken cancellation, Action<TContext> configure)
#endif
        {
            using var scope = ServiceScopeFactory.CreateScope();

            var context = new TContext
            {
                ServiceProvider = scope.ServiceProvider,
                CancellationToken = cancellation
            };

            configure.Invoke(context);

            await Delegate.Invoke(context);

            return context;
        }
    }

    public class PipelineInvoker<TInput, TOutput, TContext, TBuilder> : PipelineInvokerBase<TContext, TBuilder>, IPipelineInvoker<TInput, TOutput>
        where TContext : IPipelineContext<TInput, TOutput>, new()
        where TBuilder : IPipelineBuilder<TContext>, new()
    {
        public PipelineInvoker(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        public virtual async Task<TOutput> InvokeAsync(TInput input, CancellationToken cancellation)
        {
            var context = await InvokingAsync(cancellation, ctx => ctx.Input = input);
            return context.Output;
        }
    }

    public class PipelineInvoker<TInput, TOutput, TBuilder> : PipelineInvoker<TInput, TOutput, PipelineContext<TInput, TOutput>, TBuilder>, IPipelineInvoker<TInput, TOutput>
        where TBuilder : IPipelineBuilder<PipelineContext<TInput, TOutput>>, new()
    {
        public PipelineInvoker(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }
    }

    public class PipelineInvoker<TInput, TBuilder> : PipelineInvokerBase<PipelineContext<TInput>, TBuilder>, IPipelineInvoker<TInput>
        where TBuilder : IPipelineBuilder<PipelineContext<TInput>>, new()
    {
        public PipelineInvoker(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        public virtual async Task InvokeAsync(TInput input, CancellationToken cancellation)
        {
            await InvokingAsync(cancellation, ctx => ctx.Input = input);
        }
    }

    public class PipelineInvoker<TBuilder> : PipelineInvokerBase<PipelineContext, TBuilder>, IPipelineInvoker
        where TBuilder : IPipelineBuilder<PipelineContext>, new()
    {
        public PipelineInvoker(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        public virtual async Task InvokeAsync(CancellationToken cancellation)
        {
            await InvokingAsync(cancellation);
        }
    }
}