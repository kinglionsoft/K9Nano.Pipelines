using K9Nano.Pipelines;

namespace Pipelines.Test
{
    public class TestPipelineBuilder : PipelineBuilder<PipelineContext<TestInput, TestOutput>>
    {
        public TestPipelineBuilder()
        {
            this.Insert(new TestMiddleware1())
                .Insert(new TestMiddleware2())
                ;
        }
    }
}