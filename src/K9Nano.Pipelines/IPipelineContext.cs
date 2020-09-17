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

    public interface IPipelineContext<TInput> : IPipelineContext
    {
        TInput Input { get; set; }
    }

    public interface IPipelineContext<TInput, TOutput> : IPipelineContext<TInput>, IPipelineContext
    {
        TOutput Output { get; set; }
    }

    public class PipelineContext : IPipelineContext
    {
        public bool Terminated { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }

    public class PipelineContext<TInput> : PipelineContext, IPipelineContext<TInput>
    {
        public TInput Input { get; set; }
    }

    public class PipelineContext<TInput, TOutput> : PipelineContext<TInput>, IPipelineContext<TInput, TOutput>
    {
        public TOutput Output { get; set; }

        public void SetResult(TOutput output)
        {
            Output = output;
            Terminated = true;
        }
    }
}