// Capa de Dominio - Entidades para el Sistema de Rentas Municipales
// Basado en el modelo relacional provisto

namespace Dominio.Entidades
{
    public class Municipio
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cuit { get; set; } = string.Empty;
        public string Responsable { get; set; } = string.Empty;
        public string PaginaWeb { get; set; } = string.Empty;
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string TipoMunicipio { get; set; } = string.Empty;
        public string Jurisdiccion { get; set; } = string.Empty;
    }

    public class Estado
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }

    public class EstadoTributo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }

    public class Localidad
    {
        public int Id { get; set; }
        public string Departamento { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string CodigoPostal { get; set; } = string.Empty;
    }

    public class TipoDocumento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }

    public class Periodicidad
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }

    public class TipoImpuesto
    {
        public int Id { get; set; }
        public int MunicipioId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int PeriodicidadId { get; set; }
        public bool TieneTablaValores { get; set; }
        public string ResolucionAsociada { get; set; } = string.Empty;
        public decimal ValorFijo { get; set; }
    }

    public class ValorTipoImpuesto
    {
        public int Id { get; set; }
        public int TipoImpuestoId { get; set; }
        public DateTime PeriodoDesde { get; set; }
        public DateTime PeriodoHasta { get; set; }
        public decimal Valor { get; set; }
        public string Resolucion { get; set; } = string.Empty;
    }

    public class TipoInmueble
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }

    public class ValuacionInmueble
    {
        public int Id { get; set; }
        public int InmuebleId { get; set; }
        public DateTime FechaValuacion { get; set; }
        public decimal Evaluo { get; set; }
        public string Fuente { get; set; } = string.Empty;
        public string Resolucion { get; set; } = string.Empty;
    }

    public class ObraPublica
    {
        public int Id { get; set; }
        public int MunicipioId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal CostoTotal { get; set; }
        public int EstadoId { get; set; }
    }

    public class InmuebleObra
    {
        public int InmuebleId { get; set; }
        public int ObraId { get; set; }
        public decimal PorcentajeParticipacion { get; set; }
    }

    public class MedioPago
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Condiciones { get; set; }
        public bool TieneRecargo { get; set; }
        public decimal? PorcentajeRecargo { get; set; }
        public bool TieneComision { get; set; }
        public decimal? PorcentajeComision { get; set; }
        public string TipoIntegracion { get; set; } = string.Empty;
        public string? DetallesIntegracion { get; set; }
    }

    public class PlanPago
    {
        public int Id { get; set; }
        public int ContribuyenteId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal MontoTotal { get; set; }
    }

    public class CuotaPlan
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int NumeroCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal MontoCuota { get; set; }
        public bool Pagada { get; set; }
    }

    public class InteresMora
    {
        public int Id { get; set; }
        public int TributoId { get; set; }
        public int DiasMora { get; set; }
        public decimal MontoInteres { get; set; }
        public DateTime CalculadoEn { get; set; }
    }

    public class PlantillaNotificacion
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Cuerpo { get; set; } = string.Empty;
        public string CanalDestino { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }

    public class ColaNotificacion
    {
        public int Id { get; set; }
        public int PlantillaId { get; set; }
        public int ContribuyenteId { get; set; }
        public string CelularDestino { get; set; } = string.Empty;
        public string Canal { get; set; } = string.Empty;
        public DateTime FechaProgramada { get; set; }
        public string EstadoEnvio { get; set; } = "pendiente";
        public int Intento { get; set; } = 0;
    }

    public class Auditoria
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string TablaAfectada { get; set; } = string.Empty;
        public int IdRegistroAfectado { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string IpUsuario { get; set; } = string.Empty;
        public string Modulo { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; } = DateTime.Now;
    }

    public class IntegracionExterna
    {
        public int Id { get; set; }
        public string NombreSistema { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string UrlApi { get; set; } = string.Empty;
        public string TokenAcceso { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }
} 
