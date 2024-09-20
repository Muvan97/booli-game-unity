using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Tools
{
    public static class StringTools
    {
        public static string GetStringWithoutNumbers(string input)
            => Regex.Replace(input, "[0-9]", "", RegexOptions.IgnoreCase);
        public static bool IsNullOrEmptyOrWhitespace(this string input) => string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input);
        public static string TruncateString(this string str, int maxLength) => str?[..Math.Min(str.Length, maxLength)];

        public static string ToKMBString(this int num)
        {
            if (num > 999999999 || num < -999999999 )
            {
                return num.ToString("0,,,.###B", CultureInfo.InvariantCulture);
            }

            if (num > 999999 || num < -999999 )
            {
                return num.ToString("0,,.##M", CultureInfo.InvariantCulture);
            }

            if (num > 999 || num < -999)
            {
                return num.ToString("0,.#K", CultureInfo.InvariantCulture);
            }

            return num.ToString(CultureInfo.InvariantCulture);
        }
        
        public static string ToKMBString(this decimal num)
        {
            var numString = num.ToString();
            var length = numString.Length;
            var dischargeLength = 3;
            var abbrevations = new List<string>
                {"QaD", "TD", "DD", "UD", "Dc", "No", "Oc", "Sp", "Sx", "Qi", "Qa", "T", "B", "M", "K"};

            for (var i = 0; i < abbrevations.Count; i++)
            {
                var abbrevation = abbrevations[i];
                
                var dischargeIndex = abbrevations.Count - i;

                if (length > dischargeIndex * dischargeLength)
                {
                    var remainder = length % dischargeLength;
                    
                    if (remainder == 0)
                        return numString[..dischargeLength] + abbrevation;
                    
                    return numString[..remainder] + "." + numString[remainder..(dischargeLength - remainder + 1)] + abbrevation;
                }
            }

            return num.ToString(CultureInfo.InvariantCulture);
        }
    }
}