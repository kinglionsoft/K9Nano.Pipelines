using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K9Nano.Pipelines
{
    public class PipelineBuilder<TContext> : IPipelineBuilder<TContext>
    {
        protected IList<Func<PipelineDelegate<TContext>, PipelineDelegate<TContext>>> Components
            = new List<Func<PipelineDelegate<TContext>, PipelineDelegate<TContext>>>();

        public virtual IPipelineBuilder<TContext> Insert(Func<PipelineDelegate<TContext>, 
            PipelineDelegate<TContext>> component, 
            int? index = null)
        {
            if (index.HasValue)
            {
                if (index.Value > Components.Count)
                {
                    Components.Add(component);
                }
                else if (index.Value < 0)
                {
                    Components.Insert(0, component);
                }
                else
                {
                    Components.Insert(index.Value, component);
                }
            }
            else
            {
                Components.Add(component);
            }
            return this;
        }

        public IPipelineBuilder<TContext> RemoveAt(int index)
        {
            Components.RemoveAt(index);
            return this;
        }

        public virtual PipelineDelegate<TContext> Build()
        {
            return Components.Reverse()
                .Aggregate(
                    (PipelineDelegate<TContext>)(context => new ValueTask()),
                    (current, next) => next(current)
                );
        }
    }
}