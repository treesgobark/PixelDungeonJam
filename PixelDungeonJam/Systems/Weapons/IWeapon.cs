using FlatRedBall;
using Microsoft.Xna.Framework;
using PixelDungeonJam.Entities;

namespace PixelDungeonJam.Systems.Weapons;

public interface IWeapon
{
    string Name { get; }
    string Description { get; }
    bool IsActionable { get; }

    void Activity();
    void PrimaryAction(PositionedObject parent, Vector2 positionOffset, PlayerPointer pointer);
    void SecondaryAction(PositionedObject parent, Vector2 positionOffset, PlayerPointer pointer);
}
