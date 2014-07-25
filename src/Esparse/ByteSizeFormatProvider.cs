using System;

// ReSharper disable once CheckNamespace

// Adapted from: http://stackoverflow.com/questions/128618/c-file-size-format-provider
// Credit: http://flimflan.com/blog/FileSizeFormatProvider.aspx

sealed class ByteSizeFormatProvider : IFormatProvider, ICustomFormatter
{
    public object GetFormat(Type formatType)
    {
        return formatType == typeof(ICustomFormatter) ? this : null;
    }

    private const string FormatSpecifier = "SZ";
    private const decimal OneKiloByte = 1024m;
    private const decimal OneMegaByte = OneKiloByte * 1024m;
    private const decimal OneGigaByte = OneMegaByte * 1024m;

    public string Format(string format, object arg, IFormatProvider formatProvider)
    {
        if (format == null || !format.StartsWith(FormatSpecifier, StringComparison.Ordinal))
            return DefaultFormat(format, arg, formatProvider);

        if (arg is string)
            return DefaultFormat(format, arg, formatProvider);

        decimal size;

        try
        {
            size = Convert.ToDecimal(arg, formatProvider);
        }
        catch (InvalidCastException)
        {
            return DefaultFormat(format, arg, formatProvider);
        }

        // TODO: Localization of byte(s), KB, MB, etc.

        string suffix;
        var ignorePrecision = false;
        if (size > OneGigaByte)
        {
            size /= OneGigaByte;
            suffix = "GB";
        }
        else if (size > OneMegaByte)
        {
            size /= OneMegaByte;
            suffix = "MB";
        }
        else if (size > OneKiloByte)
        {
            size /= OneKiloByte;
            suffix = "KB";
        }
        else if (size == 1)
        {
            suffix = " byte";
            ignorePrecision = true;
        }
        else
        {
            suffix = " bytes";
            ignorePrecision = true;
        }

        var precision = ignorePrecision ? "0" : format.Substring(FormatSpecifier.Length);
        return size.ToString(GetPrecisionFormat(precision), formatProvider) + suffix;
    }

    static string GetPrecisionFormat(string precision)
    {
        if (string.IsNullOrEmpty(precision))
            return GetPrecisionFormat("2");

        if (precision.Length == 1)
        {
            switch (precision[0])
            {
                case '0': return "N0";
                case '1': return "N1";
                case '2': return "N2";
                case '3': return "N3";
                case '4': return "N4";
            }
        }

        return "N" + precision;
    }

    static string DefaultFormat(string format, object arg, IFormatProvider formatProvider)
    {
        var formattable = arg as IFormattable;
        return formattable != null
            ? formattable.ToString(format, formatProvider)
            : arg.ToString();
    }
}
