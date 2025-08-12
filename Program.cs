using System;
using System.Collections.Generic;

namespace SprinklerCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector3 A = new Vector3(97500.00, 34000.00, 2500.00);
            Vector3 B = new Vector3(85647.67, 43193.61, 2500.00);
            Vector3 C = new Vector3(91776.75, 51095.16, 2500.00);
            Vector3 D = new Vector3(103629.07, 41901.55, 2500.00);
            double spacing = 2500.0;

            Vector3 vec_AB = B - A;
            double length_AB = vec_AB.Magnitude();
            Vector3 vec_AD = D - A;
            double length_AD = vec_AD.Magnitude();
            Vector3 vec_BC = B - C;
            double length_BC = vec_BC.Magnitude();
            Vector3 vec_CD = C - D;
            double length_CD = vec_CD.Magnitude();

            Console.WriteLine($"Length AB: {length_AB}");
            Console.WriteLine($"Length AD: {length_AD}");
            Console.WriteLine($"Length BC: {length_BC}");
            Console.WriteLine($"Length BD: {length_CD}");


            Console.WriteLine($"Length AB: {length_AB:F2}");
            Console.WriteLine($"Length AD: {length_AD:F2}");
            Console.WriteLine($"Length BC: {length_BC:F2}");
            Console.WriteLine($"Length BD: {length_CD:F2}");


            // reduced length after removing 2500 offset
            int ab = (int)(Math.Round(length_AB - spacing, 2));
            int ad = (int)(Math.Round(length_AD - spacing, 2));  
            Console.WriteLine($"Length AB reduced: {ab}");
            Console.WriteLine($"Length AD reduced: {ad}");       

            // calculate GCF
            int a = ab;
            int b = ad;

            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            int gcf = a;


            Console.WriteLine($"GCF of AB and AD: {gcf}");
            

            int num_short = ab/gcf;
            int num_long = ad/gcf;

            List<Vector3> sprinklers = new List<Vector3>();
            for (int i = 0; i < num_short; i++)
            {
                double dist_short = spacing + i * spacing;
                double f_short = dist_short / length_AD;
                for (int j = 0; j < num_long; j++)
                {
                    double dist_long = spacing + j * spacing;
                    double f_long = dist_long / length_AB;
                    Vector3 pos = A + (vec_AD * f_short) + (vec_AB * f_long);
                    pos.Z = 2500.00;
                    sprinklers.Add(pos);
                }
            }

            List<(Vector3, Vector3)> pipes = new List<(Vector3, Vector3)>()
            {
                (new Vector3(98242.11, 36588.29, 3000.00), new Vector3(87970.10, 44556.09, 3500.00)),
                (new Vector3(99774.38, 38563.68, 3500.00), new Vector3(89502.37, 46531.47, 3000.00)),
                (new Vector3(101306.65, 40539.07, 3000.00), new Vector3(91034.63, 48506.86, 3000.00))
            };

            List<Vector3> connections = new List<Vector3>();
            foreach (var S in sprinklers)
            {
                double min_dist = double.MaxValue;
                Vector3 best_conn = new Vector3(0,0,0);
                foreach (var pipe in pipes)
                {
                    var (P, Q) = pipe;
                    var (conn, dist) = ClosestPointOnSegment(P, Q, S);
                    if (dist < min_dist)
                    {
                        min_dist = dist;
                        best_conn = conn;
                    }
                }
                connections.Add(best_conn);
            }

            Console.WriteLine($"Number of sprinklers needed {sprinklers.Count}");
            int count = 1;
            foreach (var S in sprinklers)
            {
                Console.WriteLine($"Sprinkler {count}: ({S.X:F2}, {S.Y:F2}, {S.Z:F2})");
                count++;
            }
            
            int connCount = 1;
            foreach (var conn in connections)
            {
                Console.WriteLine($"Connection {connCount}: ({conn.X:F2}, {conn.Y:F2}, {conn.Z:F2})");
                connCount++;
            }
        }

        static (Vector3, double) ClosestPointOnSegment(Vector3 P, Vector3 Q, Vector3 S)
        {
            Vector3 V = Q - P;
            double denom = V.Dot(V);
            if (denom == 0)
            {
                return (P, (S - P).Magnitude());
            }
            double t = (S - P).Dot(V) / denom;
            t = Math.Max(0, Math.Min(1, t));
            Vector3 closest = P + V * t;
            double dist = (S - closest).Magnitude();
            return (closest, dist);
        }
    }

    struct Vector3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Vector3 operator *(Vector3 a, double s) => new Vector3(a.X * s, a.Y * s, a.Z * s);
        public double Dot(Vector3 other) => X * other.X + Y * other.Y + Z * other.Z;
        public double Magnitude() => Math.Sqrt(Dot(this));
    }
}