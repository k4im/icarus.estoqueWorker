using RabbitMQ.Client;

namespace icarus.estoqueWorker.RabbitConsumer;

public class Base
{
    /* Variaveis utilizadas pelo consumidor*/
    public string FilaEstoque = "atualizar.estoque";
    public string ExchangeEstoque = "projeto.adicionado/api.projetos";
    public string routingKeyEstoque = "projeto.atualizar.estoque";

    public void criarFilas(IModel channel)
    {
        // Declarando a fila para eventos que foram adicionados
        channel.QueueDeclare(queue: FilaEstoque,
            durable: true,
            exclusive: false,
            autoDelete: false);

        // Definindo o balanceamento da fila para uma mensagem por vez.
        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        // Definindo o Exchange no RabbitMQ
        channel.ExchangeDeclare(exchange: ExchangeEstoque,
        type: ExchangeType.Topic,
        durable: true,
        autoDelete: false);

        // Linkando a fila de eventos atualizados ao exchange
        channel.QueueBind(queue: FilaEstoque,
            exchange: ExchangeEstoque,
            routingKey: routingKeyEstoque);
    }
}
