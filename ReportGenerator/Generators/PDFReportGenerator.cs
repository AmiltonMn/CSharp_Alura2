using iText.Kernel.Pdf;
using iText.Layout.Element;
using ReportGenerator.Util;
using System.Text;

namespace ReportGenerator.Generators;

public class PDFReporterGenerator : ReporterGeneratorBase
{

    public PDFReporterGenerator(List<Dictionary<string, string>> input) : base(input) { }

    public override string GenerateReport() 
    {
        if (Input is null || Input.Count == 0)
            throw new Exception("Input de dados inválido!");

        using var writer = new PdfWriter("report.pdf");
        using var pdf = new PdfDocument(writer);
        using var document = new iText.Layout.Document(pdf);

        if (!string.IsNullOrWhiteSpace(Title))
        {
            var title = new iText.Layout.Element.Paragraph(Title)
                .SetFontSize(20)
                .SetBold()
                .SetMarginBottom(10);

            document.Add(title);
        }

        if (!string.IsNullOrWhiteSpace(HeadLine))
        {
            var headLine = new iText.Layout.Element.Paragraph(HeadLine)
                .SetFontSize(16)
                .SetBold()
                .SetMarginBottom(10);

            document.Add(headLine);
        }

        Table table = new Table(Input.First().Keys.Count);

        foreach (var header in Input.First().Keys)
        {
            table.AddHeaderCell(new Cell().Add(new Paragraph(header).SetBold()));
        }

        foreach (var record in Input)
        {
            foreach (var value in record.Values)
            {
                table.AddCell(new Cell().Add(new Paragraph(value)));
            }
        }

        if (!string.IsNullOrWhiteSpace(FooterLine))
        {
            var footer = new iText.Layout.Element.Paragraph(FooterLine)
                .SetFontSize(12)
                .SetMarginBottom(10);

            document.Add(footer);
        }

        return Path.GetFullPath("report.pdf");
    }
}
