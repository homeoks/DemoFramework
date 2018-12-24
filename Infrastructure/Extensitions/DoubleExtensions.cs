using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extensitions
{
    public static class DoubleExtensions
    {
        public static string ToString2Decimals(this double value) => 
            new decimal(value).ToString("F");

        public static string ToString2Decimals(this double? value) => 
            !value.HasValue ? "0" : value.Value.ToString2Decimals();

        public static double Ground2Decimals(this double value) => 
            Math.Round((value * 100) / 100, 2, MidpointRounding.AwayFromZero);

        public static double Ground2Decimals(this double? value) => 
            value?.Ground2Decimals() ?? 0;

        public static double GroupNDecimals(this double value, int decimals) => 
            Math.Round(value, decimals, MidpointRounding.AwayFromZero);

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        public static string FormatCurrency(this double value) => value == 0 ? "0.00" 
            : Convert.ToDecimal(value).ToString("#,###.00");

        public static string FormatCurrencyV2(this double value)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (value == 0) return "0.00";
            return value < 0
                ? "-$" + Convert.ToDecimal(value * -1).ToString("#,###.00")
                : Convert.ToDecimal(value).ToString("#,###.00");
        }
        public static string FormatCurrency(this double? value) => 
            !value.HasValue ? new decimal(0).ToString("#,###.00") : value.Value.FormatCurrency();
    }
}
