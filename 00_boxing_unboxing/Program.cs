using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_boxing_unboxing
{
    class MyClass
    {
        public int Number { get; set; }

        public MyClass(int num = 0)
        {
            Number = num;
        }

        public static explicit operator int(MyClass @class)
        {
            return @class.Number;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // boxing - from value-type to reference-type
            // unboxing - wise-wersa

            object obj = new object();
            int number = 45;

            // boxing
            obj = (object)number;
            Console.WriteLine(obj.ToString());

            // unboxing
            number = (int)obj;
            Console.WriteLine(number);

            // boxing
            MyClass my = new MyClass(number);

            // unboxing
            int res = (int)my;
        }
    }
}
