using Microsoft.AspNetCore.Mvc;

namespace TemperatureReporter.App.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly IMeasurer measurer;

        public SampleController(IMeasurer measurer)
        {
            this.measurer = measurer;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Index()
        {
            return Ok(measurer.Get());
        }
    }
}
