using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskProject.Helpers;
using Task = TaskProject.Models.Task;
namespace TaskProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class DueDateController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public  DueDateController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("overdue/{count}")]
        public async Task<ActionResult<IEnumerable<Task>>> GetOverdueTasks(int count)
        {
            var overdueTasks = await unitOfWork.DueDateRepository.GetOverdueTasksAsync(count);
            return Ok(overdueTasks);
        }
    }
}
