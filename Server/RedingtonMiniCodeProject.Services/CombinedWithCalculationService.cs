using RedingtonMiniCodeProject.Services.Abstraction;

namespace RedingtonMiniCodeProject.Services
{
    public class CombinedWithCalculationService : ICalculationService
    {
        private readonly ICalculationInputValidationService _calculationValidator;

        public CombinedWithCalculationService(ICalculationInputValidationService calculationValidator)
        {
            _calculationValidator = calculationValidator;
            Name = nameof(CombinedWithCalculationService).Replace(nameof(ICalculationService).Remove(0, 1), string.Empty);
        }

        public string Name { get; }

        public double Calculate(double p1, double p2)
        {
            _calculationValidator.Validate(p1, p2);
            return p1 * p2;
        }
    }
}
