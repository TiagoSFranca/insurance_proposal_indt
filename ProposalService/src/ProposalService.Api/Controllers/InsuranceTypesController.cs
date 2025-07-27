using Microsoft.AspNetCore.Mvc;

namespace ProposalService.Api.Controllers;

public class InsuranceTypesController : BaseController
{
    private readonly IInsuranceTypeService _service;

    public InsuranceTypesController(IInsuranceTypeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<InsuranceTypeResponse>>> ListAll()
    {
        var result = await _service.ListAll();

        return Ok(result);
    }
}
