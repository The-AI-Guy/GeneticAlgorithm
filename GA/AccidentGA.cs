using Interfaces;
using myFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public class AccidentGA : GA<string[]>
    {
        private string filePath;
        private IParser parser;
        private IDataSet<string>[] fileData;
        private double[] percMatches;

        public AccidentGA(IDataSet<string[]>[] ds, IBreeder<string[]> _breeder, double mutationRate, string _filePath, IParser _parser, bool prefilleddata = false) 
            :base(ds, _breeder, mutationRate)
        {
            filePath = _filePath;
            breeder = _breeder;
            parser = _parser;

            percMatches = new double[data.Length];

            if (ds == null)
                throw new Exception("Must initialise a dataset");

            if(!prefilleddata)
                GetData();
        }        

        private void GetData()
        {
            fileData = parser.Parse(new StreamReader(filePath)).ToArray();            

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = getRandomSet();
            }             
        }

        private IDataSet<string[]> getRandomSet()
        {
            IDataSet<string[]> temp = Factory.getObject<IDataSet<string[]>>();

            temp.AddItem(getRandomData());

            IDataItem<string[]> di = Factory.getObject<IDataItem<string[]>>();

            di.setValue(new string[3]);

            di.getValue()[0] = r.Next(1, 4).ToString();

            temp.AddItem(di);

            temp.AddItem(getRandomData());

            if (temp.GetItem(0).getValue() != temp.GetItem(2).getValue())
                return temp;
            else
                return getRandomSet();
        }

        private IDataItem<string[]> getRandomData()
        {
            int row, col;

            row = r.Next(fileData.Length);
            col = r.Next(fileData[0].Length());

            IDataItem<String[]> di = Factory.getObject<IDataItem<string[]>>();

            di.setValue(new string[3]);
            di.getValue()[0] = col.ToString();
            di.getValue()[1] = fileData[row].GetItem(col).getValue();

            return di;
        }

        public override IDataSet<string[]>[] Evaluate()
        {
            IDataSet<string[]>[] winners = new IDataSet<string[]>[data.Length/2];

            for(int i = 0; i < data.Length; i++)
            {
                int item = int.Parse(data[i].GetItem(1).getValue()[0]);

                switch (item)
                {
                    case 1: percMatches[i] = GetMatches(data[i]); break;
                    case 2: percMatches[i] = GetMatches(data[i]); break; 
                    case 3: percMatches[i] = GetMatches(data[i]); break;  
                }
            }

            int count = 0;

            for (int i = 0; i < percMatches.Length; i += 2)
            {
                double a = percMatches[i];
                double b = percMatches[i + 1];

                if (a > b)
                    winners[count] = data[i];
                else
                    winners[count] = data[i + 1];

                count++;
            }
            return winners;
        }

        
        private double GetMatches(IDataSet<string[]> di)
        {
            IList<int> matches = Factory.getObject<IList<int>>();

            for(int i = 0; i < fileData.Length; i++)
            {
                int col = int.Parse(di.GetItem(0).getValue()[0]);
                string val = di.GetItem(0).getValue()[1];

                if (fileData[i].GetItem(col).getValue() == val)
                {
                    matches.Add(i);
                }
            }
          
            for(int i = 0; i < matches.Count; i++)
            {
                int col = int.Parse(di.GetItem(2).getValue()[0]);
                string val = di.GetItem(2).getValue()[1];

                if (fileData[matches[i]].GetItem(col).getValue() != val)
                {
                    matches.RemoveAt(i);
                }
            }

            return ((double)matches.Count / (double)fileData.Length) * 100;
        }

        public override IDataSet<string[]> mutate(IDataSet<string[]> child)
        {
            int n = r.Next(0,3);

            if(n == 1)
            {
                child.GetItem(1).getValue()[0] = r.Next(1, 4).ToString();
            }
            else
            {
                IDataItem<string[]> di = getRandomData();

                if (di.getValue()[0] == child.GetItem(n).getValue()[0])
                    child = mutate(child);
                else
                    child.GetItem(n).setValue(di.getValue());
            }

            return child;
        }

        public override void Breed(IDataSet<string[]>[] winners)
        {
            int last = 0;

            for (int i = 0; i < winners.Length; i += 2)
            {
                last = AssignChild(getRandomSet(), winners, last);
                last = AssignChild(getRandomSet(), winners, last);
            }
        }

        public override void Print()
        {
            string[] headers;

            using (StreamReader sr = new StreamReader(filePath))
            {
                headers = parser.SplitData(parser.ReadLine(sr));                
            }

            using (StreamWriter sw = new StreamWriter(@"C:\GAOutput", true))
            {
                for (int i = 0; i < data.Length; i++)
                {
                    sw.Write(percMatches[i].ToString() + "% ");

                    sw.Write(headers[int.Parse(data[i].GetItem(0).getValue()[0]) + 1]);
                    sw.Write(" ");
                    sw.Write(data[i].GetItem(0).getValue()[1].ToString());

                    switch(data[i].GetItem(1).getValue()[0])
                    {
                        case "1": sw.Write(" are equal to "); break;
                        case "2": sw.Write(" are equal to "); break;
                        case "3": sw.Write(" are equal to "); break;
                        default: throw new Exception("not 1-3");
                    }

                    sw.Write(headers[int.Parse(data[i].GetItem(2).getValue()[0]) + 1]);
                    sw.Write(" ");
                    sw.Write(data[i].GetItem(2).getValue()[1].ToString());
                    sw.WriteLine();           
                }
            }
        }
        private enum Comparisons
        {
            EqualTo = 1,
            LessThan = 2,
            GreaterThan = 3
        }
    }
}
