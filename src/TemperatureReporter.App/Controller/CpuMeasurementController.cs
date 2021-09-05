using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace TemperatureReporter.App.Controller
{
    [ApiController]
    [Route("cpu")]
    public class CpuMeasurementController : ControllerBase
    {
        [HttpGet]
        [Route("get")]
        public IActionResult Index()
        {
            return Ok("Hello!");
        }
    }
}
