namespace RedingtonMiniCodeProject.Services.Abstraction
{
    public interface ICalculationService
    {
        public string Name { get; }
        public double Calculate(double p1, double p2);
    }
}
