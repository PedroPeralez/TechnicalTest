using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TT.Repository
{
    //This class serves as an interface, although a proper interface was not possible since interfaces don't
    //allow for static methods.
    public class Repository<T>
    {
        public static T GetById(int ID)
        {
            throw (new Exception("Not implemented"));
        }

        public static IEnumerable<T> GetAll()
        {
            throw (new Exception("Not implemented"));
        }

        public static bool Update(T user)
        {
            throw (new Exception("Not implemented"));
        }

        public static int Insert(T user)
        {
            throw (new Exception("Not implemented"));
        }

        public static bool Delete(T user)
        {
            throw (new Exception("Not implemented"));
        }
    }
}