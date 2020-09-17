using System;
using System.Threading.Tasks;
using K9Nano.Pipelines;
using Microsoft.Extensions.DependencyInjection;

namespace Pipelines.Test
{
    public class TestMiddleware2 : IPipelineMiddleware<PipelineContext<TestInput, TestOutput>>
    {
        public ValueTask InvokeAsync(PipelineContext<TestInput, TestOutput> context)
        {
            Console.WriteLine($"TestMiddleware2 invoked, inputs: {context.Input}");

            var service = context.ServiceProvider.GetRequiredService<TestService>();
            context.Output = new TestOutput
            {
                Value = service.GetValue(context.Input.Name)
            };
            return new ValueTask();
        }
    }
}