using System;

namespace RedingtonMiniCodeProject.Services.Models
{
    public class LoggingInformation
    {
        public DateTime DateTime { get; set; }
        public string CalculationType { get; set; }
        public double FirstNumber { get; set; }
        public double SecondNumber { get; set; }
        public double Result { get; set; }
        public bool Errored { get; set; } = false;
        public string Exception { get; set; }

        public LoggingInformation()
        {

        }

        public LoggingInformation(DateTime datetime, string calculationType, double input1, double input2, double? result, Exception ex = null)
        {
            DateTime = datetime;
            FirstNumber = input1;
            SecondNumber = input2;
            CalculationType = calculationType;
            Result = result ?? double.MinValue;
            Errored = ex is null;
            Exception = ex?.Message;
        }

    }
}
