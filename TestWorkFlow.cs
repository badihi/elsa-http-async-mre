using Elsa.Activities.Console;
using Elsa.Activities.ControlFlow;
using Elsa.Activities.Http;
using Elsa.Activities.Signaling;
using Elsa.ActivityResults;
using Elsa.Builders;
using Elsa.Services;
using Elsa.Services.Models;
using System.Threading.Tasks;

namespace ElsaQuickstarts.Server.DashboardAndServer
{
    public class TestWorkFlow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder) => builder
            .HttpEndpoint("/wf")
            //.StartWith<StartActivity>(c => c.ActivityId = "start")
            .Then<TestActivity>(c => c.ActivityId = "test")
            .WriteLine("Hello World!");
    }

    public class StartActivity : Activity
    {
        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            return Suspend();
        }
    }

    public class TestActivity : Activity
    {
        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            await Task.Delay(10000);
            return Done();
        }
    }
}
