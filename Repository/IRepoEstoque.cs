using icarus.estoqueWorker.Entity;

namespace icarus.estoqueWorker.Repository;
public interface IRepoEstoque
{
    Task atualizarEstoque(EnvelopeRecebido model);
}
