using ANLG.Utilities.FlatRedBall.Constants;
using ANLG.Utilities.FlatRedBall.Controllers;
using FlatRedBall.Graphics.Animation;
using Microsoft.Xna.Framework;
using PixelDungeonJam.Entities;

namespace PixelDungeonJam.Utilities.PlayerControllers;

public abstract class PlayerController : AnimationController<Player, PlayerController>
{
    protected PlayerController(Player parent) : base(parent) { }

    protected override PlayerController DefaultStateAfterTimeout => Get<Idle>();
    protected override AnimationChainList ChainList => Player.KnightAnimations;
    protected override FourDirections AnimationDirection => Parent.LastDirection;
    
    public override void CustomActivity()
    {
        base.CustomActivity();
        HandleInput();
    }

    public virtual void HandleInput()
    {
        if (Parent.InputDevice.DefaultSecondaryActionInput is { IsDown: true })
        {
            Parent.CurrentWeapon.PrimaryAction(Parent, new Vector2(0, Parent.SpriteOffsetY), Parent.Pointer);
        }

        if (Parent.InputDevice.DefaultPauseInput is { WasJustPressed: true })
        {
            Parent.CycleWeaponForward();
        }

        if (Parent.InputDevice.DefaultBackInput is { WasJustPressed: true })
        {
            Parent.CycleWeaponBackward();
        }
    }
}
