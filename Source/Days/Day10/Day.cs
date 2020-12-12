using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


class Memoize<Tin, Tout> {

    public Memoize(Func<Tin, Tout> method) {
        this.method = method;
    }

    public Tout Lookup(Tin key) {
        Tout value = default;
        if (!items.TryGetValue(key, out value)) {
            value = method(key);
            items[key] = value;
        }
        return value;
    }
    Func<Tin, Tout> method;
    Dictionary<Tin, Tout> items = new Dictionary<Tin, Tout>();
}

class Day10 : BaseDay {

    private int Part1(int[] jolts) {
        int[] difference = new int[4];

        for (int i = 1; i < jolts.Length; i++) {
            difference[jolts[i] - jolts[i - 1]]++;
        }
        int product = 1;
        difference[3]++;
        for (int i = 0; i < difference.Length; i++) {
            if (difference[i] != 0) {
                product *= difference[i];
            }
        }
        return product;
    }


    static Memoize<int, long> Factorial = new Memoize<int, long>(CalculateFactorial);
    static Memoize<int, long> Combinations = new Memoize<int, long>(CalculateCombinations);


    static long CalculateFactorial(int n) {
        long value = 1;
        while (n > 1) {
            value *= n--;
        }

        return value;
    }


    static long Choose(int n, int r) {
        return Factorial.Lookup(n) / (Factorial.Lookup(r) * Factorial.Lookup(n - r));
    }


    static long CalculateCombinations(int length) {
        Trace.Write($"{length},");
        long count = 0;

        if (length < 5) {
            count++;
        }

        for (int i = 1; i <= length - 2; i++) {
            count += Choose(length - 2, i);
        }

        return count;
    }

    static string MyToString(int x) {
        return x.ToString();
    }



    public override string Run(int part, string rawData) {

        var one = (Func<int, string>)Convert.ToString;
        var two = (Func<int, string>)MyToString;

        Func<int, string> three = x => x.ToString();


        int[] nums = rawData.Lines().Select((Func<string, int>)Convert.ToInt32).Select(two).Select((Func<string, int>)Convert.ToInt32).ToArray();

        var jolts = rawData.ToIntArray();
        Array.Sort(jolts);

        if (part == 1) {
            int product = Part1(jolts);

            return product.ToString();
        }

        int first = 0;
        int last = 1;

        long combinations = 1;

        while (last < jolts.Length) {
            while (last < jolts.Length && jolts[last] == jolts[last - 1] + 1) {
                last++;
            }

            if (first + 2 < last) {
                combinations *= Combinations.Lookup(last - first);
            }
            first = last;
            last = first + 1;
        }


        return combinations.ToString(); ;
    }




}