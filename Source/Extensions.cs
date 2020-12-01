using System;
using System.Linq;

public static class Extensions {
    public static int[] ToIntArray(this string str, string separator = "\r\n") {
        return str.Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
    }
    public static int[] ToSortedIntArray(this string str, string separator = "\r\n") {
        return str.Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).OrderBy(x => x).ToArray();
    }
}
