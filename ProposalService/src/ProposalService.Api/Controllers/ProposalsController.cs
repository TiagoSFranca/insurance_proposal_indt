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
}
