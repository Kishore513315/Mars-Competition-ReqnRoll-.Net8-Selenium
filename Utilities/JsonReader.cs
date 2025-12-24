using Newtonsoft.Json.Linq;
using System.IO;

namespace MarsCompetitionReqnroll.Net8Selenium.Utilities
{
    public static class JsonReader
    {
        public static JObject GetData(string relativeFilePath)
        {
            string filePath = Path.IsPathRooted(relativeFilePath)
                ? relativeFilePath
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeFilePath);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Test data file not found: {filePath}");

            var jsonText = File.ReadAllText(filePath);
            return JObject.Parse(jsonText);
        }
    }
}


