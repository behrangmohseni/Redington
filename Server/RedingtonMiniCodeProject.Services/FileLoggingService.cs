using RedingtonMiniCodeProject.Services.Abstraction;
using RedingtonMiniCodeProject.Services.Models;
using System.IO;

namespace RedingtonMiniCodeProject.Services
{
    public class FileLoggingService : LoggerBase<LoggingInformation, ILoggingFormatter<LoggingInformation>>, ILoggingService<LoggingInformation>
    {
        public FileLoggingService(ILoggingWriter<LoggingInformation, ILoggingFormatter<LoggingInformation>> writer) : base(writer) { }
    }
}
