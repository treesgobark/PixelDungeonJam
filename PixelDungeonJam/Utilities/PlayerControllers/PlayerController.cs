using ANLG.Utilities.FlatRedBall.Constants;
using ANLG.Utilities.FlatRedBall.Controllers;
using FlatRedBall.Graphics.Animation;
using PixelDungeonJam.Entities;

namespace PixelDungeonJam.Utilities.PlayerControllers;

public abstract class PlayerController : AnimationController<Player, PlayerController>
{
    protected PlayerController(Player parent) : base(parent) { }

    protected override PlayerController DefaultStateAfterTimeout => Get<Idle>();
    protected override AnimationChainList ChainList => Player.KnightAnimations;
    protected override FourDirections AnimationDirection => Parent.LastDirection;
}
