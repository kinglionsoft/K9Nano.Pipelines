using System;

namespace K9Nano.Pipelines
{
    public interface IPipelineBuilder<TContext>
    {
        IPipelineBuilder<TContext> Insert(Func<PipelineDelegate<TContext>, PipelineDelegate<TContext>> component, int? index = null);

        IPipelineBuilder<TContext> RemoveAt(int index);

        PipelineDelegate<TContext> Build();
    }
}