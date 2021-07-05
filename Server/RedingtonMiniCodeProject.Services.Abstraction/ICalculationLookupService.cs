using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedingtonMiniCodeProject.Services.Abstraction
{
    public interface ICalculationLookupService
    {
        public IEnumerable<string> GetAllCalculationServices();
    }
}
