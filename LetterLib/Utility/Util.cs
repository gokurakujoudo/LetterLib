using System;
using System.IO;
using System.Reflection;
using SharpHelper.Util;

namespace LetterLib.Utility
{
    public static class Util {
        public static readonly string Directory = Path
            .GetDirectoryName(Assembly.GetAssembly(typeof(WordHelper)).CodeBase)
            ?.Replace(@"file:\", "");

        public static string ToFullPath(string path) => $@"{Directory}\{path}";

        public static void WriteLine(string value, int level = 0) {
            var pre = string.Concat(' '.Repeat(level * 4));
            Console.WriteLine($"{DateTime.Now:T}| {pre}{value}");
        }
    }
}
