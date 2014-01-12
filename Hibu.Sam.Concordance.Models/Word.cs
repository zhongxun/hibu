using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hibu.Sam.Concordance.Models
{
    public class Word
    {
        public string Letters { get; set; }
        public int Times { get; set; }
        public List<int> SentenceNumbers { get; set; }
    }
}
