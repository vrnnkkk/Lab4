using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lab4.Controllers
{
    public class DictionaryController : ApiController
    {
        private static Dictionary dict;

        static DictionaryController()
        {
            dict = new Dictionary();
            dict.AddWord(new Word("", "раст", "ен", "ие"));
            dict.AddWord(new Word("", "молок", "", "о"));
            dict.AddWord(new Word("", "молок", "", "а"));
        }

        [HttpGet]
        public IHttpActionResult GetDictionary()
        {
            return Ok<IEnumerable<Word>>(dict.Words.ToList());
            //return Json<IEnumerable<Word>>(dict.Words.ToList());
        }

        [HttpGet]
        public IHttpActionResult GetSameRootWords(string word)
        {
            var words = dict.Words.Where(w => word.Contains(w.Root)).ToArray();
            return Ok<IEnumerable<Word>>(words.ToList());
            //return Json<IEnumerable<Word>>(words);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Word word)
        {
            dict.AddWord(word);
            return Ok<IEnumerable<Word>>(dict.Words.ToList());
            //return Json<IEnumerable<Word>>(dict.Words.ToList());
        }

        [HttpPut]
        public IHttpActionResult Put(string word, [FromBody] Word updatedWord)
        {
            var existingWord = dict.Words.FirstOrDefault(w => w.FullWord == word);
            if (existingWord == null)
            {
                return NotFound();
            }

            existingWord.PutWord(updatedWord);
            dict.SortDictionary();
            return Ok<IEnumerable<Word>>(dict.Words.ToList());
            //return Json<IEnumerable<Word>>(dict.Words);
        }

        [HttpDelete]
        public IHttpActionResult DeleteWord(string wordToDelete)
        {
            var word = dict.Words.FirstOrDefault(w => w.FullWord == wordToDelete);

            if (word == null)
            {
                return NotFound();
            }

            dict.DeleteWord(word);
            //return Json<IEnumerable<Word>>(dict.Words);
            return Ok<IEnumerable<Word>>(dict.Words.ToList());
        }
    }
}
