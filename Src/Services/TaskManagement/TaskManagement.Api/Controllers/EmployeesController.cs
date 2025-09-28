using Microsoft.AspNetCore.Mvc;
using MediatR;
using TaskManagement.Application.Employees;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("v1/employees")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;
    public EmployeesController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmployeeDto>> GetById(Guid id, CancellationToken ct)
    {
        var dto = await _mediator.Send(new GetEmployeeById(id), ct);
        if (dto is null) return NotFound();
        return Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EmployeeDto>>> List(int skip = 0, int take = 20, CancellationToken ct = default)
    {
        if (take <= 0 || take > 100) take = 20;
        var list = await _mediator.Send(new ListEmployees(skip, take), ct);
        return Ok(list);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployee cmd, CancellationToken ct)
    {
        var id = await _mediator.Send(cmd, ct);
        return Created($"/v1/employees/{id}", new { id });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployee cmd, CancellationToken ct)
    {
        if (id != cmd.Id) return BadRequest();
        await _mediator.Send(cmd, ct);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _mediator.Send(new DeleteEmployee(id), ct);
        return NoContent();
    }
}