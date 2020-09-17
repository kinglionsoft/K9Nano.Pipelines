using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Pipelines.Test
{
    public class PipelineTest
    {
        private readonly IServiceProvider _serviceProvider;

        public PipelineTest()
        {
            var services = new ServiceCollection()
                    .AddSingleton<TestService>()
                    .AddSingleton<ITestPipelineInvoker, TestPipelineInvoker>()
                ;

            _serviceProvider = services.BuildServiceProvider();
        }

        [Theory]
        [InlineData("", "Empty")]
        [InlineData("Foo", "Foo value")]
        public async Task TestOutput(string name, string value)
        {
            var invoker = _serviceProvider.GetRequiredService<ITestPipelineInvoker>();
            var output = await invoker.InvokeAsync(new TestInput {Name = name}, default);
            Assert.Equal(value, output.Value);
        }
    }
}
