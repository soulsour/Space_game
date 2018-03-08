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
        public static event Message MessageDie;
        private int _energy = 100;
        public int Energy => _energy;

        public void EnergyLow(int n)
        {
            _energy -= n;
        }
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size) { }



        private Image ship = Image.FromFile("Assets/ship.png");

        public override void Draw()
        {
            //SplashScreen.Buffer.Graphics.DrawImage(ship, Pos.X + Size.Width, Pos.Y, 240, 240); 
            Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            //Pos.X = Pos.X - Dir.X;
            //if (Pos.X > SplashScreen.Width - Size.Width) Pos.X = 0 - (Size.Width) * 2;
        }
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if(Pos.Y<Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        public void Die()
        {
            MessageDie?.Invoke();
        }
    }
}
