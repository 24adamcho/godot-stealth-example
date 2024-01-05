using Godot;
using System;

public partial class AlertPauseAudioPlayer : AudioStreamPlayer
{
	public override void _Ready()
	{
		Node GlobalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        GlobalEvents.Connect("PlayerVisibilityTimerStarted", new Callable(this, MethodName._onPlayerVisibilityTimerStarted));
	}

	private void _onPlayerVisibilityTimerStarted()
	{
		Play(0f);
	}
}
