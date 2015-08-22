using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFactory
{
    public class myFactory
    {
        Dictionary<Type, object> objects;

        private myFactory()
        {
            objects = new Dictionary<Type, object>();

            AddRoute(typeof(IList<int>), new List<int>());
        }
        public void AddRoute(Type _interface, object _object)
        {
            objects.Add(_interface, _object);
        }

        public T getRoute<T>()
        {
            object o;
            objects.TryGetValue(typeof(T), out o);

            return (T)o;
        }

    }


}
