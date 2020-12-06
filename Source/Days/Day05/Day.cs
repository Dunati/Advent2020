using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

class Day05 : BaseDay {

    private int SeatId(string rawData) {
        int row = Seek(0, 127, rawData.Substring(0, 7), 'F');
        int column = Seek(0, 7, rawData.Substring(7), 'L');

        return (row * 8 + column);
    }

    public override string Run(int part, string rawData) {
        if (part == 1) {
            return rawData.Lines().Select(SeatId).Max().ToString();
        }
        else {
            SortedSet<int> filled = new SortedSet<int>();


            bool done = rawData.Lines().Select(SeatId).Select(filled.Add).Aggregate((x, y) => x ^ y);

            int last = filled.First();
            foreach (int i in filled) {
                if (i == last + 2) {
                    return (i - 1).ToString();
                }
                last = i;
            }

            return "not found";
        }
    }



    int Seek(int low, int high, string map, char lower) {

        for (int i = 0; i < map.Length; i++) {
            char c = map[i];
            if (c == lower) {
                high = (high + low) / 2;
            }
            else {
                low = (high + low) / 2 + 1;
            }
        }
        return low;
    }

}