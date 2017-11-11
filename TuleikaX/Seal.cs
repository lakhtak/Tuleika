using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TuleikaX
{
    public class Seal
    {
        public Vector2 SealPosition;
        public float SealAngle;
        public static Texture2D Image;

        public const float Size = 0.06f;
        private const float NoseSize = 20;
        
        public readonly List<SealChild> SealChildren = new List<SealChild>();

        public Seal(Vector2 initialPosition)
        {
            SealPosition = initialPosition;
        }

        public Rectangle Hitbox
        {
            get
            {
                var nosePosition = new Vector2(SealPosition.X + Image.Width*Size/2 - NoseSize/2, SealPosition.Y);
                var rotatedNosePosition = nosePosition.RotateAboutOrigin(SealPosition, SealAngle);

                return rotatedNosePosition.RectangleFromCenter(NoseSize, NoseSize, 1);
            }
        }

    }
}
