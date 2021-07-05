using RedingtonMiniCodeProject.Services.Abstraction;
using RedingtonMiniCodeProject.Services.Models;

namespace RedingtonMiniCodeProject.Services
{
    public class LoggingFormatter : ILoggingFormatter<LoggingInformation>
    {
        public string Format(LoggingInformation log)
        {
            return $"TIME: {log.DateTime.ToUniversalTime()}, TYPE: {log.CalculationType}, RESULT: {log.Result}, INPUTS: ({log.FirstNumber}, {log.SecondNumber})" + (log.Errored ? $", ERRPR: {log.Exception}" : string.Empty);
        }
    }
}
