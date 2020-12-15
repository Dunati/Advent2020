using System;
using System.Diagnostics;
using System.Linq;

class Day15 : BaseDay {
    public override string Run(int part, string rawData) {

        DefaultDictionary<int, int> seen = new DefaultDictionary<int, int>();

        int round = 1;

        var s = rawData.Split(',', StringSplitOptions.RemoveEmptyEntries);


        foreach (int i in s.Select((Func<string,int>)Convert.ToInt32)) {
            seen[i] = round++;
        }

        int toAdd = 0;
        int stop = 2020;
        if(part == 2) {
            stop = 30000000;
        }

        while (round < stop) {
            int nextNumber = 0;
            if (seen.ContainsKey(toAdd)) {
                nextNumber = round - seen[toAdd];
            }
            seen[toAdd] = round++;
            toAdd = nextNumber;
        }



        return toAdd.ToString();
    }
}