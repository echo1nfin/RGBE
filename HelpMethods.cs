using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rgbEncryptor
{
    internal class HelpMethods
    {
        public static byte CircleNumber(int number)
        {
            return (byte)((number % 256 + 256) % 256);
        }
    }
}
