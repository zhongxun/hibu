using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hibu.Sam.Concordance.Parsers
{
    public class EnglishParser
    {
        readonly char[] PunctuationChars = { '"', ',', '!', '?' };  //'\'',
        readonly char[] EndingChars = { ' ', '.', '!', '?', '/' };
        readonly byte[] Utf8BomArr = new byte[3] { 239, 187, 191 };

        private FileStream fileStream;
        private TextReader readFile;
        private string line;
        private int pointer;
        private int sentenceNumber;

        private List<ParserWord> words;

        public List<ParserWord> Words
        {
            get
            {
                return words;
            }
        }

        public EnglishParser(FileStream fileStream)
        {
            this.fileStream = fileStream;
            readFile = new StreamReader(this.fileStream);
            words = new List<ParserWord>();

            byte[] headerArr = new byte[3];

            fileStream.Read(headerArr, 0, 3);

            if (headerArr.SequenceEqual<byte>(Utf8BomArr))
            {
                // this is a UTF-8 file
            }
            else
            {
                fileStream.Position = 0;
            }
        }

        ~EnglishParser()
        {
            if (readFile != null)
            {
                readFile.Close();
            }

            if (fileStream != null)
            {
                fileStream.Close();               
            }
        }

        public ParserWord GetNextWord()
        {
            ParserWord word = null;

            if (string.IsNullOrEmpty(line) || pointer == line.Length)
            {
                //line = readFile.ReadLine();
                //sentenceNumber++;
                //while (line != null && line.Length == 0)
                //{
                //    line = readFile.ReadLine();
                //    sentenceNumber++;
                //}

                line = GetNextSentence();
                pointer = 0;
            }

            if (line != null)
            {
                line = line.ToLower().Trim();
                line = line.Replace("--", " ");
                line = line.Replace("_", " ");
                line = line.Replace("  ", " ");
                line = line.Replace("*", " ");

                StringBuilder sb = new StringBuilder();

                for (int i = pointer; i < line.Length; i++)
                {
                    char c = line[pointer++];
                    if (EndingChars.Contains(c) || pointer == line.Length)
                    {
                        word = new ParserWord();
                        word.Letters = sb.ToString().Replace("'s", "").Replace("'", "");
                        word.SentenceNumber = sentenceNumber;

                        break;
                    }
                    else
                    {
                        if (!PunctuationChars.Contains(c))
                        {
                            sb.Append(c);
                        }
                    }
                }
            }

            return word;
        }

        private string GetNextSentence()
        {
            string line = null;
            line = readFile.ReadLine();
            sentenceNumber++;
            while (line != null && line.Length == 0)
            {
                line = readFile.ReadLine();
                sentenceNumber++;
            }

            // only keep one space for multiple space
            // replace \r\n

            //fileStream.ReadByte();
            return line;
        }
    }
}
