using System;
using System.Threading.Tasks;
using K9Nano.Pipelines;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace Pipelines.Test
{
    public class PipelineTest
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITestOutputHelper _helper;
        public PipelineTest(ITestOutputHelper helper)
        {
            _helper = helper;
            var services = new ServiceCollection()
                    .AddSingleton<TestService>()
                    .AddSingleton(_helper)
                    .AddSingleton<ITestPipelineInvoker, TestPipelineInvoker>()
                ;

            _serviceProvider = services.BuildServiceProvider();
        }

        [Theory]
        [InlineData("", "Empty")]
        [InlineData("Foo", "Foo value, from: TestMiddleware3")]
        public async Task Middleware(string name, string value)
        {
            var invoker = _serviceProvider.GetRequiredService<ITestPipelineInvoker>();
            var output = await invoker.InvokeAsync(new TestInput { Name = name }, default);
            Assert.Equal(value, output.Value);
        }
    }
}
