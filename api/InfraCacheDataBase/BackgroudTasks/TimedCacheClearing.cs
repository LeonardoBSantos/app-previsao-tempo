using InfraCacheDataBase.BackgroudTasks.ScopedServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InfraCacheDataBase.BackgroudTasks
{
    internal class TimedCacheClearing : IHostedService, IDisposable
    {
        private readonly ILogger<TimedCacheClearing> _logger;
        public IServiceProvider Services { get; }
        private Timer? _timer;

        public TimedCacheClearing(IServiceProvider services, ILogger<TimedCacheClearing> logger)
        {
            Services = services;
            _logger = logger;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer
            (
                call => ExecuteAsync(),
                null,
                TimeSpan.FromMinutes(1),
                TimeSpan.FromMinutes(2)
            );

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task ExecuteAsync()
        {
            using (var scope = Services.CreateScope())
            {
                var scopedCacheClearingService =
                    scope.ServiceProvider
                        .GetRequiredService<IScopedCacheClearingService>();

                await scopedCacheClearingService.CacheClearing();
            }
        }
    }
}
