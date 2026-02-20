using Generators;
using Models;
using System.Text.Json;
/*
List<Dictionary<string, string>> data = new()
{
    new Dictionary<string, string>
    {
        {"Nome", "Alice"},
        {"Idade", "30"},
        {"Cidade", "Sâo Paulo"}
    },
    new Dictionary<string, string>
    {
        {"Nome", "Roberto"},
        {"Idade", "25"},
        {"Cidade", "Salvador"}
    },
    new Dictionary<string, string>
    {
        {"Nome", "Carlos"},
        {"Idade", "35"},
        {"Cidade", "Rio de Janeiro"}
    }
};

CSVReporterGenerator reportGenerator = new(data);

reportGenerator.Title = "Relatório de Usuários";
reportGenerator.HeadLine = "Lista de pessoas cadastradas no sistema";
reportGenerator.FooterLine = "Total de pessoas: " + data.Count;

string caminhoArquivo = reportGenerator.GenerateReport();

Console.WriteLine($"Caminho do arquivo: {caminhoArquivo}"); */

using (HttpClient client = new HttpClient())
{
    try 
    {
        string resposta = await client.GetStringAsync("https://guilhermeonrails.github.io/api-csharp-songs/songs.json");
        var musicas = JsonSerializer.Deserialize<List<Musica>>(resposta)!;

        List<Dictionary<string, string>> reportData = new List<Dictionary<string, string>>();

        foreach (var musica in musicas)
        {
            var record = new Dictionary<string, string>
            {
                {"Nome", musica.Song},
                {"Artista", musica.Artist},
                {"Year", (musica.Year).ToString()},
                {"Duration", (musica.Duration).ToString()}
            };

            reportData.Add(record);

            IReportGenerator reportGenerator = new CSVReporterGenerator(reportData);

            string localArquivo = reportGenerator.GenerateReport();

            Console.WriteLine($"Caminho do arquivo {localArquivo}");
        }
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}