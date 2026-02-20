using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Generators;
/// <summary>
/// Defines the contract for generating reports with customizable input, title, headline and footerline.
/// </summary>
/// <remarks>
/// Implementations of this interface allow users to configure report data and the title, headline, and footer, and generate a report as a string
/// </remarks>
public interface IReportGenerator
{
    /// <summary>
    /// Gets or sets the input data as a lista of dictionaries, where each dicitionary has key-value pairs.
    /// </summary>
    List<Dictionary<string, string>> Input { get; set; }

    /// <summary>
    /// Gets or sets the title associated with the current object
    /// </summary>
    string Title { get; set; }

    /// <summary>
    /// Gets or sets the Headline associated with the current object
    /// </summary>
    string HeadLine { get; set; }

    /// <summary>
    /// Gets or sets the FooterLine associated with the current object
    /// </summary>
    string FooterLine { get; set; }

    /// <summary>
    /// Generates a rpeort and returns it as a string
    /// </summary>
    /// <returns>
    /// A string cointaining the generated report's path.
    /// </returns>
    string GenerateReport();
}
