
    using System;
    using System.Windows.Forms;


namespace Space_Game
{
        class Program
        {
        static void Main()
        {
            Form form = new Form()
            {
                Width = Screen.PrimaryScreen.Bounds.Width,
                Height = Screen.PrimaryScreen.Bounds.Height
            };

            Game.Init(form);
            form.Show();
            Game.Load();
            Game.Draw();

            //SplashScreen.Init(form);                                
            //form.Show();                                            
            //SplashScreen.Draw();

            Application.Run(form);
            }
        }
    }

