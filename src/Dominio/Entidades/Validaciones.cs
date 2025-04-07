// Validaciones para entidades del Sistema de Rentas Municipales

using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public class Contribuyente
    {
        public int Id { get; set; }
        [Required, StringLength(100)] public string Nombres { get; set; } = string.Empty;
        [Required, StringLength(100)] public string Apellidos { get; set; } = string.Empty;
        [Required, StringLength(20)] public string NumeroDocumento { get; set; } = string.Empty;
        [EmailAddress] public string Email { get; set; } = string.Empty;
        [Phone] public string Telefono { get; set; } = string.Empty;
        [Phone] public string Celular { get; set; } = string.Empty;
        [Required] public int EstadoId { get; set; }
        [Required, StringLength(100)] public string Calle { get; set; } = string.Empty;
        [Required, StringLength(10)] public string Numero { get; set; } = string.Empty;
        [StringLength(10)] public string Orientacion { get; set; } = string.Empty;
        public string Referencias { get; set; } = string.Empty;
        [Required] public int IdMunicipio { get; set; }
        [Required] public int IdLocalidad { get; set; }
        [Required] public int IdTipoDocumento { get; set; }
    }

    public class Inmueble
    {
        public int Id { get; set; }
        [Required, StringLength(100)] public string Calle { get; set; } = string.Empty;
        [Required, StringLength(10)] public string Numero { get; set; } = string.Empty;
        [StringLength(10)] public string Orientacion { get; set; } = string.Empty;
        public string Referencias { get; set; } = string.Empty;
        [Range(0, double.MaxValue)] public decimal EvaluoFiscal { get; set; }
        [Required] public int EstadoId { get; set; }
        [Required] public int IdTipoInmueble { get; set; }
        [Required] public int IdLocalidad { get; set; }
    }

    public class Tributo
    {
        public int Id { get; set; }
        [Required] public int IdMunicipio { get; set; }
        [Required] public int IdInmueble { get; set; }
        [Required] public int IdTipoImpuesto { get; set; }
        [Required, StringLength(100)] public string Descripcion { get; set; } = string.Empty;
        [Required, StringLength(6)] public string Periodo { get; set; } = string.Empty;
        [Required] public decimal Monto { get; set; }
        [Required] public DateTime FechaEmision { get; set; }
        [Required] public DateTime FechaVencimiento { get; set; }
        [Required] public int IdEstado { get; set; }
    }

    public class Notificacion
    {
        public int Id { get; set; }
        [Required] public int IdContribuyente { get; set; }
        [Required, StringLength(20)] public string CelularDestino { get; set; } = string.Empty;
        [Required, StringLength(20)] public string Canal { get; set; } = string.Empty;
        [Required] public DateTime FechaEmision { get; set; }
        [Required] public string Mensaje { get; set; } = string.Empty;
        public bool Enviada { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public bool Leida { get; set; }
    }

    public class Usuario
    {
        public int Id { get; set; }
        [Required, StringLength(50)] public string NombreUsuario { get; set; } = string.Empty;
        [Required, StringLength(100)] public string Email { get; set; } = string.Empty;
        [Required] public string ClaveHash { get; set; } = string.Empty;
        [Required, StringLength(100)] public string NombreCompleto { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }

    public class Municipio
    {
        public int Id { get; set; }
        [Required, StringLength(100)] public string Nombre { get; set; } = string.Empty;
        [Required] public string Provincia { get; set; } = string.Empty;
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
        [Required] public string Descripcion { get; set; } = string.Empty;
    }
    public class EstadoTributo
    { public int Id { get; set; } [Required] public string Descripcion { get; set; } = string.Empty; }
    public class Localidad
    { public int Id { get; set; } [Required] public string LocalidadNombre { get; set; } = string.Empty; }
    public class TipoDocumento
    { public int Id { get; set; } [Required] public string Descripcion { get; set; } = string.Empty; }
    public class Periodicidad
    { public int Id { get; set; } [Required] public string Descripcion { get; set; } = string.Empty; }
    public class TipoImpuesto
    { public int Id { get; set; } [Required] public string Descripcion { get; set; } = string.Empty; }
    public class ValorTipoImpuesto
    { public int Id { get; set; } [Required] public decimal Valor { get; set; } }
    public class TipoInmueble
    { public int Id { get; set; } [Required] public string Descripcion { get; set; } = string.Empty; }
    public class TitularidadInmueble
    { public int Id { get; set; } [Required] public int IdInmueble { get; set; } [Required] public int IdContribuyente { get; set; } }
    public class ValuacionInmueble
    { public int Id { get; set; } [Required] public int IdInmueble { get; set; } [Required] public decimal Evaluo { get; set; } }
    public class ObraPublica
    { public int Id { get; set; } [Required] public string Descripcion { get; set; } = string.Empty; }
    public class InmuebleObra
    { public int InmuebleId { get; set; } public int ObraId { get; set; } public decimal PorcentajeParticipacion { get; set; } }
    public class MedioPago
    { public int Id { get; set; } [Required] public string Nombre { get; set; } = string.Empty; }
    public class PlanPago
    { public int Id { get; set; } [Required] public int IdContribuyente { get; set; } [Required] public string Descripcion { get; set; } = string.Empty; }
    public class CuotaPlan
    { public int Id { get; set; } [Required] public int IdPlan { get; set; } [Required] public decimal MontoCuota { get; set; } }
    public class InteresMora
    { public int Id { get; set; } [Required] public int IdTributo { get; set; } [Required] public decimal MontoInteres { get; set; } }
    public class PlantillaNotificacion
    { public int Id { get; set; } [Required] public string Titulo { get; set; } = string.Empty; }
    public class ColaNotificacion
    { public int Id { get; set; } [Required] public int IdContribuyente { get; set; } }
    public class Auditoria
    { public int Id { get; set; } [Required] public string TablaAfectada { get; set; } = string.Empty; }
}
