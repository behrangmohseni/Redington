using System;

namespace RedingtonMiniCodeProject.Services.Models
{
    public class CalculationInputValidationServiceNotFoundException : ArgumentOutOfRangeException
    {
        public CalculationInputValidationServiceNotFoundException(string calculationInputValidationService) : base(paramName: string.Empty, message: $"Calculation input validation service <{calculationInputValidationService}> not found.")
        {

        }
    }
}
