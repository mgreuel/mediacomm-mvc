using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MediaCommMvc.Web.Helpers
{
    public static class PathHelper
    {
        private static readonly char[] InvalidCharacters = Path.GetInvalidFileNameChars().Concat(Path.GetInvalidPathChars()).Distinct().ToArray();

        private static readonly string InvalidCharactersPattern = $@"[{Regex.Escape(new string(InvalidCharacters) + " $.§ß%^&;=,'^´`#")}]";

        private static readonly Regex InvalidCharactersRegex = new Regex(InvalidCharactersPattern);

        public static string GetValidDirectoryName(string directoryName)
        {
            return InvalidCharactersRegex.Replace(directoryName, "_");
        }
    }
}
