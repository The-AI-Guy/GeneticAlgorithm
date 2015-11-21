using Interfaces;
using myFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsers
{
    public class CSVParser : IParser
    {
        public CSVParser()
        {

        }

        public IList<IDataSet<string>> Parse(StreamReader sr)
        {
            IList<IDataSet<string>> data = Factory.getObject<IList<IDataSet<string>>>();

            IDataItem<string> di = null;
            IDataSet<string> ds = null;

            string[] values;


            using (sr)
            {
                //remove headers
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    values = sr.ReadLine().Split(',');
                    ds = Factory.getObject<IDataSet<string>>();
                    //start at 1 to remove the reference
                    for(int i = 1; i < values.Length; i++)
                    {
                        di = Factory.getObject<IDataItem<string>>();                                                
                        di.setValue(values[i]);

                        ds.AddItem(di);                        
                    }

                    data.Add(ds);
                }
            }

            return data;
        }

        public string ReadLine(StreamReader sr)
        {
            if (!sr.EndOfStream)
                return sr.ReadLine();
            else
                throw new EndOfStreamException("End of File!");
        }

        public string[] SplitData(string row)
        {
            return row.Split(',');
        }


    }
}
