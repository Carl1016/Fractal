using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractal
{
    public partial class Form1 : Form
    {
        int Geometry = 0;
        int Depth = 0;
        Pen pen = new Pen(Brushes.Blue);
        Point A = new Point();
        Point B = new Point();
        
        Pen eraser = new Pen(Brushes.White,3);
        
        List<Rectangle> pointset = new List<Rectangle> { };
        Graphics g;
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="" && textBox2.Text!="")
            {
                if(textBox2.Text =="0")
                {
                    MessageBox.Show("Error: Depth must more than 1");
                }
                else
                {
                    Geometry = Convert.ToInt32(textBox1.Text);
                    Depth = Convert.ToInt32(textBox2.Text);
                    Rectangle rect = new Rectangle(Geometry, Depth, 10, 10);
                }
                             
            }
            else
            {
                MessageBox.Show("Wrong input!");
            }            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle rect = new Rectangle(e.X, e.Y, 5, 5);
            g.FillRectangle(Brushes.Black, rect);
            pointset.Add(rect);
            if(pointset.Count==2)
            {
                
                g.DrawLine(pen, pointset[0].Location, pointset[1].Location);
                A.X = pointset[0].X;
                A.Y = pointset[0].Y;
                B.X = pointset[1].X;
                B.Y = pointset[1].Y;
                fractal_maker(Geometry, Depth,A,B);
                pointset.Remove(pointset[0]);
            }
            
        }
        public void fractal_maker(int Polygon_num,int depth,Point A, Point B)
        {           
            Point p = new Point();
            Point C = new Point();
            Point D = new Point();
            C.X = A.X + (B.X - A.X) / 3;
            C.Y = A.Y + (B.Y - A.Y) / 3;
            D.X = A.X + (B.X - A.X) / 3 * 2;
            D.Y = A.Y + (B.Y - A.Y) / 3 * 2;
            g.DrawLine(eraser, C, D);
            if(depth==1)
            {
                g.DrawLine(pen, A, B);
            }
            else
            {
                
                switch (Polygon_num)
                {
                    case 3:
                        p = triangle_maker(C,D,depth);
                        fractal_maker(Polygon_num, depth - 1, A, C);
                        fractal_maker(Polygon_num, depth - 1, C, p);
                        fractal_maker(Polygon_num, depth - 1, p, D);
                        fractal_maker(Polygon_num, depth - 1, D, B);
                        break;
                    case 4:
                        Point[] points = new Point[2];
                        points= Rectangle_maker(C, D);
                        fractal_maker(Polygon_num,depth-1,A, C);
                        fractal_maker(Polygon_num, depth - 1, C, points[0]);
                        fractal_maker(Polygon_num, depth - 1, points[0], points[1]);
                        fractal_maker(Polygon_num, depth - 1, points[1], D);
                        fractal_maker(Polygon_num, depth - 1, D, B);

                        break;
                    case 5:
                        Point[] points1 = new Point[3];
                        points1 = pentagon_maker(C,D);
                        fractal_maker(Polygon_num, depth - 1, A, C);
                        fractal_maker(Polygon_num, depth - 1, C, points1[0]);
                        fractal_maker(Polygon_num, depth - 1, points1[0], points1[1]);
                        fractal_maker(Polygon_num, depth - 1, points1[1], points1[2]);
                        fractal_maker(Polygon_num, depth - 1, points1[2], D);
                        fractal_maker(Polygon_num, depth - 1, D,B);
                        break;
                    case 6:
                        Point[] points2 = new Point[4];
                        points2 = hexagon_maker(C,D);
                        fractal_maker(Polygon_num, depth - 1, A, C);
                        fractal_maker(Polygon_num, depth - 1, C, points2[0]);
                        fractal_maker(Polygon_num, depth - 1, points2[0], points2[1]);
                        fractal_maker(Polygon_num, depth - 1, points2[1], points2[2]);
                        fractal_maker(Polygon_num, depth - 1, points2[2], points2[3]);
                        fractal_maker(Polygon_num, depth - 1, points2[3], D);
                        fractal_maker(Polygon_num, depth - 1, D, B);
                        break;
                    default:
                        Console.WriteLine("wrong input");
                        break;
                      
                }
                

            }

            

        }
        public Point triangle_maker(Point C, Point D,int depth)
        {
            
            double x1 = (D.X - C.X) * Math.Cos(-60*Math.PI/180) - (D.Y - C.Y) * Math.Sin(-60*Math.PI/180) + C.X;
            double y1 = (D.X - C.X) * Math.Sin(-60*Math.PI/180) + (D.Y - C.Y) * Math.Cos(-60*Math.PI/180) + C.Y;
            Point point = new Point();
            point.X = (int)x1;
            point.Y = (int)y1;
            g.DrawLine(pen, C, point);
            g.DrawLine(pen, D, point);
            return point;

        }   
        public Point[] Rectangle_maker(Point C, Point D)
        {

            Point[] points = new Point[2];
            Point point = new Point();
            double x1 = (D.X - C.X) * Math.Cos(-90 * Math.PI / 180) - (D.Y - C.Y) * Math.Sin(-90 * Math.PI / 180) + C.X;
            double y1 = (D.X - C.X) * Math.Sin(-90 * Math.PI / 180) + (D.Y - C.Y) * Math.Cos(-90 * Math.PI / 180) + C.Y;
            point.X = (int)x1;
            point.Y = (int)y1;
            g.DrawLine(pen, C, point);
            Point point1 = new Point();
            double x2 = (C.X - point.X) * Math.Cos(-90 * Math.PI / 180) - (C.Y - point.Y) * Math.Sin(-90 * Math.PI / 180) + point.X;
            double y2 = (C.X - point.X) * Math.Sin(-90 * Math.PI / 180) + (C.Y - point.Y) * Math.Cos(-90 * Math.PI / 180) + point.Y;
            point1.X = (int)x2;
            point1.Y = (int)y2;
            g.DrawLine(pen, point, point1);
            g.DrawLine(pen, point1, D);
            points[0] = point;
            points[1] = point1;
            return points;

        }
        public Point[] pentagon_maker(Point C, Point D)
        {
            Point[] points = new Point[3];
            Point point = new Point();
            double x1 = (D.X - C.X) * Math.Cos(-108 * Math.PI / 180) - (D.Y - C.Y) * Math.Sin(-108 * Math.PI / 180) + C.X;
            double y1 = (D.X - C.X) * Math.Sin(-108 * Math.PI / 180) + (D.Y - C.Y) * Math.Cos(-108 * Math.PI / 180) + C.Y;
            point.X = (int)x1;
            point.Y = (int)y1;
            g.DrawLine(pen, C, point);
            Point point1 = new Point();
            double x2 = (C.X - point.X) * Math.Cos(-108 * Math.PI / 180) - (C.Y - point.Y) * Math.Sin(-108 * Math.PI / 180) + point.X;
            double y2 = (C.X - point.X) * Math.Sin(-108 * Math.PI / 180) + (C.Y - point.Y) * Math.Cos(-108 * Math.PI / 180) + point.Y;
            point1.X = (int)x2;
            point1.Y = (int)y2;
            g.DrawLine(pen, point, point1);
            Point point2 = new Point();
            double x3 = (point.X - point1.X) * Math.Cos(-108 * Math.PI / 180) - (point.Y - point1.Y) * Math.Sin(-108 * Math.PI / 180) + point1.X;
            double y3 = (point.X - point1.X) * Math.Sin(-108 * Math.PI / 180) + (point.Y - point1.Y) * Math.Cos(-108 * Math.PI / 180) + point1.Y;
            point2.X = (int)x3;
            point2.Y = (int)y3;
            g.DrawLine(pen, point1, point2);
            g.DrawLine(pen, point2, D);
            points[0] = point;
            points[1] = point1;
            points[2] = point2;
            return points;

        }
        public Point[] hexagon_maker(Point C, Point D)
        {
            Point[] points = new Point[4];
            Point point = new Point();
            double x1 = (D.X - C.X) * Math.Cos(-120 * Math.PI / 180) - (D.Y - C.Y) * Math.Sin(-120 * Math.PI / 180) + C.X;
            double y1 = (D.X - C.X) * Math.Sin(-120 * Math.PI / 180) + (D.Y - C.Y) * Math.Cos(-120 * Math.PI / 180) + C.Y;
            point.X = (int)x1;
            point.Y = (int)y1;
            g.DrawLine(pen, C, point);
            Point point1 = new Point();
            double x2 = (C.X - point.X) * Math.Cos(-120 * Math.PI / 180) - (C.Y - point.Y) * Math.Sin(-120 * Math.PI / 180) + point.X;
            double y2 = (C.X - point.X) * Math.Sin(-120 * Math.PI / 180) + (C.Y - point.Y) * Math.Cos(-120 * Math.PI / 180) + point.Y;
            point1.X = (int)x2;
            point1.Y = (int)y2;
            g.DrawLine(pen, point, point1);
            Point point2 = new Point();
            double x3 = (point.X - point1.X) * Math.Cos(-120 * Math.PI / 180) - (point.Y - point1.Y) * Math.Sin(-120 * Math.PI / 180) + point1.X;
            double y3 = (point.X - point1.X) * Math.Sin(-120 * Math.PI / 180) + (point.Y - point1.Y) * Math.Cos(-120 * Math.PI / 180) + point1.Y;
            point2.X = (int)x3;
            point2.Y = (int)y3;
            g.DrawLine(pen, point1, point2);
            Point point3 = new Point();
            double x4 = (point1.X - point2.X) * Math.Cos(-120 * Math.PI / 180) - (point1.Y - point2.Y) * Math.Sin(-120 * Math.PI / 180) + point2.X;
            double y4 = (point1.X - point2.X) * Math.Sin(-120 * Math.PI / 180) + (point1.Y - point2.Y) * Math.Cos(-120 * Math.PI / 180) + point2.Y;
            point3.X = (int)x4;
            point3.Y = (int)y4;
            g.DrawLine(pen, point2, point3);
            g.DrawLine(pen, point3, D);
            points[0] = point;
            points[1] = point1;
            points[2] = point2;
            points[3] = point3;
            return points;

        }

    }
}
