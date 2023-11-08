using icarus.estoqueWorker;
using icarus.estoqueWorker.RabbitConsumer;
using icarus.estoqueWorker.Repository;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<IRepoEstoque, RepoEstoque>();
        services.AddScoped<IQueueConsumer, QueueConsumer>();
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
