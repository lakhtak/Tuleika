using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace TuleikaX
{
    public class SuperGame : TuleikaGame
    {
        protected static float RotationSpeed = 0.1f;            

        static SuperGame()
        {
            WindowTitle = "Тюлейка супер!";

            // seal
            MaxGrowth = 3;
            ChildDistance = 15;
            MaxChildren = 10;
        }

        protected override void ReadUserInputs()
        {
            var keyboardState = Keyboard.GetState();

            //if (keyboardState.IsKeyDown(Keys.Escape))
            //    Exit();

            CheckPause(keyboardState);

            if (Win || Paused) return;

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                SealAngle += RotationSpeed;
            }
            else if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                SealAngle -= RotationSpeed;
            }
        }
    }
}
