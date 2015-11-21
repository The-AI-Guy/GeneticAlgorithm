using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IParser
    {
        IList<IDataSet<string>> Parse(StreamReader sr);
        string ReadLine(StreamReader sr);
        string[] SplitData(string row);

    }
}
