using Godot;
using System;

public partial class GlobalEvents : Node
{
    [Signal] public delegate void PlayerSightedEventHandler(Vector3 pos);
    [Signal] public delegate void PlayerVisibilityLostEventHandler(Vector3 pos);
    [Signal] public delegate void PlayerVisibilityTimerStartedEventHandler();
    [Signal] public delegate void PlayerVisibilityTimerInProgressEventHandler(double time);
    [Signal] public delegate void PlayerVisibilityTimerEndEventHandler();
    //godot engine requires adding EventHandler to the end of signal definitions, but the actual string used
    //when connecting/emitting that event is that name without the EventHandler. Why? No reason, probably.
}
