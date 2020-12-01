using System;

class Day01 : BaseDay {

    public override string Run(int part, string rawData) {
        int[] data = rawData.ToSortedIntArray();
        if (part == 1) {
            return DoubleSum(2020, data).ToString();
        }
        else {
            return TripleSum(2020, data).ToString();
        }
    }
    static int TripleSum(int initialGoal, in Span<int> data) {
        int largest = data.Length - 1;
        while (largest > 1) {
            int goal = initialGoal - data[largest];
            if (data[0] + data[1] > goal) {
                largest--;
            }
            else {
                int result = DoubleSum(goal, data.Slice(0, largest));
                if (result != 0) {
                    return result * data[largest];
                }
                else {
                    largest--;
                }
            }
        }
        return 0;
    }

    static int DoubleSum(int goal, in Span<int> data) {
        int first = 0;
        int last = data.Length - 1;

        while (first < last) {
            int result = data[first] + data[last];
            if (result == goal) {
                return data[first] * data[last];
            }
            else if (result < goal) {
                first++;
            }
            else {
                last--;
            }
        }
        return 0;
    }

}