using Godot;
using System;

public partial class Jump : Node
{

	[Export] PlayerMovementData MovementData;
	AnimatedSprite2D animatedSprite;
	Timer coyoteJumpTimer;

	private bool wasOnFloor;
	private bool hasJustLeftLedge;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();


	CharacterBody2D body;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// body = this.GetParent<CharacterBody2D>(); // this works too
		body = GetNode<CharacterBody2D>("..");
		animatedSprite = GetNode<AnimatedSprite2D>("../AnimatedSprite2D");
		coyoteJumpTimer = GetNode<Timer>("../CoyoteJumpTimer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		// I am assuming this is like fixed update ^
		Vector2 velocity = body.Velocity;

		velocity = ApplyGravity(velocity, delta);

		velocity = HandleJump(velocity);

		// if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		// 	velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputAxis = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
		// velocity = HandleAcceleration(inputAxis, velocity, delta); // moved to Move
		
		velocity = ApplyAirResistance(inputAxis, velocity, delta);

		body.Velocity = velocity;
		wasOnFloor = body.IsOnFloor();
		// body.MoveAndSlide(); // wait whaaat
		// also holy sh*t they just handle slopes by default?!
		// okay so we moved this to Move but I'm not sure if it makes sense there
		// like realistically if an entity can jump it can probably also move, but maybe we should move it to a more generic script?
		hasJustLeftLedge = wasOnFloor && !body.IsOnFloor() && velocity.Y >= 0;
		if(hasJustLeftLedge)
		{
			coyoteJumpTimer.Start();
		}
	}

	Vector2 ApplyGravity(Vector2 velocity, double delta)
	{
		// Add the gravity.
		if (!body.IsOnFloor()) // check out how this works, can we still use it for enemy jump?
			velocity.Y += gravity * MovementData.GravityScale * (float)delta;
		return velocity;
	}

	Vector2 HandleJump(Vector2 velocity)
	{
		// Handle Jump.
		if (body.IsOnFloor() || coyoteJumpTimer.TimeLeft > 0.0f)
		{
			if (Input.IsActionJustPressed("ui_accept"))
			{
				velocity.Y = MovementData.JumpVelocity;
			}
		}
		if(!body.IsOnFloor())
		{
			if (Input.IsActionJustReleased("ui_accept") && velocity.Y < MovementData.JumpVelocity/2)
			{
				velocity.Y = MovementData.JumpVelocity/2;
			}
		}
		return velocity;
	}

	Vector2 ApplyAirResistance(Vector2 inputAxis, Vector2 velocity, double delta)
	{
		if (inputAxis == Vector2.Zero && !body.IsOnFloor())
		{
			velocity.X = Mathf.MoveToward(body.Velocity.X, 0, MovementData.AirResistance * (float) delta);
		}
		return velocity;
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
	
	public void UpdateAnimations(Vector2 inputAxis)
	{
		if (!body.IsOnFloor())
		{
			animatedSprite.Play("jump");
		}
	}
}
