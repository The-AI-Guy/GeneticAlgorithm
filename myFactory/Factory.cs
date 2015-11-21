using Interfaces;
using myGeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFactory
{
    public static class Factory
    {
        static Dictionary<Type, Type> objects;
        static bool hasBeenInit;

        public static void init()
        {
            if (!hasBeenInit)
            {
                hasBeenInit = true;
                objects = new Dictionary<Type, Type>();
                AddRoutes();
            }
        }

        private static void AddRoutes()
        {            
            AddRoute(typeof(IList<>), typeof(List<>));
            AddRoute(typeof(IDataSet<>), typeof(myDataSet<>));
            AddRoute(typeof(IDataItem<>), typeof(myDataItem<>));
            AddRoute(typeof(IBreeder<>), typeof(myBreeder<>));
        }

        public static void AddRoute(Type _interface, Type _object)
        {
            if (hasBeenInit)
                objects.Add(_interface, _object);
            else
                throw new Exception("Factory has not been initiliased");
        }


        public static T getObject<T>()
        {
            if (hasBeenInit)
            {
                Type concrete;
                Type unbound = typeof(T).GetGenericTypeDefinition();

                if (objects.TryGetValue(unbound, out concrete))
                {
                    Type[] gargs = typeof(T).GenericTypeArguments;
                    var obj = concrete.MakeGenericType(gargs);                    
                    return (T)Activator.CreateInstance(obj);
                }
                else
                    return default(T);
            }
            else
            {
                throw new Exception("Factory has not been initiliased");
            }
        }

    }


}
