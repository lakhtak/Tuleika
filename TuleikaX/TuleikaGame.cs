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
        protected static float MovingSpeed = 4.50f;
        protected static int ChildDistance = 17;
        protected static int MaxChildren = 29;
        protected static Vector2 InitialPosition = new Vector2(150, 150);
        protected Seal Seal;
        
        // food
        protected Food Food;

        // game
        protected static string WindowTitle = "Тюлейка!";
        protected bool Paused;
        protected bool PauseKeyDown;
        protected int Score;
        protected bool Win;
        protected bool Lose;

        protected TuleikaGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Seal = new Seal(InitialPosition);
            Food = new Food(Window);
            Food.CreateRandomFood();

            base.Initialize();
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

            Seal.Image = Content.Load<Texture2D>("tulka");
            Food.Image = Content.Load<Texture2D>("fish");

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

            MoveSeal();

            if (SealEatsHimself())
            {
                Lose = true;
            }

            if (SealEatsFood())
            {
                Food.CreateRandomFood();
                Score++;
                Win = Score > MaxChildren;
            }

            UpdateGifs(gameTime);

            base.Update(gameTime);
        }

        protected void UpdateGifs(GameTime gameTime)
        {
            //_sealImage.Update(gameTime.ElapsedGameTime.Ticks);
            //_foodImage.Update(gameTime.ElapsedGameTime.Ticks);
        }

        protected void MoveSeal()
        {
            Seal.SealPosition += new Vector2((float)Math.Cos(Seal.SealAngle), (float)Math.Sin(Seal.SealAngle)) * MovingSpeed;
            Seal.SealChildren.Insert(0, new SealChild(Seal.SealPosition, Seal.SealAngle));
            if (Seal.SealChildren.Count > MaxChildren * ChildDistance)
                Seal.SealChildren.Remove(Seal.SealChildren.Last());

            if (Seal.SealPosition.X > Window.ClientBounds.Width)
                Seal.SealPosition.X = 0;
            else if (Seal.SealPosition.Y > Window.ClientBounds.Height)
                Seal.SealPosition.Y = 0;
            else if (Seal.SealPosition.X < 0)
                Seal.SealPosition.X = Window.ClientBounds.Width;
            else if (Seal.SealPosition.Y < 0)
                Seal.SealPosition.Y = Window.ClientBounds.Height;
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

        protected bool SealEatsFood()
        {
            var sealRect = Seal.Hitbox;
            var foodRect = Food.Hitbox;
            return sealRect.Intersects(foodRect);
        }

        protected bool SealEatsHimself()
        {
            var sealHitbox = Seal.Hitbox;
            for (var i = 0; i < Score; i++)
            {
                if (Seal.SealChildren.Count <= ChildDistance * (i + 1)) continue;

                var childHitbox = Seal.SealChildren[ChildDistance * (i + 1)].Hitbox;
                if (childHitbox.Intersects(sealHitbox))
                    return true;
            }
            return false;
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();
            SpriteBatch.Draw(Food.Image, Food.Position, null, Color.White, 0,
                new Vector2(Food.Image.Width/2, Food.Image.Height/2), Food.Size, SpriteEffects.None, 1);
            SpriteBatch.Draw(Seal.Image, Seal.SealPosition, null, Color.White, Seal.SealAngle,
                new Vector2(Seal.Image.Width / 2, Seal.Image.Height / 2), Seal.Size, SpriteEffects.None, 1);
#if DEBUG
            SpriteBatch.DrawRectangle(LineTexture, Seal.Hitbox);
            SpriteBatch.DrawRectangle(LineTexture, Food.Hitbox);
#endif
            for (var i = 0; i < Score; i++)
            {
                if (Seal.SealChildren.Count > ChildDistance * (i + 1))
                {
                    SpriteBatch.Draw(Seal.Image, Seal.SealChildren[ChildDistance * (i + 1)].Position, null, Color.White,
                        Seal.SealChildren[ChildDistance * (i + 1)].Angle, new Vector2(Seal.Image.Width / 2, Seal.Image.Height / 2),
                        SealChild.Size, SpriteEffects.None, 1);
#if DEBUG
                    SpriteBatch.DrawRectangle(LineTexture, Seal.SealChildren[ChildDistance * (i + 1)].Hitbox);
#endif
                }
            }

            SpriteBatch.DrawString(ScoreFont, "Score:" + Score, new Vector2(50, 50), Color.Black);
            if (Win)
                SpriteBatch.DrawString(GiantFont, "YOU WIN!!! <3<3<3", new Vector2(60, 150), Color.DarkViolet);

            if (Lose)
                SpriteBatch.DrawString(GiantFont, "YOU LOSE :(:(:(", new Vector2(60, 150), Color.Black);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
