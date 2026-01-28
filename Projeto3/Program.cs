using Projeto3;

using var arquivo = new FileStream("../../../musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);

var musicas = ObterMusicas(stream);
var artista = "Coldplay";

var musicasFiltradasLINQ = 
    ObterMusicas(stream)
    .Where(musica => musica.Artista == artista)
    .Where(m => m.Duracao >= 400);

// Filtros feitos 
var musicasFiltradasExtension =
    ObterMusicas(stream)
    .FiltrarPor(musica => musica.Artista == artista)
    .FiltrarPor(m => m.Duracao >= 400);

ExibirMusicas(musicasFiltradasLINQ);

void ExibirMusicas(IEnumerable<Musica> musicas) 
{
    var cont = 1;
    Console.WriteLine("\nExibindo as músicas: ");

    foreach (var musica in musicas)
    {
        Console.WriteLine($"\t - {musica.Titulo} | {musica.Artista} - {musica.Duracao}s");
        cont++;

        if (cont > 10)
            break;
    }
} 

IEnumerable<Musica> ObterMusicas(StreamReader stream) 
{
    var linha = stream.ReadLine();

    while (linha is not null)
    {
        var partes = linha.Split(';');
        var musica = new Musica
        {
            Titulo = partes[0],
            Artista = partes[1],
            Duracao = Convert.ToInt32(partes[2])
        };

        yield return musica;
        linha = stream.ReadLine();
    }
}