using Microsoft.AspNetCore.Mvc;
using NativeBackgroundTasks.Service;

namespace NativeBackgroundTasks.Controllers
{
    [ApiController]
    [Route("api/task-runner")]
    public class TaskRunnerController(RandomStringService randomStringService) : ControllerBase
    {
        private readonly RandomStringService _randomStringService = randomStringService;

        [HttpGet]
        public async Task<IActionResult> ExecuteTaskAsync(CancellationToken cancellationToken) 
        {
            //await _randomStringService.GetRandomStringAsync(cancellationToken);
            return Ok(new { randomString = _randomStringService.RandomString });
        }
    }
}
