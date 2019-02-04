namespace Talking.Clock.Service.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Talking.Clock.Service.DateServices;

    [Route("api/[controller]")]
    [ApiController]
    public class NowController : ControllerBase
    {
        private readonly IDateService dateService;

        public NowController(IDateService dateService)
        {
            this.dateService = dateService;
        }

        [Route("")]
        public IActionResult GetNow()
        {
            return this.Ok(this.dateService.GetNow());
        }

        [Route("time")]
        public IActionResult GetDateDay()
        {
            return this.Ok(this.dateService.GetTimeNow());
        }

        [Route("day")]
        public IActionResult GetDay()
        {
            return this.Ok($"Today is {this.dateService.GetDayNow()}");
        }

        [Route("date")]
        public IActionResult GetTodayDate()
        {
            return this.Ok(this.dateService.GetDateNow());
        }
    }
}