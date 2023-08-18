using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class FileFormatException : Exception
    {
        public FileFormatException(string message) : base(message)
        {
        }
        public FileFormatException() { }

        public FileFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
