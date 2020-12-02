using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

public static class Runner {

    private static void RunAllDays() {
        foreach (Type t in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Name.StartsWith("Day")).OrderBy(x => x.Name)) {
            Run(t, 0);
        }
        foreach (Type t in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Name.StartsWith("_Day")).OrderBy(x => x.Name)) {
            Run(t, 0);
        }
    }

    public static void Main(string[] args) {
        if(!Debugger.IsAttached) {
            Trace.Listeners.Add(new ConsoleTraceListener());
        }
        Type type;
        int day = 0;
        int part = 0;
        if (args.Length > 0) {

            if (args[0] == "all") {
                RunAllDays();
                return;
            }

            if (!int.TryParse(args[0], out day)) {
                Trace.WriteLine($"Cannot parse {args[0]} as a day number");
                return;
            }
            if (args.Length > 1 && !int.TryParse(args[1], out part)) {
                Trace.WriteLine($"Cannot parse {args[1]} as a part number");
                return;
            }

            if(part <0 || part > 2) {
                Trace.WriteLine($"part {part} is not a valid part number");
                return;
            }
        }
        if (day == 0) {
            type = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Name.StartsWith("Day")).OrderBy(x => x.Name).Last();
        }
        else {
            type = Assembly.GetExecutingAssembly().GetType($"Day{day:00}");
            if (type == null) {
                Trace.WriteLine($"Cannot find class Day{day:00} specified on the command line");
                return;
            }
        }
        Run(type, part);
    }

    static void Run(Type type, int part) {
        BaseDay day = (BaseDay)Activator.CreateInstance(type);

        if (part != 0) {
            RunPart(day, part);
        }
        else {
            if (day.Part == 0) {
                RunPart(day, 1);
                RunPart(day, 2);
            }
            else {
                RunPart(day, day.Part);
            }
        }
    }


    static void RunPart(BaseDay day, int part) {
        Trace.Write($"Running {day.Name}-{part} ");
        if (day.RunTests(part)) {
            Trace.WriteLine($"\n  result: {day.Run(part)}\n");
        }
    }


}