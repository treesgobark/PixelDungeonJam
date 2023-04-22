using ANLG.Utilities.FlatRedBall.Constants;
using ANLG.Utilities.FlatRedBall.Controllers;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Entities;
using PixelDungeonJam.Factories;
using PixelDungeonJam.Models.Design;
using PixelDungeonJam.Utilities.PlayerControllers;
using Debugger = FlatRedBall.Debugging.Debugger;

namespace PixelDungeonJam.Entities
{
    public partial class Player : IHasAnimationControllers<Player, PlayerController>
    {
        private double _lastDamageTime = -1;
        private PlayerPointer _pointer;
        private string _currentWeaponId;
        private IWeaponModel _currentWeapon;
        
        private double TimeSinceLastDamage => TimeManager.CurrentScreenSecondsSince(_lastDamageTime);
        public int TeamIndex => 0;
        // ReSharper disable once ConvertToAutoPropertyWhenPossible
        public Sprite ControllerSprite => SpriteInstance;

        public FourDirections LastDirection { get; set; } = FourDirections.Right;
        public ControllerCollection<Player, PlayerController> Controllers { get; private set; }
        
        /// <summary>
        /// Initialization logic which is executed only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
        private void CustomInitialize()
        {
            ReactToDamageReceived += HandleDamageReceived;
            
            _pointer = PlayerPointerFactory.CreateNew();
            _pointer.AttachTo(this);
            _currentWeaponId = "sword_light_wood";

            InitializeControllers();
        }

        private void InitializeControllers()
        {
            Controllers = new ControllerCollection<Player, PlayerController>();
            Controllers.Add(new Idle(this));
            Controllers.InitializeStartingController<Idle>();
        }

        private void CustomActivity()
        {
            UpdatePointer();
            UpdateDirection();
            Controllers.DoCurrentControllerActivity();
        }

        private void HandleInput()
        {
            // switch (InputDevice)
            // {
            //     case { DefaultSecondaryActionInput.IsDown: true }:
            //     {
            //         if (!CanAttack) { break; }
            //
            //         var slash = SlashFactory.CreateNew();
            //         slash.TeamIndex = 0;
            //         
            //         slash.Position = Position.AddY(SpriteOffsetY)
            //                                  .Add(Vector2.UnitX.AtAngle(_pointer.Angle)
            //                                                  .AtLength(_currentWeapon.Range));
            //         
            //         float dot = Vector2.Dot(Velocity.XY(), _pointer.NormalizedDirection);
            //         if (dot > 0)
            //         {
            //             slash.Velocity = _currentWeapon.VelocityScaling * Velocity.XY().ProjectOnto(_pointer.NormalizedDirection).ToVec3(Velocity.Z);
            //         }
            //         
            //         slash.RotationZ = _pointer.Angle;
            //         slash.SpriteInstance.AnimationSpeed = 1 / _currentWeapon.AttackDuration;
            //         slash.SpriteInstance.TextureScale *= _currentWeapon.Size;
            //         slash.CircleInstance.Radius *= _currentWeapon.Size;
            //         _attackRecoveryTimer = 1 / _currentWeapon.AttackSpeed;
            //         break;
            //     }
            // }
        }

        private void UpdateDirection()
        {
            LastDirection = MovementInput switch
            {
                { X: > 0 } => FourDirections.Right,
                { X: < 0 } => FourDirections.Left,
                _ => LastDirection,
            };
        }

        private void CustomDestroy()
        {
            _pointer.Destroy();
        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {
        }

        private void UpdatePointer()
        {
            StringBuilder sb = new();
            foreach (Xbox360GamePad xbox360GamePad in InputManager.Xbox360GamePads)
            {
                sb.AppendLine(xbox360GamePad.ToString());
            }
            Debugger.Write(sb.ToString());
            float? inputAngle = MovementInput.GetAngle();
            if (inputAngle is not null)
            {
                // _pointer.SpriteInstance.Visible = true;
                _pointer.Angle = inputAngle.Value;
            }
            // else
            // {
            //     _pointer.SpriteInstance.Visible = false;
            // }
        }
        
        private async void HandleDamageReceived(decimal arg1, IDamageArea arg2)
        {
            _lastDamageTime = TimeManager.CurrentScreenTime;
            TakeDamage.Play();
            SpriteInstance.Green = 1 - FlashStrength;
            SpriteInstance.Blue = 1 - FlashStrength;
            await TimeManager.DelaySeconds(arg2.SecondsBetweenDamage - 1f / 60f);
            SpriteInstance.Red = 1;
            SpriteInstance.Green = 1;
            SpriteInstance.Blue = 1;
            SpriteInstance.Alpha = 1;
        }
    }
}
