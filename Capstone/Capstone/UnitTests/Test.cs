using NUnit.Framework;
using System;
namespace Capstone.UnitTests
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestMethod1()
        {
            // Arrange
            var myClass = new Budget();
            myClass.BankAmount = 5;

            // Act
            //var result = myClass.SquareNumber(input);

            // Assert
            Assert.AreEqual(myClass.BankAmount, 5);
        }
    }
}