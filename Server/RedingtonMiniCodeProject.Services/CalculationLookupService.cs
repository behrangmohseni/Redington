using RedingtonMiniCodeProject.Services.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace RedingtonMiniCodeProject.Services
{
    public class CalculationLookupService : ICalculationLookupService
    {
        private readonly IEnumerable<string> _calculationServicesName;

        public CalculationLookupService()
        {
            _calculationServicesName = typeof(CalculationLookupService).Assembly.ExportedTypes.Where(a => typeof(ICalculationService).IsAssignableFrom(a) && !(a.IsAbstract || a.IsInterface))
                .Select(a => a.Name.Replace(nameof(ICalculationService).Remove(0, 1), string.Empty)).ToList();
        }

        public IEnumerable<string> GetAllCalculationServices() => _calculationServicesName;
    }
}
