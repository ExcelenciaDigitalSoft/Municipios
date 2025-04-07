// Capa de Infraestructura - DbContext para el Sistema de Rentas Municipales

using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Contexto
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Municipio> Municipios => Set<Municipio>();
        public DbSet<Estado> Estados => Set<Estado>();
        public DbSet<EstadoTributo> EstadoTributo => Set<EstadoTributo>();
        public DbSet<Localidad> Localidades => Set<Localidad>();
        public DbSet<TipoDocumento> TiposDocumento => Set<TipoDocumento>();
        public DbSet<Periodicidad> Periodicidades => Set<Periodicidad>();
        public DbSet<TipoImpuesto> TiposImpuesto => Set<TipoImpuesto>();
        public DbSet<ValorTipoImpuesto> ValoresTipoImpuesto => Set<ValorTipoImpuesto>();
        public DbSet<TipoInmueble> TiposInmueble => Set<TipoInmueble>();
        public DbSet<ValuacionInmueble> ValuacionesInmueble => Set<ValuacionInmueble>();
        public DbSet<ObraPublica> ObrasPublicas => Set<ObraPublica>();
        public DbSet<InmuebleObra> InmueblesObras => Set<InmuebleObra>();
        public DbSet<MedioPago> MediosPago => Set<MedioPago>();
        public DbSet<PlanPago> PlanesPago => Set<PlanPago>();
        public DbSet<CuotaPlan> CuotasPlan => Set<CuotaPlan>();
        public DbSet<InteresMora> InteresesMora => Set<InteresMora>();
        public DbSet<PlantillaNotificacion> PlantillasNotificacion => Set<PlantillaNotificacion>();
        public DbSet<ColaNotificacion> ColaNotificaciones => Set<ColaNotificacion>();
        public DbSet<Auditoria> Auditoria => Set<Auditoria>();
        public DbSet<IntegracionExterna> IntegracionesExternas => Set<IntegracionExterna>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InmuebleObra>().HasKey(io => new { io.InmuebleId, io.ObraId });
            modelBuilder.Entity<ColaNotificacion>().Property(c => c.EstadoEnvio).HasDefaultValue("pendiente");
            modelBuilder.Entity<CuotaPlan>().Property(c => c.Pagada).HasDefaultValue(false);
            modelBuilder.Entity<MedioPago>().Property(m => m.TieneRecargo).HasDefaultValue(false);
            modelBuilder.Entity<MedioPago>().Property(m => m.TieneComision).HasDefaultValue(false);
            modelBuilder.Entity<PlantillaNotificacion>().Property(p => p.Activo).HasDefaultValue(true);
            modelBuilder.Entity<Auditoria>().Property(a => a.FechaHora).HasDefaultValueSql("GETDATE()");
        }
    }
}

