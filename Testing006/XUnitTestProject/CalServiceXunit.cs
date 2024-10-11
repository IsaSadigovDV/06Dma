using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing;

namespace XUnitTestProject
{
    public class CalServiceXunit
    {
        private readonly CalculationService _sut;
        public CalServiceXunit()
        {
            _sut = new CalculationService();
        }

        [Theory]
        [InlineData(2,3)]
        [InlineData(1,3)]
        [InlineData(10,3)]
        [InlineData(20,2)]
        public void MethodSum_ShouldReturnPositive_ForTwoPositiveNumbers(double a, double b)
        {
            //act
            
            var res = _sut.Sum(a, b);

            //assert
            Assert.Equal(a+b, res);
        }
    }
}
