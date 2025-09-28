using Microsoft.AspNetCore.Mvc;
using MediatR;
using TaskManagement.Application.Tasks;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("v1/tasks")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;
    public TasksController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskDto>> GetById(Guid id, CancellationToken ct)
    {
        var dto = await _mediator.Send(new GetTaskById(id), ct);
        if (dto is null) return NotFound();
        return Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskDto>>> List(int skip = 0, int take = 20, CancellationToken ct = default)
    {
        var list = await _mediator.Send(new ListTasks(skip, take), ct);
        return Ok(list);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTask cmd, CancellationToken ct)
    {
        var id = await _mediator.Send(cmd, ct);
        return Created($"/v1/tasks/{id}", new { id });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTask cmd, CancellationToken ct)
    {
        if (id != cmd.Id) return BadRequest();
        await _mediator.Send(cmd, ct);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _mediator.Send(new DeleteTask(id), ct);
        return NoContent();
    }

    [HttpPost("{id:guid}/assign")]
    public async Task<IActionResult> Assign(Guid id, [FromBody] AssignTaskRequest assignTaskRequest, CancellationToken ct)
    {
        // Ensure the route ID and body ID match
        if (id != assignTaskRequest.TaskId)
            return BadRequest("Route ID and body TaskId do not match");

        await _mediator.Send(assignTaskRequest, ct);
        return NoContent();
    }

}