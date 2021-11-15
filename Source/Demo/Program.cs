using System;
using System.Collections.Generic;
using System.Linq;
using CodingHelmet.DeferredAggregation;

namespace Demo
{
    internal static class Program
    {
        private static string ApplyKingArthurSpeech(string line) =>
            line.Replace("three", "five");

        private static void Main()
        {
            try
            {
                IEnumerable<string> data = new DataSource(TimeSpan.FromMilliseconds(10))
                    .Fetch(40)
                    .AggregateStream(0, (sum, line) => sum + line.Length)
                    .Select(ApplyKingArthurSpeech)
                    .AsEnumerable();

                Console.WriteLine(string.Join(Environment.NewLine, data));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}
