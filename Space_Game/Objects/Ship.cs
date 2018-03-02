using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Game
{
     class Ship:BaseObject
    {
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size) { }



        private Image ship = Image.FromFile("Assets/ship.png");

        public override void Draw()
        {
            SplashScreen.Buffer.Graphics.DrawImage(ship, Pos.X + Size.Width, Pos.Y, 240, 240); 

        }
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X > SplashScreen.Width - Size.Width) Pos.X = 0 - (Size.Width) * 2;
        }
    }
}
