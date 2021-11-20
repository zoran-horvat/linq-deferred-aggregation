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

        private static void Print(IEnumerable<string> data) =>
            Console.WriteLine(string.Join(Environment.NewLine, data));

        private static void UseLegacy(IEnumerable<string> data)
        {
            int MaxLength(int max, string line) => Math.Max(max, line.Length);
            int TotalLength(int sum, string line) => sum + line.Length;

            (int lengthBefore, int maxBefore, int lengthAfter, int maxAfter) = data
                .AggregateStreamLegacy(0, TotalLength)
                .AggregateStreamLegacy(0, MaxLength, (len1, max1) => (len1, max1))
                .Select(ApplyKingArthurSpeech)
                .Where(line => line.Contains("o"))
                .AggregateStreamLegacy(0, TotalLength, (prior, len2) => (prior.len1, prior.max1, len2))
                .AggregateStreamLegacy(0, MaxLength, (prior, max2) => (prior.len1, prior.max1, prior.len2, max2))
                .Reduce(Print);

            Console.WriteLine(
                $"Length before: {lengthBefore}; " +
                $"maximum length before: {maxBefore}; " +
                $"length after: {lengthAfter} " +
                $"maximum length after: {maxAfter}");
        }

        private static void Use(IEnumerable<string> data)
        {
            int MaxLength(int max, string line) => Math.Max(max, line.Length);
            int TotalLength(int sum, string line) => sum + line.Length;

            try
            {
                (int lengthBefore, int lengthAfter) = data
                    .AggregateStream(0, TotalLength)
                    .Select(ApplyKingArthurSpeech)
                    .Where(line => line.Contains("o"))
                    .AggregateStream(0, TotalLength)
                    .Reduce(Print);

            Console.WriteLine(
                $"Length before: {lengthBefore}; " +
                $"length after: {lengthAfter}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"NEW IMPLEMENTATION ERROR: {ex.Message}{Environment.NewLine}{ex}");
            }
        }

        private static void Main()
        {
            try
            {
                IEnumerable<string> data = new DataSource(TimeSpan.FromMilliseconds(10)).Fetch(40);

                Console.WriteLine("EXECUTION #1");
                UseLegacy(data);

                Console.WriteLine();
                Console.WriteLine("EXECUTION #2");
                UseLegacy(data);

                Use(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex}");
            }
        }
    }
}
