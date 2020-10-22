using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.MSUnitTests
{
    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public void Log_ValidMessage_RaiseMessageLoggedEvent()
        {
            // Arrange
            Logger logger = new Logger();

            DateTime id = DateTime.MinValue;

            logger.MessageLogged += (sender, args) => { id = args.LogDate; };

            // Act
            logger.Log("a");

            // Assert
            Assert.AreNotEqual(DateTime.MinValue, id);
        }

        [TestMethod]
        public void Log_ValidMessage_SetLastMessage()
        {
            // Arrange
            Logger logger = new Logger();

            // Act
            logger.Log("a");

            // Assert
            Assert.AreEqual("a", logger.LastMessage);
        }
    }
}
