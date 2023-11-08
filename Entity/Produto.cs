using System.ComponentModel.DataAnnotations;

namespace icarus.estoqueWorker.Entity;
public class Produto
{
    public Produto(string nome, double valor, int quantidade)
    {
        Nome = verificarNome(nome);
        Valor = verificarValorProduto(valor);
        Quantidade = verificarQuantidade(quantidade);
    }
    public int Id { get; private set; }

    [Required]
    public string Nome { get; private set; }

    [Required]
    public double Valor { get; private set; }

    [Required]
    public int Quantidade { get; set; }


    double verificarValorProduto(double valor)
    {
        if (valor < 0) throw new Exception("O valor não pode ser um valor menor que 0");
        return valor;
    }

    int verificarQuantidade(int quantidade)
    {
        if (quantidade < 0) throw new Exception("A quantidade não pode ser um valor menor que 0");
        return quantidade;
    }

    string verificarNome(string nome)
    {
        if (string.IsNullOrEmpty(nome)) throw new Exception("O nome não pode estar vazio!");
        if (!Regex.IsMatch(nome, @"^[a-zA-Z ]+$")) throw new Exception("O nome não pode conter caracteres especiais");
        return nome;
    }
    public void diminuirQuantidade(int quantidade)
    {
        this.Quantidade -= quantidade;
    }
    public void atualizarProduto(Produto model)
    {
        if(this.Nome != model.Nome) this.Nome = model.Nome;
        if(this.Quantidade != model.Quantidade) this.Quantidade = model.Quantidade;
        if(this.Valor != model.Valor) this.Valor = model.Valor; 
    }  
}
