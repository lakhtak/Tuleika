using System.Windows;
using Point = System.Drawing.Point;

namespace Tuleika
{
    struct GameFieldObject
    {
        public readonly UIElement CanvasChild;
        public readonly int X;
        public readonly int Y;

        public Point Position
        {
            get
            {
                return new Point(X, Y);
            }
        }

        public GameFieldObject(Point point, UIElement canvasChild)
            : this(point.X, point.Y, canvasChild)
        {
        }

        public GameFieldObject(int x, int y, UIElement canvasChild)
        {
            X = x;
            Y = y;
            CanvasChild = canvasChild;
        }
    }
}
