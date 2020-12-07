using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Day07 : BaseDay {

    string bagregex = "(([0-9]?) ?([a-z ]+?)|(no other)) bags?";

    Dictionary<string, Dictionary<string, int>> rules;

    public override string Run(int part, string rawData) {
        Regex bags = new Regex(bagregex);


        if (part == 1) {
            rules = new Dictionary<string, Dictionary<string, int>>();
            foreach (var line in rawData.Lines()) {

                var matches = bags.Matches(line);

                string container = matches[0].Groups[3].Value;
                foreach (Match match in matches.Skip(1)) {
                    int.TryParse(match.Groups[2].Value, out int count);
                    string name = match.Groups[3].Value;

                    Dictionary<string, int> values = null;
                    if (!rules.TryGetValue(name, out values)) {
                        values = new Dictionary<string, int>();
                        rules[name] = values;
                    }

                    values[container] = count;
                }
            }
            //252 too high
            return Count("shiny gold").ToString();
        }

        else {
            rules = new Dictionary<string, Dictionary<string, int>>();
            foreach (var line in rawData.Lines()) {

                var matches = bags.Matches(line);

                string container = matches[0].Groups[3].Value;
                foreach (Match match in matches.Skip(1)) {
                    int.TryParse(match.Groups[2].Value, out int count);
                    string name = match.Groups[3].Value;

                    Dictionary<string, int> values = null;
                    if (!rules.TryGetValue(container, out values)) {
                        values = new Dictionary<string, int>();
                        rules[container] = values;
                    }
                    values[name] = count;
                }
            }

            var counts = new Dictionary<string, int>();
            int total = Contains("shiny gold", counts);
            return total.ToString();
        }

    }

    int Contains(string color, Dictionary<string, int> included) {
        if (included.TryGetValue(color, out int count)) {
            return count;
        }
        else {
            int total = 0;
            foreach(var bag in rules[color]) {
                if (bag.Value > 0) {
                    total += bag.Value * (1+Contains(bag.Key, included));
                }
            }

            included[color] = total;
            return total;
        }
    }
    int Count(string color) {
        HashSet<string> included = new HashSet<string>();
        Queue<string> search = new Queue<string>();
        search.Enqueue(color);

        while (search.Count > 0) {
            color = search.Dequeue();
            if (rules.TryGetValue(color, out var containers)) {
                foreach (string bagColor in containers.Keys) {
                    if (!included.Contains(bagColor)) {
                        search.Enqueue(bagColor);
                        included.Add(bagColor);
                    }
                }
            }
        }
        return included.Count;
    }
}