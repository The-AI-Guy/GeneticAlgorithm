using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IBreeder<T>
    {
        IDataSet<T> CreateChild(IDataSet<T> p1, IDataSet<T> p2);
    }
}
