using System;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Task1();
            Task2();
            Task3();
        }

        static void Task1()
        {
            Console.Write("Введите число n: ");
            int n = int.Parse(Console.ReadLine());
            Console.Write("Введите число m: ");
            int m = int.Parse(Console.ReadLine());
            //номер 1
            // При m = 1 значение --m равно 0, возникает деление на ноль
            if (m == 1)
            {
                Console.WriteLine("1) (n++/--m)++ : Нельзя вычислить (деление на ноль)");
            }
            else
            {
                int n1 = n;
                int m1 = m;
                int result1 = n1++ / --m1;
                result1++;
                Console.WriteLine("1) (n++/--m)++ = " + result1 + ", n = " + n1 + ", m = " + m1);
            }
            //номер 2
            int n2 = n;
            int m2 = m;
            bool result2 = ++m2 < n2--;
            Console.WriteLine("2) ++m < n-- : " + result2 + ", n = " + n2 + ", m = " + m2);
            //номер 3
            int n3 = n;
            int m3 = m;
            bool result3 = --m3 > ++n3;
            Console.WriteLine("3) --m > ++n : " + result3 + ", n = " + n3 + ", m = " + m3);
            //номер 4
            Console.Write("Введите число x: ");
            double x = double.Parse(Console.ReadLine());
            // Проверка ОДЗ: деление на ноль
            if (x == 0 )
            {
                Console.WriteLine("4) Нельзя вычислить: выход из ОДЗ");
            }
            else
            {
                double sum = Math.Exp(x) + Math.Tan(x);
                double result4 = Math.Sign(sum) * Math.Pow(Math.Abs(sum), 1.0 / 3.0);
                double y = result4 + 1.0 / x;
                Console.WriteLine("4) cbrt(e^x + tg(x)) + 1/x = " + y);
            }
        }

        static void Task2()
        {
            Console.Write("Введите координату X: ");
            double x = double.Parse(Console.ReadLine());
            Console.Write("Введите координату Y: ");
            double y = double.Parse(Console.ReadLine());
            bool result5 = (x * x + y * y <= 1.0) && (x >= 0 || y >= 0 || x + y >= -1.0);

            Console.WriteLine("Точка (" + x + "; " + y + ") попадает в область: " + result5);
        }

        static void Task3()
        {
            // 1. Вычисления с двойной точностью (double)
            double aD = 1000.0;
            double bD = 0.0001;

            double d1 = Math.Pow(aD + bD, 4);
            double d2 = Math.Pow(aD, 4);
            double d3 = 6.0 * Math.Pow(aD, 2) * Math.Pow(bD, 2);
            double d4 = 4.0 * aD * Math.Pow(bD, 3);
            double resultD1 = d1 - (d2 + d3 + d4);

            double d5 = Math.Pow(bD, 4);
            double d6 = 4.0 * Math.Pow(aD, 3) * bD;
            double resultD2 = d5 + d6;

            double resultD = resultD1 / resultD2;
            // 2. Вычисления с одинарной точностью (float)
            float aF = 1000.0f;
            float bF = 0.0001f;

            float f1 = (float)Math.Pow(aF + bF, 4);
            float f2 = (float)Math.Pow(aF, 4);
            float f3 = (float)(6.0 * Math.Pow(aF, 2) * Math.Pow(bF, 2));
            float f4 = (float)(4.0 * aF * Math.Pow(bF, 3));
            float resultF1 = f1 - (f2 + f3 + f4);

            float f5 = (float)Math.Pow(bF, 4);
            float f6 = (float)(4.0 * Math.Pow(aF, 3) * bF);
            float resultF2 = f5 + f6;

            float resultF = resultF1 / resultF2;

            Console.WriteLine("Результат double: " + resultD);
            Console.WriteLine("Результат float:  " + resultF);
        }
    }
}