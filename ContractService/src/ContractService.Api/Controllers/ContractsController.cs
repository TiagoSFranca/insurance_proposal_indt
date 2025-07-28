using ContractService.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractService.Api.Controllers;

public class ContractsController : BaseController
{
    private readonly IContractService _service;

    public ContractsController(IContractService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Sign([FromBody] SignContractRequest request)
    {
        var result = await _service.Sign(request);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Messages);
    }

    [HttpGet]
    public async Task<ActionResult<PageResponse<ContractBriefResponse>>> Search(
        [FromQuery] SearchContractRequest request,
        [FromQuery] SimplePageRequest page)
    {
        var result = await _service.Search(request, PageRequest.Of(page.Number, page.Limit));

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Messages);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContractResponse>> Get(Guid id)
    {
        var result = await _service.Get(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }
}
