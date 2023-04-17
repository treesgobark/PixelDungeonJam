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
using Microsoft.Xna.Framework.Audio;

namespace PixelDungeonJam.Entities
{
    public partial class Enemy
    {
        protected double LastDamageTime { get; set; }
        protected abstract SoundEffect TakeDamage { get; }
        
        /// <summary>
        /// Initialization logic which is executed only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
        private void CustomInitialize()
        {
            ReactToDamageReceived += HandleDamageReceived;
        }

        private void CustomActivity()
        {


        }

        private void CustomDestroy()
        {


        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
        
        private async void HandleDamageReceived(decimal arg1, IDamageArea arg2)
        {
            LastDamageTime = TimeManager.CurrentScreenTime;
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
