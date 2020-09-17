using K9Nano.Pipelines;
using Microsoft.Extensions.DependencyInjection;

namespace Pipelines.Test
{
    public class TestPipelineInvoker : PipelineInvoker<TestInput, TestOutput, TestPipelineBuilder>, ITestPipelineInvoker
    {
        public TestPipelineInvoker(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }
    }
}