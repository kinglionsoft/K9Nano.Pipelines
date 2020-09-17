using System;
using System.Threading;

namespace K9Nano.Pipelines
{
    public class PipelineContext<TInput, TOutput> : IPipelineContext<TInput, TOutput>
    {
        public bool Terminated { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
        public CancellationToken CancellationToken { get; set; }
        public TInput Input { get; set; }
        public TOutput Output { get; set; }

        public void SetResult(TOutput output)
        {
            Output = output;
            Terminated = true;
        }
    }
}