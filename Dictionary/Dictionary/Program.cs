using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Dictionary
{
    public class CheckDictionary
    {
        public char value;
        public bool end = false;
        public Dictionary<char, CheckDictionary> children;

        public CheckDictionary()
        {
            this.children = new Dictionary<char, CheckDictionary>();
        }
        public CheckDictionary(char value)
        {
            this.value = value;
            this.children = new Dictionary<char, CheckDictionary>();
        }

        public bool ConatinsLetter(char next)
        {
            return this.children.ContainsKey(next);
        }
        private CheckDictionary AddLetter(char next)
        {
            this.children.Add(next, new CheckDictionary(next));
            return this.children[next];

        }
        public void AddWord(String key)
        {
            char[] splitWord = key.ToCharArray();
            CheckDictionary temp = this;
            foreach (char c in splitWord)
            {
                if (temp.ConatinsLetter(c))
                    temp = temp.children[c];
                else
                    temp = temp.AddLetter(c);
            }
            temp.end = true;

        }

        public bool CheckWord(String key)
        {
            char[] splitWord = key.ToCharArray();
            CheckDictionary temp = this;
            foreach (char c in splitWord)
            {
                if (temp.ConatinsLetter(c))
                    temp = temp.children[c];
                else
                    return false;
            }
            return temp.end;
        }

        public void LoadDictionary()
        {
            string filePath = ConfigurationSettings.AppSettings["FilePath"];
            List<string> words = new List<string>();
            Stopwatch ws = new Stopwatch();
            ws.Start();
            if (!string.IsNullOrEmpty(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    //sr.ReadToEnd();
                    String word;
                    while ((word = sr.ReadLine()) != null)
                    {
                        words.Add(word);
                    }
                }
            }
            ws.Stop();
            Console.WriteLine("Time for reading dictionary - " + ws.ElapsedMilliseconds + "ms");
            ws.Restart();
            foreach (string word in words)
            {
                this.AddWord(word);
            }
            ws.Stop();
            Console.WriteLine("Time for forming trie ds for dictionary  - " + ws.ElapsedMilliseconds + "ms");
        }

        private Dictionary<string, bool> GetInCorrectWords(string filePath)
        {
            List<string> words = new List<string>();
            StringBuilder s = new StringBuilder();
            Stopwatch ws = new Stopwatch();
            ws.Start();
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n', '"', '!' };
            if (!string.IsNullOrEmpty(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        words.AddRange(line.Split(delimiterChars));
                    }
                }
            }
            ws.Stop();
            Console.WriteLine("Time for reading incorrect file - " + ws.ElapsedMilliseconds + "ms");
            ws.Restart();
            Dictionary<string, bool> inCorrectWords = new Dictionary<string, bool>();
            foreach (string word in words)
            {
                if (!inCorrectWords.ContainsKey(word) && !this.CheckWord(word))
                {
                    inCorrectWords.Add(word, true);
                }
            }
            ws.Stop();
            Console.WriteLine("Time for finding incorrect words- " + ws.ElapsedMilliseconds + "ms");
            Console.WriteLine("Incorrect words count - " + inCorrectWords.Count());
            return inCorrectWords;
        }

        public static void Main(String[] args)
        {
            CheckDictionary completeDictionary = new CheckDictionary();
            completeDictionary.LoadDictionary();
            Dictionary<string, bool> inCorrectWords = new Dictionary<string, bool>();
            Console.WriteLine("Enter full file path to test - Press enter to choose dummy.txt from debug/App_Data folder");
            string filename = Console.ReadLine();
            if (String.IsNullOrEmpty(filename))
            {
                filename = ConfigurationSettings.AppSettings["FileToTest"];
            }
            inCorrectWords = completeDictionary.GetInCorrectWords(filename);
            foreach (KeyValuePair<string, bool> s in inCorrectWords)
            {
                Console.WriteLine(s.Key);
            }
            Console.ReadKey();
        }
        
    }
}
