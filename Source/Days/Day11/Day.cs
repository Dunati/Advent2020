using System;
using System.Diagnostics;
using System.Linq;

class Day11 : BaseDay {

    const char Occupied = '#';
    const char Empty = 'L';
    public override string Run(int part, string rawData) {

        var lines = rawData.Lines().ToArray();
        int width = lines[0].Length;
        int height = lines.Length;

        char[,] seats = new char[width, height];

        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                seats[x, y] = lines[y][x];
            }
        }

        Func<int, int, int> occupied = (int x, int y) => {
            if (x < 0 || y < 0 || x >= width || y >= height) {
                return 0;
            }
            return seats[x, y] == Occupied ? 1 : 0;
        };

        Func<int, int, int> neighbors = (int x, int y) => {
            return
            occupied(x - 1, y - 1) +
            occupied(x - 1, y - 0) +
            occupied(x - 1, y + 1) +
            occupied(x - 0, y - 1) +
            occupied(x - 0, y + 1) +
            occupied(x + 1, y - 1) +
            occupied(x + 1, y + 0) +
            occupied(x + 1, y + 1);
        };

        char[,] adjacent = new char[width, height];


        bool changed = true;
        while (changed) {
            changed = false;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    adjacent[x, y] = (char)neighbors(x, y);
                }
            }

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    if (seats[x, y] == Empty) {
                        if (adjacent[x, y] == 0) {
                            seats[x, y] = Occupied;
                            changed = true;
                        }
                    }
                    else if (seats[x, y] == Occupied) {
                        if (adjacent[x, y] >= 4) {
                            seats[x, y] = Empty;
                            changed = true;
                        }
                    }
                    adjacent[x, y] = '\x0';
                }
            }
        }
        int count = 0;
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
               if( seats[x, y] == Occupied) {
                    count++;
                }
            }
        }


        return count.ToString();
    }

}