using Microsoft.Xna.Framework;

namespace TuleikaX
{
    public class SealChild
    {
        public readonly Vector2 Position;
        public readonly float Angle;
        public const float Size = 0.06f;

        public SealChild(Vector2 position, float angle)
        {
            Position = position;
            Angle = angle;
        }

        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)(Position.X - Seal.Image.Width * Size / 4),
                    (int)(Position.Y - Seal.Image.Width * Size / 4), (int)(Seal.Image.Width * Size / 2),
                    (int)(Seal.Image.Width * Size / 2));
            }
        }
    }
}
