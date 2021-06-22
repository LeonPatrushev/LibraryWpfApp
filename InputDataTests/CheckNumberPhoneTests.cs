using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InputDataLibrary;

namespace InputDataTests
{
    [TestClass]
    public class CheckNumberPhoneTests
    {
        /// <summary>
        /// Тест метода CheckFullNumberPhone с корректными данными и использованием плюса.
        /// </summary>
        [TestMethod]
        public void CheckFullNumberPhone_validDataUsingPlusInNumber_returnTrue()
        {
            //Arrange
            string numberPhone = "+79222222222";
            //Act
            CheckNumberPhone objectCheckNumberPhone = new CheckNumberPhone();
            bool result = objectCheckNumberPhone.CheckFullNumberPhone(numberPhone);
            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Тест метода CheckFullNumberPhone с корректными данными и использованием плюса.
        /// </summary>
        [TestMethod]
        public void CheckFullNumberPhone_validDataNoUsingPlusInNumber_returnTrue()
        {
            //Arrange
            string numberPhone = "89222222222";
            //Act
            CheckNumberPhone objectCheckNumberPhone = new CheckNumberPhone();
            bool result = objectCheckNumberPhone.CheckFullNumberPhone(numberPhone);
            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Тест метода CheckFullNumberPhone с пустой строкой.
        /// </summary>
        [TestMethod]
        public void CheckFullEmail_EmptyString_returnFalse()
        {
            //Arrange
            string numberPhone = "";
            //Act
            CheckNumberPhone objectCheckNumberPhone = new CheckNumberPhone();
            bool result = objectCheckNumberPhone.CheckFullNumberPhone(numberPhone);
            //Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Тест метода CheckFullNumberPhone с недостаточным количеством символов.
        /// </summary>
        [TestMethod]
        public void CheckFullEmail_LowChar_returnFalse()
        {
            //Arrange
            string numberPhone = "89999";
            //Act
            CheckNumberPhone objectCheckNumberPhone = new CheckNumberPhone();
            bool result = objectCheckNumberPhone.CheckFullNumberPhone(numberPhone);
            //Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Тест метода CheckFullNumberPhone с избыточным количеством символов.
        /// </summary>
        [TestMethod]
        public void CheckFullEmail_MoreChar_returnFalse()
        {
            //Arrange
            string numberPhone = "8999999999999999";
            //Act
            CheckNumberPhone objectCheckNumberPhone = new CheckNumberPhone();
            bool result = objectCheckNumberPhone.CheckFullNumberPhone(numberPhone);
            //Assert
            Assert.IsFalse(result);
        }
    }
}
