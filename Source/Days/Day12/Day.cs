using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;



class Day12 : BaseDay {
    public override string Run(int part, string rawData) {

        Point position = new Point();
        Point facing = new Point(1, 0);
        foreach (var instruction in rawData.Lines().Select(x => new { dir = x[0], len = int.Parse(x.Substring(1)) })) {
            switch (instruction.dir) {
            case 'N': position.Y -= instruction.len; break;
            case 'S': position.Y += instruction.len; break;
            case 'E': position.X += instruction.len; break;
            case 'W': position.X -= instruction.len; break;
            case 'L': for (int t = 0; t < instruction.len; t += 90) facing = new Point(+facing.Y, -facing.X); break;
            case 'R': for (int t = 0; t < instruction.len; t += 90) facing = new Point(-facing.Y, +facing.X); break;
            case 'F': position = new Point(facing.X * instruction.len + position.X, facing.Y * instruction.len + position.Y); break;
            }
        }

        return (Math.Abs(position.X) + Math.Abs(position.Y)).ToString();
    }
}