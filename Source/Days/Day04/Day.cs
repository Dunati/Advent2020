using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class Day04 : BaseDay {
    static SortedSet<string> EyeColors = new SortedSet<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth", };
    static Dictionary<string, Predicate<string>> AllFields = new Dictionary<string, Predicate<string>>() {
        {
            "byr", byr => {
                if(int.TryParse(byr, out var year)) {
                    return year >= 1920 && year <=2002;
                }
                return false;
            }
        },
        {
            "iyr", iyr => {
                if(int.TryParse(iyr, out var year)) {
                    return year >= 2010 && year <=2020;
                }
                return false;
            }
        },
        {
            "eyr", eyr => {
                if(int.TryParse(eyr, out var year)) {
                    return year >= 2020 && year <=2030;
                }
                return false;
            }
        },
        {
            "hgt", hgt => {
                if (hgt.EndsWith("cm")) {
                    hgt = hgt.Substring(0, hgt.Length-2);
                    if(int.TryParse(hgt, out int height)) {
                        return height >=150 && height <= 193;
                    }
                    return false;
                }
                if (hgt.EndsWith("in")) {
                    hgt = hgt.Substring(0, hgt.Length-2);
                    if(int.TryParse(hgt, out int height)) {
                        return height >=59 && height <= 76;
                    }
                }
                return false;
            }
        },
        {
            "hcl", hcl => {
                if(hcl[0] == '#' && hcl.Length == 7) {
                       return int.TryParse(hcl.Substring(1), System.Globalization.NumberStyles.HexNumber, null, out var dummy);
                }
                return false;
            }
        },
        {
            "ecl", ecl => EyeColors.Contains(ecl)
        },
        {
            "pid", pid => {
                return pid.Length == 9 && int.TryParse(pid, out int dummy);
            }
        },
        {
            "cid", cid => true
        },
    };
    public override string Run(int part, string rawData) {
        int count = 0;
        foreach (string passport in rawData.Lines()) {
            string[] fields = passport.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var allFields = new SortedSet<string>(AllFields.Keys);

            foreach (var field in fields) {
                 string[] nameValue = field.Split(':');

                if (part == 1 || AllFields[nameValue[0]](nameValue[1])) {
                    allFields.Remove(nameValue[0]);
                }
            }

            if (allFields.Count == 0 || allFields.Count == 1 && allFields.First() == "cid") {
                count++;
            }

        }
        return count.ToString();
    }
}