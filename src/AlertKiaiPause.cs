using Godot;
using System;
using System.Linq;

public partial class AlertKiaiPause : Node
{
    private Timer timer;

    [Export]
    private Node[] pausable;

    public override void _Ready()
    {
        GlobalEvents globalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        globalEvents.Connect("PlayerVisibilityTimerStarted", new Callable(this, MethodName._onPlayerVisibilityTimerStarted));

        timer = GetNode<Timer>("Timer");
        timer.Timeout += _onTimerRunout;
    }

    private void disableProcessStateThreadsafe(Node p)
    {
        p.ProcessMode = ProcessModeEnum.Disabled;
    }
    private void enableProcessStateThreadsafe(Node p)
    {
        p.ProcessMode = ProcessModeEnum.Pausable;
    }

    private void _onPlayerVisibilityTimerStarted() {
        foreach(Node p in pausable)
        {
            CallDeferred(MethodName.disableProcessStateThreadsafe, p);
        }
        timer.Start();
    }

    private void _onTimerRunout() {
        foreach(Node p in pausable)
        {
            CallDeferred(MethodName.enableProcessStateThreadsafe, p);
        }
    }
}
