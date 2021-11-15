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
            int length = data
                .AggregateStream(0, (sum, line) => sum + line.Length)
                .Select(ApplyKingArthurSpeech)
                .Reduce();

            Console.WriteLine($"Length before transform: {length}");

            IEnumerable<string> result = data
                .AggregateStream(0, (sum, line) => sum + line.Length)
                .Select(ApplyKingArthurSpeech)
                .AsEnumerable();
            Print(result);
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
