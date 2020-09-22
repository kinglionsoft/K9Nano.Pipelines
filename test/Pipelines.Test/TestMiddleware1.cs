using System;
using System.Threading.Tasks;
using K9Nano.Pipelines;

namespace Pipelines.Test
{
    public class TestMiddleware1 : IPipelineMiddleware<PipelineContext<TestInput, TestOutput>>
    {
        public Task InvokeAsync(PipelineContext<TestInput, TestOutput> context)
        {
            Console.WriteLine($"TestMiddleware1 invoked, inputs: {context.Input}");
            if (string.IsNullOrEmpty(context.Input.Name))
            {
                context.SetResult(new TestOutput { Value = "Empty" });
            }
            return Task.CompletedTask;
        }
    }
}