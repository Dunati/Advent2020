using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Specialized;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.IO;

[MemoryDiagnoser]
public class Day08 : BaseDay {

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


    public struct Instruction {
        public Instruction(string x) { OpCode = x.Substring(0, 3); Arg = int.Parse(x.Substring(4)); }
        public string OpCode;
        public int Arg;
    }

    private (int, bool) Process(int pc, int acc, BitVector count, bool change) {

        while (pc < instructions.Length) {
            int offset = 1;
            var instruction = instructions[pc];
            if (count.Set(pc)) {
                return (acc, false);
            }
            switch (instruction.OpCode) {
            case "nop": {
                if (change) {
                    var result = Process(pc + instruction.Arg, acc, count, false);
                    if (result.Item2) {
                        return result;
                    }
                }
            }
            break;
            case "acc": acc += instruction.Arg; break;
            case "jmp": {
                if (change) {
                    var result = Process(pc + 1, acc, count, false);
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

    public string Part1() {
        return Process(0, 0, new BitVector(),false).Item1.ToString();
    }

    static Instruction[] instructions;

    [GlobalSetup]
    public void LoadInstructions() {
        instructions = File.ReadAllText(@"C:\work\AdventOfCode\2020\Source\Days\Day08\input1.txt").Lines().Select(x => new Instruction(x)).ToArray();
    }

    [Benchmark]
    public string Part2() {
        return Process(0, 0, new BitVector(), true).Item1.ToString();
    }
    public override string Run(int part, string rawData) {

        instructions = rawData.Lines().Select(x => new Instruction(x)).ToArray();
        DoneParsing();
        if (part == 1) {
            return Part1();
        }
        try {
            return Part2();
        }

        finally {
            BenchmarkRunner.Run<Day08>();
        }
    }


}