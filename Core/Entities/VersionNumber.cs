using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class VersionNumber
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }


        public VersionNumber(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }
        public VersionNumber(int firstNumber,int secondNumber,int major, int minor)
        {
            Major = major;
            Minor = minor;
            FirstNumber = firstNumber;
            SecondNumber = secondNumber;    
        }
    }
}
