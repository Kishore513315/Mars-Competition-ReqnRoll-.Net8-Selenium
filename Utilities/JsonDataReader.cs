using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace MarsCompetitionReqnroll.Net8Selenium.Utilities
{
    public static class JsonDataReader
    {
        private static readonly string _jsonFilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "educationTestData.json");

        public static EducationTestData GetEducationData(string testCaseId)
        {
            if (!File.Exists(_jsonFilePath))
                throw new FileNotFoundException($"Test data file not found at {_jsonFilePath}");

            var json = File.ReadAllText(_jsonFilePath);
            var allData = JsonConvert.DeserializeObject<EducationTestDataRoot>(json);

            var testData = allData?.EducationTests
                .FirstOrDefault(x => x.TestCaseId.Equals(testCaseId, StringComparison.OrdinalIgnoreCase));

            if (testData == null)
                throw new Exception($"No test data found for TestCaseId: {testCaseId}");

            return testData;
        }
    }

    public class EducationTestDataRoot
    {
        public EducationTestData[] EducationTests { get; set; } = Array.Empty<EducationTestData>();
    }

    public class EducationTestData
    {
        public string TestCaseId { get; set; } = string.Empty;
        public string University { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
    }
}

