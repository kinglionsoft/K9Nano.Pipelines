using System;
using System.Threading.Tasks;
using K9Nano.Pipelines;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Pipelines.Test
{
    public class TestMiddleware2 : IPipelineMiddleware<PipelineContext<TestInput, TestOutput>>
    {
        public Task InvokeAsync(PipelineContext<TestInput, TestOutput> context)
        {
            var log = context.ServiceProvider.GetService<ITestOutputHelper>();
            log.WriteLine($"TestMiddleware2 invoked, inputs: {context.Input}");

            var service = context.ServiceProvider.GetRequiredService<TestService>();
            context.Output = new TestOutput
            {
                Value = service.GetValue(context.Input.Name, nameof(TestMiddleware2))
            };
            return Task.CompletedTask;
        }
    }
}