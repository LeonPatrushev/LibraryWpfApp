using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InputDataLibrary;

namespace InputDataTests
{
    [TestClass]
    public class CheckEmptyStringTests
    {
        /// <summary>
        /// Тест метода CheckFullEmptyString с корректными данными.
        /// </summary>
        [TestMethod]
        public void CheckFullEmptyString_validData_returnTrue()
        {
            //Arrange 
            string line= "Asdas";
            //Act
            CheckEmptyString objectCheckEmptyString = new CheckEmptyString();
            bool result = objectCheckEmptyString.CheckFullEmptyString(line);
            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Тест метода CheckFullEmptyString с некорректными данными.
        /// </summary>
        [TestMethod]
        public void CheckFullEmptyString_invalidData_returnTrue()
        {
            //Arrange 
            string line = "";
            //Act
            CheckEmptyString objectCheckEmptyString = new CheckEmptyString();
            bool result = objectCheckEmptyString.CheckFullEmptyString(line);
            //Assert
            Assert.IsFalse(result);
        }
    }
}
