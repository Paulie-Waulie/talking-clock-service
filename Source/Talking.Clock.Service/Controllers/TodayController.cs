namespace Talking.Clock.Service.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Talking.Clock.Service.DateServices;

    [Route("api/[controller]")]
    [ApiController]
    public class TodayController : ControllerBase
    {
        private readonly IDateService dateService;

        public TodayController(IDateService dateService)
        {
            this.dateService = dateService;
        }

        [Route("day")]
        public IActionResult GetTodayDay()
        {
            return this.Ok($"Today is {this.dateService.GetTodayDay()}");
        }

        [Route("date")]
        public IActionResult GetTodayDate()
        {
            return this.Ok(this.dateService.GetTodayDate());
        }
    }
}