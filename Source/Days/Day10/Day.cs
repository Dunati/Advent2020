using System;
using System.Diagnostics;
using System.Linq;

class Day10 : BaseDay {

    private int Part1(int[] jolts) {
        int[] difference = new int[4];

        for (int i = 1; i < jolts.Length; i++) {
            difference[jolts[i] - jolts[i - 1]]++;
        }
        int product = 1;
        for (int i = 0; i < difference.Length; i++) {
            if (difference[i] != 0) {
                product *= difference[i] + 1;
            }
        }
        return product;
    }

    public override string Run(int part, string rawData) {

        var jolts = rawData.ToIntArray();
        Array.Sort(jolts);

        int product = Part1(jolts);


        return product.ToString(); ;
    }
}