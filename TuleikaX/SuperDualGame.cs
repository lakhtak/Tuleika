using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TuleikaX
{
    public class SuperDualGame : TuleikaGame
    {
        protected static float RotationSpeed = 0.1f;
        public Seal Seal2;
        public Vector2 InitialPosition2 = new Vector2(400, 400);

        static SuperDualGame()
        {
            WindowTitle = "Тюлейка супер вдвоем!";
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            Seal2 = new Seal(Content, InitialPosition2);
        }

        protected override void Update(GameTime gameTime)
        {
            ReadUserInputs();

            if (Win || Lose || Paused) return;

            MoveSeal(Seal);
            MoveSeal(Seal2);

            SealEatsFood(Seal);
            SealEatsFood(Seal2);

            UpdateGifs(gameTime);
        }

        protected override void ReadUserInputs()
        {
            var keyboardState = Keyboard.GetState();

            //if (keyboardState.IsKeyDown(Keys.Escape))
            //    Exit();

            CheckPause(keyboardState);

            if (Win || Paused) return;

            // player 1
            if (keyboardState.IsKeyDown(Keys.D))
            {
                Seal.Angle += RotationSpeed;
            }
            else if (keyboardState.IsKeyDown(Keys.A))
            {
                Seal.Angle -= RotationSpeed;
            }

            // player 2
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Seal2.Angle += RotationSpeed;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                Seal2.Angle -= RotationSpeed;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();
            Food.Draw(SpriteBatch);
            Seal.Draw(SpriteBatch);
            Seal2.Draw(SpriteBatch);
#if DEBUG
            SpriteBatch.DrawRectangle(LineTexture, Seal.Hitbox);
            SpriteBatch.DrawRectangle(LineTexture, Seal2.Hitbox);
            SpriteBatch.DrawRectangle(LineTexture, Food.Hitbox);
#endif
            foreach (var child in Seal.ActiveChildren)
            {
                child.Draw(SpriteBatch);
#if DEBUG
                SpriteBatch.DrawRectangle(LineTexture, child.Hitbox);
#endif
            }

            foreach (var child in Seal2.ActiveChildren)
            {
                child.Draw(SpriteBatch);
#if DEBUG
                SpriteBatch.DrawRectangle(LineTexture, child.Hitbox);
#endif
            }

            SpriteBatch.DrawString(ScoreFont, "Seal 1:" + (Seal.ActiveChildrenCount - Seal.InitialChildrenCount), new Vector2(50, 50), Color.DarkRed);
            SpriteBatch.DrawString(ScoreFont, "Seal 2:" + (Seal2.ActiveChildrenCount - Seal.InitialChildrenCount), new Vector2(300, 50), Color.DarkGreen);
            if (Win)
                SpriteBatch.DrawString(GiantFont, string.Format("SEAL {0} WIN!!! <3<3<3", Seal.ActiveChildrenCount >= MaxChildren ? "1" : "2"), new Vector2(60, 150), Color.DarkViolet);

            if (Lose)
                SpriteBatch.DrawString(GiantFont, "YOU LOSE :(:(:(", new Vector2(60, 150), Color.Black);

            SpriteBatch.End();
        }
    }
}
