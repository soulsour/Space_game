using System.Drawing;

namespace Space_Game
{
    public interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }
}
