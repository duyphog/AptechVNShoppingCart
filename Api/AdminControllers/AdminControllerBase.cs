using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.AdminControllers
{
    [ApiController]
    [Authorize(Policy = "RequireAdminRole")]
    [Route("api/admin/[controller]")]
    public class AdminControllerBase : Controller
    {
    }
}
