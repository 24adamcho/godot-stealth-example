using Godot;
using System;

public partial class textStatusSignifier : Label
{
	private Node GlobalEvents;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GlobalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");
		GlobalEvents.Connect("PlayerSighted", new Callable(this, MethodName._OnPlayerDetected));
		GlobalEvents.Connect("PlayerVisibilityLost", new Callable(this, MethodName._OnPlayerLost));
	}

	private void _OnPlayerDetected()
	{
		Text = "Player sighted!";
	}

	private void _OnPlayerLost()
	{
		Text = "";
	}
}
