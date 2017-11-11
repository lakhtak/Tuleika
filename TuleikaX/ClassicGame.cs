using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace TuleikaX
{
    public class ClassicGame : TuleikaGame
    {
        static ClassicGame()
        {
            WindowTitle = "Тюлейка классик!";
        }

        protected override void ReadUserInputs()
        {
            var keyboardState = Keyboard.GetState();

            //if (keyboardState.IsKeyDown(Keys.Escape))
            //    Exit();

            CheckPause(keyboardState);

            if (Win || Lose || Paused) return;

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                if (SealAngle.Equals(Direction.Up) || SealAngle.Equals(Direction.Down))
                    SealAngle = Direction.Right;
            }
            else if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                if (SealAngle.Equals(Direction.Up) || SealAngle.Equals(Direction.Down))
                    SealAngle = Direction.Left;
            }
            else if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                if (SealAngle.Equals(Direction.Left) || SealAngle.Equals(Direction.Right))
                    SealAngle = Direction.Up;
            }
            else if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                if (SealAngle.Equals(Direction.Left) || SealAngle.Equals(Direction.Right))
                    SealAngle = Direction.Down;
            }
        }
    }
}
