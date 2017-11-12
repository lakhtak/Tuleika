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
                if (Seal.Angle.Equals(Direction.Up) || Seal.Angle.Equals(Direction.Down))
                    Seal.Angle = Direction.Right;
            }
            else if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                if (Seal.Angle.Equals(Direction.Up) || Seal.Angle.Equals(Direction.Down))
                    Seal.Angle = Direction.Left;
            }
            else if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                if (Seal.Angle.Equals(Direction.Left) || Seal.Angle.Equals(Direction.Right))
                    Seal.Angle = Direction.Up;
            }
            else if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                if (Seal.Angle.Equals(Direction.Left) || Seal.Angle.Equals(Direction.Right))
                    Seal.Angle = Direction.Down;
            }
        }
    }
}
