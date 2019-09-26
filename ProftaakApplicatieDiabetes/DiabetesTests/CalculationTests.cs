using Data.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiabetesTests
{
    [TestClass]
    public class CalculationTests
    {
        [TestMethod]
        public void CalculateMealtimeDose_Returns7_WhenWeightIs70()
        {
            //Arange
            var weight = 70;
            var currentBS = 220;
            var targetBS = 120;
            var carbs = 60;
            var memory = new CalculationContextMemory();

            //Act
            var output = memory.CalculateMealtimeDose(weight, carbs, currentBS, targetBS);

            //Assert
            Assert.AreEqual(output, 7);
        }

        [TestMethod]
        public void CalculateTDI_Returns38_WhenWeightIs70()
        {
            //Arange
            var weight = 70;
            var memory = new CalculationClass();

            //Act
            var output = memory.CalculateTotalDoseInsuline(weight);

            //Assert
            Assert.AreEqual(output, 38);
        }

        [TestMethod]
        public void CalculateCorrectionFactor_Returns47_WhenWeightIs70()
        {
            //Arange
            var weight = 70;
            var memory = new CalculationClass();

            //Act
            var output = memory.CalculateCorrectionFactor(weight);

            //Assert
            Assert.AreEqual(output, 47);
        }

        [TestMethod]
        public void CalculateSugarCorrection_Returns47_WhenWeightIs70()
        {
            //Arange
            var weight = 70;
            var currentBS = 220;
            var targetBS = 120;
            var memory = new CalculationClass();

            //Act
            var output = memory.CalculateSugarCorrection(currentBS, targetBS, weight);

            //Assert
            Assert.AreEqual(output, 2);
        }
        
            [TestMethod]
        public void CalculateCHO_Returns5_WhenWeightIs70()
        {
            //Arange
            var weight = 70;
            var carbs = 60;
            var memory = new CalculationClass();

            //Act
            var output = memory.CalculateCHO(carbs, weight);

            //Assert
            Assert.AreEqual(output, 5);
        }
    }
}
