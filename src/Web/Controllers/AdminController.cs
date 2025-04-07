// Controlador administrativo para tareas especiales del sistema

using Aplicacion.Servicios;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly PlanPagoService _planService;
        private readonly NotificacionService _notiService;
        private readonly RolService _rolService;
        private readonly UsuarioRolService _usuarioRolService;

        // Agregando todos los servicios necesarios
        private readonly ContribuyenteService _contribuyenteService;
        private readonly InmuebleService _inmuebleService;
        private readonly TributoService _tributoService;
        private readonly MunicipioService _municipioService;
        private readonly EstadoService _estadoService;
        private readonly EstadoTributoService _estadoTributoService;
        private readonly LocalidadService _localidadService;
        private readonly TipoDocumentoService _tipoDocumentoService;
        private readonly PeriodicidadService _periodicidadService;
        private readonly TipoImpuestoService _tipoImpuestoService;
        private readonly ValorTipoImpuestoService _valorTipoImpuestoService;
        private readonly TipoInmuebleService _tipoInmuebleService;
        private readonly TitularidadInmuebleService _titularidadInmuebleService;
        private readonly ValuacionInmuebleService _valuacionInmuebleService;
        private readonly ObraPublicaService _obraPublicaService;
        private readonly InmuebleObraService _inmuebleObraService;
        private readonly MedioPagoService _medioPagoService;
        private readonly CuotaPlanService _cuotaPlanService;
        private readonly InteresMoraService _interesMoraService;
        private readonly PlantillaNotificacionService _plantillaNotificacionService;
        private readonly ColaNotificacionService _colaNotificacionService;
        private readonly AuditoriaService _auditoriaService;

        public AdminController(
            UsuarioService usuarioService,
            PlanPagoService planService,
            NotificacionService notiService,
            RolService rolService,
            UsuarioRolService usuarioRolService,
            ContribuyenteService contribuyenteService,
            InmuebleService inmuebleService,
            TributoService tributoService,
            MunicipioService municipioService,
            EstadoService estadoService,
            EstadoTributoService estadoTributoService,
            LocalidadService localidadService,
            TipoDocumentoService tipoDocumentoService,
            PeriodicidadService periodicidadService,
            TipoImpuestoService tipoImpuestoService,
            ValorTipoImpuestoService valorTipoImpuestoService,
            TipoInmuebleService tipoInmuebleService,
            TitularidadInmuebleService titularidadInmuebleService,
            ValuacionInmuebleService valuacionInmuebleService,
            ObraPublicaService obraPublicaService,
            InmuebleObraService inmuebleObraService,
            MedioPagoService medioPagoService,
            CuotaPlanService cuotaPlanService,
            InteresMoraService interesMoraService,
            PlantillaNotificacionService plantillaNotificacionService,
            ColaNotificacionService colaNotificacionService,
            AuditoriaService auditoriaService)
        {
            _usuarioService = usuarioService;
            _planService = planService;
            _notiService = notiService;
            _rolService = rolService;
            _usuarioRolService = usuarioRolService;
            _contribuyenteService = contribuyenteService;
            _inmuebleService = inmuebleService;
            _tributoService = tributoService;
            _municipioService = municipioService;
            _estadoService = estadoService;
            _estadoTributoService = estadoTributoService;
            _localidadService = localidadService;
            _tipoDocumentoService = tipoDocumentoService;
            _periodicidadService = periodicidadService;
            _tipoImpuestoService = tipoImpuestoService;
            _valorTipoImpuestoService = valorTipoImpuestoService;
            _tipoInmuebleService = tipoInmuebleService;
            _titularidadInmuebleService = titularidadInmuebleService;
            _valuacionInmuebleService = valuacionInmuebleService;
            _obraPublicaService = obraPublicaService;
            _inmuebleObraService = inmuebleObraService;
            _medioPagoService = medioPagoService;
            _cuotaPlanService = cuotaPlanService;
            _interesMoraService = interesMoraService;
            _plantillaNotificacionService = plantillaNotificacionService;
            _colaNotificacionService = colaNotificacionService;
            _auditoriaService = auditoriaService;
        }

        // Métodos existentes (usuarios, roles, notificaciones, etc.) se mantienen igual

    }



        [HttpPost("usuarios/activar/{id}")]
        public async Task<IActionResult> ActivarUsuario(int id)
        {
            var user = await _usuarioService.ObtenerPorIdAsync(id);
            if (user == null) return NotFound();
            user.Activo = true;
            _usuarioService.Actualizar(user);
            await _usuarioService.GuardarCambiosAsync();
            return Ok("Usuario activado");
        }

        [HttpPost("usuarios/desactivar/{id}")]
        public async Task<IActionResult> DesactivarUsuario(int id)
        {
            var user = await _usuarioService.ObtenerPorIdAsync(id);
            if (user == null) return NotFound();
            user.Activo = false;
            _usuarioService.Actualizar(user);
            await _usuarioService.GuardarCambiosAsync();
            return Ok("Usuario desactivado");
        }

        [HttpPost("planes/generar")]
        public async Task<IActionResult> GenerarPlan([FromBody] PlanPago plan)
        {
            await _planService.AgregarAsync(plan);
            await _planService.GuardarCambiosAsync();
            return Ok("Plan de pago generado");
        }

        [HttpPost("notificaciones/emitir")]
        public async Task<IActionResult> EmitirNotificacion([FromBody] Notificacion noti)
        {
            await _notiService.AgregarAsync(noti);
            await _notiService.GuardarCambiosAsync();
            return Ok("Notificación emitida");
        }
    }
}
