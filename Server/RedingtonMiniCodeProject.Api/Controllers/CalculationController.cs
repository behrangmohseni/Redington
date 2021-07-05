using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedingtonMiniCodeProject.Services.Abstraction;
using RedingtonMiniCodeProject.Services.Models;
using RedingtonMiniCodeProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RedingtonMiniCodeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        private readonly ICalculationLookupService calculationLookupService;
        private readonly ICalculationServiceFactory calculationServiceFactory;
        private readonly ILoggingService<LoggingInformation> loggingService;

        public CalculationController(ICalculationLookupService calculationLookupService, ICalculationServiceFactory calculationServiceFactory, ILoggingService<LoggingInformation> loggingService)
        {
            this.calculationLookupService = calculationLookupService;
            this.calculationServiceFactory = calculationServiceFactory;
            this.loggingService = loggingService;
        }

        [Route("types")]
        [HttpGet]
        public IActionResult GetAllCalculationTypes()
        {
            return new JsonResult(calculationLookupService.GetAllCalculationServices());
        }

        [Route("calc/{type}/{input1}/{input2}")]
        [HttpGet]
        public IActionResult Calculate([FromRoute] CalculationInput input)
        {
            return HandleCalculation(input.Type, input.Input1, input.Input2);
        }

        private IActionResult HandleCalculation(string type, double input1, double input2)
        {
            CalculationResult calculationResult;
            LoggingInformation log;
            try
            {
                var service = calculationServiceFactory.GetCalculationService(type);
                var result = service.Calculate(input1, input2); ;
                calculationResult = new CalculationResult(result);
                log = new LoggingInformation(DateTime.Now, type, input1, input2, result);
            }
            catch (Exception ex)
            {
                calculationResult = new CalculationResult(ex);
                log = new LoggingInformation(DateTime.Now, type, input1, input2, null, ex);
                SetStatusCode(ex);
            }

            try
            {
                loggingService.Log(log);
            }
            catch (Exception ex)
            {
                Debug.Fail("Error with logging system", ex.Message);
            }

            return new JsonResult(calculationResult);

            void SetStatusCode(Exception ex)
            {
                Response.StatusCode = ex switch
                {
                    CalculationInputValidationServiceNotFoundException or CalculationServiceNotFoundException or InputNumbersInvalidException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError,
                };
            }
        }
    }
}
