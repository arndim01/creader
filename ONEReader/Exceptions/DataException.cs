using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONEReader.Exceptions
{
    public abstract class DataException: ArgumentException
    {
        public DataException() : base()
        {

        }
        public DataException(string message) : base(message)
        {

        }
        public DataException(string message, Exception innerException):base(message, innerException)
        {

        }
    }
}
