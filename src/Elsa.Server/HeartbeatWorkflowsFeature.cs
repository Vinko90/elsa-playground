using Elsa.Attributes;
using Elsa.Options;
using Elsa.Server.Workflows;
using StartupBase = Elsa.Services.Startup.StartupBase;

namespace Elsa.Server;

[Feature("Workflows:Heartbeat")]
public class HeartbeatWorkflowsFeature : StartupBase
{
    public override void ConfigureElsa(ElsaOptionsBuilder elsa, IConfiguration configuration)
    {
        elsa.AddWorkflow<HeartbeatWorkflow>();
    }
}