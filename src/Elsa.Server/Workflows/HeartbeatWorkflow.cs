using Elsa.Activities.Console;
using Elsa.Activities.Temporal;
using Elsa.Builders;
using NodaTime;

namespace Elsa.Server.Workflows;

public class HeartbeatWorkflow : IWorkflow
{
    private readonly IClock _clock;
    
    public HeartbeatWorkflow(IClock clock) => _clock = clock;

    public void Build(IWorkflowBuilder builder)
    {
        builder
            .WithDisplayName("Custom Timer")
            .Timer(Duration.FromSeconds(10))
            .WriteLine(() => $"Heartbeat at {_clock.GetCurrentInstant()}");
    }
}