using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hibu.Sam.Concordance.Models
{
    public class Dict
    {
        private Node rootNode;

        public Dict()
        {
            rootNode = new Node();
        }

        public void AddWord(string word, int sentenceNumber)
        {
            Node node = rootNode;
            Node newNode = null;

            for (int i = 0; i < word.Length; i++)
            {
                char c = word[i];

                newNode = new Node() { Key = c };

                var n = node.Children.Where(o => o.Key == c).FirstOrDefault();

                if (n == null)
                {
                    node.Children.Add(newNode);
                    newNode.Parent = node;
                    node = newNode;
                }
                else
                {
                    node = n;
                };
            }

            node.Times++;

            node.SentenceNumbers.Add(sentenceNumber);
        }

        public List<Word> GetWords()
        {
            List<Word> words = new List<Word>();

            Queue<Node> queue = new Queue<Node>();
            foreach (Node child in rootNode.Children)
            {
                queue.Enqueue(child);
            }

            while (queue.Count > 0)
            {
                Node node = queue.Dequeue();
                if (node.Times > 0)
                {
                    words.Add(new Word()
                    {
                        Letters = node.Letters,
                        Times = node.Times,
                        SentenceNumbers = node.SentenceNumbers
                    });
                }

                foreach (Node child in node.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return words;
        }

        public int MyProperty { get; set; }
    }
}
