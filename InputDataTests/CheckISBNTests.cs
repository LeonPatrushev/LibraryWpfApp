using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InputDataLibrary;

namespace InputDataTests
{
    [TestClass]
    public class CheckISBNTests
    {
        /// <summary>
        /// Тест метода CheckFullISBN с корректными данными.
        /// </summary>
        [TestMethod]
        public void CheckFullISBN_validData_returnTrue()
        {
            //Arrange
            string isbn = "978-0-86243-680-0";
            //Act
            CheckISBN objectCheckISBN = new CheckISBN();
            bool result = objectCheckISBN.CheckFullISBN(isbn);
            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Тест метода CheckFullISBN с некорректным количеством символов.
        /// </summary>
        [TestMethod]
        public void CheckFullISBN_invalidNumberChar_returnException()
        {
            //Arrange
            string isbn = "86243-680-0";
            //Act
            CheckISBN objectCheckISBN = new CheckISBN();
            //Assert
            Assert.ThrowsException<Exception>(() => objectCheckISBN.CheckFullISBN(isbn), "Некорректное количество символов в ISBN");
        }

        /// <summary>
        /// Тест метода CheckFullISBN с некорректными символами.
        /// </summary>
        [TestMethod]
        public void CheckFullISBN_invalidChar_returnException()
        {
            //Arrange
            string isbn = "978-o-86i43-6@0-0";
            //Act
            CheckISBN objectCheckISBN = new CheckISBN();
            //Assert
            Assert.ThrowsException<Exception>(() => objectCheckISBN.CheckFullISBN(isbn), "Использованы некорректные символы в ISBN");
        }

        /// <summary>
        /// Тест метода CheckFullISBN с некорректным префиксом.
        /// </summary>
        [TestMethod]
        public void CheckFullISBN_invalidPrefix_returnException()
        {
            //Arrange
            string isbn = "980-o-86i43-6@0-0";
            //Act
            CheckISBN objectCheckISBN = new CheckISBN();
            //Assert
            Assert.ThrowsException<Exception>(() => objectCheckISBN.CheckFullISBN(isbn), "Некорректный префикс в ISBN");
        }

        /// <summary>
        /// Тест метода CheckFullISBN с некорректным кодом страны.
        /// </summary>
        [TestMethod]
        public void CheckFullISBN_invalidCountryCode_returnException()
        {
            //Arrange
            string isbn = "978-4545457-5-80-0";
            //Act
            CheckISBN objectCheckISBN = new CheckISBN();
            //Assert
            Assert.ThrowsException<Exception>(() => objectCheckISBN.CheckFullISBN(isbn), "Неправельный код страны в ISBN");
        }

        /// <summary>
        /// Тест метода CheckFullISBN с некорректным контрольным числом.
        /// </summary>
        [TestMethod]
        public void CheckFullISBN_invalidControlNumber_returnException()
        {
            //Arrange
            string isbn = "978-0-86243-680-3";
            //Act
            CheckISBN objectCheckISBN = new CheckISBN();
            //Assert
            Assert.ThrowsException<Exception>(() => objectCheckISBN.CheckFullISBN(isbn), "Неправильное контрольное число в ISBN");
        }
    }
}
