using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Tests.Application;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Tests.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkerController : ControllerBase
    {
         private readonly ILogger<WorkerController> _logger;
        private readonly IMediator _mediator;

        public WorkerController(ILogger<WorkerController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Crea una nueva tarea.
        /// </summary>
        /// <remarks>
        /// Request:
        ///
        ///     POST Create
        ///     {
        ///        "groupId": "string",
        ///        "workerId": "string",
        ///        "data": {
        ///           "Id": int,
        ///           "Value": "string"
        ///        }
        ///     }
        ///
        /// </remarks>
        /// <param name="command">La nueva tarea incluye los campos Id y Value.</param>
        /// <returns>Detalles de la tarea</returns>
        [HttpPost("CreateTestData")]
        public async Task<IActionResult> CreateTestDataJobAsync([FromBody] CreateWorkerTestCommand command)
        {
            var job = await _mediator.Send(command);
            return CreatedAtAction(nameof(ByIdAsync), new { id = job.Id }, job);
        }

        /// <summary>
        /// Elimina todas las tareas pertenecientes a un grupo en común.
        /// </summary>
        /// <param name="groupId">Nombre del grupo de tareas a eliminar</param>
        /// <returns>True</returns>
        [HttpDelete("DeleteByGroupId/{groupId}")]
        public async Task<IActionResult> DeleteByGroupIdAsync([FromRoute] string groupId)
        {
            var isDeleted = await _mediator.Send(new DeleteByGroupIdCommand(groupId));
            return Ok(isDeleted);
        }

        /// <summary>
        /// Retorna una tarea especifica.
        /// </summary>
        /// <param name="id">Id de la tarea</param>
        /// <returns>Detalles de la tarea</returns>
        [HttpGet("ById/{id}")]
        public async Task<IActionResult> ByIdAsync([FromRoute] long id)
        {
            var job = await _mediator.Send(new GetJobByIdQuery(id));
            return Ok(job);
        }

        /// <summary>
        /// Retorna todas las tareas.
        /// </summary>
        /// <param name="count">Cantidad de tareas a obtener</param>
        /// <returns>Los detalles de las tareas</returns>
        [HttpGet("{count}")]
        public async Task<IActionResult> GetAsync([FromRoute] int count = 10)
        {
            var jobs = await _mediator.Send(new GetJobsQuery(count));
            return Ok(jobs);
        }

        /// <summary>
        /// Retorna todas las tareas.
        /// </summary>
        /// <param name="groupId">Nombre del grupo de tareas</param>
        /// <param name="count">Cantidad de tareas a obtener</param>
        /// <returns>Los detalles de las tareas</returns>
        [HttpGet("ByGroupId/{groupId}/{count}")]
        public async Task<IActionResult> ByGroupIdAsync([FromRoute] string groupId, int count = 10)
        {
            var jobs = await _mediator.Send(new GetJobsByGroupIdQuery(groupId, count));
            return Ok(jobs);
        }

        /// <summary>
        /// Retorna la lista de las proximas tareas a ejecutarse.
        /// </summary>
        /// <param name="count">Cantidad de tareas a obtener</param>
        /// <returns>Los detalles de las tareas</returns>
        [HttpGet("Nexts/{count}")]
        public async Task<IActionResult> NextsAsync([FromRoute] int count = 10)
        {
            var jobs = await _mediator.Send(new GetNextJobsQuery(count));
            return Ok(jobs);
        }
    }
}
