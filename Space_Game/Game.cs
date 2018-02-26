using System;
using System.Drawing;
using System.Windows.Forms;

namespace Space_Game
{
    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        {

        }

        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            Buffer.Render();

            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }

        public static BaseObject[] _objs;
        public static void Load()
        {
            _objs = new BaseObject[60];
            for (int i = 0; i < _objs.Length/3; i++)
                _objs[i] = new BaseObject(new  Point(600, i * 15), new Point(5- i, i*2), new Size(20, 20));
            
            for (int i = _objs.Length / 3; i < _objs.Length/3*2; i++)
                _objs[i] = new Star(new Point(300, i*5), new Point( - i+5, 0), new Size(5,5));
            for (int i = _objs.Length/3*2; i < _objs.Length; i++)
                _objs[i] = new Pulsar(new Point(600, i +20), new Point(-i,0), new Size(5, 5));
        }
    }
}
