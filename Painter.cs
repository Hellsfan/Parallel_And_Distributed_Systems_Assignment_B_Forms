using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel_And_Distributed_Systems_Assignment_B_Forms
{
    internal class Painter
    {
        public int Id { get; set; }
        public Dictionary<Circle, bool> CirclesPool { get; set; }

        public Painter(Dictionary<Circle, bool> _CirclesPool, int id)
        {
            CirclesPool = _CirclesPool;
            Id = id;
        }


        public void PaintCircle(Circle circle, Graphics g)
        {
            Font f = new Font("Arial", 15);
            Thread.Sleep(20);
            g.FillEllipse(Brushes.Black, new Rectangle(circle.X, circle.Y, circle.R, circle.R));
            g.FillEllipse(Brushes.White, new Rectangle(circle.X + 2, circle.Y + 2, circle.R - 4, circle.R - 4));
            g.DrawString(Id.ToString(), f, Brushes.Black, circle.X + 2, circle.Y + 2);
        }

        public bool IsCirclePainted(List<Painter> painters, Circle circle)
        {
            foreach (var painter in painters)
            {
                if (painter.CirclesPool[circle] == true)
                {
                    return true;
                };
            }

            this.CirclesPool[circle] = true;
            return false;
        }
    }
}
