using System;
using System.Threading;

namespace K9Nano.Pipelines
{
    public interface IPipelineContext
    {
        bool Terminated { get; set; }

        IServiceProvider ServiceProvider { get; set; }

        CancellationToken CancellationToken { get; set; }
    }

    public interface IPipelineContext<TInput, TOutput> : IPipelineContext
    {
        TInput Input { get; set; }

        TOutput Output { get; set; }
    }
}