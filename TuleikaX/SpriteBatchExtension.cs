using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TuleikaX
{
    static class SpriteBatchExtension
    {
        public static void DrawLine(this SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 end)
        {
            var edge = end - start;
            var angle = (float)Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(texture,
                new Rectangle(
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(),
                    1),
                null,
                Color.Red,
                angle,
                new Vector2(0, 0),
                SpriteEffects.None,
                0);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Texture2D texture, Rectangle rectangle)
        {
            spriteBatch.Draw(texture, rectangle, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}
