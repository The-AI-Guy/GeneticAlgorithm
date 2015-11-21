using GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using System.IO;
using myFactory;
using Parsers;

namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory.init();
            //testGA();
            //testFactory();
            TestFileGA();

            //wait
            Console.ReadLine();
        }

        public static void TestFileGA()
        {
            int pSize = 1000;
            double mutationRate = 0.05;



            IDataSet<string[]>[] ds = new IDataSet<string[]>[pSize];

            for(int i = 0; i < pSize; i++)
            {
                ds[i] = Factory.getObject<IDataSet<string[]>>();
            }

            IParser parser = new CSVParser();

            AccidentGA ga = new AccidentGA(ds, null, mutationRate, @"C:\Users\matt\Documents\GitHub\GeneticAlgorithm\Data\accidents2014.csv", parser);

            for (int i = 0; i < 1000; i++)
            {
                ga.Run();
                if (i % 10000 == 0)
                    Console.WriteLine(i);
            }
        }

        public static void testGA()
        {
            int aSize = 100;
            int iSize = 100;
            double mutationRate = 0.1;

            GA<float> test = new FloatGA(createDataSet<float>(aSize, iSize), Factory.getObject<IBreeder<float>>(), mutationRate);

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

        public static void TestParser()
        {
            StreamReader sr = new StreamReader(@"C:\Users\matt\Documents\GitHub\GeneticAlgorithm\Data\accidents2014.csv");
        }

        public static IDataSet<T>[] createDataSet<T>(int aSize, int iSize)
        {
            IDataSet<T> [] temp = new IDataSet<T>[aSize];
            Random r = new Random(DateTime.Now.Millisecond);

            for(int i = 0; i < aSize; i++)
            {
                temp[i] = Factory.getObject<IDataSet<T>>();
                temp[i].CreateItems(iSize);

                for (int j = 0; j < iSize; j++ )
                    temp[i].SetValue((T)Convert.ChangeType(r.NextDouble(), typeof(T)), j);
            }
            return temp;
        }

        public static void testFactory()
        {
            Factory.init();
            List<int> test1 = (List<int>)Factory.getObject<IList<int>>();
            IDataSet<int> test2 = Factory.getObject<IDataSet<int>>();
            IDataItem<int> test3 = Factory.getObject<IDataItem<int>>();
        }

    }
}
;