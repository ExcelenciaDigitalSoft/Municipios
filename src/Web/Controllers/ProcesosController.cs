// Endpoints administrativos - Parte 3: Planes de Pago, Cuotas, Intereses, Auditoría, Notificaciones

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Aplicacion.Servicios;
using Dominio.Entidades;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize]
    public class ProcesosController : ControllerBase
    {
        private readonly PlanPagoService _planPagoService;
        private readonly CuotaPlanService _cuotaPlanService;
        private readonly InteresMoraService _interesMoraService;
        private readonly AuditoriaService _auditoriaService;
        private readonly PlantillaNotificacionService _plantillaNotificacionService;
        private readonly ColaNotificacionService _colaNotificacionService;

        public ProcesosController(
            PlanPagoService planPagoService,
            CuotaPlanService cuotaPlanService,
            InteresMoraService interesMoraService,
            AuditoriaService auditoriaService,
            PlantillaNotificacionService plantillaNotificacionService,
            ColaNotificacionService colaNotificacionService)
        {
            _planPagoService = planPagoService;
            _cuotaPlanService = cuotaPlanService;
            _interesMoraService = interesMoraService;
            _auditoriaService = auditoriaService;
            _plantillaNotificacionService = plantillaNotificacionService;
            _colaNotificacionService = colaNotificacionService;
        }

        [HttpGet("planes")] public async Task<IActionResult> GetPlanes() => Ok(await _planPagoService.ListarAsync());
        [HttpGet("cuotas")] public async Task<IActionResult> GetCuotas() => Ok(await _cuotaPlanService.ListarAsync());
        [HttpGet("intereses")] public async Task<IActionResult> GetIntereses() => Ok(await _interesMoraService.ListarAsync());
        [HttpGet("auditorias")] public async Task<IActionResult> GetAuditorias() => Ok(await _auditoriaService.ListarAsync());
        [HttpGet("plantillas")] public async Task<IActionResult> GetPlantillas() => Ok(await _plantillaNotificacionService.ListarAsync());
        [HttpGet("cola")] public async Task<IActionResult> GetCola() => Ok(await _colaNotificacionService.ListarAsync());

        [HttpPost("plantillas")] public async Task<IActionResult> CrearPlantilla([FromBody] PlantillaNotificacion p) { await _plantillaNotificacionService.AgregarAsync(p); await _plantillaNotificacionService.GuardarCambiosAsync(); return Ok(); }
        [HttpPost("cola")] public async Task<IActionResult> CrearCola([FromBody] ColaNotificacion c) { await _colaNotificacionService.AgregarAsync(c); await _colaNotificacionService.GuardarCambiosAsync(); return Ok(); }
    }
}
