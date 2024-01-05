using Godot;
using System;

public partial class quit : Node
{
    public override void _Input(InputEvent @event)
    {
        if(@event.IsActionPressed("quit"))
        {
            GetTree().Quit();
        }
    }
}
