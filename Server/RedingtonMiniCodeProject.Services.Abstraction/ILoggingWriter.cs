namespace RedingtonMiniCodeProject.Services.Abstraction
{
    public interface ILoggingWriter<T, TFormatter> 
        where TFormatter : ILoggingFormatter<T>
    {
        public void Write(T log);
    }
}
