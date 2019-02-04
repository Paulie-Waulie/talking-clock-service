namespace Talking.Clock.Service.Controllers
{
    using Microsoft.AspNetCore.Mvc;
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

            return this.NotFound(null);
        }

        [Route("{dateString}/day")]
        public IActionResult GetDateDay([FromRoute] string dateString)
        {
            if (DateParser.TryParseFromIso8601(dateString, out var date))
            {
                return this.Ok(this.dateService.GetDateDay(date));
            }

            return this.NotFound(null);
        }
    }
}