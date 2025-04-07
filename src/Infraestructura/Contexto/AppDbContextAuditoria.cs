// DbContext con Auditoría automática
//soporte para auditoría automática. 
//Registra cambios en cualquier entidad (Add, Update, Delete) 
//en la tabla Auditoria.

using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace Infraestructura.Contexto
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Auditoria> Auditoria => Set<Auditoria>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entradas = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .ToList();

            foreach (var entrada in entradas)
            {
                var auditoria = new Auditoria
                {
                    TablaAfectada = entrada.Entity.GetType().Name,
                    IdRegistroAfectado = ObtenerIdEntidad(entrada),
                    Accion = entrada.State.ToString(),
                    Descripcion = JsonSerializer.Serialize(entrada.CurrentValues.ToObject()),
                    FechaHora = DateTime.UtcNow,
                    IdUsuario = 0, // Reemplazar con usuario actual si hay contexto
                    IpUsuario = "0.0.0.0",
                    Modulo = "AppDbContext"
                };
                Auditoria.Add(auditoria);
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        private int ObtenerIdEntidad(EntityEntry entrada)
        {
            var propiedad = entrada.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey());
            return propiedad?.CurrentValue != null ? Convert.ToInt32(propiedad.CurrentValue) : 0;
        }
    }
}
