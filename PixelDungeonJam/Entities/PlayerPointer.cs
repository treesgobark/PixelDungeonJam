using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Debugging;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.Math.Geometry;
using Microsoft.Xna.Framework;

namespace PixelDungeonJam.Entities
{
    public partial class PlayerPointer
    {
        public Vector2 NormalizedDirection => Vector2.UnitX.AtAngle(Angle);
        
        /// <summary>
        /// Initialization logic which is executed only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
        private void CustomInitialize()
        {
        }

        private void CustomActivity()
        {
            SetPositionAndRotation();
            // Debugger.Write($"Pointer rotation: {RotationZ}");
        }

        private void CustomDestroy()
        {
        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {
        }

        private void SetPositionAndRotation()
        {
            RelativePosition = CalculatePosition().AddY(OffsetY);
            RelativeRotationZ = CalculateRotationZ();
        }

        private Vector3 CalculatePosition()
        {
            Vector2 vec2 = Vector2.One.AtAngle(Angle).AtLength(Radius);
            return new Vector3(vec2, 0);
        }

        private float CalculateRotationZ()
        {
            return Angle - MathF.PI / 2f;
        }
    }
}
