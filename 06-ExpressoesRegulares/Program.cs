using Expressoes;
using System.Text.RegularExpressions;

using var arquivo = new FileStream("../../../musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);

//var musicas = ObterMusicas(stream)
//    .Where(m => m.Artista.Equals("COLDPLAY", StringComparison.OrdinalIgnoreCase));

//ExibirMusicasEmTabela(musicas.Take(50));

var linha = "The Broken Road;Rolling Stones;6:39;Rock, Blues Rock;13/09/1974";

var match = Regex.Match(linha, @"\d:\d\d");
if (match.Success)
{
    Console.WriteLine($"Duração encontrada!! {match.Value}");
}
else
{
    Console.WriteLine("Duração não encontrada!");
}

void ExibirMusicas(IEnumerable<Musica> musicas)
{
    Console.WriteLine("\nExibindo as músicas: ");

    foreach (var musica in musicas)
    {
        Console.WriteLine($"\t - {musica.Titulo} | {musica.Artista} - {musica.Duracao}s [{musica.Lancamento}]");
    }
}

void Interning()
{
    var artista1 = "Coldlplay"; // interning - string literal
    var artista2 = "Coldlplay";
    var artista3 = new String("Coldlplay"); // não faz interning
    var artista4 = "COLDPLAY";
    var artista5 = string.Intern(artista1.ToUpper()); // HEAP x

    Console.WriteLine(artista1 == artista2); // True
    Console.WriteLine(ReferenceEquals(artista1, artista3));
    Console.WriteLine(ReferenceEquals(artista1, artista4));
    Console.WriteLine(ReferenceEquals(artista4, artista5));
}

void ExibirMusicasEmTabela(IEnumerable<Musica> musicas)
{
    var titulo = "\nMúsicas do arquivo: ";

    Console.WriteLine(titulo);

    var colunaTitulo = "Título".PadRight(40);
    var colunaArtista = "Artista".PadRight(30);
    var colunaDuracao = "Duração".PadRight(10);
    var colunaLancamento = "Lançada Em".PadRight(15);

    Console.WriteLine($"{colunaTitulo}{colunaArtista}{colunaDuracao}{colunaLancamento}");
    var borda = "".PadRight(100, '=');
    Console.WriteLine(borda);

    foreach (var musica in musicas)
    {
        Console.WriteLine($"{musica.Titulo,-40}{musica.Artista,-30}{musica.Duracao / 60.0f,-10:F2}{musica.Lancamento,-15:dd/MM/yyyy}");
    }
}

IEnumerable<Musica> ObterMusicas(StreamReader stream)
{
    var linha = stream.ReadLine();

    while (linha is not null)
    {
        var partes = linha.Split(';');
        if (partes.Length == 5)
        {
            var musica = new Musica
            {
                Titulo = string.IsNullOrWhiteSpace(partes[0]) ? "Título não encontrado!" : partes[0],
                Artista = string.IsNullOrWhiteSpace(partes[1]) ? "Artista não encontrado!" : partes[1],
                Duracao = int.TryParse(partes[2], out int duracao) ? duracao : 350,
                Generos = partes[3].Split(',', StringSplitOptions.TrimEntries),
                Lancamento = DateTime.TryParse(partes[4], out var data) ? data : DateTime.Today
            };
            yield return musica;
        }
        linha = stream.ReadLine();
    }
}