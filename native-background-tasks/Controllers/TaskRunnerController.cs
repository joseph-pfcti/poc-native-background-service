using Microsoft.AspNetCore.Mvc;
using NativeBackgroundTasks.Service;

namespace NativeBackgroundTasks.Controllers
{
    [ApiController]
    [Route("api/task-runner")]
    public class TaskRunnerController(RandomStringService randomStringService, IQueue queue) : ControllerBase
    {
        private readonly RandomStringService _randomStringService = randomStringService;
        private readonly IQueue _queue = queue;

        [HttpGet]
        public Task<IActionResult> ExecuteTaskAsync(CancellationToken cancellationToken) 
        {
            //await _randomStringService.GetRandomStringAsync(cancellationToken);
            return Task.FromResult<IActionResult>(Ok(new { randomString = _randomStringService.RandomString }));
        }

        [HttpGet("queue")]
        public async Task<IActionResult> GetAllPendingMessagesFromQueue(CancellationToken cancellationToken)
        {
            List<string> messages = await _queue.PullAllFromQueueAsync(cancellationToken);
            return Ok(new { messages });
        }

        [HttpPost("queue")]
        public async Task<IActionResult> CreateMessage([FromBody] string message)
        { 
            await _queue.PushToQueueAsync(message);
            return Created("Created", new { message });
        }
    }
}
