using Microsoft.Xna.Framework.Input;

namespace TuleikaX
{
    public class SuperGame : TuleikaGame
    {
        protected static float RotationSpeed = 0.1f;            

        static SuperGame()
        {
            WindowTitle = "Тюлейка супер!";
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
                Seal.SealAngle += RotationSpeed;
            }
            else if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                Seal.SealAngle -= RotationSpeed;
            }
        }
    }
}
