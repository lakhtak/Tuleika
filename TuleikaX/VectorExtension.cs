
using Microsoft.Xna.Framework;
namespace TuleikaX
{
    static class VectorExtension
    {
        public static Rectangle RectangleFromCenter(this Vector2 center, float inputWidth, float inputHeight, float size)
        {
            var height = (int)(inputHeight * size);
            var width = (int)(inputWidth * size);
            var x = (int)center.X;
            var y = (int)center.Y;

            return new Rectangle(x - width / 2, y - height / 2, width, height);
        }

        public static Vector2 RotateAboutOrigin(this Vector2 point, Vector2 origin, float rotation)
        {
            return Vector2.Transform(point - origin, Matrix.CreateRotationZ(rotation)) + origin;
        }
    }
}
