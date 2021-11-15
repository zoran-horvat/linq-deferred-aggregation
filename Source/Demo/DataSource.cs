using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Demo
{
    internal class DataSource
    {
        public DataSource(TimeSpan fetchStepDuration)
        {
            this.FetchStepDuration = fetchStepDuration;
            this.FetchTimer = new Stopwatch();
        }

        private TimeSpan FetchStepDuration { get; }
        private Stopwatch FetchTimer { get; set; }

        public IEnumerable<string> Fetch(int size) =>
            this.FetchNumbers(size).Select(i => SpeakNumber(i / 10, i % 10));

        private IEnumerable<int> FetchNumbers(int size) =>
            Enumerable.Range(0, Math.Min(size, 100)).Select(i => this.FetchNumber(i, size));

        private int FetchNumber(int number, int size)
        {
            if (number == 0)
            {
                this.FetchTimer = Stopwatch.StartNew();
                Console.Write("FETCHING DATA... ");
            }
            Thread.Sleep(this.FetchStepDuration);
            if (number == size - 1)
            {
                Console.WriteLine($"DONE in {FetchTimer.Elapsed}.");
            }
            return number;
        }

        private static string SpeakNumber(int tens, int units) =>
            tens == 0 ? SpeakUnits(units)
            : tens == 1 ? SpeakTeens(units)
            : units == 0 ? SpeakTens(tens)
            : $"{SpeakTens(tens)}-{SpeakUnits(units)}";

        private static string SpeakUnits(int value) =>
            new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" }[value];

        private static string SpeakTeens(int units) =>
            new[] { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" }[units];

        private static string SpeakTens(int value) =>
            new[] { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" }[value];
    }
}
