using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InputDataLibrary;

namespace InputDataTests
{
    [TestClass]
    public class CheckEmailTests
    {
        /// <summary>
        /// Тест метода CheckFullEmail с корректными данными.
        /// </summary>
        [TestMethod]
        public void CheckFullEmail_validData_returnTrue()
        {
            //Arrange
            string email = "leonpatrushev@yandex.ru";
            //Act
            CheckEmail objectCheckEmail = new CheckEmail();
            bool result = objectCheckEmail.CheckFullEmail(email);
            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Тест метода CheckFullEmail с пустой строкой.
        /// </summary>
        [TestMethod]
        public void CheckFullEmail_EmptyString_returnFalse()
        {
            //Arrange
            string email = "";
            //Act
            CheckEmail objectCheckEmail = new CheckEmail();
            bool result = objectCheckEmail.CheckFullEmail(email);
            //Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Тест метода CheckFullEmail с данными без домена 1го уровня.
        /// </summary>
        [TestMethod]
        public void CheckFullEmail_NoFirstLevelDomain_returnFalse()
        {
            //Arrange
            string email = "@yandex.ru";
            //Act
            CheckEmail objectCheckEmail = new CheckEmail();
            bool result = objectCheckEmail.CheckFullEmail(email);
            //Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Тест метода CheckFullEmail с данными без домена 2го уровня.
        /// </summary>
        [TestMethod]
        public void CheckFullEmail_NoSecondLevelDomain_returnFalse()
        {
            //Arrange
            string email = "leonpatrushev@.ru";
            //Act
            CheckEmail objectCheckEmail = new CheckEmail();
            bool result = objectCheckEmail.CheckFullEmail(email);
            //Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Тест метода CheckFullEmail с данными без домена 3го уровня.
        /// </summary>
        [TestMethod]
        public void CheckFullEmail_NoThirdLevelDomain_returnFalse()
        {
            //Arrange
            string email = "leonpatrushev@yandex.";
            //Act
            CheckEmail objectCheckEmail = new CheckEmail();
            bool result = objectCheckEmail.CheckFullEmail(email);
            //Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Тест метода CheckFullEmail с данными без знака @.
        /// </summary>
        [TestMethod]
        public void CheckFullEmail_NoSymbolDog_returnFalse()
        {
            //Arrange
            string email = "leonpatrushevyandex.ru";
            //Act
            CheckEmail objectCheckEmail = new CheckEmail();
            bool result = objectCheckEmail.CheckFullEmail(email);
            //Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Тест метода CheckFullEmail с данными без знака точки.
        /// </summary>
        [TestMethod]
        public void CheckFullEmail_NoSymbolDot_returnFalse()
        {
            //Arrange
            string email = "leonpatrushev@yandexru";
            //Act
            CheckEmail objectCheckEmail = new CheckEmail();
            bool result = objectCheckEmail.CheckFullEmail(email);
            //Assert
            Assert.IsFalse(result);
        }
    }
}
