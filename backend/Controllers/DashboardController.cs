using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OndeFoi.DTOs;
using OndeFoi.Services;

namespace OndeFoi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _service;

        public DashboardController(DashboardService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<DashboardResponseDto> Resumo()
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var resultado = _service.Resumo(usuarioId);

            return Ok(resultado.Dado);
        }
    }


}