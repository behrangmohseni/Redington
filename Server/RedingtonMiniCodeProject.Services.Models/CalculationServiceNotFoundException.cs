using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedingtonMiniCodeProject.Services.Models
{
    public class CalculationServiceNotFoundException : ArgumentOutOfRangeException
    {
        public CalculationServiceNotFoundException(string calculationService) : base(paramName: string.Empty, message: $"Calculation service <{calculationService}> not found.")
        {

        }
    }
}
