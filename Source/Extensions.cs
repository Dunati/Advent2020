using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions {
    public static int[] ToIntArray(this string str, string separator = "\r\n") {
        return str.Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
    }
    public static int[] ToSortedIntArray(this string str, string separator = "\r\n") {
        return str.Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).OrderBy(x => x).ToArray();
    }
    public static IEnumerable<String> Lines(this string str) {
        return str.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
    }

    // https://stackoverflow.com/questions/47815660/does-c-sharp-7-have-array-enumerable-destructuring
    public static void Deconstruct<T>(this IList<T> list, out T first, out IList<T> rest) {

        first = list.Count > 0 ? list[0] : default(T); // or throw
        rest = list.Skip(1).ToList();
    }

    public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out IList<T> rest) {
        first = list.Count > 0 ? list[0] : default(T); // or throw
        second = list.Count > 1 ? list[1] : default(T); // or throw
        rest = list.Skip(2).ToList();
    }
}