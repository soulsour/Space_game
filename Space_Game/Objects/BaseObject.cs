using System;
using System.Drawing;


namespace Space_Game
{
    abstract class BaseObject:ICollision
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        //Image aster = Image.FromFile("Assets/asteroid.png");
        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public abstract void Draw();
        //{
        //    Game.Buffer.Graphics.DrawImage(aster, Pos.X, Pos.Y, 60, 40);
        //    Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X + 5, Pos.Y +5, Size.Width, Size.Height);
        //}

        public abstract void Update();
        //{
        //    Pos.X = Pos.X + Dir.X;
        //    Pos.Y = Pos.Y + Dir.Y;
        //    if (Pos.X < 0) Dir.X = -Dir.X;
        //    if (Pos.X > Game.Width) Dir.X = -Dir.X;
        //    if (Pos.Y < 0) Dir.Y = -Dir.Y;
        //    if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;

        //}
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(Pos, Size);
    }
}
