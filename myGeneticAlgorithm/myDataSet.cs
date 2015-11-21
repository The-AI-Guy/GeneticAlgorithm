using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace myGeneticAlgorithm
{
    public class myDataSet<T> : IDataSet<T>
    {
        public List<IDataItem<T>> data { get; set; }

        public myDataSet()
        {
            data = new List<IDataItem<T>>();
        }

        public myDataSet(int size)
        {
            data = new List<IDataItem<T>>();

            InitData(size);
        }

        private void InitData(int size)
        {
            for (int i = 0; i < size; i++)
            {
                data.Add(new myDataItem<T>());
            }
        }

        public void SetItem(IDataItem<T> item, int index)
        {
            data[index] = item;
        }

        public void SetValue(T value, int index)
        {
            data[index].setValue(value);
        }

        public IDataItem<T> GetItem(int index)
        {
            return data.ElementAt(index);
        }

        public void AddItem(IDataItem<T> item)
        {
            data.Add(item);
        }

        public void CreateItems(int size)
        {
            InitData(size);
        }

        public IList<IDataItem<T>> GetItems()
        {
            return data;
        }

        public int Length()
        {
            return data.Count;
        }        
    }
}
