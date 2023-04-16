using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Entities;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.Math.Geometry;
using Microsoft.Xna.Framework;
using PixelDungeonJam.DataTypes;
using PixelDungeonJam.Factories;
using System.Diagnostics;
using Debugger = FlatRedBall.Debugging.Debugger;

namespace PixelDungeonJam.Entities
{
    public partial class Player
    {
        private double _lastDamageTime = -1;
        private double _attackRecoveryTimer = 0;
        private PlayerPointer _pointer;
        private SpriteDirection _lastDirection = SpriteDirection.Right;
        private WeaponData _currentWeapon;
        
        private double TimeSinceLastDamage => TimeManager.CurrentScreenSecondsSince(_lastDamageTime);
        private bool CanAttack => _attackRecoveryTimer <= 0;
        
        public int TeamIndex { get; }
        
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
            _currentWeapon = WeaponData["Sword1"];
        }

        private void CustomActivity()
        {
            UpdatePointer();
            UpdateAnimation();
            UpdateCooldowns();
            HandleInput();
        }

        private void UpdateCooldowns()
        {
            _attackRecoveryTimer -= TimeManager.SecondDifference;
        }

        private void HandleInput()
        {
            switch (InputDevice)
            {
                case { DefaultSecondaryActionInput.WasJustPressed: true }:
                {
                    if (!CanAttack) { break; }

                    var slash = SlashFactory.CreateNew();
                    slash.TeamIndex = 0;
                    slash.Position = Position.AddY(SpriteOffsetY) + Vector2.One.AtAngle(_pointer.Angle).AtLength(_currentWeapon.Range).ToVector3();
                    slash.Velocity = _currentWeapon.VelocityScaling * Velocity;
                    slash.RotationZ = _pointer.Angle;
                    slash.SpriteInstance.AnimationSpeed = 1 / _currentWeapon.AttackDuration;
                    slash.SpriteInstance.TextureScale *= _currentWeapon.Size;
                    slash.CircleInstance.Radius *= _currentWeapon.Size;
                    _attackRecoveryTimer = 1 / _currentWeapon.AttackSpeed;
                    break;
                }
            }
        }

        private void UpdateAnimation()
        {
            _lastDirection = MovementInput switch
            {
                { X: > 0 } => SpriteDirection.Right,
                { X: < 0 } => SpriteDirection.Left,
                _ => _lastDirection,
            };
            
            SpriteInstance.CurrentChainName = (MovementInput, _lastDirection) switch
            {
                ({ X: > 0 }, _) => "RunRight",
                ({ X: < 0 }, _) => "RunLeft",
                (_, SpriteDirection.Right) => "IdleRight",
                (_, SpriteDirection.Left) => "IdleLeft",
                _ => throw new UnreachableException((MovementInput, _lastDirection).ToString()),
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
        
        // ******** begin private types ********
        
        private enum SpriteDirection { Right, Left }
        
        // ******** end private types ********
    }
}
