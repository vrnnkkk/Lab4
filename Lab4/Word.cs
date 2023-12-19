using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Word
    {
        public string Prefix { get; private set; }
        public string Root { get; private set; }
        public string Suffix { get; private set; }
        public string Ending { get; private set; }

        public string FullWord
        {
            get
            {
                return Prefix + Root + Suffix + Ending;
            }
        }

        public string SplittedWord { get; private set; }
        public int Count { get; private set; }

        //конструктор: получает prefix, root, suffix, ending и складывает их в одно слово 
        public Word(string prefix, string root, string suffix, string ending)
        {
            Prefix = "";
            Root = "";
            Suffix = "";
            Ending = "";

            SplittedWord = "";

            Count = 0;

            if (!string.IsNullOrEmpty(prefix))
            {
                Prefix = prefix;
                SplittedWord += prefix + '-';
                Count += 1;
            }

            if (!string.IsNullOrEmpty(root))
            {
                Root = root;
                Count += 1;
                if (!string.IsNullOrEmpty(suffix) || !string.IsNullOrEmpty(ending))
                    SplittedWord += root + '-';
                else
                    SplittedWord += root;
            }

            if (!string.IsNullOrEmpty(suffix))
            {
                Suffix = suffix;
                Count += 1;
                if (!string.IsNullOrEmpty(ending))
                    SplittedWord += suffix + '-';
                else
                    SplittedWord += suffix;
            }

            if (!string.IsNullOrEmpty(ending))
            {
                Ending = ending;
                Count += 1;
                SplittedWord += ending;
            }
        }

        //метод сравнения переданного слово с данным по корню
        public bool HasTheSameRoot(Word word)
        {
            return word.Root.Equals(Root);
        }

        //метод замены слова
        public void PutWord(Word updatedWord)
        {
            Prefix = updatedWord.Prefix;
            Root = updatedWord.Root;
            Suffix = updatedWord.Suffix;
            Ending = updatedWord.Ending;
            SplittedWord = updatedWord.SplittedWord;
            Count = updatedWord.Count;
        }

        //метод сравнения переданного слово с данным целиком
        public bool CompareWord(string w)
        {
            return string.Equals(FullWord, w, StringComparison.CurrentCultureIgnoreCase);
        }

    }
}
