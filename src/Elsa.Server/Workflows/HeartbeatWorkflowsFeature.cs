using Elsa.Attributes;
using Elsa.Options;
using StartupBase = Elsa.Services.Startup.StartupBase;

namespace Elsa.Server.Workflows;

[Feature("Workflows:Heartbeat")]
public class HeartbeatWorkflowsFeature : StartupBase
{
    public override void ConfigureElsa(ElsaOptionsBuilder elsa, IConfiguration configuration)
    {
        elsa.AddWorkflow<HeartbeatWorkflow>();
    }
}