using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hibu.Sam.Concordance.Utilities
{
    public class Helper
    {
        public static string RootPath
        {
            get
            {
                return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }
    }
}
