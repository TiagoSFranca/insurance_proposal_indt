using Microsoft.AspNetCore.Mvc;
using ProposalService.Domain.Interfaces;
using ProposalService.Domain.Requests;

namespace ProposalService.Api.Controllers;

public class ProposalController : BaseController
{
    private readonly IProposalService _service;

    public ProposalController(IProposalService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateProposalRequest request)
    {
        var resut = await _service.Create(request);

        return Ok(resut);
    }
}
