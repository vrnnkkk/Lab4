using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab4
{
    public class Dictionary
    {
        //совокупность объектов класса Word 
        private readonly List<Word> dictionary = new List<Word>();

        public IEnumerable<Word> Words
        {
            get
            {
                return dictionary;
            }

        }

        //метод добавления нового слова 
        public void AddWord(Word word)
        {
            dictionary.Add(word);
            SortDictionary();
        }

        //метод для удаления слова 
        public void DeleteWord(Word word)
        {
            dictionary.Remove(word);
            SortDictionary();
        }

        //метод поиска индекса переданного слова в словаре
        public Word GetWordByText(String word)
        {
            return dictionary.FirstOrDefault(w => w.CompareWord(word));
        }

        public void SortDictionary()
        {
            dictionary.Sort((x, y) => x.Count.CompareTo(y.Count));
        }
    }
}
