using Projeto4;

using var arquivo = new FileStream("../../../musicas.csv", FileMode.Open, FileAccess.Read);
using var stream = new StreamReader(arquivo);

OperacoesDeVerificacaoDeExistencia(stream);

void OperacoesDeVerificacaoDeExistencia(StreamReader reader) 
{
    var musicas = ObterMusicas(reader).ToList();

    var artistas = musicas
        .GroupBy(m => m.Artista)
        .Where(g => g.Any(m => m.Duracao >= 500));

    Console.WriteLine("Artista com músicas de +500s");
    foreach (var artista in artistas)
    {
        Console.WriteLine($"Artista: {artista.Key}");
    }

    Console.WriteLine("Artistas com música de reggae");
    var reggae = musicas
        .GroupBy(m => m.Artista)
        .Where(g => g.Any(m => m.Generos.Contains("Reggae")));

    foreach (var artista in reggae)
    {
        Console.WriteLine($"\t - {artista.Key}");
    }
}


void ArtistaComMaiorQuantidade(StreamReader reader) 
{
    var artista = ObterMusicas(stream)
    .GroupBy(m => m.Artista)
    .Select(g => new { Artista = g.Key, Musicas = g, Total = g.Count() })
    .MaxBy(a => a.Total)!;

    Console.WriteLine($"O artista com maior quantidade de músicas: {artista.Artista} - {artista.Total} músicas");
}

void OperacoesDeObtencaoDeElementos(StreamReader reader) 
{
    var musicas = ObterMusicas(reader).ToList();

    var primeiraMusica = musicas.FirstOrDefault()!;
    Console.WriteLine($"A primeira música é {primeiraMusica.Titulo}");

    var maiorDuracao = musicas.MaxBy(m => m.Duracao)!;
    Console.WriteLine($"A música com maior duração: {maiorDuracao.Titulo} sendo {maiorDuracao.Duracao}s");
}

void OperacoesDeAgrupamento(StreamReader reader) 
{
    var artistas = ObterMusicas(stream)
    .GroupBy(m => m.Artista);

    Console.WriteLine($"\nExibindo as músicas de cada artista: ");
    foreach (var artista in artistas.Take(5))
    {
        Console.WriteLine($"\nArtista {artista.Key} com {artista.Count()} músicas:");

        foreach (var musica in artista)
        {
            Console.WriteLine($"\t - {musica.Titulo}");
        }
    }
}

void EstatisticasDeMusicas(StreamReader reader) 
{
    var musicas = ObterMusicas(reader).ToList();

    Console.WriteLine($"\nExistem {musicas.Count()} músicas na coleção!");
    Console.WriteLine($"\nExistem {musicas.Count(m => m.Duracao >= 600)} músicas com mais do que 10 minutos na coleção!");
    Console.WriteLine($"\nA música com menor duração da coleção leva {musicas.Min(m => m.Duracao)} segundos");
    Console.WriteLine($"\nA música com maior duração da coleção leva {musicas.Max(m => m.Duracao)} segundos");
    Console.WriteLine($"\nA duração média das músicas da coleção é {Math.Round(musicas.Average(m => m.Duracao), 2)}");
    Console.WriteLine($"\nVocê vai levar {musicas.Sum(m => m.Duracao) / (3600*24)} dias para ouvir toda a coleção!");
}


void OperacoesDeProjecao2(StreamReader stream) 
{
    var generos = ObterMusicas(stream)
        .SelectMany(m => m.Generos)
        .Distinct()
        .OrderBy(g => g);

    foreach (var item in generos)
    {
        Console.WriteLine($"Gênero: {item}");
    }
}

void OperacoesDeProjecao(StreamReader stream)
{
    var artistas = ObterMusicas(stream) // projeção/transformação
    .Select(m => m.Artista)
    .Distinct()
    .OrderBy(a => a);

    foreach (var artista in artistas)
    {
        Console.WriteLine($"Artista: {artista}");
    }
}

void OperacoesDeFiltroEORdenacao()
{
    var musicasFiltradasLINQ =
    ObterMusicas(stream)
    .Where(musica => musica.Artista == "Coldplay")
    .OrderBy(musica => musica.Titulo)
    .ThenBy(musica => musica.Duracao)
    .Take(10);

    ExibirMusicas(musicasFiltradasLINQ);
}

void ExibirMusicas(IEnumerable<Musica> musicas)
{
    Console.WriteLine("\nExibindo as músicas: ");

    foreach (var musica in musicas)
    {
        Console.WriteLine($"\t - {musica.Titulo} | {musica.Artista} - {musica.Duracao}s");
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
            Duracao = Convert.ToInt32(partes[2]),
            Generos = partes[3].Split(',').Select(g => g.Trim())
        };

        yield return musica;
        linha = stream.ReadLine();
    }
}