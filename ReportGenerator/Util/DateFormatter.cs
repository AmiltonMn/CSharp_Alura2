using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator.Util;

internal class DateFormatter : IDataFormatter
{
    public string FormatDate(string input)
    {
        if (DateTime.TryParse(input, out DateTime date))
        {
            return date.ToString("dd/MM/yyyy");
        }

        return input;
    }
}
