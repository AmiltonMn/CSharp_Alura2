using System;

namespace Projeto3;

static class MusicasExtensions
{

    public static IEnumerable<Musica> FiltrarPor(this IEnumerable<Musica> musicas, Func<Musica, bool> condicao)
    {
        foreach (var musica in musicas)
        {
            if (condicao(musica)) yield return musica;
        }
    }
}
