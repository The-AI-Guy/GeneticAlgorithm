﻿using Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public class FloatGA : GA<float>
    {
        
        public FloatGA(IDataSet<float>[] ds, IBreeder<float> _breeder, double mutationRate)
            : base(ds,_breeder, mutationRate)
        {
            breeder = _breeder;
        }

        public override IDataSet<float>[] Evaluate()
        {
            float[] valuations = new float[data.Length];            

            for (int i = 0; i < data.Length; i++ )
            {
                for (int j = 0; j < data[i].GetItems().Count; j++)
                    valuations[i] += (float)data[i].GetItem(j).getValue();
            }

            IDataSet<float>[] winners = new IDataSet<float>[valuations.Length / 2];

            int count = 0;

            for (int i = 0; i < valuations.Length; i += 2)
            {
                float a = valuations[i];
                float b = valuations[i+1];

                if(a > b)
                    winners[count] = data[i];
                else
                    winners[count] = data[i+1];

                count++;
            }

            return winners;
        }

        public override IDataSet<float> mutate(IDataSet<float> child)
        {
            if(r.NextDouble() < mutationRate )
            {
                int index = r.Next(data[0].Length());

                child.SetValue((float)r.NextDouble(), index);
            }

            return child;
        }

        public override void Print()
        {
            float overall = 0;

            using (StreamWriter sw = new StreamWriter(@"C:\GAOutput", true))
            {
                for (int i = 0; i < data.Length; i++)
                {
                    float total = 0;

                    for (int j = 0; j < data[i].Length(); j++)
                    {
                        overall += data[i].GetItem(j).getValue();
                        total += data[i].GetItem(j).getValue();
                    }

                    //sw.WriteLine(total);
                }
                sw.WriteLine("AVERAGE : " + overall / data.Length);
                //sw.WriteLine("---------------------------------------------------");
            }
        }
    }
}
