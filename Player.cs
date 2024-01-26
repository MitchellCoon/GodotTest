using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// [Export] PlayerMovementData MovementData;
	// AnimatedSprite2D animatedSprite;
	// Timer coyoteJumpTimer;

	// private bool wasOnFloor;
	// private bool hasJustLeftLedge;

	// // Get the gravity from the project settings to be synced with RigidBody nodes.
	// public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// public override void _Ready()
	// {
	// 	animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	// 	coyoteJumpTimer = GetNode<Timer>("CoyoteJumpTimer");
	// }

	// public override void _PhysicsProcess(double delta)
	// {
	// 	// I am assuming this is like fixed update ^
	// 	Vector2 velocity = Velocity;

	// 	velocity = ApplyGravity(velocity, delta);

	// 	velocity = HandleJump(velocity);

	// 	// if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
	// 	// 	velocity.Y = JumpVelocity;

	// 	// Get the input direction and handle the movement/deceleration.
	// 	// As good practice, you should replace UI actions with custom gameplay actions.
	// 	Vector2 inputAxis = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
	// 	velocity = HandleAcceleration(inputAxis, velocity, delta);
		
	// 	velocity = ApplyFriction(inputAxis, velocity, delta);

	// 	Velocity = velocity;
	// 	wasOnFloor = IsOnFloor();
	// 	MoveAndSlide(); // wait whaaat
	// 	// also holy sh*t they just handle slopes by default?!
	// 	hasJustLeftLedge = wasOnFloor && !IsOnFloor() && velocity.Y >= 0;
	// 	if(hasJustLeftLedge)
	// 	{
	// 		coyoteJumpTimer.Start();
	// 	}
	// }

	// Vector2 ApplyGravity(Vector2 velocity, double delta)
	// {
	// 	// Add the gravity.
	// 	if (!IsOnFloor()) // check out how this works, can we still use it for enemy jump?
	// 		velocity.Y += gravity * MovementData.GravityScale * (float)delta;
	// 	return velocity;
	// }

	// Vector2 HandleJump(Vector2 velocity)
	// {
	// 	// Handle Jump.
	// 	if (IsOnFloor() || coyoteJumpTimer.TimeLeft > 0.0f)
	// 	{
	// 		if (Input.IsActionJustPressed("ui_accept"))
	// 		{
	// 			velocity.Y = MovementData.JumpVelocity;
	// 		}
	// 	}
	// 	if(!IsOnFloor())
	// 	{
	// 		if (Input.IsActionJustReleased("ui_accept") && velocity.Y < MovementData.JumpVelocity/2)
	// 		{
	// 			velocity.Y = MovementData.JumpVelocity/2;
	// 		}
	// 	}
	// 	return velocity;
	// }

	// Vector2 ApplyFriction(Vector2 inputAxis, Vector2 velocity, double delta)
	// {
	// 	if (inputAxis == Vector2.Zero && IsOnFloor())
	// 	{
	// 		velocity.X = Mathf.MoveToward(Velocity.X, 0, MovementData.Friction * (float) delta);
	// 	}
	// 	else if (inputAxis == Vector2.Zero && !IsOnFloor())
	// 	{
	// 		velocity.X = Mathf.MoveToward(Velocity.X, 0, MovementData.AirResistance * (float) delta);
	// 	}
	// 	return velocity;
	// }

	// Vector2 HandleAcceleration(Vector2 inputAxis, Vector2 velocity, double delta)
	// {
	// 	if (inputAxis != Vector2.Zero)
	// 	{
	// 		// velocity.X = direction.X * Speed;
	// 		velocity.X = Mathf.MoveToward(velocity.X, MovementData.Speed * inputAxis.X, MovementData.Acceleration * (float) delta);
	// 	}
	// 	return velocity;
	// }
	
	// public void UpdateAnimations(Vector2 inputAxis)
	// {
	// 	if (inputAxis != Vector2.Zero)
	// 	{
	// 		animatedSprite.FlipH = (inputAxis.X < 0);
	// 		animatedSprite.Play("run");
	// 	}
	// 	else
	// 	{
	// 		animatedSprite.Play("idle");
	// 	}
	// 	if (!IsOnFloor())
	// 	{
	// 		animatedSprite.Play("jump");
	// 	}
	// }

}
