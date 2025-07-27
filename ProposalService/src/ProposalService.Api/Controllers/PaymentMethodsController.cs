using Microsoft.AspNetCore.Mvc;

namespace ProposalService.Api.Controllers;

public class PaymentMethodsController : BaseController
{
    private readonly IPaymentMethodService _service;

    public PaymentMethodsController(IPaymentMethodService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<PaymentMethodResponse>>> ListAll()
    {
        var result = await _service.ListAll();

        return Ok(result);
    }
}
