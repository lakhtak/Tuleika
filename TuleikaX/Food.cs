using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TuleikaX
{
    public class Food
    {
        public Vector2 Position;
        public static Texture2D Image;
        public const float Size = 0.1f;

        private readonly Random _random = new Random();
        private readonly GameWindow _gameWindow ;

        public Food(GameWindow gameWindow)
        {
            _gameWindow = gameWindow;
        }

        public void CreateRandomFood()
        {
            Position = new Vector2(_random.Next(0, _gameWindow.ClientBounds.Width), _random.Next(0, _gameWindow.ClientBounds.Height));
        }

        public Rectangle Hitbox
        {
            get
            {
                return Position.RectangleFromCenter(Image.Width * 0.8f, Image.Height * 0.9f, Size);
            }
        }
    }
}
