using Godot;
using System;

public enum JumpState { Grounded, Rising, Falling };

public partial class NewJump : Node
{

	// Goals for Monday 11-06:
	// Add double jump
	// Check how double jump and wall jump in video are implemented
	// Also polish sword

	// Goals for Tuesday 11-07:
	// Input buffering for jump
	// Bonus: Start porting attack scripts, learn how hitboxes work

	// Okay so I think we still have some weird stuff going on between inputmanager and jump
	// We should be using list and not queue for inputs, but jump uses queue
	// also we never wrote the detect jump method



	[Export] PlayerMovementData MovementData;
	AnimatedSprite2D animatedSprite;
	Timer coyoteJumpTimer;

	private bool wasOnFloor;
	private bool hasJustLeftLedge;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();


	CharacterBody2D body;





	// for new jump:
	[Export] InputManager inputManager;
	[Export] MovementOverride movement;

	private bool desiredJump;
	private int jumpPhase;
	private JumpState jumpState;
	private float jumpBufferCounter;
    private bool jumpBufferTimeStarted = false;
	private float coyoteTimeCounter;
	private Vector2 velocity;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// body = this.GetParent<CharacterBody2D>(); // this works too
		body = GetNode<CharacterBody2D>("..");
		animatedSprite = GetNode<AnimatedSprite2D>("../AnimatedSprite2D");
		coyoteJumpTimer = GetNode<Timer>("../CoyoteJumpTimer");
        inputManager = GetNode<InputManager>("../InputManager");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		desiredJump |= inputManager.DetectJumpInput();
        if (desiredJump)
        {
            if (!inputManager.HasInputBeenRequested(ButtonInput.Jump))
            {
                inputManager.AddInputRequestToList(ButtonInput.Jump, Time.GetTicksMsec()/1000f);
            }
            if (!jumpBufferTimeStarted)
            {
                jumpBufferCounter = inputManager.GetInputBufferTime(ButtonInput.Jump);
                jumpBufferTimeStarted = true;
            }
            else
            {
                jumpBufferCounter -= (float) delta;
                if (jumpBufferCounter <= 0f)
                {
                    jumpBufferCounter = 0f;
                    desiredJump = false;
                    jumpBufferTimeStarted = false;
                }
            }
        }
        if (inputManager.DetectJumpInput()) // wait why is this not in the above block? test later
        {
            inputManager.SetPreviousPressedTime(ButtonInput.Jump, Time.GetTicksMsec()/1000f); // wait why is this not in the above block?
        }
	}

	public override void _PhysicsProcess(double delta)
	{
		velocity = body.Velocity;
        if (body.IsOnFloor())
        {
            // animator.SetBool(Constants.IS_GROUNDED_BOOL, true);
            jumpPhase = 0;
            if (jumpState != JumpState.Grounded)
            {
                // if (animator != null) animator.SetBool(Constants.JUMP_FALL_ANIMATION, false);
                // if (animator != null) animator.SetBool(Constants.JUMP_LAND_ANIMATION, true);
            }
            jumpState = JumpState.Grounded;
            coyoteTimeCounter = inputManager.GetInputBufferTime(ButtonInput.CoyoteJump);
            inputManager.SetPreviousActionTime(Action.Grounded, Time.GetTicksMsec()/1000f);
        }
        else
        {
            // animator.SetBool(Constants.IS_GROUNDED_BOOL, false);
            coyoteTimeCounter -= (float) delta;
        }

         bool isJumpButtonHeld = Input.IsActionPressed("ui_accept"); // update this for custom actions later

        if (inputManager.HasInputBeenRequested(ButtonInput.Jump) && CanJump())
        {
            JumpAction();
        }

		// this section is for short hop, fix this later:

        if (velocity.Y < 0 && isJumpButtonHeld && !body.IsOnFloor())
        {
            if (jumpState != JumpState.Rising)
            {
                // if (animator != null) animator.SetTrigger(Constants.JUMP_RISE_ANIMATION);
                // if (animator != null) animator.SetBool(Constants.JUMP_FALL_ANIMATION, false);
                // if (animator != null) animator.SetBool(Constants.JUMP_LAND_ANIMATION, false);
            }
            jumpState = JumpState.Rising;
            movement.GravityScale = movement.upwardMovementMultiplier;
        }
        else if (velocity.Y < 0 && !isJumpButtonHeld && !body.IsOnFloor())
        {
            if (jumpState != JumpState.Rising)
            {
                // if (animator != null) animator.SetTrigger(Constants.JUMP_RISE_ANIMATION);
                // if (animator != null) animator.SetBool(Constants.JUMP_FALL_ANIMATION, false);
                // if (animator != null) animator.SetBool(Constants.JUMP_LAND_ANIMATION, false);
            }
            jumpState = JumpState.Rising;
            movement.GravityScale = movement.upwardMovementShortJumpMultiplier;
        }
        else if (velocity.Y > 0 && !body.IsOnFloor())
        {
            if (jumpState != JumpState.Falling)
            {
                // if (animator != null) animator.SetBool(Constants.JUMP_FALL_ANIMATION, true);
                // if (animator != null) animator.SetBool(Constants.JUMP_LAND_ANIMATION, false);
            }
            jumpState = JumpState.Falling;
            movement.GravityScale = movement.downwardMovementMultiplier;

        }
        else
        {
            movement.GravityScale = movement.DefaultGravityScale;
        }

        // clamp fall speed to terminal velocity
        if (velocity.Y < 0)
        {
            float clampedYSpeed = Mathf.Clamp(velocity.Y, -Constants.GRAVITY * movement.terminalVelocity, 0);
            velocity = new Vector2(velocity.X, clampedYSpeed);
        }

        velocity = ApplyGravity(velocity, delta);

        body.Velocity = velocity;


		// old physics process:
		// // I am assuming this is like fixed update ^
		// Vector2 velocity = body.Velocity;

		// velocity = ApplyGravity(velocity, delta);

		// velocity = HandleJump(velocity);

		// // if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		// // 	velocity.Y = JumpVelocity;

		// // Get the input direction and handle the movement/deceleration.
		// // As good practice, you should replace UI actions with custom gameplay actions.
		// Vector2 inputAxis = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
		// // velocity = HandleAcceleration(inputAxis, velocity, delta); // moved to Move
		
		// velocity = ApplyAirResistance(inputAxis, velocity, delta);

		// body.Velocity = velocity;
		// wasOnFloor = body.IsOnFloor();
		// // body.MoveAndSlide(); // wait whaaat
		// // also holy sh*t they just handle slopes by default?!
		// // okay so we moved this to Move but I'm not sure if it makes sense there
		// // like realistically if an entity can jump it can probably also move, but maybe we should move it to a more generic script?
		// hasJustLeftLedge = wasOnFloor && !body.IsOnFloor() && velocity.Y >= 0;
		// if(hasJustLeftLedge)
		// {
		// 	coyoteJumpTimer.Start();
		// }
	}

	Vector2 ApplyGravity(Vector2 velocity, double delta)
	{
		// Add the gravity.
		if (!body.IsOnFloor()) // check out how this works, can we still use it for enemy jump?
			velocity.Y += gravity * movement.GravityScale * (float)delta;
		return velocity;
	}

	Vector2 HandleJump(Vector2 velocity)
	{
		// Handle Jump.
		if (body.IsOnFloor() || coyoteJumpTimer.TimeLeft > 0.0f)
		{
			if (Input.IsActionJustPressed("ui_accept"))
			{
				velocity.Y = movement.JumpVelocity;
			}
		}
		if(!body.IsOnFloor())
		{
			if (Input.IsActionJustReleased("ui_accept") && velocity.Y < movement.JumpVelocity/2)
			{
				velocity.Y = movement.JumpVelocity/2;
			}
		}
		return velocity;
	}

	Vector2 ApplyAirResistance(Vector2 inputAxis, Vector2 velocity, double delta)
	{
		if (inputAxis == Vector2.Zero && !body.IsOnFloor())
		{
			velocity.X = Mathf.MoveToward(body.Velocity.X, 0, movement.AirResistance * (float) delta);
		}
		return velocity;
	}

	Vector2 HandleAcceleration(Vector2 inputAxis, Vector2 velocity, double delta)
	{
		if (inputAxis != Vector2.Zero)
		{
			// velocity.X = direction.X * Speed;
			velocity.X = Mathf.MoveToward(velocity.X, movement.Speed * inputAxis.X, MovementData.Acceleration * (float) delta);
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

	bool CanJump()
    {
        return (!(inputManager.GetPreviousActionTime(Action.Grounded) == -1)
        && Time.GetTicksMsec()/1000f - inputManager.GetPreviousActionTime(Action.Grounded) <= inputManager.GetInputBufferTime(ButtonInput.Jump))
        || jumpPhase < movement.maxAirJumps;
        // add additional condition here when we add enemy jumping
    }
	
	private void JumpAction()
    {
        if ((coyoteTimeCounter < 0f || jumpBufferCounter < 0f))
        {
            jumpPhase += 1;
        }
        jumpBufferCounter = 0f;
        coyoteTimeCounter = 0f;
        desiredJump = false;
        jumpBufferTimeStarted = false;
        inputManager.SetPreviousActionTime(Action.Grounded, -1);
        inputManager.SetPreviousPressedTime(ButtonInput.Jump, -1);
        inputManager.RemoveAllInstancesOfActionFromInputList(ButtonInput.Jump);
        // float jumpSpeed = Mathf.Sqrt(2f * movement.GravityScale * movement.jumpHeight); // this might be wrong now
        float jumpSpeed = movement.jumpHeight; // just using this temporarily
        if (velocity.Y < 0f)
        {
            // if already ascending, the final velocity will be the maximum of the jump speed and the current speed
            jumpSpeed = Mathf.Max(jumpSpeed + velocity.Y, 0f);
        }
        if (!body.IsOnFloor() && velocity.Y > 0f)
        {
            // first zero out y velocity if falling
            velocity.Y = 0;
        }
        velocity.Y -= jumpSpeed;
        //PlayJumpSound();
    }

}
