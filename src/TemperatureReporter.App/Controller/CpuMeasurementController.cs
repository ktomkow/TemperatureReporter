using Microsoft.AspNetCore.Mvc;

namespace TemperatureReporter.App.Controller
{
    [ApiController]
    [Route("cpu")]
    public class CpuMeasurementController : ControllerBase
    {
        private readonly IMeasurer measurer;

        public CpuMeasurementController(IMeasurer measurer)
        {
            this.measurer = measurer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(measurer.Get());
        }
    }
}
