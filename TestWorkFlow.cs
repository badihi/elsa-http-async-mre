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
            //.SignalReceived("Finalize")
            .StartWith<TestActivity>()
            .WriteLine("Hello World!");
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