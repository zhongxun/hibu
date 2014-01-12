using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hibu.Sam.Concordance.Models
{
    public class Node
    {
        public Char? Key { get; set; }
        public Node Parent { get; set; }
        public List<Node> Children { get; set; }
        public List<int> SentenceNumbers { get; set; }
        public int Times { get; set; }

        public string Letters
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Insert(0, Key);
                Node p = Parent;
                while (p != null)
                {
                    sb.Insert(0, p.Key);
                    p = p.Parent;
                }

                return sb.ToString();
            }
        }

        public Node()
        {
            Key = null;
            Children = new List<Node>();
            SentenceNumbers = new List<int>();
        }
    }
}
