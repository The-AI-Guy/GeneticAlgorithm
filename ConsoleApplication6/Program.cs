using GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using System.IO;

namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
            int aSize = 100;
            int iSize = 100;

            GA<float> test = new FloatGA(createDataSet<float>(aSize, iSize), new myBreeder<float>());

            DirectoryInfo di = new DirectoryInfo(@"C:\");
            di.GetFiles().First(f => f.Name == "GAOutput").Delete();

            for (int i = 0; i < 2000000; i++)
            {
                test.Run();
                if (i % 10000 == 0)
                    Console.WriteLine(i);
            }

            Console.WriteLine("done!");
            Console.ReadLine();
        }

        public static IDataSet<T>[] createDataSet<T>(int aSize, int iSize)
        {
            IDataSet<T> [] temp = new IDataSet<T>[aSize];
            Random r = new Random(DateTime.Now.Millisecond);

            for(int i = 0; i < aSize; i++)
            {
                temp[i] = new myDataSet<T>(iSize);
                for (int j = 0; j < iSize; j++ )
                    temp[i].SetValue((T)Convert.ChangeType(r.NextDouble(), typeof(T)), j);
            }
            return temp;
        }
    }
}
