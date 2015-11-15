using System.Text.RegularExpressions;

namespace MediaCommMvc.Web.Helpers
{
    public static class PathHelper
    {
        private static readonly Regex InvalidFileNameCharactersRegex = new Regex("[^0-9a-zA-Z.]+");

        private static readonly Regex InvalidDirectoryNameCharactersRegex = new Regex("[^0-9a-zA-Z]+");

        public static string GetValidDirectoryName(string directoryName)
        {
            return InvalidDirectoryNameCharactersRegex.Replace(directoryName, "_").Trim('_');
        }

        public static string GetValidFileName(string fileName)
        {
            return InvalidFileNameCharactersRegex.Replace(fileName, "_").Trim('_');
        }
    }
}
