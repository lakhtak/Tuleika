using Microsoft.Xna.Framework.Input;

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
                if (Seal.SealAngle.Equals(Direction.Up) || Seal.SealAngle.Equals(Direction.Down))
                    Seal.SealAngle = Direction.Right;
            }
            else if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                if (Seal.SealAngle.Equals(Direction.Up) || Seal.SealAngle.Equals(Direction.Down))
                    Seal.SealAngle = Direction.Left;
            }
            else if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                if (Seal.SealAngle.Equals(Direction.Left) || Seal.SealAngle.Equals(Direction.Right))
                    Seal.SealAngle = Direction.Up;
            }
            else if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                if (Seal.SealAngle.Equals(Direction.Left) || Seal.SealAngle.Equals(Direction.Right))
                    Seal.SealAngle = Direction.Down;
            }
        }
    }
}
