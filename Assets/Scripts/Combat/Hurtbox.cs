using Godot;
using MonoCustomResourceRegistry;
using System;
using System.Diagnostics;

[RegisteredType(nameof(Hurtbox), "", nameof(Area2D))]
public partial class Hurtbox : Area2D
{
	[Export] float damage = 1;
	[Export] int collisionLayer = 0;
	[Export] int collosionMask = 2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_area_entered(Hitbox hitbox)
	{
		GD.Print("test");
		if (hitbox == null) return;
		if (Owner.HasMethod("TakeDamage"))
		{
			// TakeDamage(hitbox.damage); // update this later
		}
		// Hide();
		// GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("Disabled", true);
		// EmitSignal("Hit");

		// for testing
		TakeDamage(1f);
	}

	public void _on_body_entered(Node2D body)
	{
		// Hide();
		// GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("Disabled", true);
		// EmitSignal("Hit");
		GD.Print("test2");
	}

	public void _on_area_entered(Area2D hitbox)
	{
		GD.Print("test3");
	}

	void TakeDamage(float damage)
	{
		// move this somewhere else later
		GD.Print("took " + damage + " damage");
	}
}
