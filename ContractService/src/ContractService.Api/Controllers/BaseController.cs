using Microsoft.AspNetCore.Mvc;

namespace ContractService.Api.Controllers;

[ApiController]
[Route("contract/api/[controller]")]
public abstract class BaseController : ControllerBase
{

}
