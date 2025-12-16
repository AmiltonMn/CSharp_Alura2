using System.Collections;

Musica musica1 = new Musica
{
    Titulo = "Que País é Esse?",
    Artista = "Legião Urbana",
    Duracao = 250
};

Musica musica2 = new Musica
{
    Titulo = "Tempo Perdido",
    Artista = "Legião Urbana",
    Duracao = 455
};

Musica musica3 = new Musica
{
    Titulo = "Pro Dia Nascer Feliz",
    Artista = "Barão Vermelho",
    Duracao = 345
};

Musica musica4 = new Musica
{
    Titulo = "Eduardo e Mônica",
    Artista = "Legião Urbana",
    Duracao = 530
};

Musica musica5 = new Musica
{
    Titulo = "Geração Coca-Cola",
    Artista = "Legião Urbana",
    Duracao = 350
};

Playlist playlist = new Playlist { Nome = "Rock nacional" };

playlist.Add(musica1);
playlist.Add(musica2);
playlist.Add(musica3);
playlist.Add(musica4);
playlist.Add(musica5);

ExibirPlaylist(playlist);

Console.WriteLine($"\nMúsica aleatória: {playlist.ObterAleatoria()!.Titulo}");

playlist.OrdenarPorDuracao();
ExibirPlaylist(playlist);

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
}

class Playlist : ICollection<Musica>
{
    private List<Musica> musicas = new();
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