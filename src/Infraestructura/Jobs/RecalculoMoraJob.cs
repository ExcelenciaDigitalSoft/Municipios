// Job para recalcular intereses por mora sobre tributos impagos

using Aplicacion.Servicios;
using Dominio.Entidades;
using Microsoft.Extensions.Logging;

namespace Infraestructura.Jobs
{
    public class RecalculoMoraJob
    {
        private readonly TributoService _tributoService;
        private readonly InteresMoraService _interesService;
        private readonly ILogger<RecalculoMoraJob> _logger;

        public RecalculoMoraJob(TributoService tributoService, InteresMoraService interesService, ILogger<RecalculoMoraJob> logger)
        {
            _tributoService = tributoService;
            _interesService = interesService;
            _logger = logger;
        }

        public async Task EjecutarAsync()
        {
            var hoy = DateTime.Today;
            var tributos = (await _tributoService.ListarAsync())
                .Where(t => !t.Pagado && t.FechaVencimiento < hoy).ToList();

            foreach (var t in tributos)
            {
                var dias = (hoy - t.FechaVencimiento).Days;
                var interes = new InteresMora
                {
                    IdTributo = t.Id,
                    DiasMora = dias,
                    MontoInteres = Math.Round(t.Monto * 0.01m * dias, 2),
                    CalculadoEn = hoy
                };

                await _interesService.AgregarAsync(interes);
            }

            await _interesService.GuardarCambiosAsync();
            _logger.LogInformation($"Intereses recalculados para {tributos.Count} tributos vencidos.");
        }
    }
}
