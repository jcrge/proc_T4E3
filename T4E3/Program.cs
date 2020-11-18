using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace T4E3
{
    class Program
    {
        private static readonly object l = new object();
        private delegate int UpdateVarOp(int n);

        static void Main(string[] args)
        {
            int n = 0;
            Thread t1 = new Thread(() => UpdateVar(ref n, m => m + 1));
            Thread t2 = new Thread(() => UpdateVar(ref n, m => m - 1));

            t1.Start();
            t2.Start();
        }

        private static void UpdateVar(ref int n, UpdateVarOp op)
        {
            Func<int, bool> stopCond = m => Math.Abs(m) == 1000;

            while (!stopCond(n))
            {
                lock (l)
                {
                    if (!stopCond(n))
                    {
                        n = op(n);
                        Console.SetCursorPosition(1, 1);
                        Console.Write("{0,-5}", n);
                    }
                }
            }
        }
    }
}
