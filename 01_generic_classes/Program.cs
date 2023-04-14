using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_generic_classes
{
    // -----------
    //   Genrics
    // -----------
    /* Classes
     * Structures
     * Collections
     * Methods
     * Interfaces
     * Delegates
     */

    #region Generic Delegates --------------------------------------

    /// <typeparam name="T1">Type of first operand</typeparam>
    /// <typeparam name="T2">Type of second operand</typeparam>
    /// <returns>Is elements equals</returns>
    public delegate bool CompareDelegate<T1, T2>(T1 elem1, T2 elem2);
    public delegate Type OperationDelegate<Type>(Type a, Type b);
    public delegate float SummaDelegate2(float a, float b);
    public delegate int SummaDelegate(int a, int b);
    #endregion

    #region Generic Interface ----------------------
    interface IMyComparable<T>
    {
        void CompareMyObjects(T ojb1, T obj2);
    }

    interface IIndexer<T>
    {
        T this[int index] { get; set; }
    }

    class A : IMyComparable<A>
    {
        public void CompareMyObjects(A ojb1, A obj2)
        {
            throw new NotImplementedException();
        }
    }
    class B : IMyComparable<B>, IIndexer<string>
    {
        string[] lines;
        public string this[int index] { get => lines[index]; set => lines[index] = value; }

        public void CompareMyObjects(B ojb1, B obj2)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    // ---------------------------------------------------------------
    // We can set the limits to generic type by add "where T : limit"
    // limits:
    //      struct      - must be a value type
    //      class       - must be a reference type
    //      BaseClass   - must have this base class
    //      interface   - must implement this intefrace
    //      new()       - must have a default constructor (set to end)   

    #region Generic Class ----------------------------------------
    class MyArray<Type> : IIndexer<Type>
    {
        private Type[] array;

        public Type this[int index]
        {
            get
            {
                if (index < 0 || index >= array.Length)
                    throw new IndexOutOfRangeException();
                return array[index];
            }
            set
            {
                if (index < 0 || index >= array.Length)
                    throw new IndexOutOfRangeException();
                array[index] = value;
            }
        }
           
        public MyArray(int size = 10)
        {
            Random rnd = new Random();
            array = new Type[size];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = default(Type);
            }
        }

        public void AddElement(Type elem)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = elem;
        }

        public override string ToString()
        {
            string res = "";
            foreach (var item in array)
            {
                res += item + ", ";
            }
            return res;
        }
    }

    /// <summary>Generic Class for Point</summary>
    /// <typeparam name="T1">Type of X coord</typeparam>
    /// <typeparam name="T2">Type of Y coord</typeparam>
    class Point<T1, T2> where T1 : IComparable<T1>, new() 
                        where T2 : struct
    {
        // do not allow to use arif and logic operations for generic types: T1, T2
        public T1 X { get; set; }
        public T2 Y { get; set; }

        // Default ctor
        public Point()
        {
            //X = 0;       // error
            //Y = null;    // error

            // -------- Default values:
            // for reference: null
            // numbers: 0
            // boolean: false
            X = default(T1);
            Y = default;

            // with limited new()
            T1 item = new T1();

            // with interface limit
            item.CompareTo(X);
        }

        public Point(T1 x, T2 y)
        {            
            this.X = x;
            Y = y;

            // typeof - return Type of an element
            if (typeof(T1) == typeof(int))
            {
                Console.WriteLine("T1 is int");   
            }
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }

    class Vector<T> where T : struct, IComparable<T> // required by Point<T1, T2>
    {
        private Point<T, T>[] points;

        public Vector()
        {
            points = new Point<T, T>[3]
            {
                new Point<T, T>(default(T), default(T)),
                new Point<T, T>(default(T), default(T)),
                new Point<T, T>(default(T), default(T))
            };
        }

        public void Show()
        {
            Console.WriteLine("--------- Points ---------");
            foreach (var p in points)
            {
                Console.WriteLine(p);
            }
        }
    }
    #endregion

    class Program
    {        
        static void Main(string[] args)
        {
            /////// delegates
            CompareDelegate<double, decimal> compareDelegate = Compare;
            OperationDelegate<int> summaDelegate = Add;
            OperationDelegate<float> summaDelegate2 = Sub;

            Console.WriteLine(summaDelegate2.Invoke(3, 6));

            /////////// classes        

            MyArray<float> myArray = new MyArray<float>();
            myArray.AddElement(77.33F);
            myArray[0].ToString();
            Console.WriteLine(myArray.ToString());

            MyArray<bool> myArray2 = new MyArray<bool>();
            myArray2.AddElement(true);
            Console.WriteLine(myArray2.ToString());

            Point<int, double> p = new Point<int, double>(5, 6.7);
            Console.WriteLine(p);
            Console.WriteLine(p.GetType().ToString());
            Console.WriteLine(p.GetType().Name);

            Vector<double> vector = new Vector<double>();
            vector.Show();

            //////////// methods
            short a = 10;
            int b = 10;

            if (CompareHashCode<short, int>(a, b))
                Console.WriteLine("Hash codes equals!");

            ShowObject<int>(55);
            ShowObject<Point<int, int>>(new Point<int, int>());

            Mult<int, double, decimal>(5, 1.5);

            // pause
            Console.ReadKey();
        }

        #region Generic Method ------------------------
        static void ShowObject<T>(T obj)
        {
            Console.WriteLine(obj.ToString());
        }

        static bool CompareHashCode<T1, T2>(T1 obj1, T2 obj2)
        {
            return obj1.GetHashCode() == obj2.GetHashCode();
        }

        static TRes Mult<T1, T2, TRes>(T1 op1, T2 op2)
        {
            TRes result = default(TRes);
            return result;
        }
        #endregion

        static int Add(int a, int b) => a + b;
        static float Sub(float a, float b) => a - b;
        static bool Compare(double n1, decimal n2) => (decimal)n1 == n2;
    }
}
