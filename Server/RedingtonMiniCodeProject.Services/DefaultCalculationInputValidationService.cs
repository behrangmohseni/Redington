using RedingtonMiniCodeProject.Services.Models;
using RedingtonMiniCodeProject.Services.Abstraction;

namespace RedingtonMiniCodeProject.Services
{
    public class DefaultCalculationInputValidationService : ICalculationInputValidationService
    {
        public DefaultCalculationInputValidationService()
        {
            Name = nameof(DefaultCalculationInputValidationService).Replace(nameof(ICalculationInputValidationService).Remove(0, 1), string.Empty);
        }
        public string Name { get; }

        public void Validate(double p1, double p2)
        {
            var isValid = (p1 >= 0 && p1 <= 1) && (p2 >= 0 && p2 <= 1);
            if (!isValid)
                throw new InputNumbersInvalidException(p1, p2);
        }
    }
}
