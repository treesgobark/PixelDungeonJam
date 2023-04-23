using FlatRedBall;
using Microsoft.Xna.Framework;
using PixelDungeonJam.Entities;

namespace PixelDungeonJam.Systems.Weapons;

public class Unarmed : IWeapon
{
    public const string WeaponId = "unarmed";

    public string Id => WeaponId;
    public string Name => "Unarmed";
    public string Description => "Imagine if I had a real weapon!";
    public bool IsActionable => false;

    public void Activity() { }
    public void PrimaryAction(PositionedObject parent, Vector2 positionOffset, PlayerPointer pointer) { }
    public void SecondaryAction(PositionedObject parent, Vector2 positionOffset, PlayerPointer pointer) { }
}
