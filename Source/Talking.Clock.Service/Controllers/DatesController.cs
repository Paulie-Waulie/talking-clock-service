namespace Talking.Clock.Service.Controllers
{
    using Humanizer;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("api/[controller]")]
    [ApiController]
    public class DatesController : ControllerBase
    {
        [Route("{dateString}/day")]
        public IActionResult GetDateDay([FromRoute] string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime date))
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