
using Microsoft.Xna.Framework;

namespace TuleikaX
{
    public struct SealChild
    {
        public readonly Vector2 Position;
        public readonly float Angle;
        public readonly float Size;

        public SealChild(Vector2 position, float angle, float size)
        {
            Position = position;
            Angle = angle;
            Size = size;
        }
    }
}
