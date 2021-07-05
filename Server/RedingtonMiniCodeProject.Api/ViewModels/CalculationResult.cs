using System;

namespace RedingtonMiniCodeProject.ViewModels
{

    public class CalculationResultValue
    {
        public double Value { get; set; }
    }

    public class CalculationResult
    {
        public CalculationResultValue Result { get; set; }
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }

        public CalculationResult(double value)
        {
            Result = new CalculationResultValue() { Value = value };
            IsValid = true;
            ErrorMessage = string.Empty;
        }

        public CalculationResult(Exception ex)
        {
            IsValid = false;
            ErrorMessage = ex.Message;
        }
    }
}
