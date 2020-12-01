using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

public static class Runner {

    private static void RunAllDays() {
        foreach (Type t in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Name.StartsWith("Day")).OrderBy(x => x.Name)) {
            Run(t, 0);
        }
    }

    public static void Main(string[] args) {

        Type type;
        int day = 0;
        int part = 0;
        if (args.Length > 0) {

            if (args[0] == "all") {
                RunAllDays();
                return;
            }

            day = int.Parse(args[0]);
            if (args.Length > 1) {
                part = int.Parse(args[1]);
            }
        }
        if (day == 0) {
            type = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Name.StartsWith("Day")).OrderBy(x => x.Name).Last();
        }
        else {
            type = Assembly.GetExecutingAssembly().GetType($"Day{day:00}");
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