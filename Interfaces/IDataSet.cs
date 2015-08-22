using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDataSet<T>
    {
        IDataItem<T> GetItem(int index);
        IList<IDataItem<T>> GetItems();
        
        void AddItem(IDataItem<T> item);
        void SetValue(T value, int index);

        int Length();
    }
}
