// Job programado para enviar notificaciones desde la cola
// Ya tenés el job NotificacionJob listo para ser ejecutado por Hangfire. 
// Toma notificaciones pendientes de la cola y simula el envío marcándolas como enviadas o fallidas.

using Aplicacion.Servicios;
using Dominio.Entidades;
using Microsoft.Extensions.Logging;

namespace Infraestructura.Jobs
{
    public class NotificacionJob
    {
        private readonly ColaNotificacionService _colaService;
        private readonly ILogger<NotificacionJob> _logger;

        public NotificacionJob(ColaNotificacionService colaService, ILogger<NotificacionJob> logger)
        {
            _colaService = colaService;
            _logger = logger;
        }

        public async Task EjecutarAsync()
        {
            var pendientes = (await _colaService.ListarAsync())
                .Where(n => n.EstadoEnvio == "pendiente")
                .ToList();

            foreach (var item in pendientes)
            {
                try
                {
                    // Simulación de envío (API externa real debería ir aquí)
                    _logger.LogInformation($"Enviando notificación a {item.CelularDestino} por {item.Canal}...");
                    item.EstadoEnvio = "enviado";
                    item.Intento++;
                }
                catch (Exception ex)
                {
                    item.Intento++;
                    item.EstadoEnvio = "fallido";
                    _logger.LogError(ex, $"Error al enviar notificación ID {item.Id}: {ex.Message}");
                }
            }

            await _colaService.GuardarCambiosAsync();
        }
    }
}
