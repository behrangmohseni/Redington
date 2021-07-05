namespace RedingtonMiniCodeProject.Services.Abstraction
{
    public interface ILoggingFormatter<T> 
    {
        public string Format(T log);
    }
}
