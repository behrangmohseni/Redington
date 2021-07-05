using RedingtonMiniCodeProject.Services.Abstraction;

namespace RedingtonMiniCodeProject.Services
{
    public class EitherCalculationService : ICalculationService
    {
        private readonly ICalculationInputValidationService _calculationValidator;

        public EitherCalculationService(ICalculationInputValidationService calculationValidator)
        {
            _calculationValidator = calculationValidator;
            Name = nameof(EitherCalculationService).Replace(nameof(ICalculationService).Remove(0, 1), string.Empty);
        }

        public string Name { get; }

        public double Calculate(double p1, double p2)
        {
            _calculationValidator.Validate(p1, p2);
            return (p1 + p2) - (p1 * p2);
        }
    }
}
