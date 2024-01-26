using Godot;
using System;

public partial class Attack : Node
{
	// [Export] PlayerMovementController controller;
    // [Export] Animator animator;
    InputManager inputManager;
    // [Export] Hitbox hitbox;

    // [Export] AttackData attackData;
    // [Export] AttackData defaultAttack;

    // [Export] Transform projectileOrigin;
    // [Export] GameObject projectilePrefab;

    private float timeOfLastAttack;
    private float nextAttackTime = 0f;
    private ButtonInput currentInput;
	public bool canAttack = true;
	public MotionInput action; // might not need to be public
    AnimationPlayer animator;
	// [Export] List<MotionInput> commands;
    // [Export] List<AttackData> commandAttacks;
    // Dictionary<MotionInput, AttackData> commandAttackDictionary = new Dictionary<MotionInput, AttackData>();
    // AttackData commandAttack;




	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        inputManager = GetNode<InputManager>("../InputManager");
        animator = GetNode<AnimationPlayer>("../AnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (canAttack && inputManager.HasInputBeenRequested(ButtonInput.Attack1) && IsAttackBuffered(ButtonInput.Attack1))
        {
            //Debug.Log("checking command input in Attack");
            action = inputManager.DetectCommandInput();
            // if(commandAttackDictionary.TryGetValue(action, out commandAttack))
            // {
            //     Debug.Log("using " + action);
            //     SetAttackData(commandAttack);
            // }
            // else
            // {
            //     Debug.Log("using default attack");
            //     SetAttackData(defaultAttack);
            // }

            // I think if we do everything from the animations, we shouldn't need this part:
            // if(attackData.attackType == AttackType.Melee)
            // {
            //     hitbox.UpdateAttackData(attackData);
            // }
            currentInput = ButtonInput.Attack1;
            ExecuteAttack();


            // combo system:
            // ExitAttack();
        }
	}

	// public void SetAttackData(AttackData newAttackData)
    // {
    //     attackData = newAttackData;
    // }

	private void ExecuteAttack()
    {
        timeOfLastAttack = Time.GetTicksMsec()/1000f;
        inputManager.SetPreviousActionTime(Action.Attack, Time.GetTicksMsec()/1000f);
        inputManager.RemoveAllInstancesOfActionFromInputList(currentInput);
        // nextAttackTime = Time.time + attackData.duration; // this should be determined by animations/canAttack flag, not hard coded
        // animator.SetTrigger(attackData.animationName);
        animator.Play("Attack");

    }

	private bool IsAttackBuffered(ButtonInput attack)
    {
        return Time.GetTicksMsec()/1000f < inputManager.GetMostRecentInputTime(attack) + inputManager.GetInputBufferTime(attack);
    }

	// public void SpawnProjectile()
    // {
    //     if (controller.IsFacingRight())
    //     {
    //         Instantiate(projectilePrefab, projectileOrigin.transform.position, transform.rotation);
    //     }
    //     else
    //     {
    //         Instantiate(projectilePrefab, projectileOrigin.transform.position, transform.rotation * Quaternion.Euler(0, 180, 0));
    //     }
    // }

}
