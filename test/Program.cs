using System;
using System.Data;
using System.IO;
using common;

namespace test
{
    public class Program
    {
        static void Main(string[] args)
        {
            int a = 1,b=2;
            new Program().permute(a,b);
            Console.WriteLine("&1+++++&2",a,b);
            Console.ReadKey();
        }
        public int permute(int Origin,int Target) {
            var temp = Origin;
            Origin = Target;
            Target = temp;
            return 0;
        }
        
    }
}
