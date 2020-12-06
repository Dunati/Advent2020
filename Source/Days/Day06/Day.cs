using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

class Day06 : BaseDay {
    public override string Run(int part, string rawData) {
            int total = 0;
        if (part == 1) {
            HashSet<char> answers = new HashSet<char>();
            foreach (var line in rawData.Split('\n').Select(x => x.Trim())) {
                if (line.Length == 0) {
                    total += answers.Count;
                    answers.Clear();
                }
                else {
                    foreach (var answer in line) {
                        answers.Add(answer);
                    }
                }
            }
            total += answers.Count;
        }
        else {
            char[] answers = new char[26];
            int people = 0;
            foreach (var line in rawData.Split('\n').Select(x => x.Trim())) {
                if (line.Length == 0) {
                    total += answers.Where(x => x == people).Count();
                    answers = new char[26];
                    people = 0;
                }
                else {
                    people++;
                    foreach (var answer in line) {
                        answers[answer - 'a']++;                        
                    }
                }
            }
            total += answers.Where(x => x == people).Count();
        }

        return total.ToString();
    }
}