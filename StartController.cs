using Elsa.Dispatch;
using Elsa.Persistence;
using Elsa.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElsaQuickstarts.Server.DashboardAndServer
{
    public class StartController : Controller
    {
        private readonly IWorkflowRunner workflowInvoker;
        private readonly IWorkflowRegistry workflowRegistry;
        private readonly IWorkflowFactory workflowFactory;
        private readonly IWorkflowInstanceStore workflowInstanceStore;
        private readonly IWorkflowInstanceDispatcher workflowInstanceDispatcher;

        public StartController(IWorkflowRunner workflowInvoker, IWorkflowRegistry workflowRegistry, IWorkflowFactory workflowFactory, IWorkflowInstanceStore workflowInstanceStore, IWorkflowInstanceDispatcher workflowInstanceDispatcher)
        {
            this.workflowInvoker = workflowInvoker;
            this.workflowRegistry = workflowRegistry;
            this.workflowFactory = workflowFactory;
            this.workflowInstanceStore = workflowInstanceStore;
            this.workflowInstanceDispatcher = workflowInstanceDispatcher;
        }

        [Route("/start")]
        public async Task<string> Get()
        {
            var workflow = await workflowRegistry.FindAsync(f => f.Name == "TestWorkFlow");
            var instance = await workflowFactory.InstantiateAsync(workflow);
            await workflowInstanceStore.AddAsync(instance);
            await workflowInvoker.RunWorkflowAsync(workflow, instance);
            await workflowInstanceDispatcher.DispatchAsync(new ExecuteWorkflowInstanceRequest(instance.Id, "activity-start"));
            return instance.Id;
        }
    }
}
