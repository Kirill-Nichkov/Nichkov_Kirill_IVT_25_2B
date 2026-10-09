using System;
namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            double a = 0.2;
            double b = 1.0;
            int k = 10;
            int n = 10;
            double eps = 0.0001;

            double h = (b - a) / k;

            for (double x = a; x <= b + h / 2.0; x += h)
            {
                double t = (x - 1.0) / (x + 1.0);

                double sn = 0;
                double p1 = t;
                for (int m = 0; m <= n; m++)
                {
                    sn += p1 / (2 * m + 1);
                    p1 *= t * t;
                }

                double se = 0;
                double p2 = t;
                int j = 0;
                double term = p2 / (2 * j + 1);
                while (Math.Abs(term) > eps)
                {
                    se += term;
                    j++;
                    p2 *= t * t;
                    term = p2 / (2 * j + 1);
                }

                double y = 0.5 * Math.Log(x);

                Console.WriteLine("X={0:F2}\tSN={1:F6}\tSE={2:F6}\tY={3:F6}", x, sn, se, y);
            }

        }
    }
}