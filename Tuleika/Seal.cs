using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Point = System.Drawing.Point;

namespace Tuleika
{
    internal class Seal
    {
        public readonly int Size;

        private readonly Brush _color;

        private int _length;

        private readonly int _startingLength;

        public readonly List<GameFieldObject> SealPoints = new List<GameFieldObject>();

        private readonly Canvas _gameField;

        public Seal(Canvas gameField, int size, Brush color, int startingLength)
        {
            Size = size;
            _color = color;
            _startingLength = startingLength;
            _length = startingLength;
            _gameField = gameField;
        }

        public void PaintInitial(Point startingPosition)
        {
            for (var i = 0; i < _startingLength; i++)
            {
                Paint(new Point(startingPosition.X + Size * (_startingLength - i), startingPosition.Y));
            }
        }

        public void Paint(Point newPosition)
        {
            var newSealPoint = new GameFieldObject(newPosition, new Ellipse {Fill = _color, Width = Size, Height = Size});

            Canvas.SetTop(newSealPoint.CanvasChild, newPosition.Y);
            Canvas.SetLeft(newSealPoint.CanvasChild, newPosition.X);

            _gameField.Children.Add(newSealPoint.CanvasChild);
            SealPoints.Add(newSealPoint);

            // Restrict the tail of the snake
            if (SealPoints.Count > _length)
            {
                var tail = SealPoints.First();
                _gameField.Children.Remove(tail.CanvasChild);
                SealPoints.Remove(tail);
            }
        }

        public void IncreaseLength()
        {
            _length++;
        }

        public bool Eaten(Point headPosition)
        {
            for (var q = 0; q < (SealPoints.Count - Size*2); q++)
            {
                var point = new Point(SealPoints[q].X, SealPoints[q].Y);
                if (Math.Abs(point.X - headPosition.X) < Size &&
                    Math.Abs(point.Y - headPosition.Y) < Size)
                {
                    return true;
                }
            }
            return false;
        }
    }
}