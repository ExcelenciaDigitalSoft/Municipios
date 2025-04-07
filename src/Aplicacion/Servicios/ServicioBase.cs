// Capa de Aplicación - Servicios del sistema de Rentas Municipales

using Dominio.Entidades;
using Infraestructura.Repositorios;

namespace Aplicacion.Servicios
{
    public class ServicioBase<T> where T : class
    {
        protected readonly IRepositorio<T> _repo;
        public ServicioBase(IRepositorio<T> repo) => _repo = repo;

        public Task<IEnumerable<T>> ListarAsync() => _repo.ObtenerTodosAsync();
        public Task<T?> ObtenerPorIdAsync(int id) => _repo.ObtenerPorIdAsync(id);
        public Task AgregarAsync(T entidad) => _repo.AgregarAsync(entidad);
        public void Actualizar(T entidad) => _repo.Actualizar(entidad);
        public void Eliminar(T entidad) => _repo.Eliminar(entidad);
        public Task GuardarCambiosAsync() => _repo.GuardarCambiosAsync();
    }

    public class ContribuyenteService : ServicioBase<Contribuyente> { public ContribuyenteService(IRepositorio<Contribuyente> repo) : base(repo) {} }
    public class InmuebleService : ServicioBase<Inmueble> { public InmuebleService(IRepositorio<Inmueble> repo) : base(repo) {} }
    public class TributoService : ServicioBase<Tributo> { public TributoService(IRepositorio<Tributo> repo) : base(repo) {} }
    public class PagoService : ServicioBase<Pago> { public PagoService(IRepositorio<Pago> repo) : base(repo) {} }
    public class NotificacionService : ServicioBase<Notificacion> { public NotificacionService(IRepositorio<Notificacion> repo) : base(repo) {} }
    public class UsuarioService : ServicioBase<Usuario> { public UsuarioService(IRepositorio<Usuario> repo) : base(repo) {} }
    public class MunicipioService : ServicioBase<Municipio> { public MunicipioService(IRepositorio<Municipio> repo) : base(repo) {} }
    public class EstadoService : ServicioBase<Estado> { public EstadoService(IRepositorio<Estado> repo) : base(repo) {} }
    public class EstadoTributoService : ServicioBase<EstadoTributo> { public EstadoTributoService(IRepositorio<EstadoTributo> repo) : base(repo) {} }
    public class LocalidadService : ServicioBase<Localidad> { public LocalidadService(IRepositorio<Localidad> repo) : base(repo) {} }
    public class TipoDocumentoService : ServicioBase<TipoDocumento> { public TipoDocumentoService(IRepositorio<TipoDocumento> repo) : base(repo) {} }
    public class PeriodicidadService : ServicioBase<Periodicidad> { public PeriodicidadService(IRepositorio<Periodicidad> repo) : base(repo) {} }
    public class TipoImpuestoService : ServicioBase<TipoImpuesto> { public TipoImpuestoService(IRepositorio<TipoImpuesto> repo) : base(repo) {} }
    public class ValorTipoImpuestoService : ServicioBase<ValorTipoImpuesto> { public ValorTipoImpuestoService(IRepositorio<ValorTipoImpuesto> repo) : base(repo) {} }
    public class TipoInmuebleService : ServicioBase<TipoInmueble> { public TipoInmuebleService(IRepositorio<TipoInmueble> repo) : base(repo) {} }
    public class TitularidadInmuebleService : ServicioBase<TitularidadInmueble> { public TitularidadInmuebleService(IRepositorio<TitularidadInmueble> repo) : base(repo) {} }
    public class ValuacionInmuebleService : ServicioBase<ValuacionInmueble> { public ValuacionInmuebleService(IRepositorio<ValuacionInmueble> repo) : base(repo) {} }
    public class ObraPublicaService : ServicioBase<ObraPublica> { public ObraPublicaService(IRepositorio<ObraPublica> repo) : base(repo) {} }
    public class InmuebleObraService : ServicioBase<InmuebleObra> { public InmuebleObraService(IRepositorio<InmuebleObra> repo) : base(repo) {} }
    public class MedioPagoService : ServicioBase<MedioPago> { public MedioPagoService(IRepositorio<MedioPago> repo) : base(repo) {} }
    public class PlanPagoService : ServicioBase<PlanPago> { public PlanPagoService(IRepositorio<PlanPago> repo) : base(repo) {} }
    public class CuotaPlanService : ServicioBase<CuotaPlan> { public CuotaPlanService(IRepositorio<CuotaPlan> repo) : base(repo) {} }
    public class InteresMoraService : ServicioBase<InteresMora> { public InteresMoraService(IRepositorio<InteresMora> repo) : base(repo) {} }
    public class PlantillaNotificacionService : ServicioBase<PlantillaNotificacion> { public PlantillaNotificacionService(IRepositorio<PlantillaNotificacion> repo) : base(repo) {} }
    public class ColaNotificacionService : ServicioBase<ColaNotificacion> { public ColaNotificacionService(IRepositorio<ColaNotificacion> repo) : base(repo) {} }
    public class AuditoriaService : ServicioBase<Auditoria> { public AuditoriaService(IRepositorio<Auditoria> repo) : base(repo) {} }
    public class IntegracionExternaService : ServicioBase<IntegracionExterna> { public IntegracionExternaService(IRepositorio<IntegracionExterna> repo) : base(repo) {} }
}
