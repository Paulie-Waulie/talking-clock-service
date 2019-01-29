namespace Talking.Clock.Service.Controllers
{
    using Humanizer;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Talking.Clock.Service.DateServices;

    [Route("api/[controller]")]
    [ApiController]
    public class DatesController : ControllerBase
    {
        private readonly IDateService dateService;

        public DatesController(IDateService dateService)
        {
            this.dateService = dateService;
        }

        [Route("{dateString}/date")]
        public IActionResult GetDate([FromRoute] string dateString)
        {
            if (DateParser.TryParseFromIso8601(dateString, out var date))
            {
                return this.Ok(this.dateService.GetDate(date));
            }

            return this.NotFound();
        }

        [Route("{dateString}/day")]
        public IActionResult GetDateDay([FromRoute] string dateString)
        {
            if (DateParser.TryParseFromIso8601(dateString, out var date))
            {
                return this.Ok(this.dateService.GetDateDay(date));
            }

            return this.NotFound();
        }

        [Route("today/day")]
        public IActionResult GetTodayDay()
        {
            return this.Ok(this.dateService.GetTodayDay());
        }

        [Route("today/date")]
        public IActionResult GetTodayDate()
        {
            return this.Ok(this.dateService.GetTodayDate());
        }
    }
}