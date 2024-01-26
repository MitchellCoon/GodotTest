using Godot;
using System;

using MonoCustomResourceRegistry;

[RegisteredType(nameof(PlayerMovementData), "", nameof(Resource))]
public partial class PlayerMovementData : Resource
{
    [Export] public float Speed {get; set;} = 300.0f;
	[Export] public float JumpVelocity {get; set;} = -400.0f;
	[Export] public float Acceleration {get; set;} = 800.0f;
	[Export] public float Friction {get; set;} = 1000.0f;
    [Export] public float GravityScale {get; set;} = 1.0f;
    [Export] public float AirResistance {get; set;} = 200.0f;


    public PlayerMovementData()
    {
        Speed = 300.0f;
        JumpVelocity = -400.0f;
        Acceleration = 800.0f;
        Friction = 1000.0f;
        GravityScale = 1.0f;
        AirResistance = 200.0f;
    }

}
