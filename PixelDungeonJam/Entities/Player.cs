using ANLG.Utilities.FlatRedBall.Constants;
using ANLG.Utilities.FlatRedBall.Controllers;
using ANLG.Utilities.FlatRedBall.NonStaticUtilities;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Entities;
using PixelDungeonJam.Factories;
using PixelDungeonJam.Systems.Weapons;
using PixelDungeonJam.Utilities;
using PixelDungeonJam.Utilities.PlayerControllers;
using System.Collections.Generic;
using Debugger = FlatRedBall.Debugging.Debugger;

namespace PixelDungeonJam.Entities
{
    public partial class Player : IHasAnimationControllers<Player, PlayerController>
    {
        public int TeamIndex => 0;
        // ReSharper disable once ConvertToAutoPropertyWhenPossible
        public Sprite ControllerSprite => SpriteInstance;
        public WeaponCollection Weapons { get; private set; }
        public IWeapon CurrentWeapon => Weapons.CurrentItem;
        public PlayerPointer Pointer;

        public FourDirections LastDirection { get; set; }
        public ControllerCollection<Player, PlayerController> Controllers { get; private set; }
        
        /// <summary>
        /// Initialization logic which is executed only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
        private void CustomInitialize()
        {
            ReactToDamageReceived += HandleDamageReceived;
            
            Pointer = PlayerPointerFactory.CreateNew();
            Pointer.AttachTo(this);
            // CurrentWeapon = new Unarmed();
            Weapons = new WeaponCollection();
            Weapons.LoadWeapon("sword_light_wood");
            Weapons.LoadWeapon("sword_heavy_wood");
            LastDirection = FourDirections.Right;;

            InitializeControllers();
        }

        private void InitializeControllers()
        {
            Controllers = new ControllerCollection<Player, PlayerController>();
            Controllers.Add(new Idle(this));
            Controllers.Add(new Run(this));
            Controllers.InitializeStartingController<Idle>();
        }

        private void CustomActivity()
        {
            UpdatePointer();
            UpdateDirection();
            UpdateWeapon();
            Controllers.DoCurrentControllerActivity();
        }

        private void CustomDestroy()
        {
            Pointer.Destroy();
        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {
        }

        public void CycleWeaponForward()
        {
            if (CurrentWeapon.IsActionable)
            {
                Weapons.CycleToNextItem();
                Debugger.Write($"Switched to {CurrentWeapon.Name}");
            }
        }

        public void CycleWeaponBackward()
        {
            if (CurrentWeapon.IsActionable)
            {
                Weapons.CycleToPreviousItem();
                Debugger.Write($"Switched to {CurrentWeapon.Name}");
            }
        }

        private void UpdateWeapon()
        {
            CurrentWeapon.Activity();
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

        private void UpdatePointer()
        {
            StringBuilder sb = new();
            foreach (Xbox360GamePad xbox360GamePad in InputManager.Xbox360GamePads)
            {
                sb.AppendLine(xbox360GamePad.ToString());
            }
            // Debugger.Write(sb.ToString());
            float? inputAngle = MovementInput.GetAngle();
            if (inputAngle is not null)
            {
                // Pointer.SpriteInstance.Visible = true;
                Pointer.Angle = inputAngle.Value;
            }
            // else
            // {
            //     Pointer.SpriteInstance.Visible = false;
            // }
        }
        
        private async void HandleDamageReceived(decimal arg1, IDamageArea arg2)
        {
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
