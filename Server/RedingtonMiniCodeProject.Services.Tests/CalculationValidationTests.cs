using RedingtonMiniCodeProject.Services;
using RedingtonMiniCodeProject.Services.Abstraction;
using RedingtonMiniCodeProject.Services.Models;
using Xunit;

namespace RedingtonMiniCodeProject.Service.Test
{
    public class CalculationValidationTests
    {
        private readonly ICalculationInputValidationService _validationService;

        public CalculationValidationTests()
        {
            _validationService = new DefaultCalculationInputValidationService();
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(0, 0)]
        [InlineData(0.25, 0.5)]
        [InlineData(0.5, 0.25)]
        public void DefaultCalculationInputValidationService_ValidInputs_DoesNotThrowErrors(double p1, double p2)
        {
            _validationService.Validate(p1, p2);
        }

        [Theory]
        [InlineData(1.1, 0.5)]
        [InlineData(-0.1, 0.5)]
        [InlineData(1.1, -0.1)]
        [InlineData(-0.1, 1.1)]
        public void DefaultCalculationInputValidationService_InvalidInputs_ThrowsInputNumbersInvalidException(double p1, double p2)
        {
            Assert.Throws<InputNumbersInvalidException>(() => _validationService.Validate(p1, p2));
        }

        [Fact]
        public void DefaultCalculationInputValidationService_HasCorrectName()
        {
            Assert.Equal("Default", _validationService.Name);
        }
    }
}
