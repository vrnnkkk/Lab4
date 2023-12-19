using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Lab4;
using Lab4.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace UnitTests
{
    public class DictionaryControllerTests
    {
        [Fact]
        public void TestGetDictionary()
        {
            var controller = new DictionaryController();

            var result = controller.GetDictionary();
            if (result is OkNegotiatedContentResult<List<Word>> okResult)
            {
                var words = okResult.Content;
                Assert.NotNull(words);
                Assert.Equal(3, words.Count());
            }
        }

        [Theory]
        [InlineData("растение", 1)]
        [InlineData("молоко", 2)]
        public void TestGetSameRootWords(string inputWord, int expectedCount)
        {
            var controller = new DictionaryController();

            var result = controller.GetSameRootWords(inputWord) as OkNegotiatedContentResult<IEnumerable<Word>>;
            var words = result?.Content;

            Assert.NotNull(result);
            Assert.NotNull(words);
            Assert.Equal(expectedCount, words.Count());
        }

        [Fact]
        public void TestPost()
        {
            var controller = new DictionaryController();
            var word = new Word("", "test", "", "word");

            var result = controller.Post(word) as OkNegotiatedContentResult<IEnumerable<Word>>;
            var words = result?.Content;

            Assert.NotNull(result);
            Assert.NotNull(words);
            Assert.True(words.Any(w => w.FullWord == word.FullWord));
        }

        [Fact]
        public void TestPut()
        {
            var controller = new DictionaryController();
            var initialWord = new Word("", "initial", "", "word");
            controller.Post(initialWord);

            var updatedWord = new Word("", "updated", "", "word");

            var result = controller.Put(initialWord.FullWord, updatedWord) as OkNegotiatedContentResult<IEnumerable<Word>>;
            var words = result?.Content;

            Assert.NotNull(result);
            Assert.NotNull(words);
            Assert.True(words.Any(w => w.FullWord == updatedWord.FullWord));
        }

        [Fact]
        public void TestDeleteWord()
        {
            var controller = new DictionaryController();
            var wordToDelete = new Word("", "delete", "", "word");
            controller.Post(wordToDelete);

            var result = controller.DeleteWord(wordToDelete.FullWord);

            if (result is OkNegotiatedContentResult<IEnumerable<Word>> okResult)
            {
                var words = okResult.Content;
                Assert.NotNull(words);
                Assert.False(words.Any(w => w.FullWord == wordToDelete.FullWord));
            }
            else
            {
                Assert.True(false, $"Unexpected result type: {result.GetType()}");
            }
        }
    }
}
