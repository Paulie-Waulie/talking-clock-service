namespace Talking.Clock.Service.Controllers
{
    using System;
    using Humanizer;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class DatesController : ControllerBase
    {
        [Route("{dateString}/day")]
        public IActionResult GetDay([FromRoute] string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime date))
            {
                return this.Ok(date.DayOfWeek.Humanize());
            }

            return this.NotFound();
        }
    }
}