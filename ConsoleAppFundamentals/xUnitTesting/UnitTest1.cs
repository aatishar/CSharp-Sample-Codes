using System;
using Xunit;
using ConsoleAppForUnitTesting;
using Xunit.Abstractions;

namespace xUnitTesting
{
    public class UnitTestCalculator
    {
        private readonly ITestOutputHelper output;

        public UnitTestCalculator(ITestOutputHelper outputHelper)
        {
            this.output = outputHelper;
        }

        [Fact]
        public void AddNumbersTest()
        {
            decimal x = 1;
            decimal y = 1;
            decimal expectedResult = 2;

            var result = Program.Add(x, y);
            
            Assert.Equal(expectedResult, result );
        }

        [Theory]
        [InlineData(3,3,6)]
        [InlineData(1,2,3)]
        [InlineData(2,1,3)]
        [InlineData(2.5, 1.1, 3.6)]
        [InlineData( 1.1,2.5, 3.6)]
        public void AddNumberTheory(decimal x, decimal y, decimal expectedResult)
        {
            var result = Program.Add(x, y);

            output.WriteLine($"{x} + {y} = {result} (expected result:{expectedResult})");
            Assert.Equal(expectedResult, result);
        }
    }
}
