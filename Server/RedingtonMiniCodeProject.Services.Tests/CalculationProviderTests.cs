using RedingtonMiniCodeProject.Services;
using RedingtonMiniCodeProject.Services.Abstraction;
using RedingtonMiniCodeProject.Services.Models;
using Xunit;

namespace RedingtonMiniCodeProject.Service.Test
{
    public class CalculationServiceTests
    {
        private readonly ICalculationService _combineWithCalculationService;
        private readonly ICalculationService _eitherCalculationService;
        private readonly ICalculationInputValidationService _calculationValidationService;
        public CalculationServiceTests()
        {
            _calculationValidationService = new DefaultCalculationInputValidationService();
            _combineWithCalculationService = new CombinedWithCalculationService(_calculationValidationService);
            _eitherCalculationService = new EitherCalculationService(_calculationValidationService);
        }
        [Theory]
        [InlineData(0, 1, 0)]
        [InlineData(1, 0, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(0, 0, 0)]
        [InlineData(0.5, 0.5, 0.25)]
        public void CombinedWithCalculationServiceTest_ValidInputs_CorrectResult(double p1, double p2, double expectedResult)
        {
            var result = _combineWithCalculationService.Calculate(p1, p2);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 1)]
        [InlineData(0, 0, 0)]
        [InlineData(0.5, 0.5, 0.75)]
        public void EitherCalculationServiceTest_ValidInputs_CorrectResult(double p1, double p2, double expectedResult)
        {
            var result = _eitherCalculationService.Calculate(p1, p2);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(1.1 , 0.5)]
        [InlineData(-0.1, 0.5)]
        [InlineData(1.1, -0.1)]
        [InlineData(-0.1, 1.1)]
        public void CombinedWithCalculationServiceTest_InvalidInputs_ThrowInputNumbersInvalidException(double p1, double p2)
        {
            Assert.Throws<InputNumbersInvalidException>(() => _combineWithCalculationService.Calculate(p1, p2));
        }

        [Theory]
        [InlineData(1.1, 0.5)]
        [InlineData(-0.1, 0.5)]
        [InlineData(1.1, -0.1)]
        [InlineData(-0.1, 1.1)]
        public void EitherCalculationServiceTest_InvalidInputs_ThrowInputNumbersInvalidException(double p1, double p2)
        {
            Assert.Throws<InputNumbersInvalidException>(() => _eitherCalculationService.Calculate(p1, p2));
        }

        [Fact]
        public void EitherCalculationServiceTest_HasCorrectName()
        {
            Assert.Equal("Either", _eitherCalculationService.Name);
        }

        [Fact]
        public void CombinedWithCalculationServiceTest_HasCorrectName()
        {
            Assert.Equal("CombinedWith", _combineWithCalculationService.Name);
        }


    }
}
