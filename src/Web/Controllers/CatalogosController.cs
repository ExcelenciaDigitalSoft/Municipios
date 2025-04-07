// Endpoints administrativos - Parte 2: Municipios, Estados, Localidades, Tipos

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Aplicacion.Servicios;
using Dominio.Entidades;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize]
    public class CatalogosController : ControllerBase
    {
        private readonly MunicipioService _municipioService;
        private readonly EstadoService _estadoService;
        private readonly EstadoTributoService _estadoTributoService;
        private readonly LocalidadService _localidadService;
        private readonly TipoDocumentoService _tipoDocumentoService;
        private readonly PeriodicidadService _periodicidadService;
        private readonly TipoImpuestoService _tipoImpuestoService;
        private readonly TipoInmuebleService _tipoInmuebleService;

        public CatalogosController(
            MunicipioService municipioService,
            EstadoService estadoService,
            EstadoTributoService estadoTributoService,
            LocalidadService localidadService,
            TipoDocumentoService tipoDocumentoService,
            PeriodicidadService periodicidadService,
            TipoImpuestoService tipoImpuestoService,
            TipoInmuebleService tipoInmuebleService)
        {
            _municipioService = municipioService;
            _estadoService = estadoService;
            _estadoTributoService = estadoTributoService;
            _localidadService = localidadService;
            _tipoDocumentoService = tipoDocumentoService;
            _periodicidadService = periodicidadService;
            _tipoImpuestoService = tipoImpuestoService;
            _tipoInmuebleService = tipoInmuebleService;
        }

        [HttpGet("municipios")] public async Task<IActionResult> GetMunicipios() => Ok(await _municipioService.ListarAsync());
        [HttpGet("estados")] public async Task<IActionResult> GetEstados() => Ok(await _estadoService.ListarAsync());
        [HttpGet("estados-tributo")] public async Task<IActionResult> GetEstadosTributo() => Ok(await _estadoTributoService.ListarAsync());
        [HttpGet("localidades")] public async Task<IActionResult> GetLocalidades() => Ok(await _localidadService.ListarAsync());
        [HttpGet("tipos-documento")] public async Task<IActionResult> GetTiposDocumento() => Ok(await _tipoDocumentoService.ListarAsync());
        [HttpGet("periodicidades")] public async Task<IActionResult> GetPeriodicidades() => Ok(await _periodicidadService.ListarAsync());
        [HttpGet("tipos-impuesto")] public async Task<IActionResult> GetTiposImpuesto() => Ok(await _tipoImpuestoService.ListarAsync());
        [HttpGet("tipos-inmueble")] public async Task<IActionResult> GetTiposInmueble() => Ok(await _tipoInmuebleService.ListarAsync());
    }
}
