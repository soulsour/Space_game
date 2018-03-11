using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace Space_Game
{
    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        delegate void Log();
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BaseObject[] _objs;
        private static Asteroid[] _asteroids;
        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));
        private static Timer _timer = new Timer { Interval = 100 };
        static StreamWriter sw = new StreamWriter($"log.txt");
        public static MedChest[] _medChest;
        public static Star[] _star;
        public static int Points { get; set; } = 0;
        private static List<Bullet> _bullets = new List<Bullet>();
        private static int _numbAster = 10;
        private static int _level = 1;
        public static Random rnd = new Random();

        static Game(){ }

        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            
            _timer.Start();
            _timer.Tick += Timer_Tick;
            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += Finish;
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) _bullets.Add( new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1)));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw(); Update();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);

            _ship?.Draw();
            if (_ship != null)
                Buffer.Graphics.DrawString("Ship energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.Yellow, 0, 0);
            Buffer.Graphics.DrawString("Score:" + Game.Points, SystemFonts.DefaultFont, Brushes.LightGreen, 100, 0);
            Buffer.Graphics.DrawString("Level:" + _level, new Font(FontFamily.GenericSansSerif, 16), Brushes.LightCoral, 0, 20);
            foreach (Bullet b in _bullets)
                b?.Draw();
            foreach (Asteroid obj in _objs)
                obj?.Draw();
            foreach (Star obj in _star)
                obj.Draw();
            foreach (MedChest obj in _medChest)
                obj?.Draw();
            Buffer.Render();
        }

        public static void Update()
        {
            // Звезды
            foreach (Star s in _star)
                s.Update();

            foreach (Bullet b in _bullets)
                b?.Update();

            // Астероиды
            foreach (Asteroid a in _objs)
                a?.Update();

            for (var i = 0; i < _objs.Length; i++)
            {
                if (_objs[i] == null) continue;
                _objs[i].Update();
                for (int j = 0; j < _bullets.Count; j++)
                {
                    if (_objs[i] != null && _bullets[j].Collision(_objs[i]))
                    {
                        ShowLog(AsteroidDie);
                        Game.Points += 1;
                        System.Media.SystemSounds.Hand.Play();
                        _objs[i] = null;
                        _bullets.RemoveAt(j);
                        j--;
                        _numbAster--;
                    }
                }

                if (_objs[i] == null || !_ship.Collision(_objs[i])) continue;
                ShowLog(KickShip);
                _ship.EnergyLow(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0) _ship.Die();
            }

            if (_numbAster <= 0)
            {
                _numbAster = _objs.Length + 1;
                _level++;
                LoadAsteroids(_numbAster);
            }

            // Аптечки
            foreach (MedChest m in _medChest)
            {
                m?.Update();
            

            }

            for (var i = 0; i < _medChest.Length; i++)
            {
                if (_medChest == null) continue;

                for (int j = 0; j < _bullets.Count; j++)
                {
                    if ((_bullets[j] != null) && (_medChest[i] != null) && _bullets[j].Collision(_medChest[i]))
                    {
                        ShowLog(AddMedChest);
                        _ship?.EnergyUp(rnd.Next(1, 10));
                        System.Media.SystemSounds.Question.Play();
                        _medChest[i] = null;
                        _bullets.RemoveAt(j);
                        j--;
                    }
                }
            }
        }



        public static void Load()
        {
            LoadAsteroids(_numbAster);

            _star = new Star[30];
            for (var i = 0; i < _star.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _star[i] = new Star(new Point(Game.Width, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }

            _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(30, 30));

        }
        public static void Finish()
        {
            ShowLog(DieShip);
            sw.Close();
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60,
            FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }
        private static void AsteroidDie()
        {
            Console.WriteLine("Астероид сбит.");
            sw.WriteLine("Астероид сбит.");
        }
        private static void KickShip()
        {
            Console.WriteLine("Корабль поврежден.");
            sw.WriteLine("Корабль поврежден.");
        }
        private static void DieShip()
        {
            Console.WriteLine("Корабль уничтожен.");
            sw.WriteLine("Корабль уничтожен.");
        }
        private static void AddMedChest()
        {
            Console.WriteLine("Аптечка активирована.");
            sw.WriteLine("Аптечка активирована.");
        }
        private static void ShowLog(Log _log) => _log.Invoke();

        private static void LoadAsteroids(int numb)
        {

            _objs = new Asteroid[numb];
            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Asteroid(new Point(Game.Width, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r + 10, r));
            }

            _medChest = new MedChest[numb - 3];
            for (var i = 0; i < _medChest.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _medChest[i] = new MedChest(new Point(Game.Width, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(30, 30));
            }
        }
    }
}
