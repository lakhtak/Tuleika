using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TuleikaX
{
    public class Seal
    {
        public Vector2 Position;
        public float Angle;
        public static Texture2D Image;
        
        public const float Size = 0.06f;
        private const float NoseSize = 20;
        public const int InitialChildrenCount = 0;
        public const int ChildDistance = 17;
        public const float MovingSpeed = 4.50f;

        public readonly List<SealChild> Children = new List<SealChild>();

        public Seal(ContentManager content, Vector2 initialPosition)
        {
            Position = initialPosition;
            Image = content.Load<Texture2D>("tulka");
            ActiveChildrenCount = InitialChildrenCount;
        }

        public int ActiveChildrenCount;

        public IEnumerable<SealChild> ActiveChildren
        {
            get
            {
                return
                    Children.Where(
                        (t, i) => i >= ChildDistance && i/ChildDistance <= ActiveChildrenCount && i%ChildDistance == 0);
            }
        }

        public Rectangle Hitbox
        {
            get
            {
                var nosePosition = new Vector2(Position.X + Image.Width*Size/2 - NoseSize/2, Position.Y);
                var rotatedNosePosition = nosePosition.RotateAboutOrigin(Position, Angle);

                return rotatedNosePosition.RectangleFromCenter(NoseSize, NoseSize, 1);
            }
        }

        public bool EatsHimself()
        {
            return ActiveChildren.Any(child => child.Hitbox.Intersects(Hitbox));
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Position, null, Color.White, Angle,
                new Vector2(Image.Width / 2, Image.Height / 2), Size, SpriteEffects.None, 1);
        }
    }
}
