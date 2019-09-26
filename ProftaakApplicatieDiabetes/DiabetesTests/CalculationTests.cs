using Data.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiabetesTests
{
    [TestClass]
    class CalculationTests
    {
        [TestMethod]
        public void CalculateCHO_CalculatesCHO()
        {
            //Arrange
            var carbs = 50;
            var weight = 70;
            var memory = new CalculationContextMemory();
            //Act

            var output = memory.CalculateCHO(carbs, weight);

            //Assert
        }
    }
}
