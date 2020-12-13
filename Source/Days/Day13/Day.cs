using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

class Day13 : BaseDay {


    string Part1(string rawData) {
        var lines = rawData.Lines();
        int timestamp = lines.Select((Func<string, int>)Convert.ToInt32).First();
        int[] buses = lines.Skip(1).First().Split(',').Where(x => x != "x").Select((Func<string, int>)Convert.ToInt32).ToArray();

        int minWait = int.MaxValue;
        int busId = 0;
        for (int i = 0; i < buses.Length; i++) {
            int wait = (timestamp) % buses[i];
            if (wait != 0) {
                wait = buses[i] - wait;
            }
            if (wait < minWait) {
                minWait = wait;
                busId = buses[i];
            }
        }


        return (minWait * busId).ToString();
    }

    public override string Run(int part, string rawData, bool isTest) {

        if (part == 1) return Part1(rawData);



        int[] busIndex = rawData.Lines().Skip(1).First().Split(',').Select(x => { if (x == "x") return 0; return Convert.ToInt32(x); }).ToArray();

        int firstBus = busIndex[0];
        var busses = new List<(int, int)>().ToList();
        for (int i = 0; i < busIndex.Length; i++) {
            if (busIndex[i] != 0) {
                busses.Add((busIndex[i], i));
            }
        }


        BigInteger increment = busses[0].Item1;
        BigInteger value = 0;
        int bus = 1;
        while (bus < busses.Count) {
            if ((value + busses[bus].Item2) % busses[bus].Item1 == 0) {
                increment *= busses[bus].Item1;
                bus++;
            }
            else {
                value += increment;
            }
        }

        return value.ToString();

    }
}