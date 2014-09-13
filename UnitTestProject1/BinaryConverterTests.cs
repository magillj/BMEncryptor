using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BMEncryptor;

namespace UnitTestProject1
{
    [TestClass]
    public class BinaryConverterTests
    {
        public readonly string SUPPORTED_CHARS =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890 .";

        BinaryConverter converter = new BinaryConverter();
        Random random = new Random();

        [TestMethod]
        public void convertToAndFromBinaryProperly()
        {
            string startText = "";

            Assert.AreEqual(startText,
                converter.convertBinaryToText(converter.convertTextToBinary(startText)));
        }

        [TestMethod]
        public void handleUnsupportedChars()
        {
            string startText = "|~`!@#$%^&*()_-+=;:'";

            Assert.AreEqual("                    ",
                converter.convertBinaryToText(converter.convertTextToBinary(startText)));
        }

        [TestMethod]
        public void convertsToBinaryFast()
        {
            string startText = "";
            // Generate start text with a lot of random, acceptable chars
            for (int i = 0; i < 10000; i++)
            {
                startText += SUPPORTED_CHARS[random.Next(0, SUPPORTED_CHARS.Length)];
            }

            DateTime before = DateTime.Now;
            converter.convertBinaryToText(converter.convertTextToBinary(startText));
            DateTime after = DateTime.Now;

            bool testPassed = false;
            if (before.Subtract(after) < TimeSpan.FromMilliseconds(5))
                testPassed = true;
            Assert.AreEqual(true, testPassed);
        }
    }
}
