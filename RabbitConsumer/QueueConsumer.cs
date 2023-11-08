using System.Text;
using icarus.estoqueWorker.Entity;
using icarus.estoqueWorker.Repository;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace icarus.estoqueWorker.RabbitConsumer;
public class QueueConsumer : Base, IQueueConsumer
{
    private readonly IConfiguration _config;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IRepoEstoque _repo;


    public QueueConsumer(IConfiguration config, IRepoEstoque repo)
    {
        _config = config;
        _repo = repo;
        var factory = new ConnectionFactory()
        {
            HostName = _config["RabbitMQ"],
            Port = int.Parse(_config["RabbitPort"]),
            UserName = Environment.GetEnvironmentVariable("RABBIT_MQ_USER"),
            Password = Environment.GetEnvironmentVariable("RABBIT_MQ_PWD"),
        };
        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            criarFilas(_channel);
            _connection.ConnectionShutdown += RabbitMQFailed;
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Não foi possivel se conectar ao Message Bus: {e.Message}");
        }
    }
   public void VerificarFila()
        => ConsumirProdutosDisponiveis(_channel);

    void ConsumirProdutosDisponiveis(IModel channel)
    {
        if (_channel.MessageCount(FilaEstoque) != 0)
        {
            // Definindo um consumidor
            var consumer = new EventingBasicConsumer(channel);

            // Definindo o que o consumidor recebe
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    // transformando o body em um array
                    byte[] body = ea.Body.ToArray();

                    // transformando o body em string
                    var message = Encoding.UTF8.GetString(body);
                    var projeto = JsonConvert.DeserializeObject<EnvelopeRecebido>(message);

                    // Estará realizando a operação de adicição dos projetos no banco de dados
                    for (int i = 0; i <= channel.MessageCount(FilaEstoque); i++)
                    {
                        await _repo.atualizarEstoque(projeto);
                    }

                    // seta o valor no EventSlim
                    // msgsRecievedGate.Set();
                    Console.WriteLine($"--> Dado consumido da fila [{FilaEstoque}]");
                    Console.WriteLine(message);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                }
                catch (Exception e)
                {
                    channel.BasicNack(ea.DeliveryTag,
                    multiple: false,
                    requeue: true);
                    Console.WriteLine($"Erro ao consumir mensagem: {e.Message}");
                }

            };
            // Consome o evento
            channel.BasicConsume(queue: FilaEstoque,
                         autoAck: false,
             consumer: consumer);
        }

    }

    void RabbitMQFailed(object sender, ShutdownEventArgs e)
      => Console.WriteLine($"--> Não foi possivel se conectar ao Message Bus: {e}");

}
