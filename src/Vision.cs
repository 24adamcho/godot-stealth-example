using Godot;
using System;
using System.Runtime.InteropServices;

public partial class Vision : Godot.Node3D
{
	[Export]
	float cone_radius = 35;
	[Export]
	float detect_distance = 8f;
	[Export]
	string target_name = "Player";
	[Export]
	float target_capsule_collider_height = 2f;

	private Node3D target;
	private Vector3 facing;
	private Area3D visibility;
	private CollisionShape3D vision_collider;

	private Node GlobalEvents;
	
	public override void _Ready()
	{
		GlobalEvents = GetNode<GlobalEvents>("/root/GlobalEvents");

        var shape = new SphereShape3D
        {
            Radius = detect_distance
        };
		visibility = GetNode<Area3D>("Visibility");
		vision_collider = visibility.GetNode<CollisionShape3D>("CollisionShape3D");
        vision_collider.Shape = shape;

		target = null;
	}

	public override void _Process(double delta)
	{
		facing = GlobalTransform.Basis.Z;
		if(target != null)
		{
			if(check_los(target))
			{
				//check if the vector from the vision source to the target is within the angle limit
				var targeting = Position.DirectionTo(target.Position);
				// GD.Print("Facing:   ", facing);
				// GD.Print("Targeting:", targeting);
				// GD.Print("Dot:      ",facing.Dot(targeting));
				// GD.Print("Const:",1-(cone_radius/180));

				if(facing.Dot(targeting) > 1-(cone_radius/180))
				{
					// GD.Print("TARGETING");
					GlobalEvents.EmitSignal("PlayerSighted");
				}
				else
				{
					// GD.Print("Not targeting");
					GlobalEvents.EmitSignal("PlayerVisibilityLost");
				}
			}
			else
			{
				GlobalEvents.EmitSignal("PlayerVisibilityLost");
			}
		}
	}

	private bool check_los(Node3D body) {
		var space = GetWorld3D().DirectSpaceState;

		Vector3 offset = new Vector3(0, target_capsule_collider_height/2, 0);
		Vector3 top = body.Position + offset;
		Vector3 center = body.Position;
		Vector3 bottom = body.Position - offset;

		Vector3[] guesses = { top, center, bottom };

		foreach(Vector3 pos in guesses)
		{
			var query = PhysicsRayQueryParameters3D.Create(
				Position, 
				pos, 
				visibility.CollisionMask
			);
			// query.Exclude = new Godot.Collections.Array<Rid> {  };
			var result = space.IntersectRay(query);

			if(result.Count > 0)
			{
				var raycollider = (Node3D)result["collider"];
				if(body.Name == raycollider.Name)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void _on_Visibility_body_entered(Node3D body)
	{
		if(target == null && body.Name == target_name)
		{
			target = body;
		}
	}

	private void _on_Visibility_body_exited(Node3D body)
	{
		if(target != null)
		{
			target = null;
			GlobalEvents.EmitSignal("PlayerVisibilityLost");
		}
	}
}
