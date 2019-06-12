using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04PrimeIndexer
{

    public class RandomResponse {
        private Random r = new Random();
        public int Value {
            get {
                return r.Next(100000);
            }
            set
            {
                r = new Random(value);
            }
        }
    }


    enum DOW { Monday, Tuesday, Wednesday }

    public class Schedule
    {
        HashSet<DOW> WorkdaysList;
    }


    public class TenStorage<T>
    {
        private T[] strArr = new T[10];

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= strArr.Length)
                    throw new IndexOutOfRangeException("Cannot store more than 10 objects");

                return strArr[index];
            }
            set
            {
                if (index < 0 || index >= strArr.Length)
                    throw new IndexOutOfRangeException("Cannot store more than 10 objects");

                strArr[index] = value;
            }
        }
    }


    public class String10Storage
    {
        private string[] strArr = new string[10];

        public string this[int index]
        {
            get
            {
                if (index < 0 || index >= strArr.Length)
                    throw new IndexOutOfRangeException("Cannot store more than 10 objects");

                return strArr[index];
            }
            set
            {
                if (index < 0 || index >= strArr.Length)
                    throw new IndexOutOfRangeException("Cannot store more than 10 objects");

                strArr[index] = value;
            }
        }
    }

    public class PrimeArray
    {
        public delegate void LoggerDelegateType(int n, long prime, string msg);
        public LoggerDelegateType Logger;


        public void doit()
        {
            Logger?.Invoke(5, 5, "Message");
        }

        public bool this[long index]
        {
            get
            {
                if (index <= 0) throw new IndexOutOfRangeException();
                return IsPrime(index);
            }
        }

        private bool IsPrime(long n)
        {
            long m = n / 2;
            for (long i = 2; i <= m; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

    }



    class Program
    {
        static void DisplayMessage(int n, long prime, string msg) { }
        static void SaveMessage(int n, long prime, string msg) {  }

        static void Main(string[] args)
        {
            PrimeArray pa = new PrimeArray();

            Random random = new Random();
            if (random.Next(2) == 0)
            {
                pa.Logger += DisplayMessage;
            }
            if (random.Next(2) == 0)
            {
                pa.Logger += SaveMessage;
            }

            bool isIt = pa[5];

            /*

            String10Storage strStore = new String10Storage();

            strStore[0] = "One";
            strStore[1] = "Two";
            strStore[2] = "Three";
            strStore[3] = "Four";

            strStore[7] = "Seven";

            for (int i = 0; i < 10; i++)
                Console.WriteLine(strStore[i]);


            TenStorage<int> tenStoreInts = new TenStorage<int>();

            tenStoreInts[5] = 3443;
            */

            Console.ReadKey();
        }
    }
}
