using System.Collections;

Musica musica1 = new()
{
    Titulo = "Que País é Esse?",
    Artista = "Legião Urbana",
    Duracao = 250
};

Musica musica2 = new()
{
    Titulo = "Tempo Perdido",
    Artista = "Legião Urbana",
    Duracao = 455
};

Musica musica3 = new()
{
    Titulo = "Pro Dia Nascer Feliz",
    Artista = "Barão Vermelho",
    Duracao = 345
};

Musica musica4 = new()
{
    Titulo = "Eduardo e Mônica",
    Artista = "Legião Urbana",
    Duracao = 530
};

Musica musica5 = new()
{
    Titulo = "Geração Coca-Cola",
    Artista = "Legião Urbana",
    Duracao = 350
};

Playlist rockNacional = new() { Nome = "Rock nacional" };

rockNacional.Add(musica1);
rockNacional.Add(musica2);
rockNacional.Add(musica3);
rockNacional.Add(musica4);
rockNacional.Add(musica5);
rockNacional.Add(musica2);

Console.WriteLine($"\nMúsica aleatória: {rockNacional.ObterAleatoria()!.Titulo}");

rockNacional.OrdenarPorDuracao();

var legiaoUrbana = new Playlist() { Nome = "Mais populares da Legião" };
legiaoUrbana.Add(musica1);
legiaoUrbana.Add(musica2);
legiaoUrbana.Add(musica4);
legiaoUrbana.Add(musica5);


var player = new PlayerDeMusica();
player.AdicionarNaFila(musica1);
player.AdicionarNaFila(rockNacional);

ExibirFila(player);
ExibirHistorico(player);

var proxima = player.ProximaMusica();

if (proxima is not null)
    Console.WriteLine($"\nTocando a música {proxima.Titulo}");
else
    Console.WriteLine("\nFila de reprodução vazia!");

ExibirFila(player);
ExibirHistorico(player);

var anterior = player.MusicaAnterior();

if (anterior is not null)
    Console.WriteLine($"\nTocando a música {anterior.Titulo}");
else
    Console.WriteLine("\nHistórico de reprodução vazio!");

ExibirFila(player);
ExibirHistorico(player);

void ExibirHistorico(PlayerDeMusica player) 
{
    Console.WriteLine("\nExibindo o Histórico: ");
    foreach (var musica in player.Historico()) 
    {
        Console.WriteLine($"\t - {musica.Titulo}");
    }
}

void ExibirFila(PlayerDeMusica player)
{
    Console.WriteLine("\nExibindo a fila de reprodução:");

    foreach (var musica in player.Fila())
    {
        Console.WriteLine($"\t - {musica.Titulo}");
    }
}

void ExibirMaisTocadas(Playlist playlist1, Playlist playlist2) 
{
    Dictionary<Musica, int> ranking = [];

    foreach (var musica in playlist1)
    {
        ranking.Add(musica, 1);
    }

    foreach (var musica in playlist2)
    {
        if (ranking.TryGetValue(musica, out int contagem))
        {
            contagem++;
            ranking[musica] = contagem;
        }
        else 
        { 
            ranking[musica] = 1;
        }
    }

    List<KeyValuePair<Musica, int>> listaRanking = [..ranking];

    listaRanking.Sort(new PorContagem());

    Console.WriteLine("\nTop 3 músicas mais incluídas nas playlists:");
    int contador = 1;
    foreach (var par in listaRanking)
    {
        Console.WriteLine($"\t{contador} - {par.Key.Titulo}");
        contador++;

        if (contador > 3) break;
    }
}

void ExibirPlaylist(Playlist playlist) 
{
    Console.WriteLine($"Tocando as músicas de: {playlist.Nome}");

    foreach (var musica in playlist)
    {
        Console.WriteLine($"- {musica.Titulo} {musica.Duracao}s | {musica.Artista}");
    }
}

class Musica : IComparable
{
    public string Titulo { get; set; }
    public string Artista { get; set; }
    public int Duracao { get; set; }

    public int CompareTo(object? other)
    {
        if (other is null) return -1;
        if (other is Musica outraMusica) return this.Duracao.CompareTo(outraMusica.Duracao);
        return -1;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj is Musica outraMusica) 
            return this.Titulo.Equals(outraMusica.Titulo) && 
                   this.Artista.Equals(outraMusica.Artista);

        return false;
    }

    public override int GetHashCode()
    {
        return this.Titulo.GetHashCode() ^ this.Artista.GetHashCode();
    }
}

class PorContagem : IComparer<KeyValuePair<Musica, int>>
{
    public int Compare(KeyValuePair<Musica, int> x, KeyValuePair<Musica, int> y)
    {
        return y.Value.CompareTo(x.Value);
    }
}

class Playlist : ICollection<Musica>
{
    private List<Musica> musicas = new();
    private HashSet<Musica> set = [];

    public string Nome { get; set; }

    public int Count => musicas.Count();

    public bool IsReadOnly => false;

    public Musica? ObterPeloTitulo(string titulo) 
    {
        return musicas.Where(m => m.Titulo.Equals(titulo)).FirstOrDefault();
    }

    public void OrdenarPorDuracao() 
    {
        musicas.Sort();
    }

    public Musica? ObterAleatoria() 
    {
        if (musicas.Count == 0) return null;

        Random rand = new();
        int numAleatorio = rand.Next(0, musicas.Count() - 1);

        return musicas[numAleatorio];
    }

    public bool RemoverMusica() 
    {
        Console.WriteLine("Digite o título da música que deseja remover: ");
        string? titulo = Console.ReadLine();

        Musica? musica = ObterPeloTitulo(titulo!);

        if (musica is not null)
        {
            musicas.Remove(musica);
            return true;
        }
        else
        {
            Console.WriteLine("Música não encontrada!");
            return false;
        };
    }

    public void Add(Musica musica)
    {
        if(set.Add(musica))
            musicas.Add(musica);
    }

    public void Clear()
    {
        musicas.Clear();
    }

    public bool Contains(Musica item)
    {
        return musicas.Contains(item);
    }

    public void CopyTo(Musica[] array, int arrayIndex)
    {
        musicas.CopyTo(array, arrayIndex);
    }

    public IEnumerator<Musica> GetEnumerator()
    {
        return musicas.GetEnumerator();
    }

    public bool Remove(Musica item)
    {
        return musicas.Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class PlayerDeMusica 
{
    private Queue<Musica> fila = [];
    private Stack<Musica> pilha = [];

    public void AdicionarNaFila(Musica musica) 
    {
        fila.Enqueue(musica);
    }

    public void AdicionarNaFila(Playlist playlist) 
    {
        foreach (var musica in playlist)
            AdicionarNaFila(musica);
    }

    public Musica? ProximaMusica() 
    {
        if (fila.Count == 0) return null;
        var musica = fila.Dequeue();
        pilha.Push(musica);
        return musica;
    }

    public Musica? MusicaAnterior() 
    {
        if (pilha.Count == 0) return null;
        return pilha.Pop();
    }

    public IEnumerable<Musica> Fila() 
    {
        foreach (var musica in fila)
            yield return musica;
    }

    public IEnumerable<Musica> Historico() 
    {
        foreach(var musica in pilha)
            yield return musica;
    }
}