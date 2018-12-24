using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Extensitions
{
    public static class StringExtension
    {
        public static int ParseToInt(this string src, int defaultValue = 0)
        {
            int value;
            var isNumeric = int.TryParse(src, out value);
            if (isNumeric)
                return value;
            return defaultValue;
        }

        public static int ParseToInt(this object src, int defaultValue = 0)
        {
            int value;
            var isNumeric = int.TryParse(src.ToString(), out value);
            if (isNumeric)
                return value;
            return defaultValue;
        }

        public static long ParseToLong(this string source)
        {
            long value;
            return long.TryParse(source, out value) ? value : value;
        }

        public static decimal ParseToDecimal(this string source, decimal defaultValue = -1)
        {
            if (source.Contains("."))
                source = source.Replace(".", ",");
            if (source.Contains(":"))
                source = source.Replace(":", "");
            decimal value;
            return decimal.TryParse(source, out value) ? value : defaultValue;
        }

        public static double ParseToDouble(this string source)
        {
            double value;
            return double.TryParse(source, out value) ? value : value;
        }

        public static string GenerateRandom(int length = 8)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return new string(Enumerable.Repeat(chars, length).Select(x => x[random.Next(x.Length)]).ToArray());
        }

        public static string ConvertStringToBase64(this string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            var base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        public static string ConvertBase64ToString(this string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }

        public static Stream ConvertBase64ToStream(this string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            return new MemoryStream(bytes);
        }


        public static string ReplaceMultiSpaces(this string value)
        {
            var options = RegexOptions.None;
            var regex = new Regex("[ ]{2,}", options);
            value = regex.Replace(value, " ");
            return value;
        }

        public static string CollapseSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", " ");
        }

        public static string Random(this string[] values)
        {
            var random = new Random();
            var valueRandom = random.Next(0, values.Length - 1);
            return values[valueRandom];
        }

    }
}
