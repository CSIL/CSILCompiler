using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lexer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer.Implementation.Tests
{
    [TestClass()]
    public class RegexCodeTokenizerTests
    {
        [TestMethod()]
        public void RegexCodeTokenizerTest()
        {
            RegexCodeTokenizer tokenizer = new RegexCodeTokenizer("aB19");
            Assert.AreNotEqual(null, tokenizer);
        }

        [TestMethod()]
        public void GetTest()
        {
            RegexCodeTokenizer tokenizer = new RegexCodeTokenizer("aB1");
            Assert.AreEqual(tokenizer.Get("[a-z]"), "a");
            Assert.AreEqual(tokenizer.Get("[A-Z]"), "B");
            Assert.AreEqual(tokenizer.Get("[0-9]"), "1");
        }
    }
}