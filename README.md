
# Worker Estoque

Trata-se de um worker utilizado para consumir os dados que encontram-se atualmente em fila no RabbitMQ.

O processo será executado dentro do worker a cada 8ms para verificar se possui novos dados nas filas as quais está atualmente assinado.

Em caso positivo estará realizando os devidos processos necessários para tratativa dos dados.

![Fluxograma-estoque drawio](https://github.com/k4im/icarus.estoqueWorker/assets/108486349/88aac489-01da-4d0c-9527-458d4e2e0a2b)


## Tecnologias utilizadas

![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white) ![RabbitMQ](https://img.shields.io/badge/Rabbitmq-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white) ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white) ![GitHub Actions](https://img.shields.io/badge/github%20actions-%232671E5.svg?style=for-the-badge&logo=githubactions&logoColor=white)

## Deploy dotnet

Para fazer o deploy desse projeto através do dotnet

```bash
  dotnet run
```

## Deploy docker

Para fazer o deploy desse projeto através do docker

```bash
  docker run k4im:/worker-projeto:v0.1
```

