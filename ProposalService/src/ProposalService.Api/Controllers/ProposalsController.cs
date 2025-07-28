using Microsoft.AspNetCore.Mvc;
using ProposalService.Api.Models;

namespace ProposalService.Api.Controllers;

public class ProposalsController : BaseController
{
    private readonly IProposalService _service;

    public ProposalsController(IProposalService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateProposalRequest request)
    {
        var result = await _service.Create(request);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Messages);
    }

    [HttpGet]
    public async Task<ActionResult<PageResponse<ProposalBriefResponse>>> Search(
        [FromQuery] SearchProposalRequest request,
        [FromQuery] SimplePageRequest page)
    {
        var result = await _service.Search(request, PageRequest.Of(page.Number, page.Limit));

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Messages);
    }

    [HttpGet("/{id}")]
    public async Task<ActionResult<ProposalResponse>> Get(Guid id)
    {
        var result = await _service.Get(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("/{id}/updateStatus")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromQuery] int idStatus)
    {
        var result = await _service.UpdateStatus(id, idStatus);

        if (result.IsSuccess)
            return Ok();

        return BadRequest(result.Messages);
    }
}
