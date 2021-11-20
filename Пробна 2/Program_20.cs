//15
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Figures
{
    abstract class Polygon
    {
        private bool correct;
        public double[] Sides { get; set; }

        public bool Correct
        {
            get
            {
                return correct;
            }
            set
            {
                correct = SetCorrect();
            }
        }


        public Polygon(int n)
        {
            Sides = new double[n];

        }

        public Polygon(double d)
        {
            Sides = new double[4];
            Array.Fill(Sides, d);
            Correct = SetCorrect();
        }

        public virtual bool SetCorrect()
        {
            return !Sides.Any(s => s <= 0);
        }


        public double GetSide(int n)
        {
            return Sides[n];
        }

        public void SetSide(int n, double d)
        {
            Sides[n] = d;
        }

        public int GetCount()
        {
            return Sides.Length;
        }

        public double Perimeter()
        {
            return Correct ? this.Sides.Sum() : 0;
        }

        public abstract double Area();


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var s in Sides)
            {
                sb.Append(s.ToString("f2") + " ");
            }
            return $"{{ {this.GetType().Name}: ({sb.ToString()})}}";

        }

    }

    class Triangle : Polygon
    {
        public Triangle(double a, double b, double c) : base(3)
        {
            SetSide(0, a);
            SetSide(1, b);
            SetSide(2, c);
            base.Correct = SetCorrect();

        }
        override
        public bool SetCorrect()
        {
            return !Sides.Any(s => s <= 0) && Sides[0] + Sides[1] > Sides[2];
        }

        public override double Area()
        {
            double s = (Sides.Sum()) / 2;
            return Math.Sqrt(s * (s - Sides[0]) * (s - Sides[1]) * (s - Sides[2]));
        }
    }

    class Square : Polygon
    {
        public Square(double d) : base(d)
        {


        }

        public override double Area()
        {
            return Sides[0] * Sides[0];
        }
    }

    class Isosceles : Triangle
    {
        public Isosceles(double a, double b) : base(a, b, b)
        {
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"C:\Users\Asus\source\repos\Figures\Figures\input.txt");
            var polygons = new List<Polygon>();


            foreach (var line in lines)
            {
                var curr = line.Replace('.', ',');
                var tokens = curr.Split(new char[] { ' ', '|', ';' }, StringSplitOptions.RemoveEmptyEntries);
                switch (tokens.Length)
                {
                    case 1:
                        polygons.Add(new Square(double.Parse(tokens[0])));
                        break;
                    case 2:
                        polygons.Add(new Isosceles(double.Parse(tokens[0]), double.Parse(tokens[1])));
                        break;
                    case 3:
                        polygons.Add(new Triangle(double.Parse(tokens[0]), double.Parse(tokens[1]), double.Parse(tokens[2])));
                        break;

                }
            }


            var ordered = polygons.Where(p => p.Correct == true).OrderBy(p => p.GetType().Name).OrderByDescending(p => Math.Round(p.Area(), 1)).ToList();

            foreach (var item in ordered)
            {
                Console.WriteLine(item.ToString());
            }

        }
    }
}