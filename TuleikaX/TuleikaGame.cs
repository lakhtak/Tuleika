using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace TuleikaX
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public abstract class TuleikaGame : Game
    {
        protected GraphicsDeviceManager Graphics;
        protected SpriteBatch SpriteBatch;

        protected Texture2D LineTexture;
        protected SpriteFont ScoreFont;
        protected SpriteFont GiantFont;

        // seal
        protected static Vector2 InitialPosition = new Vector2(150, 150);
        protected const int MaxChildren = 30;
        protected Seal Seal;
        
        // food
        protected Food Food;

        // game
        protected static string WindowTitle = "Тюлейка!";
        protected bool Paused;
        protected bool PauseKeyDown;
        protected bool Win;
        protected bool Lose;

        protected TuleikaGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Window.Title = WindowTitle;

            Seal = new Seal(Content, InitialPosition);
            Food = new Food(Window, Content);
            Food.CreateRandomFood();

            ScoreFont = Content.Load<SpriteFont>("SealFont");
            GiantFont = Content.Load<SpriteFont>("WinFont");

            LineTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            LineTexture.SetData(new[] { Color.Red });
        }
        
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            ReadUserInputs();

            if (Win || Lose || Paused) return;

            MoveSeal(Seal);

            if (Seal.EatsHimself())
            {
                Lose = true;
            }

            SealEatsFood(Seal);

            UpdateGifs(gameTime);
        }

        protected void UpdateGifs(GameTime gameTime)
        {
            //_sealImage.Update(gameTime.ElapsedGameTime.Ticks);
            //_foodImage.Update(gameTime.ElapsedGameTime.Ticks);
        }

        protected void MoveSeal(Seal seal)
        {
            seal.Position += new Vector2((float)Math.Cos(seal.Angle), (float)Math.Sin(seal.Angle)) * Seal.MovingSpeed;
            seal.Children.Insert(0, new SealChild(seal.Position, seal.Angle));
            if (seal.Children.Count > MaxChildren * Seal.ChildDistance)
                seal.Children.Remove(seal.Children.Last());

            if (seal.Position.X > Window.ClientBounds.Width)
                seal.Position.X = 0;
            else if (seal.Position.Y > Window.ClientBounds.Height)
                seal.Position.Y = 0;
            else if (seal.Position.X < 0)
                seal.Position.X = Window.ClientBounds.Width;
            else if (seal.Position.Y < 0)
                seal.Position.Y = Window.ClientBounds.Height;
        }

        protected abstract void ReadUserInputs();

        protected void CheckPause(KeyboardState keyboardState)
        {
            if (!PauseKeyDown && keyboardState.IsKeyDown(Keys.Space))
            {
                Paused = !Paused;
                PauseKeyDown = true;
            }
            if (PauseKeyDown && keyboardState.IsKeyUp(Keys.Space))
            {
                PauseKeyDown = false;
            }
        }

        protected void SealEatsFood(Seal seal)
        {
            if (!seal.Hitbox.Intersects(Food.Hitbox)) return;

            seal.ActiveChildrenCount++;
            Food.CreateRandomFood();
            Win = seal.ActiveChildrenCount > MaxChildren;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();
            Food.Draw(SpriteBatch);
            Seal.Draw(SpriteBatch);
#if DEBUG
            SpriteBatch.DrawRectangle(LineTexture, Seal.Hitbox);
            SpriteBatch.DrawRectangle(LineTexture, Food.Hitbox);
#endif
            foreach (var child in Seal.ActiveChildren)
            {
                child.Draw(SpriteBatch);                
#if DEBUG
                SpriteBatch.DrawRectangle(LineTexture, child.Hitbox);
#endif
            }

            SpriteBatch.DrawString(ScoreFont, "Score:" + (Seal.ActiveChildrenCount - Seal.InitialChildrenCount), new Vector2(50, 50), Color.Black);
            if (Win)
                SpriteBatch.DrawString(GiantFont, "YOU WIN!!! <3<3<3", new Vector2(60, 150), Color.DarkViolet);

            if (Lose)
                SpriteBatch.DrawString(GiantFont, "YOU LOSE :(:(:(", new Vector2(60, 150), Color.Black);

            SpriteBatch.End();
        }
    }
}
