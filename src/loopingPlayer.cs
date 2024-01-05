using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class loopingPlayer : AudioStreamPlayer3D
{
    public override void _Ready()
    {
        Play(0f);
    }
    private void _on_finished()
    {
        Play(0f);
    }
}
