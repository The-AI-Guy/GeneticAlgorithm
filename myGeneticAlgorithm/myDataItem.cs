using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myGeneticAlgorithm
{
    public class myDataItem<T> : IDataItem<T>
    {
        T item;

        public myDataItem()
        {
            
        }
        public myDataItem(T val)
        {
            item = val;
        }

        public T getValue()
        {
            return item;
        }
       
        public void setValue(T value)
        {
            item = value;
        }
    }
}
