using System;
using System.Diagnostics;
using System.IO;

class BaseDay {
    public virtual string Run(int part, string rawData) => "Not Implemented";


    public string Run(int part) {
        string partName = $"Source/Days/{Name}/input{part}.txt";
        if (!File.Exists(partName)) {
            partName = $"Source/Days/{Name}/input1.txt";

            if (!File.Exists(partName)) {
                return $"input {partName} not found";
            }
        }

        return Run(part, File.ReadAllText(partName));
    }

    public string TestInput(int part, int testNum) {
        string testName = $"Source/Days/{Name}/test{part}-{testNum}.txt";
        if (!File.Exists(testName)) {
            if (part != 1) {
                return TestInput(1, testNum);
            }
            if (testNum != 1) {
                return TestInput(part, 1);
            }
            return null;
        }

        return File.ReadAllText(testName);
    }

    public string TestResult(int part, int testNum) {
        string testName = $"Source/Days/{Name}/result{part}-{testNum}.txt";
        if (!File.Exists(testName)) {
            return null;
        }

        return File.ReadAllText(testName);
    }


    public bool RunTests(int part) {
        int testNum = Test == 0 ? 1 : Test;
        bool passed = true;
        while (true) {
            string expected = TestResult(part, testNum);
            if (expected == null) {
                return passed;
            }

            try {
                string input = TestInput(part, testNum);
                string result = Run(part, input);
                if (result != expected) {
                    throw new Exception($"Test Failed: expected '{expected}', received '{result}'");
                }
                Trace.Write($".");
            }
            catch (Exception e) {
                Trace.WriteLine($"\nTest {part}-{testNum} failed: {e.Message}\n  ");
                passed = false;
            }
            if (Test != 0) {
                return passed;
            }
            testNum++;
        }
    }

    public string Name { get => this.GetType().Name.TrimStart('_'); }

    public int Part { get => 0; }

    public int Test { get => 0; }
}