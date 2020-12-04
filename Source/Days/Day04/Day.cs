using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class Day04 : BaseDay {
    static SortedSet<string> EyeColors = new SortedSet<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth", };

    static bool NumberBetween(string value, int min, int max) {
        if (int.TryParse(value, out var v)) {
            return v >= min && v <= max;
        }
        return false;
    }

    static bool ValidHeight(string hgt) {
        if (hgt.EndsWith("cm")) {
            hgt = hgt.Substring(0, hgt.Length - 2);
            if (int.TryParse(hgt, out int height)) {
                return height >= 150 && height <= 193;
            }
            return false;
        }
        if (hgt.EndsWith("in")) {
            hgt = hgt.Substring(0, hgt.Length - 2);
            if (int.TryParse(hgt, out int height)) {
                return height >= 59 && height <= 76;
            }
        }
        return false;
    }

    static bool ValidHairColor(string hcl) {
        if (hcl[0] == '#' && hcl.Length == 7) {
            return int.TryParse(hcl.Substring(1), System.Globalization.NumberStyles.HexNumber, null, out var dummy);
        }
        return false;
    }

    static Dictionary<string, Predicate<string>> AllFields = new Dictionary<string, Predicate<string>>() {
        { "byr", byr => NumberBetween(byr, 1920, 2002) },
        { "iyr", iyr => NumberBetween(iyr, 2010, 2020) },
        { "eyr", eyr => NumberBetween(eyr, 2020, 2030) },
        { "hgt", hgt => ValidHeight(hgt) },
        { "hcl", ValidHairColor },
        { "ecl", ecl => EyeColors.Contains(ecl) },
        { "pid", pid =>  pid.Length == 9 && int.TryParse(pid, out int dummy) },
        { "cid", cid => true },
    };
    public override string Run(int part, string rawData) {
        int count = 0;
        foreach (string passport in rawData.Lines()) {
            string[] fields = passport.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var allFields = new SortedSet<string>(AllFields.Keys);

            foreach (var field in fields) {
                var (name, value, _) = field.Split(':');

                if (part == 1 || AllFields[name](value)) {
                    allFields.Remove(name);
                }
            }

            if (allFields.Count == 0 || allFields.Count == 1 && allFields.First() == "cid") {
                count++;
            }

        }
        return count.ToString();
    }
}