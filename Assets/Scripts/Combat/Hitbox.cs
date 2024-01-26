using Godot;
using System;
using System.Collections.Generic;

public partial class Hitbox : Area2D
{
	// other hitbox script:

	// [Export] public float damage = 1;
	// [Export] int collisionLayer = 2;
	// [Export] int collosionMask = 0;




	// advanced hitbox script:

	// [Export] float width = 300f;
	// [Export] float height = 400f;
	// [Export] float damage = 50f;
	// [Export] float angle = 90f;
	// [Export] float baseKnockback = 100f;
	// [Export] float knockbackScaling = 2f;
	// [Export] float duration = 1500f;
	// [Export] float hitlagModifier = 1f;
	// [Export] String type = "normal";
	// [Export] float angleFlipper = 0f;

	// Node parent;
	// Node hitbox;
	// Node parentState;

	// float knockback;
	// float framez = 0f;
	// List<Node> playerList = new List<Node>();

	// // Called when the node enters the scene tree for the first time.
	// public override void _Ready()
	// {
	// 	parent = GetParent<Node>();
	// 	hitbox = GetNode<Node>("HitboxShape");
	// 	// parentState = parent.selfState; ???
	// }

	// // Called every frame. 'delta' is the elapsed time since the previous frame.
	// public override void _Process(double delta)
	// {
	// }

	public void _on_area_entered(Area2D area)
	{
		GD.Print("take damage");
		if (area.IsInGroup("Hurtbox"))
		{
			GD.Print("take damage");
		}
	}

}
