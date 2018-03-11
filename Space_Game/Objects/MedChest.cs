using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Game
{
    public class MedChest : BaseObject
    {
        static Image _medChest;

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MedChest(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            _medChest = Image.FromFile("Assets/MedChest.png");
        }
        #endregion

        #region Методы
        /// <summary>
        /// Отрисовка объекта
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_medChest, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Перемещение объекта
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X / 2;
            if (Pos.X < 0 + Size.Width) Pos.X = Game.Width - Size.Width;
        }
        #endregion
    }
}
