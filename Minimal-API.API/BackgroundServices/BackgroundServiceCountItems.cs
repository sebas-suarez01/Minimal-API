using Microsoft.EntityFrameworkCore;
using Minimal_API.Domain.Items;
using Minimal_API.Persistance;

namespace Minimal_API.API.BackgroundServices;

public class BackgroundServiceCountItems : BackgroundService
{
    private readonly TimeSpan _period = new TimeSpan(0, 1, 0, 0);
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<BackgroundServiceCountItems> _logger;

    public BackgroundServiceCountItems(IServiceScopeFactory scopeFactory, ILogger<BackgroundServiceCountItems> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await DoWorkAsync(stoppingToken);
            await Task.Delay(_period, stoppingToken);
        }
    }

    private async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AgencyDbContext>();
            var numberOfItems = await context.Set<ItemModel>().CountAsync(stoppingToken);
            _logger.LogInformation($"Number of items: {numberOfItems} items");
        }
    }
}