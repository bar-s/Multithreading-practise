using System;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    class Program
    {
        private static long n;
        static void Main(string[] args)
        {
            n = 0;

            Task.Run((Action)DoMagic);
            Task.Run((Action)DoMagic);

            while (true)
            {
                var x1 = Interlocked.Read(ref n);
                var x2 = Interlocked.Read(ref n);
                if (x1 > x2)
                    throw new ApplicationException(string.Format("x1:{0} - x2:{1} - diff: {2}", x1, x2, x2 - x1));
            }
        }

        private static void DoMagic()
        {
            while (true)
            {
                if (n < long.MaxValue)
                {
                    Interlocked.Add(ref n,1);
                    Console.WriteLine(n);
                }
                else
                {
                    n = 0;
                }
            }
        }
    }
}
