namespace RedingtonMiniCodeProject.Services.Abstraction
{
    public interface ICalculationServiceFactory
    {
        public ICalculationService GetCalculationService(string calculationServiceName, string inputValidationServiceName = "Default");
    }
}
