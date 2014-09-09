using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BMEncryptor;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        BinaryConverter converter = new BinaryConverter();
        EncryptionEngine en = new EncryptionEngine();

        [TestMethod]
        public void convertToAndFromBinaryProperly()
        {
            string startText = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890 .";

            Assert.AreEqual(startText, converter.convertBinaryToText(converter.convertTextToBinary(startText)));
        }

        [TestMethod]
        public void handleUnsupportedChars()
        {
            string startText = "|~`!@#$%^&*()_-+=;:'";

            Assert.AreEqual("                    ", converter.convertBinaryToText(converter.convertTextToBinary(startText)));
        }

        [TestMethod]
        public void convertsToBinaryFast()
        {
            string startText = "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";
            DateTime before = DateTime.Now;
            converter.convertBinaryToText(converter.convertTextToBinary(startText));
            DateTime after = DateTime.Now;

            bool testPassed = false;
            if (before.Subtract(after) < TimeSpan.FromMilliseconds(5))
                testPassed = true;
            Assert.AreEqual(true, testPassed);
        }

        [TestMethod]
        public void scrambleText()
        {
            bool passedTest = true;
            string startText = "Hello. This is a test";
            string endText = en.scrambleLetters(startText);

            if (startText == endText)
                passedTest = false;
            for (int i = 0; i < startText.Length; i++)
                if (!endText.Contains(startText.Substring(i, 1)))
                    passedTest = false;

            Assert.AreEqual(true, passedTest);
        }

        [TestMethod]
        public void unScrambleText()
        {
            string text = "Hello this is a test";

            string before = en.scrambleLetters(text);

            string after = en.unScrambleLetters(before);

            Assert.AreEqual(text, after);
        }

        [TestMethod]
        public void randomizeAndDeRandomizeText()
        {
            bool[] key = {true,true,false,true,false,false};
            string text = "Hello. This is a test";
            string randomizedText = en.randomizeText(text, key);
            string deRandomizedText = en.deRandomizeText(randomizedText, key);            

            Assert.AreEqual(text, deRandomizedText);
            
        }
    }
}
