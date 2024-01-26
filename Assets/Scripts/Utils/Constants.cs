using System.Collections;
using System.Collections.Generic;

public static class Constants
{
    // constants can go here, e.g.:
    // const float MAGIC_NUMBER = float.MaxValue;

    // tags
    public const string UNTAGGED = "Untagged";
    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";
    public const string MOVING_PLATFORM_TAG = "MovingPlatform";

    // layers
    public const string DEFAULT_LAYER = "Default";
    public const string PLAYER_LAYER = "Player";
    public const string INVINCIBLE_LAYER = "Invincible";

    // animations
    public const string MELEE_ATTACK_ANIMATION = "MeleeAttack";
    public const string PROJECTILE_ATTACK_ANIMATION = "ProjectileAttack";
    public const string HURT_ANIMATION = "Hurt";
    public const string JUMP_RISE_ANIMATION = "Jump";
    public const string JUMP_FALL_ANIMATION = "Fall";
    public const string JUMP_LAND_ANIMATION = "Land";
    public const string BOUNCE_ANIMATION = "Bounce";
    public const string IDLE_TRANSITION_ANIMATION = "IdleTransition";
    public const string IS_MOVING_BOOL = "isMoving";
    public const string IS_GROUNDED_BOOL = "isGrounded";
    public const string JUDGEMENT_CAT_END_CHARGE_ANIMATION = "JudgementCatEndCharge";
    public const string JUDGEMENT_CAT_END_SLASH_ANIMATION = "JudgementCatEndSlash";
    public const string JUDGEMENT_CAT_END_SHEATH_ANIMATION = "JudgementCatEndSheath";

    // common
    public const float GRAVITY = 9.8f;

    // command inputs
    public static readonly List<ButtonInput> NUMPAD236ATTACK_INPUTS_RIGHT = new List<ButtonInput>(new[] {ButtonInput.Direction2, ButtonInput.Direction3, ButtonInput.Direction6, ButtonInput.Attack1});
    public static readonly List<ButtonInput> NUMPAD623ATTACK_INPUTS_RIGHT = new List<ButtonInput>(new[] {ButtonInput.Direction6, ButtonInput.Direction2, ButtonInput.Direction3, ButtonInput.Attack1});
    //public static readonly ReadOnlyCollection<Input> NUMPAD236ATTACK_INPUTS = new ReadOnlyCollection<Input>(new[] {Input.Direction2, Input.Direction3, Input.Direction6, Input.Attack1});
    public static readonly List<ButtonInput> NUMPAD236ATTACK_INPUTS_LEFT = new List<ButtonInput>(new[] {ButtonInput.Direction2, ButtonInput.Direction1, ButtonInput.Direction4, ButtonInput.Attack1});
    public static readonly List<ButtonInput> NUMPAD623ATTACK_INPUTS_LEFT = new List<ButtonInput>(new[] {ButtonInput.Direction4, ButtonInput.Direction2, ButtonInput.Direction1, ButtonInput.Attack1});

    public static readonly List<List<ButtonInput>> ALL_MOTION_INPUTS_RIGHT = new List<List<ButtonInput>>(new[] {NUMPAD236ATTACK_INPUTS_RIGHT, NUMPAD623ATTACK_INPUTS_RIGHT});
    public static readonly List<List<ButtonInput>> ALL_MOTION_INPUTS_LEFT = new List<List<ButtonInput>>(new[] {NUMPAD236ATTACK_INPUTS_LEFT, NUMPAD623ATTACK_INPUTS_LEFT});

    public static readonly List<MotionInput> ALL_COMMAND_INPUTS = new List<MotionInput>(new[] {MotionInput.Numpad236, MotionInput.Numpad623});

    public static readonly List<float> NUMPAD236ATTACK_BUFFER_TIMES = new List<float>(new[] {0.15f, 0.15f, 0.15f});
    public static readonly List<float> NUMPAD623ATTACK_BUFFER_TIMES = new List<float>(new[] {0.15f, 0.15f, 0.15f});
    public static readonly List<List<float>> ALL_MOTION_INPUTS_BUFFER_TIMES = new List<List<float>>(new[] {NUMPAD236ATTACK_BUFFER_TIMES, NUMPAD623ATTACK_BUFFER_TIMES});

    
}
