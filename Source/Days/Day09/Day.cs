using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;

partial class Day09 : BaseDay {



    private long Part1(int preamble, string rawData) {
        int oldest = 0;
        long[] numbers = new long[preamble];

        var input = rawData.Lines().Select(x => long.Parse(x)).GetEnumerator();

        while (oldest < preamble) {
            input.MoveNext();
            numbers[oldest++] = input.Current;
        }
        oldest = 0;

        var sums = new DefaultDictionary<long,long>();

        for (int i = 0; i < preamble - 1; i++) {
            for (int j = 0; j < preamble; j++) {
                sums[numbers[i] + numbers[j]]++;
            }
        }

        while (input.MoveNext()) {
            if (sums[input.Current] == 0) {
                return input.Current;
            }
            long oldestValue = numbers[oldest];
            for (int i = 0; i < preamble; i++) {
                if (i != oldest) {
                    sums[numbers[i] + oldestValue]--;
                }
            }
            numbers[oldest] = input.Current;

            for (int i = 0; i < preamble; i++) {
                if (i != oldest) {
                    sums[numbers[i] + input.Current]++;
                }
            }

            oldest = (oldest + 1) % preamble;
        }

        return 0;
    }



    public long Part2(long sum, long target, Span<long> values, int first, int last) {
        return ((sum + values[last]) - target) switch {
            < 0 => Part2(sum+values[last], target, values, first, last + 1),
            > 0 => Part2(sum-values[first], target, values, first + 1, last),
            _ => values.Slice(first, last - first).Min() + values.Slice(first, last - first).Max()
        };
    }

    public override string Run(int part, string rawData, bool isTest) {
        int preamble = 25;
        if (isTest) {
            preamble = 5;
        }

        long value = Part1(preamble, rawData);

        if (part == 1) {
            return value.ToString();
        }



        Trace.WriteLine(Part2(0, value, rawData.Lines().Select(x => long.Parse(x)).ToArray(), 0, 0));

        Queue<long> numbers = new Queue<long>();

        var input = rawData.Lines().Select(x => long.Parse(x)).GetEnumerator();

        long sum = 0;

        while (input.MoveNext()) {

            long newSum = sum + input.Current;

            if (newSum == value) {
                numbers.Enqueue(input.Current);
                break;
            }
            else {
                while (newSum > value && numbers.Count > 0) {
                    newSum -= numbers.Dequeue();
                }
                sum = newSum;

                if (sum == value) {
                    numbers.Enqueue(input.Current);
                    break;
                }

                numbers.Enqueue(input.Current);
            }
        }


        return (numbers.Min() + numbers.Max()).ToString();

    }
}