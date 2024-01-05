using Godot;
using System;

public partial class speeeeeeeeeen : Node3D
{
	[Export]
	public float _SPEEEEEN = 0.5f;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		Rotate(new Vector3(0, 1, 0), _SPEEEEEN * (float)delta);
	}
}
