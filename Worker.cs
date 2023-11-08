using icarus.estoqueWorker.RabbitConsumer;

namespace icarus.estoqueWorker;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _provider;

    public Worker(ServiceProvider provider)
    {
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try 
            {
                using var scope = _provider.CreateScope();
                IQueueConsumer consumer = scope.ServiceProvider.GetService<IQueueConsumer>();
                consumer.VerificarFila();        
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            await Task.Delay(8000, stoppingToken);
        }
    }
}
