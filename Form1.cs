using System.Net.Sockets;

namespace Parallel_And_Distributed_Systems_Assignment_B_Forms
{
    public partial class Form1 : Form
    {
        public static object locker = new object();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();

            List<Brush> listOfBrushes = new List<Brush>() { Brushes.Red, Brushes.Green, Brushes.Blue, Brushes.Yellow, Brushes.Violet };

            CirclesObject CirclesObject = new CirclesObject(100, 30);
            List<Painter> listOfPainters = new List<Painter>();

            for (int i = 0; i < 100; i++)
            {
                Painter painter = new Painter(CirclesObject.CirclesPool,i+1);
                listOfPainters.Add(painter);
            }

            Parallel.ForEach(listOfPainters, painter =>
            {
                while (CirclesObject.CirclesQueue.Count > 0)
                {
                    if (CirclesObject.CirclesQueue.TryDequeue(out Circle circleToPaint))
                    {
                        bool check;

                        lock (locker)
                        {
                            check = !painter.IsCirclePainted(listOfPainters, circleToPaint);
                        }

                        if (check) painter.PaintCircle(circleToPaint, g);
                    }
                }
            });
        }
    }
}