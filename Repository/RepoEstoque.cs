using icarus.estoqueWorker.Data;
using icarus.estoqueWorker.Entity;
using Microsoft.EntityFrameworkCore;

namespace icarus.estoqueWorker.Repository;
public class RepoEstoque : IRepoEstoque
{
    public async Task atualizarEstoque(EnvelopeRecebido model)
    {
        try
        {
            using var db = new DataContext();
            var produto = await db.Produtos
                .FirstOrDefaultAsync(x => x.Id == model.ProdutoUtilizado);
            if (produto == null) Console.WriteLine("Produto nulo");
            produto.Quantidade -= model.QuantidadeUtilizado;
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            Console.WriteLine("Não é possivel realizar esta operação, a mesma já foi realizada");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Não foi possivel realizar a operação no repo: {e.Message}");
        }
    }
}
