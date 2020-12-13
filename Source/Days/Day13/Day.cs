using System;
using System.Diagnostics;
using System.Linq;

class Day13 : BaseDay {
    public override string Run(int part, string rawData) {

        var lines = rawData.Lines();
        int timestamp = lines.Select((Func<string, int>)Convert.ToInt32).First();
        int[] buses = lines.Skip(1).First().Split(',').Where(x => x != "x").Select((Func<string, int>)Convert.ToInt32).ToArray();

        int minWait = int.MaxValue;
        int busId = 0;
        for (int i = 0; i < buses.Length; i++) {
            int wait = (timestamp) % buses[i];
            if(wait != 0) {
                wait = buses[i] - wait;
            }
            if (wait < minWait) {
                minWait = wait;
                busId = buses[i];
            }
        }


        return (minWait * busId).ToString();
    }
}