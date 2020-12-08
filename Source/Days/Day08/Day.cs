using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Specialized;

class Day08 : BaseDay {

    unsafe struct BitVector {
        fixed uint bitVectors[623 / 32 + 1];

        public bool Set(int i) {
            int index = i >> 5;
            int position = i & 0b11111;

            uint value = bitVectors[index];
            uint newvalue = (uint)(value | (1u << position));

            bitVectors[index] = newvalue;

            return value == newvalue;
        }

    }


    struct Instruction {
        public Instruction(string x) { OpCode = x.Substring(0, 3); Arg = int.Parse(x.Substring(4)); }
        public string OpCode;
        public int Arg;
    }

    private (int, bool) Process(in Instruction[] instructions, int pc, int acc, BitVector count, bool change) {

        while (pc < instructions.Length) {
            int offset = 1;
            var instruction = instructions[pc];
            if (count.Set(pc)) {
                return (acc, false);
            }
            switch (instruction.OpCode) {
            case "nop": {
                if (change) {
                    var result = Process(instructions, pc + instruction.Arg, acc, count, false);
                    if (result.Item2) {
                        return result;
                    }
                }
            }
            break;
            case "acc": acc += instruction.Arg; break;
            case "jmp": {
                if (change) {
                    var result = Process(instructions, pc + 1, acc, count, false);
                    if (result.Item2) {
                        return result;
                    }
                }
                offset = instruction.Arg;
            }
            break;
            }
            pc += offset;
        }
        return (acc, true);
    }


    public override string Run(int part, string rawData) {

        var instructions = rawData.Lines().Select(x => new Instruction(x)).ToArray();
        DoneParsing();
        if (part == 1) {
            return Process(instructions, 0, 0, new BitVector(),false).Item1.ToString();
        }

        return Process(instructions, 0, 0, new BitVector(), true).Item1.ToString();
    }


}