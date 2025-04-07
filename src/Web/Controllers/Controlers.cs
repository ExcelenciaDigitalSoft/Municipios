// Capa Web - Controladores básicos para el sistema de Rentas Municipales

using Aplicacion.Servicios;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public abstract class ControladorBase<TServicio, TEntidad> : ControllerBase where TServicio : ServicioBase<TEntidad> where TEntidad : class, new()
    {
        protected readonly TServicio _service;
        public ControladorBase(TServicio service) => _service = service;

        [HttpGet] public async Task<IActionResult> Get() => Ok(await _service.ListarAsync());
        [HttpGet("{id}")] public async Task<IActionResult> Get(int id) => Ok(await _service.ObtenerPorIdAsync(id));
        [HttpPost] public async Task<IActionResult> Post([FromBody] TEntidad entidad) { await _service.AgregarAsync(entidad); await _service.GuardarCambiosAsync(); return Ok(); }
        [HttpPut("{id}")] public IActionResult Put(int id, [FromBody] TEntidad entidad) { typeof(TEntidad).GetProperty("Id")?.SetValue(entidad, id); _service.Actualizar(entidad); return Ok(); }
        [HttpDelete("{id}")] public IActionResult Delete(int id) { _service.Eliminar(new TEntidad { }); typeof(TEntidad).GetProperty("Id")?.SetValue(_service, id); return Ok(); }
    }

    [Route("api/[controller]")][ApiController] public class ContribuyentesController : ControladorBase<ContribuyenteService, Contribuyente> { public ContribuyentesController(ContribuyenteService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class InmueblesController : ControladorBase<InmuebleService, Inmueble> { public InmueblesController(InmuebleService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class TributosController : ControladorBase<TributoService, Tributo> { public TributosController(TributoService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class PagosController : ControladorBase<PagoService, Pago> { public PagosController(PagoService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class NotificacionesController : ControladorBase<NotificacionService, Notificacion> { public NotificacionesController(NotificacionService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class UsuariosController : ControladorBase<UsuarioService, Usuario> { public UsuariosController(UsuarioService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class MunicipiosController : ControladorBase<MunicipioService, Municipio> { public MunicipiosController(MunicipioService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class EstadosController : ControladorBase<EstadoService, Estado> { public EstadosController(EstadoService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class EstadoTributoController : ControladorBase<EstadoTributoService, EstadoTributo> { public EstadoTributoController(EstadoTributoService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class LocalidadesController : ControladorBase<LocalidadService, Localidad> { public LocalidadesController(LocalidadService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class TiposDocumentoController : ControladorBase<TipoDocumentoService, TipoDocumento> { public TiposDocumentoController(TipoDocumentoService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class PeriodicidadesController : ControladorBase<PeriodicidadService, Periodicidad> { public PeriodicidadesController(PeriodicidadService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class TiposImpuestoController : ControladorBase<TipoImpuestoService, TipoImpuesto> { public TiposImpuestoController(TipoImpuestoService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class ValoresTipoImpuestoController : ControladorBase<ValorTipoImpuestoService, ValorTipoImpuesto> { public ValoresTipoImpuestoController(ValorTipoImpuestoService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class TiposInmuebleController : ControladorBase<TipoInmuebleService, TipoInmueble> { public TiposInmuebleController(TipoInmuebleService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class TitularidadInmuebleController : ControladorBase<TitularidadInmuebleService, TitularidadInmueble> { public TitularidadInmuebleController(TitularidadInmuebleService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class ValuacionesInmuebleController : ControladorBase<ValuacionInmuebleService, ValuacionInmueble> { public ValuacionesInmuebleController(ValuacionInmuebleService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class ObrasPublicasController : ControladorBase<ObraPublicaService, ObraPublica> { public ObrasPublicasController(ObraPublicaService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class InmueblesObrasController : ControladorBase<InmuebleObraService, InmuebleObra> { public InmueblesObrasController(InmuebleObraService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class MediosPagoController : ControladorBase<MedioPagoService, MedioPago> { public MediosPagoController(MedioPagoService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class PlanesPagoController : ControladorBase<PlanPagoService, PlanPago> { public PlanesPagoController(PlanPagoService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class CuotasPlanController : ControladorBase<CuotaPlanService, CuotaPlan> { public CuotasPlanController(CuotaPlanService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class InteresesMoraController : ControladorBase<InteresMoraService, InteresMora> { public InteresesMoraController(InteresMoraService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class PlantillasNotificacionController : ControladorBase<PlantillaNotificacionService, PlantillaNotificacion> { public PlantillasNotificacionController(PlantillaNotificacionService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class ColaNotificacionesController : ControladorBase<ColaNotificacionService, ColaNotificacion> { public ColaNotificacionesController(ColaNotificacionService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class AuditoriaController : ControladorBase<AuditoriaService, Auditoria> { public AuditoriaController(AuditoriaService s) : base(s) {} }
    [Route("api/[controller]")][ApiController] public class IntegracionesExternasController : ControladorBase<IntegracionExternaService, IntegracionExterna> { public IntegracionesExternasController(IntegracionExternaService s) : base(s) {} }
}
