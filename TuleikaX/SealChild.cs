using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Seal.Image, Position, null, Color.White,
                Angle, new Vector2(Seal.Image.Width/2, Seal.Image.Height/2),
                Size, SpriteEffects.None, 1);
        }
    }
}
