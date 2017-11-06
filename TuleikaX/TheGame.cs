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
    public class TheGame : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        private Texture2D _sealImage;
        private Texture2D _foodImage;
        //private GifAnimation.GifAnimation _sealImage;
        //private GifAnimation.GifAnimation _foodImage;
        private SpriteFont _scoreFont;
        private SpriteFont _giantFont;

        // seal
        private const int MaxGrowth = 3;
        private const float RotationSpeed = 0.05f;
        private const float MovingSpeed = 3.00f;
        const float GrowSpeed = 0.02f;
        private const float InitialSize = 0.1f;
        private const float ChildSize = 0.05f;
        private const int ChildDistance = 20;
        private const int MaxChildren = 30;
        private float _sealSize = InitialSize;
        private Vector2 _sealPosition;
        private float _sealAngle;
        private readonly List<SealChild> _children = new List<SealChild>();

        // food
        private float _foodSize = 0.2f;
        private Vector2 _foodPosition;
        private Random _random = new Random();
        
        // game
        private bool _paused;
        private bool _pauseKeyDown;
        private int _score;
        private bool _win;

        public TheGame()
        {
            _graphics = new GraphicsDeviceManager(this);
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
            _sealPosition = new Vector2(150, 150);
            CreateRandomFood();

            base.Initialize();
        }

        private void CreateRandomFood()
        {
            _foodPosition = new Vector2(_random.Next(0, Window.ClientBounds.Width), _random.Next(0, Window.ClientBounds.Height));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Window.Title = "Тюлейка!";

            //_sealImage = Content.Load<GifAnimation.GifAnimation>("SealAnimation");
            _sealImage = Content.Load<Texture2D>("tulka");
            //_foodImage = Content.Load<GifAnimation.GifAnimation>("FoodAnimation");
            _foodImage = Content.Load<Texture2D>("fish");
            _scoreFont = Content.Load<SpriteFont>("SealFont");
            _giantFont = Content.Load<SpriteFont>("WinFont");
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

            if (_win || _paused) return;

            MoveSeal();

            if (SealEatsFood())
            {
                CreateRandomFood();
                _sealSize = InitialSize + GrowSpeed * (_score % MaxGrowth + 1);
                _score++;
                _win = _score > MaxChildren * MaxGrowth;
            }

            UpdateGifs(gameTime);

            base.Update(gameTime);
        }

        private void UpdateGifs(GameTime gameTime)
        {
            //_sealImage.Update(gameTime.ElapsedGameTime.Ticks);
            //_foodImage.Update(gameTime.ElapsedGameTime.Ticks);
        }

        private void MoveSeal()
        {
            _sealPosition += new Vector2((float)Math.Cos(_sealAngle), (float)Math.Sin(_sealAngle)) * MovingSpeed;
            _children.Insert(0, new SealChild { Vector = _sealPosition, Angle = _sealAngle });
            if (_children.Count > MaxChildren * ChildDistance)
                _children.Remove(_children.Last());
            
            if (_sealPosition.X > Window.ClientBounds.Width)
                _sealPosition.X = 0;
            else if (_sealPosition.Y > Window.ClientBounds.Height)
                _sealPosition.Y = 0;
            else if (_sealPosition.X < 0)
                _sealPosition.X = Window.ClientBounds.Width;
            else if (_sealPosition.Y < 0)
                _sealPosition.Y = Window.ClientBounds.Height;
        }

        private void ReadUserInputs()
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            CheckPause(keyboardState);

            if (_win || _paused) return;

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                _sealAngle += RotationSpeed;
            }
            else if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                _sealAngle -= RotationSpeed;
            }
        }

        private void CheckPause(KeyboardState keyboardState)
        {
            if (!_pauseKeyDown && keyboardState.IsKeyDown(Keys.Space))
            {
                _paused = !_paused;
                _pauseKeyDown = true;
            }
            if (_pauseKeyDown && keyboardState.IsKeyUp(Keys.Space))
            {
                _pauseKeyDown = false;
            }
        }

        private bool SealEatsFood()
        {
            var sealRect = RectangleFromCenter(_sealPosition, _sealImage.Width, _sealImage.Height, _sealSize);
            var foodRect = RectangleFromCenter(_foodPosition, _foodImage.Width, _foodImage.Height, _foodSize);
            return sealRect.Intersects(foodRect);
        }

        private Rectangle RectangleFromCenter(Vector2 center, float inputWidth, float inputHeight, float size)
        {
            var height = (int)(inputHeight*size);
            var width = (int)(inputWidth*size);
            var x = (int)center.X;
            var y = (int)center.Y;

            return new Rectangle(x - width / 2, y - height / 2, width, height);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            //_spriteBatch.Draw(_foodImage.GetTexture(), _foodPosition, null, Color.White, 0, new Vector2(_foodImage.Width / 2, _foodImage.Height / 2), _foodSize, SpriteEffects.None, 1);
            _spriteBatch.Draw(_foodImage, _foodPosition, null, Color.White, 0, new Vector2(_foodImage.Width / 2, _foodImage.Height / 2), _foodSize, SpriteEffects.None, 1);
            //_spriteBatch.Draw(_sealImage.GetTexture(), _sealPosition, null, Color.White, _sealAngle, new Vector2(_sealImage.Width / 2, _sealImage.Height / 2), _size, SpriteEffects.None, 1);
            _spriteBatch.Draw(_sealImage, _sealPosition, null, Color.White, _sealAngle, new Vector2(_sealImage.Width / 2, _sealImage.Height / 2), _sealSize, SpriteEffects.None, 1);

            for (var i = 0; i < (_score - 1) / MaxGrowth; i++)
            {
                if (_children.Count > ChildDistance * (i + 1))
                {
                    _spriteBatch.Draw(_sealImage, _children[ChildDistance * (i + 1)].Vector, null, Color.White, _children[ChildDistance * (i + 1)].Angle, new Vector2(_sealImage.Width / 2, _sealImage.Height / 2), ChildSize, SpriteEffects.None, 1);
                }
            }

            _spriteBatch.DrawString(_scoreFont, "Score:" + _score, new Vector2(50, 50), Color.Black);
            if (_win)
                _spriteBatch.DrawString(_giantFont, "YOU WIN!!! <3<3<3", new Vector2(60, 150), Color.DarkViolet);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
