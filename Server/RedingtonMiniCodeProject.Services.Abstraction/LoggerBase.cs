using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedingtonMiniCodeProject.Services.Abstraction
{
    public abstract class LoggerBase<T, TFormatter> : ILoggingService<T>
        where TFormatter : ILoggingFormatter<T>
    {
        protected ILoggingWriter<T, TFormatter> Writer { get; }
        public LoggerBase(ILoggingWriter<T, TFormatter> writer)
        {
            Writer = writer;
        }
        public virtual void Log(T info)
        {
            Writer.Write(info);
        }
    }
}
