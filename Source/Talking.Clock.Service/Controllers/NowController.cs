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
        public IActionResult GetDateDay()
        {
            return this.Ok(this.dateService.GetNow());
        }
    }
}