using System;
using System.Threading.Tasks;

namespace K9Nano.Pipelines
{
    public static class PipeLineBuilderExtensions
    {
        public static IPipelineBuilder<TContext> Insert<TContext>(this IPipelineBuilder<TContext> builder,
            Func<TContext, Task> action,
            int? index = null)
            where TContext : IPipelineContext
        {
            PipelineDelegate<TContext> pipelineDelegate(PipelineDelegate<TContext> next)
            {
                return async (context) =>
                {
                    if (context.Terminated)
                    {
                        return;
                    }
                    await action.Invoke(context);
                    await next.Invoke(context);
                };
            }

            return builder.Insert(pipelineDelegate, index);
        }

        public static IPipelineBuilder<TContext> Insert<TContext>(this IPipelineBuilder<TContext> builder,
            IPipelineMiddleware<TContext> middleware,
            int? index = null)
            where TContext : IPipelineContext
        {
            PipelineDelegate<TContext> pipelineDelegate(PipelineDelegate<TContext> next)
            {
                return async (context) =>
                {
                    if (context.Terminated)
                    {
                        return;
                    }
                    await middleware.InvokeAsync(context);
                    await next.Invoke(context);
                };
            }
            return builder.Insert(pipelineDelegate, index);
        }
    }
}