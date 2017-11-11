using System;
using System.Collections.Generic;
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

        protected Texture2D SealImage;
        protected Texture2D FoodImage;
        protected Texture2D LineTexture;
        //protected GifAnimation.GifAnimation SealImage;
        //protected GifAnimation.GifAnimation FoodImage;
        protected SpriteFont ScoreFont;
        protected SpriteFont GiantFont;

        // seal
        protected static int MaxGrowth = 1;
        protected static float MovingSpeed = 4.50f;
        protected static float GrowSpeed = 0.02f;
        protected static float InitialSize = 0.06f;
        protected static float ChildSize = 0.06f;
        protected static int ChildDistance = 17;
        protected static int MaxChildren = 30;
        protected float SealSize = InitialSize;
        protected Vector2 SealPosition;
        protected float SealAngle;
        protected readonly List<SealChild> SealChildren = new List<SealChild>();

        // food
        protected float FoodSize = 0.1f;
        protected Vector2 FoodPosition;
        protected Random Random = new Random();

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
            SealPosition = new Vector2(150, 150);
            CreateRandomFood();

            base.Initialize();
        }

        protected void CreateRandomFood()
        {
            FoodPosition = new Vector2(Random.Next(0, Window.ClientBounds.Width), Random.Next(0, Window.ClientBounds.Height));
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

            //_sealImage = Content.Load<GifAnimation.GifAnimation>("SealAnimation");
            SealImage = Content.Load<Texture2D>("tulka");
            //_foodImage = Content.Load<GifAnimation.GifAnimation>("FoodAnimation");
            FoodImage = Content.Load<Texture2D>("fish");

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
                CreateRandomFood();
                SealSize = InitialSize + GrowSpeed * (Score % MaxGrowth + 1);
                Score++;
                Win = Score > MaxChildren * MaxGrowth;
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
            SealPosition += new Vector2((float)Math.Cos(SealAngle), (float)Math.Sin(SealAngle)) * MovingSpeed;
            SealChildren.Insert(0, new SealChild(SealPosition, SealAngle, ChildSize));
            if (SealChildren.Count > MaxChildren * ChildDistance)
                SealChildren.Remove(SealChildren.Last());

            if (SealPosition.X > Window.ClientBounds.Width)
                SealPosition.X = 0;
            else if (SealPosition.Y > Window.ClientBounds.Height)
                SealPosition.Y = 0;
            else if (SealPosition.X < 0)
                SealPosition.X = Window.ClientBounds.Width;
            else if (SealPosition.Y < 0)
                SealPosition.Y = Window.ClientBounds.Height;
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
            var sealRect = RectangleFromCenter(SealPosition, SealImage.Width, SealImage.Height, SealSize);
            var foodRect = RectangleFromCenter(FoodPosition, FoodImage.Width, FoodImage.Height, FoodSize);
            return sealRect.Intersects(foodRect);
        }

        protected static Rectangle RectangleFromCenter(Vector2 center, float inputWidth, float inputHeight, float size)
        {
            var height = (int)(inputHeight * size);
            var width = (int)(inputWidth * size);
            var x = (int)center.X;
            var y = (int)center.Y;

            return new Rectangle(x - width / 2, y - height / 2, width, height);
        }

        protected bool SealEatsHimself()
        {
            var sealHitbox = GetSealHitbox();
            for (var i = 0; i < (Score - 1) / MaxGrowth; i++)
            {
                if (SealChildren.Count <= ChildDistance * (i + 1)) continue;

                var childHitbox = GetSealChildHitbox(SealChildren[ChildDistance * (i + 1)]);
                if (childHitbox.Intersects(sealHitbox))
                    return true;
            }
            return false;
        }

        protected Rectangle GetSealHitbox()
        {
            var halfWidth = (int)(SealImage.Width * SealSize / 2);

            if (SealAngle.Equals(Direction.Up))
                return new Rectangle((int)SealPosition.X, (int)(SealPosition.Y - halfWidth), 1, halfWidth);
            if (SealAngle.Equals(Direction.Down))
                return new Rectangle((int)SealPosition.X, (int)SealPosition.Y, 1, halfWidth);
            if (SealAngle.Equals(Direction.Left))
                return new Rectangle((int)(SealPosition.X - halfWidth), (int)SealPosition.Y, halfWidth, 1);
            if (SealAngle.Equals(Direction.Right))
                return new Rectangle((int)SealPosition.X, (int)SealPosition.Y, halfWidth, 1);

            return new Rectangle();
        }

        protected Rectangle GetSealChildHitbox(SealChild seal)
        {
            var width = (int)(SealImage.Width * seal.Size);
            var halfWidth = width / 2;

            if (seal.Angle.Equals(Direction.Up) || seal.Angle.Equals(Direction.Down))
                return new Rectangle((int)seal.Position.X, (int)(seal.Position.Y - halfWidth), 1, width);
            if (seal.Angle.Equals(Direction.Left) || seal.Angle.Equals(Direction.Right))
                return new Rectangle((int)(seal.Position.X - halfWidth), (int)seal.Position.Y, width, 1);

            return new Rectangle();
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();
            //SpriteBatch.Draw(_foodImage.GetTexture(), _foodPosition, null, Color.White, 0, new Vector2(_foodImage.Width / 2, _foodImage.Height / 2), _foodSize, SpriteEffects.None, 1);
            SpriteBatch.Draw(FoodImage, FoodPosition, null, Color.White, 0,
                new Vector2(FoodImage.Width/2, FoodImage.Height/2), FoodSize, SpriteEffects.None, 1);
            //SpriteBatch.Draw(_sealImage.GetTexture(), _sealPosition, null, Color.White, _sealAngle, new Vector2(_sealImage.Width / 2, _sealImage.Height / 2), _size, SpriteEffects.None, 1);
            SpriteBatch.Draw(SealImage, SealPosition, null, Color.White, SealAngle,
                new Vector2(SealImage.Width/2, SealImage.Height/2), SealSize, SpriteEffects.None, 1);
#if DEBUG
            SpriteBatch.DrawLine(LineTexture, GetSealHitbox());
#endif
            for (var i = 0; i < (Score - 1) / MaxGrowth; i++)
            {
                if (SealChildren.Count > ChildDistance * (i + 1))
                {
                    SpriteBatch.Draw(SealImage, SealChildren[ChildDistance*(i + 1)].Position, null, Color.White,
                        SealChildren[ChildDistance*(i + 1)].Angle, new Vector2(SealImage.Width/2, SealImage.Height/2),
                        SealChildren[ChildDistance*(i + 1)].Size, SpriteEffects.None, 1);
#if DEBUG
                    SpriteBatch.DrawLine(LineTexture, GetSealChildHitbox(SealChildren[ChildDistance * (i + 1)]));
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
