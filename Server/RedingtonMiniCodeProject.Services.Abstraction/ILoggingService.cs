namespace RedingtonMiniCodeProject.Services.Abstraction
{
    public interface ILoggingService<T>
    {
        public void Log(T info);
    }
}
