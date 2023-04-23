using ANLG.Utilities.FlatRedBall.Extensions;
using FlatRedBall;
using Microsoft.Xna.Framework;
using PixelDungeonJam.DataTypes;
using PixelDungeonJam.Entities;
using PixelDungeonJam.Factories;
using System;

namespace PixelDungeonJam.Systems.Weapons;

public class MeleeWeapon : IWeapon
{
    private float _cooldownRemaining = -1;

    public MeleeWeapon(SwordDesign swordDesign)
    {
        var hiltLookup = Player.HiltDesign[swordDesign.Hilt];
        var crossguardLookup = Player.CrossguardDesign[swordDesign.Crossguard];
        var bladeLookup = Player.BladeDesign[swordDesign.Blade];
        
        Name = swordDesign.Name;
        Description = swordDesign.Description;
        Damage = bladeLookup.Damage;
        Weight = bladeLookup.Weight;
        AttackDurationRatio = bladeLookup.AttackDurationRatio;
        Range = bladeLookup.Range;
        Handling = hiltLookup.Handling;
        Guard = crossguardLookup.Guard;
    }

    public string Name { get; }
    public string Description { get; }
    public float Damage { get; }
    public float Weight { get; }
    public float AttackDurationRatio { get; }
    public float Range { get; }
    public float Handling { get; }
    public float Guard { get; }
    
    public bool IsActionable => _cooldownRemaining <= 0;
    public float CooldownTime => 1 / Handling;
    public float TotalSecondsPerAttack => AttackDuration + CooldownTime;
    public float AttackDuration => AttackDurationRatio * Weight;
    public float DamagePerSecond => Damage * TotalSecondsPerAttack;

    public void Activity()
    {
        _cooldownRemaining -= TimeManager.SecondDifference;
    }

    public void PrimaryAction(PositionedObject parent, Vector2 positionOffset, PlayerPointer pointer)
    {
        if (!IsActionable) { return; }
        
        var slash = SlashFactory.CreateNew();
        slash.TeamIndex = 0;
        
        slash.Position = parent.Position.Add(positionOffset)
            .Add(Vector2Extensions.FromAngleAndLength(pointer.Angle, Range));
        
        float dot = Vector2.Dot(parent.Velocity.XY(), pointer.NormalizedDirection);
        if (dot > 0)
        {
            slash.Velocity = parent.Velocity.XY().ProjectOnto(pointer.NormalizedDirection).ToVec3(parent.Velocity.Z);
        }
        
        slash.RotationZ = pointer.Angle;
        slash.SpriteInstance.AnimationSpeed = 1 / AttackDuration;
        // slash.SpriteInstance.TextureScale *= _currentWeapon.Size;
        // slash.CircleInstance.Radius *= _currentWeapon.Size;
        
        _cooldownRemaining = TotalSecondsPerAttack;
    }

    public void SecondaryAction(PositionedObject parent, Vector2 positionOffset, PlayerPointer pointer)
    {
        if (!IsActionable) { return; }
    }
}
