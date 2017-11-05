using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Point = System.Drawing.Point;

namespace Tuleika
{
    class Food
    {
        private static readonly Random Randomizer = new Random();

        private readonly Canvas _gameField;

        private readonly int _size;

        private readonly Brush _color;

        public List<GameFieldObject> FoodPoints = new List<GameFieldObject>();

        private Seal _seal;

        private readonly int _count;

        public Food(Canvas gameField, Seal seal, Brush color, int size, int startingCount)
        {
            _gameField = gameField;
            _color = color;
            _size = size;
            _seal = seal;
            _count = startingCount;
            PaintInitial();
        }

        public void PaintInitial()
        {
            for (var n = 0; n < _count; n++)
            {
                PaintRandomPoint();
            }
        }

        public void PaintRandomPoint()
        {
            Point newPoint;
            do
            {
                newPoint = new Point(Randomizer.Next(0, (int) _gameField.Width - _size),
                    Randomizer.Next(0, (int) _gameField.Height - _size));
            } while (_seal.SealPoints.Any(sealPoint => IntersectWithSealPoint(newPoint, sealPoint.Position))); // do not appear where seal is

            var newFood = new GameFieldObject(newPoint, new Ellipse {Fill = _color, Width = _size, Height = _size});

            Canvas.SetTop(newFood.CanvasChild, newPoint.Y);
            Canvas.SetLeft(newFood.CanvasChild, newPoint.X);
            _gameField.Children.Add(newFood.CanvasChild);
            FoodPoints.Add(newFood);
        }

        public bool IntersectWithSealPoint(Point food, Point seal)
        {
            return (Math.Abs(food.X - seal.X) < _seal.Size) &&
                   (Math.Abs(food.Y - seal.Y) < _seal.Size);
        }

        public void Eaten(GameFieldObject gameFieldObject)
        {
            FoodPoints.Remove(gameFieldObject);
            _gameField.Children.Remove(gameFieldObject.CanvasChild);
            PaintRandomPoint();
        }
    }
}
