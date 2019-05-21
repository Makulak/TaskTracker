using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Syncfusion.DataSource.Extensions;
using TaskTracker.Models;

public class PlatformCulture
{
    public string PlatformString { get; private set; }
    public string LanguageCode { get; private set; }
    public string LocaleCode { get; private set; }

    public PlatformCulture(string cultureCode)
    {
        if (string.IsNullOrEmpty(cultureCode))
        {
            throw new ArgumentException("Expected culture identifier", "cultureCode");
        }

        PlatformString = cultureCode.Replace("_", "-");
        var dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);

        if (dashIndex > 0)
        {
            var parts = PlatformString.Split('-');
            LanguageCode = parts[0];
            LocaleCode = parts[1];
        }
        else
        {
            LanguageCode = PlatformString;
            LocaleCode = "";
        }
    }
}