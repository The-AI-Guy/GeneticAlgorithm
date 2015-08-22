using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public abstract class GA<T>
    {
        protected Random r;
        protected IDataSet<T> [] data;

        public GA(IDataSet<T>[] ds)
        {
            data = ds;
            r = new Random(DateTime.Now.Millisecond);
        }

        public void AddItem(IDataSet<T> di)
        {
            IDataSet<T>[] newData = new IDataSet<T>[data.Length + 1];
            
            for(int i = 0; i < data.Length; i++)
            {
                newData[i] = data[i];
            }

            newData[newData.Length - 1] = di;
            
            data = newData;
        }

        private void Shuffle()
        {
            IDataSet<T> temp;

            for(int i = 0; i < data.Length; i++)
            {
                int source = r.Next(data.Length);
                int destination = r.Next(data.Length);

                temp = data[destination];

                data[destination] = data[source];
                data[source] = temp;
            }
        }

        public void Run()
        {
            //Print();            
            Breed(Evaluate());
            Shuffle();
            Print();
        }

        public abstract void Print();
        public abstract IDataSet<T>[] Evaluate();
        public abstract void Breed(IDataSet<T>[] winners);
        public abstract IDataSet<T> mutate(IDataSet<T> child);

    }
}
