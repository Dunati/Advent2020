using System;
using System.Diagnostics;
using System.Linq;

class Day02 : BaseDay {



    public override string Run(int part, string rawData) {

        if (part == 1) {
            return rawData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new char[] { '-', ' ', ':' }, StringSplitOptions.RemoveEmptyEntries))
                .Select(y => new { min = int.Parse(y[0]), max = int.Parse(y[1]), letter = y[2][0], password = y[3] })
                .Where(z => { int count = z.password.Where(c => c == z.letter).Count(); return count >= z.min && count <= z.max; })
                .Count().ToString();
        }
        else {

            return rawData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new char[] { '-', ' ', ':' }, StringSplitOptions.RemoveEmptyEntries))
                .Select(y => new { min = int.Parse(y[0]), max = int.Parse(y[1]), letter = y[2][0], password = y[3] })
                .Where(z => z.password[z.min-1]==z.letter ^ z.password[z.max-1] == z.letter)
                .Count().ToString();
        }
    }
}