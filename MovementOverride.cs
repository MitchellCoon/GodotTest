using Godot;
using System;

using MonoCustomResourceRegistry;

[RegisteredType(nameof(MovementOverride), "", nameof(Resource))]
public partial class MovementOverride : Resource
{
    [Export] public float Speed {get; set;} = 300.0f;
	[Export] public float JumpVelocity {get; set;} = -400.0f;
	[Export] public float Acceleration {get; set;} = 800.0f;
	[Export] public float Friction {get; set;} = 1000.0f;
    [Export] public float GravityScale {get; set;} = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    [Export] public float DefaultGravityScale {get; set;} = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    [Export] public float AirResistance {get; set;} = 200.0f;
    [Export] public int maxAirJumps {get; set;} = 1;
    [Export] public float terminalVelocity {get; set;} = 3f;
    [Export] public float jumpHeight {get; set;} = 1000.0f;
    [Export] public float downwardMovementMultiplier {get; set;} = 10f;
    [Export] public float upwardMovementMultiplier {get; set;} = 7f;
    [Export] public float upwardMovementShortJumpMultiplier {get; set;} = 15f;
    



    public MovementOverride()
    {
        Speed = 300.0f;
        JumpVelocity = -400.0f;
        Acceleration = 800.0f;
        Friction = 1000.0f;
        GravityScale = 1.0f;
        AirResistance = 200.0f;
    }

}
