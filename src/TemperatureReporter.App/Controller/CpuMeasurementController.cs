using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TemperatureReporter.App.Controller
{
    [ApiController]
    [Route("cpu")]
    public class CpuMeasurementController : ControllerBase
    {
        private readonly MeasurementGetter measurer;

        public CpuMeasurementController(MeasurementGetter measurer)
        {
            this.measurer = measurer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await measurer.Get());
        }
    }
}
