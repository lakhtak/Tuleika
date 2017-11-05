
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Brushes = System.Windows.Media.Brushes;
using Point = System.Drawing.Point;

namespace Tuleika
{
    class Game
    {
        private static readonly Point StartingPoint = new Point(100, 100);

        private int _score;
        private readonly Label _scoreLabel;

        private readonly Seal _seal;
        private readonly Food _food;
        private Point _currentPosition;
        private Direction _currentDirection;
        private Direction _previousDirection;
        private readonly Canvas _gameField;
        private readonly DispatcherTimer _timer;

        public event EventHandler Over;

        protected virtual void GameOver()
        {
            EventHandler handler = Over;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public Game(Canvas gameField, int speed, int sealSize, int sealLength, int foodSize, int foodCount, Label scoreLabel)
        {
            _gameField = gameField;
            _scoreLabel = scoreLabel;

            _seal = new Seal(_gameField, sealSize, Brushes.Green, sealLength);
            _currentPosition = StartingPoint;

            _seal.Paint(StartingPoint);

            _food = new Food(_gameField, _seal, Brushes.Red, foodSize, foodCount);

            Over += OnGameOver;

            _timer = new DispatcherTimer { Interval = new TimeSpan(speed) };
            _timer.Tick += OnTimerTick;
            _timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (_currentDirection == Direction.None)
                return;

            MoveSeal();

            // border hit
            if ((_currentPosition.X <= 0) || (_currentPosition.X >= _gameField.Width - _seal.Size) ||
                (_currentPosition.Y <= 0) || (_currentPosition.Y >= _gameField.Height - _seal.Size))
                GameOver();
            
            // food hit
            foreach (
                var currentFood in
                    _food.FoodPoints.Where(
                        currentFood => _food.IntersectWithSealPoint(currentFood.Position, _currentPosition)))
            {
                _seal.IncreaseLength();
                _score++;
                _scoreLabel.Content = _score;

                _food.Eaten(currentFood);
                break;
            }

            // hit body
            if (_seal.Eaten(_currentPosition))
                GameOver();
        }

        public void OnKeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    RedirectSeal(Direction.Down);
                    break;
                case Key.Up:
                    RedirectSeal(Direction.Up);
                    break;
                case Key.Left:
                    RedirectSeal(Direction.Left);
                    break;
                case Key.Right:
                    RedirectSeal(Direction.Right);
                    break;
            }
        }

        private void MoveSeal()
        {
            if (_currentDirection == Direction.None)
                return;

            switch (_currentDirection)
            {
                case Direction.Down:
                    _currentPosition.Y += _seal.Size;
                    break;
                case Direction.Up:
                    _currentPosition.Y -= _seal.Size;
                    break;
                case Direction.Left:
                    _currentPosition.X -= _seal.Size;
                    break;
                case Direction.Right:
                    _currentPosition.X += _seal.Size;
                    break;
            }
            _seal.Paint(_currentPosition);
        }

        public void RedirectSeal(Direction newDirection)
        {
            switch (newDirection)
            {
                case Direction.None:
                    break;
                case Direction.Down:
                    if (_previousDirection != Direction.Up)
                        _currentDirection = Direction.Down;
                    break;
                case Direction.Up:
                    if (_previousDirection != Direction.Down)
                        _currentDirection = Direction.Up;
                    break;
                case Direction.Left:
                    if (_previousDirection != Direction.Right)
                        _currentDirection = Direction.Left;
                    break;
                case Direction.Right:
                    if (_previousDirection != Direction.Left)
                        _currentDirection = Direction.Right;
                    break;

            }
            _previousDirection = _currentDirection;
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            _timer.Stop();
            MessageBox.Show("Your score is " + _score, "Game Over", MessageBoxButton.OK, MessageBoxImage.Hand);
        }
    }
}
