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
        protected IBreeder<T> breeder;
        protected double mutationRate;

        public GA(IDataSet<T>[] ds, IBreeder<T> _breeder, double _mutationRate)
        {
            data = ds;
            breeder = _breeder;
            mutationRate = _mutationRate;

            myFactory.Factory.init();

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
            Print();            
            Breed(Evaluate());
            Shuffle();
            //Print();
        }

        public abstract void Print();
        public abstract IDataSet<T>[] Evaluate();
        
        public abstract IDataSet<T> mutate(IDataSet<T> child);

        public virtual void Breed(IDataSet<T>[] winners)
        {
            int last = 0;

            for (int i = 0; i < winners.Length; i += 2)
            {
                IDataSet<T> p1 = winners[i];
                IDataSet<T> p2 = winners[i + 1];

                IDataSet<T> c1 = breeder.CreateChild(p1, p2);
                IDataSet<T> c2 = breeder.CreateChild(p2, p1);

                last = AssignChild(c1, winners, last);
                last = AssignChild(c2, winners, last);
            }
        }

        protected int AssignChild(IDataSet<T> child, IDataSet<T>[] winners, int last)
        {
            int lastloser = last;

            for (int i = last; i < data.Length; i++)
            {
                if (winners.Contains(data[i]))
                    continue;
                else
                {
                    data[i] = mutate(child);
                    lastloser = i + 1;
                    break;
                }
            }

            return lastloser;
        }

    }
}
