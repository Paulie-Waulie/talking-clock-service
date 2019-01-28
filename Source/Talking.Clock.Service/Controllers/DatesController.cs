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
        [Route("{dateString}/day")]
        public IActionResult GetDateDay([FromRoute] string dateString)
        {
            if (DateParser.TryParseFromIso8601(dateString, out var date))
            {
                return this.Ok(date.DayOfWeek.Humanize());
            }

            return this.NotFound();
        }

        [Route("today/day")]
        public IActionResult GetTodayDay()
        {
            return this.Ok(DateTime.Now.DayOfWeek.Humanize());
        }
    }
}