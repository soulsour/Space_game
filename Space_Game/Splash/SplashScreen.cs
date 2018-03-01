using System;
using System.Drawing;
using System.Windows.Forms;

namespace Space_Game
{
    class SplashScreen
    {
        
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static BaseObject obj;

        
        public static int Width { get; set; }
        public static int Height { get; set; }
       

      
       
        static SplashScreen() { }
       
        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width; Height = form.Height;
            form.Text = "Space game";
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Button btnBegin = new Button()
            {
                Parent = form,
                Left = 10,
                Top = 10,
                Width = 160,
                Text = "Начало игры",
                
            };

            int tmpX = btnBegin.Left + btnBegin.Width;

            Button btnScore = new Button()
            {
                Parent = form,
                Left = tmpX + 50,
                Top = 10,
                Width = 160,
                Text = "Рекорды",
                
            };

            tmpX = btnScore.Left + btnScore.Width;

            Button btnExit = new Button()
            {
                Parent = form,
                Left = tmpX + 50,
                Top = 10,
                Width = 160,
                Text = "Выход",
                
            };

            tmpX = btnExit.Left + btnExit.Width;

            Label label = new Label()
            {
                Parent = form,
                Left = tmpX + 60,
                Top = 10,
                Width = 160,
                Text = "Andrew",
                ForeColor = Color.OrangeRed,
                BackColor = Color.Black
            };

            Load();

            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        public static void Draw()
        {
            ClearScreen();                         
            obj.Draw();                             
            Buffer.Render();                        
        }

        public static void Load()
        {
            obj = new Ship(new Point(-480, 200), new Point(-10, 0), new Size(240, 240));
        }


        public static void Update()
        {
            obj.Update();
        }


        public static void ClearScreen()
        {
            Buffer.Graphics.Clear(Color.Black);
        }


        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw(); Update();
        }
    }
}
