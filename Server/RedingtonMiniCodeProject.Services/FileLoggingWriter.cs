using RedingtonMiniCodeProject.Services.Abstraction;
using RedingtonMiniCodeProject.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedingtonMiniCodeProject.Services
{
    public class FileLoggingWriter<T> : ILoggingWriter<T, ILoggingFormatter<T>>
    {
        private readonly ILoggingFormatter<T> _formatter;
        private readonly FileLoggerProperties _properties;
        private readonly object _lock = new object();

        public FileLoggingWriter(ILoggingFormatter<T> formatter, FileLoggerProperties properties)
        {
            _formatter = formatter;
            _properties = properties;
        }
        public void Write(T info)
        {
            var log = _formatter.Format(info);
            lock (_lock)
            {
                if (_properties.EraseFileIfExists && File.Exists(_properties.Path))
                    File.WriteAllLines(_properties.Path, new[] { log });
                else
                {
                    File.AppendAllLines(_properties.Path, new[] { log });
                }
            }
        }
    }
}
