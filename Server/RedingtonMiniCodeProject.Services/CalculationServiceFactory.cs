using RedingtonMiniCodeProject.Services.Abstraction;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RedingtonMiniCodeProject.Services.Models;
namespace RedingtonMiniCodeProject.Services
{
    public class CalculationServiceFactory : ICalculationServiceFactory
    {
        private readonly IReadOnlyDictionary<string, ICalculationInputValidationService>
            _calculationInputValidationServices;

        private readonly ConcurrentDictionary<(string, string), ICalculationService> 
            _calculationServicesCache;

        public CalculationServiceFactory()
        {
            _calculationServicesCache = new ConcurrentDictionary<(string, string), ICalculationService>();
            // Find all validation services
            _calculationInputValidationServices = typeof(CalculationServiceFactory).
                Assembly.ExportedTypes.
                Where(type => typeof(ICalculationInputValidationService).IsAssignableFrom(type)
                    && !(type.IsAbstract || type.IsInterface)).
                Select(type => Activator.CreateInstance(type))
                .Cast<ICalculationInputValidationService>().ToImmutableDictionary(validation => validation.Name, validation => validation);    
        }

        public ICalculationService GetCalculationService(string calculationServiceName, string inputValidationServiceName = "Default")
        {
            if (_calculationServicesCache.TryGetValue((calculationServiceName, inputValidationServiceName), out var service))
            {
                return service;
            }

            if (!_calculationInputValidationServices.ContainsKey(inputValidationServiceName))
                throw new CalculationInputValidationServiceNotFoundException(inputValidationServiceName);

            var validationService = _calculationInputValidationServices[inputValidationServiceName];

            var calculationServiceType = typeof(CalculationServiceFactory).Assembly.ExportedTypes.SingleOrDefault(type => type.Name == $"{calculationServiceName}{nameof(ICalculationService).Remove(0, 1)}");

            if (calculationServiceType is not null)
            {
                var instance = (ICalculationService)Activator.CreateInstance(calculationServiceType, validationService);
                _calculationServicesCache.TryAdd((calculationServiceName, inputValidationServiceName), instance);
                return instance;
            }
            throw new CalculationServiceNotFoundException(calculationServiceName);

        }
    }
}
