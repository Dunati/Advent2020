using System;
using System.Diagnostics;
using System.Linq;

class Day11 : BaseDay {

    const char Occupied = '#';
    const char Empty = 'L';
    const char Floor = '.';
    int width = 0;
    int height = 0;

    char[,] seats = null;

    int IsOccupied(ValueTuple<int, int> t) => IsOccupied(t.Item1, t.Item2);

    int IsOccupied(int x, int y) {
        if (x < 0 || y < 0 || x >= width || y >= height) {
            return 0;
        }
        return seats[x, y] == Occupied ? 1 : 0;
    }

    int DirectNeighbors(int x, int y) {
        return
            IsOccupied(x - 1, y - 1) +
            IsOccupied(x - 1, y - 0) +
            IsOccupied(x - 1, y + 1) +
            IsOccupied(x - 0, y - 1) +
            IsOccupied(x - 0, y + 1) +
            IsOccupied(x + 1, y - 1) +
            IsOccupied(x + 1, y + 0) +
            IsOccupied(x + 1, y + 1);
    }

    (int, int) NextSeat(int x, int y, int xDir, int yDir) {
        x += xDir;
        y += yDir;

        if (x < 0 || y < 0 || x >= width || y >= height || seats[x, y] != Floor)
            return (x, y);

        return NextSeat(x, y, xDir, yDir);
    }

    int VisibleNeighbors(int x, int y) {
        int total = 0;
        if (seats[x, y] == Floor) return total;


        total += IsOccupied(NextSeat(x, y, -1, -1));
        total += IsOccupied(NextSeat(x, y, -1, -0));
        total += IsOccupied(NextSeat(x, y, -1, +1));
        total += IsOccupied(NextSeat(x, y, -0, -1));
        total += IsOccupied(NextSeat(x, y, -0, +1));
        total += IsOccupied(NextSeat(x, y, +1, -1));
        total += IsOccupied(NextSeat(x, y, +1, -0));
        total += IsOccupied(NextSeat(x, y, +1, +1));

        return total;
    }

    private string Part1() {
        char[,] adjacent = new char[width, height];


        bool changed = true;
        while (changed) {
            changed = false;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    adjacent[x, y] = (char)DirectNeighbors(x, y);
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
                if (seats[x, y] == Occupied) {
                    count++;
                }
            }
        }


        return count.ToString();
    }

    public void Print() {
        Trace.WriteLine("----------------------------------------------------------");
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                Trace.Write(seats[x, y]);
            }
            Trace.WriteLine("");
        }
    }

    private string Part2() {
        char[,] adjacent = new char[width, height];


        bool changed = true;
        while (changed) {
            changed = false;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    adjacent[x, y] = (char)VisibleNeighbors(x, y);
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
                        if (adjacent[x, y] >= 5) {
                            seats[x, y] = Empty;
                            changed = true;
                        }
                    }
                    adjacent[x, y] = '\x0';
                }
            }

            //Print();
        }
        int count = 0;
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                if (seats[x, y] == Occupied) {
                    count++;
                }
            }
        }


        return count.ToString();
    }

    public override string Run(int part, string rawData) {

        var lines = rawData.Lines().ToArray();

        width = lines[0].Length;
        height = lines.Length;

        seats = new char[width, height];

        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                seats[x, y] = lines[y][x];
            }
        }

        if (part == 1) {
            return Part1();
        }
        return Part2();
    }

}