using FlatRedBall.Graphics.Animation;
using PixelDungeonJam.Entities;

namespace PixelDungeonJam.Utilities.PlayerControllers;

public class Idle : PlayerController
{
    public Idle(Player parent) : base(parent) { }

    protected override string ChainName => "Idle";
}
