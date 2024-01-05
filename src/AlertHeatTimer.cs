using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class AlertHeatTimer : Node
{
    private Node GlobalEvents;
    private Timer timer;
    private double time_reset;
    private bool triggered;
	public override void _Ready()
	{
		GlobalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        GlobalEvents.Connect("PlayerSighted", new Callable(this, MethodName._OnPlayerSighted));

        timer = GetNode<Timer>("Timer");
        time_reset = timer.TimeLeft;
        timer.Timeout += _OnTimerEnd;

        triggered = false;
	}

    public override void _Process(double delta)
    {
        if(!triggered) return;

        GlobalEvents.EmitSignal("PlayerVisibilityTimerInProgress", timer.TimeLeft);
    }

    private void _OnPlayerSighted()
    {
        if(triggered) {
            timer.Start(time_reset);
            return;
        }

        triggered = true;
        timer.Start();
        GlobalEvents.EmitSignal("PlayerVisibilityTimerStarted");
    }

    private void _OnTimerEnd()
    {
        GlobalEvents.EmitSignal("PlayerVisibilityTimerEnd");
        triggered = false;
    }
}
