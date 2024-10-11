using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing;

/*
 1. test methodlarinin return type-olmur. Yeni public void
 2. testler xususi proyeklertdir ve testler layihelerde bir defe yazilir
 ve bu sebebden de adlandirma cox onemlidir
 */

namespace Calculation_sutTests
{
    [TestClass]
    public class Calc_suts
    {
        private readonly CalculationService _sut;
        public Calc_suts()
        {
            _sut = new CalculationService();
        }

        [TestMethod]
        [DataRow(10, 5)]
        [DataRow(3, 5)]
        [DataRow(1, 5)]
        [DataRow(9, 8)]
        [DataRow(100, 5)]
        [DataRow(109, 5)]
        public void MethodSum_ShouldReturnPositiveNumber_ForTwoPositiveNumbers(double a, double b)
        {
            //AAA
            // SUT -> SYSTEM UNDER TEST

            //Arrange (teshkil etmek)
           


            //Act (hereket etmek)
            var res = _sut.Sum(a, b);

            // Assert (icra etmek)
            Assert.AreEqual(a + b, res);
        }

        [TestMethod]
        [DynamicData(nameof(Numbers),DynamicDataSourceType.Method)]
        public void MethodDivide_ShouldReturnGreatarThanZeroNumber_ForTwoNumbers(double a, double b)
        {
            //arrange
           
         
            // act
            var res = _sut.Divide(a, b);

            //assert
            Assert.AreEqual(a / b, res);
        }

        [TestMethod]
        public void MethodDivide_ShouldReturnZero_ForZeroNumbers()
        {
           
            double a = 10.3;
            double b = 0;

            var res = _sut.Divide(a, b);

            Assert.AreEqual(0, res);
        }


        public static IEnumerable<object[]> Numbers()
        {
            return new[]
            {
            new object[] { 1, 2 },
            new object[] { 10, 22 },
            new object[] { 100, 22 },
            new object[] { 109, 12 },
            new object[] { 10, 22 },
        };
        }

    }
}
