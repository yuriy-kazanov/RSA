using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            long q = 541;
            long p = 617;
            long n = p * q;
            long fi = (q - 1) * (p - 1);
            var list = new List<long>();
            for (int i = 3; i < fi; i += 2)
            {
                if (fi % i == 0)
                {
                    continue;
                }
                if (isPrime(i))
                {
                    if (NOD(i, fi) == 1)
                    {
                        list.Add(i);
                    }
                }
            }
            Random rd = new Random();
            long e = list[rd.Next(0, list.Count)];
            long d = 3;
            while ((d * e - 1) % fi != 0 || d == e)
            {
                d++;
            }
            Console.Write("Введите сообщение (число < {0}): ", n - 1);
            long message = Convert.ToInt64(Console.ReadLine());
            long cifr = Pow(message, e, n);
            First(cifr, n);

            Console.ReadKey();
        }

        static void First(long cifr, long n)
        {
            long x = Convert.ToInt64(Math.Floor(Math.Sqrt(n))) + 1; 
            for (int k = 0; k < n; k++)
            {
                long y = (x + k) * (x  + k) - n;
                long x_y = Convert.ToInt64(Math.Floor(Math.Sqrt(y)));
                if (x_y * x_y == y)
                {
                    long X = x + k;
                    long Y = Convert.ToInt64(Math.Sqrt(X * X - n));
                    Console.WriteLine("p = {0}", X + Y);
                    Console.WriteLine("q = {0}", X - Y);
                    Console.WriteLine(k);
                }
            }
        }


        static bool isPrime(long x)
        {
            if (x == 2)
                return true;
            if (x == 0 || x == 1 || x % 2 == 0)
                return false;
            long i = 3;
            while (i * i <= x && x % i != 0)
            {
                i += 2;
            }
            return i * i > x;
        }

        static long NOD(long x, long y)
        {
            if (y == 0)
                return x;
            return NOD(y, x % y);
        }

        static long Pow(long x, long y, long n)
        {
            if (y == 0)
                return 1;
            if (y % 2 == 0)
            {
                long t = Pow(x, y / 2, n);
                return mul(t, t, n) % n;
            }
            return (mul(Pow(x, y - 1, n), x, n)) % n;
        }

        static long mul(long x, long y, long n)
        {
            if (y == 1)
                return x;
            if (y % 2 == 0)
            {
                long t = mul(x, y / 2, n);
                return (2 * t) % n;
            }
            return (mul(x, y - 1, n) + x) % n;
        }
    }
}
