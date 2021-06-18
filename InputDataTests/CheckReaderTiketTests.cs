using System;
using InputDataLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InputDataTests
{
    [TestClass]
    public class CheckReaderTiketTests
    {
        /// <summary>
        /// Тест метода  с корректными данными.
        /// </summary>
        [TestMethod]
        public void CheckFullReaderTicket_validData_returnTrue()
        {
            //Arrange
            string readerTicket = "О7985221";
            //Act
            CheckReaderTicket objectCheckReaderTicket = new CheckReaderTicket();
            bool result = objectCheckReaderTicket.CheckFullReaderTicket(readerTicket);
            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Тест метода CheckFullReaderTicket с некорректными символами.
        /// </summary>
        [TestMethod]
        public void CheckFullReaderTicket_invalidSymbol_returnException()
        {
            //Arrange
            string readerTicket = "О79aР@221";
            //Act
            CheckReaderTicket objectCheckReaderTicket = new CheckReaderTicket();
            //Assert
            Assert.ThrowsException<Exception>(() => objectCheckReaderTicket.CheckFullReaderTicket(readerTicket), "Использованы некорректные символы");
        }

        /// <summary>
        /// Тест метода CheckFullReaderTicket с некорректным кодом доступа.
        /// </summary>
        [TestMethod]
        public void CheckFullReaderTicket_invalidAccessRights_returnException()
        {
            //Arrange
            string readerTicket = "Я795461";
            //Act
            CheckReaderTicket objectCheckReaderTicket = new CheckReaderTicket();
            //Assert
            Assert.ThrowsException<Exception>(() => objectCheckReaderTicket.CheckFullReaderTicket(readerTicket), "Неправильный код доступа");
        }
    }
}
