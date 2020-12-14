using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class Day14 : BaseDay {

    DefaultDictionary<ulong, ulong> memory = new DefaultDictionary<ulong, ulong>();
    private string Part1(string rawData) {

        ulong or = 0;
        ulong and = 0;
        memory = new DefaultDictionary<ulong, ulong>();

        foreach (var line in rawData.Lines()) {
            var op = line.Split(" = ");
            if (op[0] == "mask") {
                or = 0;
                and = 0;
                foreach (char c in op[1]) {
                    or *= 2;
                    and *= 2;
                    switch (c) {
                    case '1': or += 1; and += 0; break;
                    case '0': or += 0; and += 0; break;
                    case 'X': or += 0; and += 1; break;
                    }
                }
            }
            else {
                ulong address = Convert.ToUInt64(op[0].Substring(4, op[0].Length - 5));
                ulong arg = Convert.ToUInt64(op[1]);

                ulong value = arg & and;
                value = value | or;

                memory[address] = value;

            }
        }
        return memory.Values.Aggregate((x, y) => x + y).ToString();
    }

    public override string Run(int part, string rawData) {

        if (part == 1)
            return Part1(rawData);

        memory = new DefaultDictionary<ulong, ulong>();

        ulong or = 0;
        ulong and = 0;
        ulong floating = 0;

        foreach (var line in rawData.Lines()) {
            var op = line.Split(" = ");

            if (op[0] == "mask") {
                or = 0;
                and = 0;
                floating = 0;

                foreach (char c in op[1]) {
                    or *= 2;
                    and *= 2;
                    floating <<= 1;
                    switch (c) {
                    case '1': or += 1; and += 1; floating +=0; break;
                    case '0': or += 0; and += 1; floating +=0; break;
                    case 'X': or += 0; and += 1; floating +=1; break;
                    }
                }
            }
            else {
                ulong address = Convert.ToUInt64(op[0].Substring(4, op[0].Length - 5));
                ulong arg = Convert.ToUInt64(op[1]);

                address &= and;
                address |= or;

                SetAddress(1L << 36, address, arg, floating);
            }
        }

        return memory.Values.Aggregate((x, y) => x + y).ToString();

    }

    void SetAddress(ulong bit, ulong address, ulong arg, ulong floating) {
        ulong mask = bit & floating;
        while (mask == 0 && bit > 0) {
            bit >>= 1;
            mask = bit & floating;
        }
        if (mask != 0) {
            SetAddress(bit >> 1, address | mask, arg, floating);
            SetAddress(bit >> 1, address & ~mask, arg, floating);
        }
        else {
            memory[address] = arg;
        }
    }
}