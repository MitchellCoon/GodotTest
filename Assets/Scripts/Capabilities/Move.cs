using Godot;
using System;

public partial class Move : Node
{
	[Export] PlayerMovementData MovementData;
	AnimatedSprite2D animatedSprite;
	CharacterBody2D body;
	public bool IsFacingRight = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		body = GetNode<CharacterBody2D>("..");
		animatedSprite = GetNode<AnimatedSprite2D>("../AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = body.Velocity;
		Vector2 inputAxis = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
		velocity = HandleAcceleration(inputAxis, velocity, delta);
		velocity = ApplyFriction(inputAxis, velocity, delta);
		body.Velocity = velocity;
		body.MoveAndSlide(); // wait whaaat
	}

	Vector2 HandleAcceleration(Vector2 inputAxis, Vector2 velocity, double delta)
	{
		if (inputAxis != Vector2.Zero)
		{
			// velocity.X = direction.X * Speed;
			velocity.X = Mathf.MoveToward(velocity.X, MovementData.Speed * inputAxis.X, MovementData.Acceleration * (float) delta);
		}
		return velocity;
	}
	Vector2 ApplyFriction(Vector2 inputAxis, Vector2 velocity, double delta)
	{
		if (inputAxis == Vector2.Zero && body.IsOnFloor())
		{
			velocity.X = Mathf.MoveToward(body.Velocity.X, 0, MovementData.Friction * (float) delta);
		}
		return velocity;
	}

	public void UpdateAnimations(Vector2 inputAxis)
	{
		if (!body.IsOnFloor()) return;
		if (inputAxis != Vector2.Zero)
		{
			animatedSprite.FlipH = (inputAxis.X < 0);
			IsFacingRight = !IsFacingRight;
			animatedSprite.Play("run");
		}
		else
		{
			animatedSprite.Play("idle");
		}
	}

	
}
