using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ReportGenerator.Util;

internal interface IDataFormatter
{
    string FormatDate(string input);
}
