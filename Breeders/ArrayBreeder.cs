using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeders
{
    public class ArrayBreeder<T> : IBreeder<T>
    {
        public IDataSet<T> CreateChild(IDataSet<T> p1, IDataSet<T> p2)
        {
            if (p1.Length() != p2.Length())
                throw new Exception("parent sizes don't match");

            IDataSet<T> child = myFactory.Factory.getObject<IDataSet<T>>();

            for (int i = 0; i < p1.Length(); i++)
            {
                if (i % 2 == 0)
                    child.SetValue(p1.GetItem(i).getValue(), i);
                else
                    child.SetValue(p2.GetItem(i).getValue(), i);
            }

            return child;
        }

    }
}
