using System;
using System.Diagnostics;
using System.Numerics;
using System.Linq;



class Day12 : BaseDay {

    private string Part1(ref string rawData) {
        Vector2 position = new Vector2();
        Vector2 facing = new Vector2(1, 0);
        foreach (var instruction in rawData.Lines().Select(x => new { dir = x[0], len = int.Parse(x.Substring(1)) })) {
            switch (instruction.dir) {
            case 'N': position.Y -= instruction.len; break;
            case 'S': position.Y += instruction.len; break;
            case 'E': position.X += instruction.len; break;
            case 'W': position.X -= instruction.len; break;
            case 'L': for (int t = 0; t < instruction.len; t += 90) facing = new Vector2(+facing.Y, -facing.X); break;
            case 'R': for (int t = 0; t < instruction.len; t += 90) facing = new Vector2(-facing.Y, +facing.X); break;
            case 'F': position = new Vector2(facing.X * instruction.len + position.X, facing.Y * instruction.len + position.Y); break;
            }
        }

        return (Math.Abs(position.X) + Math.Abs(position.Y)).ToString();
    }

    public override string Run(int part, string rawData) {

        if (part == 1)
            return Part1(ref rawData);




        Vector2 position = new Vector2();
        Vector2 facing = new Vector2(1, 0);
        Vector2 waypoint = new Vector2(10, -1);

        foreach (var instruction in rawData.Lines().Select(x => new { dir = x[0], len = int.Parse(x.Substring(1)) })) {
            switch (instruction.dir) {
            case 'N': waypoint.Y -= instruction.len; break;
            case 'S': waypoint.Y += instruction.len; break;
            case 'E': waypoint.X += instruction.len; break;
            case 'W': waypoint.X -= instruction.len; break;                                            
            case 'L': for (int t = 0; t < instruction.len; t += 90) waypoint = new Vector2(+waypoint.Y, -waypoint.X); break;
            case 'R': for (int t = 0; t < instruction.len; t += 90) waypoint = new Vector2(-waypoint.Y, +waypoint.X); break;
            case 'F': for (int i = 0; i < instruction.len; i++) position += waypoint;break;
            }
        }

        return (Math.Abs(position.X) + Math.Abs(position.Y)).ToString();
    }
}