using System;

namespace RedingtonMiniCodeProject.Services.Models
{
    public class InputNumbersInvalidException : ArgumentOutOfRangeException
    {
        public InputNumbersInvalidException(double p1, double p2) : base(paramName: string.Empty ,
            message: $"Invalid input numbers, both numbers should be in the range of {{0 to 1}}. P1: {p1}, P2: {p2}") {}
    }
}
