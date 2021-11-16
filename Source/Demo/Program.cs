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

        private static void Use(IEnumerable<string> data)
        {
            int MaxLength(int max, string line) => Math.Max(max, line.Length);
            int TotalLength(int sum, string line) => sum + line.Length;

            (int lengthBefore, int maxLength) = data
                .AggregateStream(0, TotalLength)
                .AggregateStream(0, MaxLength)
                .Select(ApplyKingArthurSpeech)
                .Reduce(Print);

            Console.WriteLine(
                $"Length before: {lengthBefore}; " +
                $"maximum length before: {maxLength}");
        }

        private static void Main()
        {
            try
            {
                IEnumerable<string> data = new DataSource(TimeSpan.FromMilliseconds(10)).Fetch(40);

                Console.WriteLine("EXECUTION #1");
                Use(data);

                Console.WriteLine();
                Console.WriteLine("EXECUTION #2");
                Use(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex}");
            }
        }
    }
}
