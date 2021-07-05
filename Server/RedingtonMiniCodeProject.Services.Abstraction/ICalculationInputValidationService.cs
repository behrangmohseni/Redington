namespace RedingtonMiniCodeProject.Services.Abstraction
{
    public interface ICalculationInputValidationService
    {
        public string Name { get; }
        public void Validate(double p1, double p2);
    }
}
