using Godot;
using System;
using System.Security.Cryptography;

public partial class LabelTimeRemaining : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Node GlobalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
        GlobalEvents.Connect("PlayerVisibilityTimerInProgress", new Callable(this, MethodName._onTimerChange));
        GlobalEvents.Connect("PlayerVisibilityTimerEnd", new Callable(this, MethodName._onTimerEnd));

		Text = "";
	}
	
	private void _onTimerChange(double time)
	{
		Text = String.Format("Time remaining:\n{0:0.00}", time);
	}

	private void _onTimerEnd()
	{
		Text = "";
	}
}
