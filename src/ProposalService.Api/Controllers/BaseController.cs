using Microsoft.AspNetCore.Mvc;

namespace ProposalService.Api.Controllers;

[ApiController]
[Route("proposal/api/[controller]")]
public abstract class BaseController : ControllerBase
{

}
