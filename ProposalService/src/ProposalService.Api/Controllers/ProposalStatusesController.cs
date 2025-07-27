using Microsoft.AspNetCore.Mvc;

namespace ProposalService.Api.Controllers;

public class ProposalStatusesController : BaseController
{
    private readonly IProposalStatusService _service;

    public ProposalStatusesController(IProposalStatusService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProposalStatusResponse>>> ListAll()
    {
        var result = await _service.ListAll();

        return Ok(result);
    }
}
