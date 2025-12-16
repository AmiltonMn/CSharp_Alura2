using System.Collections;

var carrinho = new List<Produto> 
{
    new() { Nome = "Leite", Preco = 7.99},
    new() { Nome = "Manteiga", Preco = 3.45}
};

Semana semana = new();

foreach (var dia in semana)
{
    Console.WriteLine(dia);
}

public class Produto
{
    public string Nome { get; set; }
    public double Preco { get; set; }
};

public class DiasDaSemanaEnumerator : IEnumerator<String>
{
    private string[] Dias = { "Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado" };

    private int pos = -1;

    public string Current => Dias[pos];

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        
    }

    public bool MoveNext()
    {
        pos++;
        return pos < Dias.Length;
    }

    public void Reset()
    {
        pos = -1;
    }
}

public class Semana : IEnumerable<string>
{
    public IEnumerator<string> GetEnumerator()
    {
        return new DiasDaSemanaEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}