using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public interface IDamageable
{

    // Vector3 Position { get; }

    // void Damage(float damageAmount)
    // {

    // }

    void TakeDamage(float damage)
    {

    }

    float MaxHealth{ get; set;}
    float CurrentHealth{ get; set;}

    void Die()
    {
        
    }

    // Okay so we will have AttackData objects which contain all the data for an attack, and possibly also a method that gets called when the Attack script uses the object
    // The AttackData implementation will play an animation, activate hitboxes, and those hitboxes will trigger Effects when they collide with eligible targets
    // The Effect that gets triggered will be specified in the AttackData, but the implementation for the Effect will be separate
    // Each entity that can be affected by Effects will have a list of active Effects, and an EffectManager that dictates what happens when under those Effects

    // For example, Deathdealer calls the Attack method with the Reap AttackData object. The AttackData implementation will call the relevant resource and card manager methods,
    // then play the attack animation, which will activate the hitbox for the attack. When the hitbox contacts an enemy, the Effect associated with Reap will be triggered,
    // applying the Effect to the enemy



    // Regarding resources like health, currently we have a Health object but we should make a generic resources object that can allow for both discreet and continuous resources

}
