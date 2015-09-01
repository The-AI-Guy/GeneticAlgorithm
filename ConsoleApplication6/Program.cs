using GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using System.IO;
using myFactory;

namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
            //testGA();
            testFactory();


            //wait
            Console.ReadLine();
        }

        public static void testGA()
        {
            int aSize = 100;
            int iSize = 100;
            double mutationRate = 0.1;

            GA<float> test = new FloatGA(createDataSet<float>(aSize, iSize), Factory.getRoute<IBreeder<float>>(), mutationRate);

            DirectoryInfo di = new DirectoryInfo(@"C:\");

            try
            {
                di.GetFiles().First(f => f.Name == "GAOutput").Delete();
            }
            catch
            {
                //no matching elements
            }

            for (int i = 0; i < 2000000; i++)
            {
                test.Run();
                if (i % 10000 == 0)
                    Console.WriteLine(i);
            }

            Console.WriteLine("done!");
            
        }

        public static IDataSet<T>[] createDataSet<T>(int aSize, int iSize)
        {
            IDataSet<T> [] temp = new IDataSet<T>[aSize];
            Random r = new Random(DateTime.Now.Millisecond);

            for(int i = 0; i < aSize; i++)
            {
                temp[i] = Factory.getRoute<IDataSet<T>>();
                temp[i].CreateItems(iSize);

                for (int j = 0; j < iSize; j++ )
                    temp[i].SetValue((T)Convert.ChangeType(r.NextDouble(), typeof(T)), j);
            }
            return temp;
        }

        public static void testFactory()
        {
            Factory.init();
            List<int> test = (List<int>)Factory.getRoute<IList<int>>();
        }

    }
}
