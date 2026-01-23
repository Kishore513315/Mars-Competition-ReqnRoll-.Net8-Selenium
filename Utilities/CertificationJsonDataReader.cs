using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using MarsCompetitionReqnroll.Net8Selenium.Models;

namespace MarsCompetitionReqnroll.Net8Selenium.Utilities
{
    public static class CertificationJsonDataReader
    {
        private static readonly string _jsonFilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                         "TestData",
                         "CertificationTestData.json");

        private static CertificationTestDataRoot LoadData()
        {
            if (!File.Exists(_jsonFilePath))
                throw new FileNotFoundException(
                    $"Test data file not found at {_jsonFilePath}");

            var json = File.ReadAllText(_jsonFilePath);
            return JsonConvert.DeserializeObject<CertificationTestDataRoot>(json)
                   ?? new CertificationTestDataRoot();
        }

        public static CertificationTestData GetCertificationData(string testCaseId)
        {
            var allData = LoadData();

            var testData = allData.CertificationTests
                .FirstOrDefault(x =>
                    x.TestCaseId.Equals(testCaseId, StringComparison.OrdinalIgnoreCase));

            if (testData == null)
                throw new Exception($"No certification test data found for {testCaseId}");

            return testData;
        }

        public static string GetMessage(string messageKey)
        {
            var allData = LoadData();

            return messageKey switch
            {
                "MANDATORY_FIELDS_ERROR" =>
                    allData.Messages.MANDATORY_FIELDS_ERROR,

                "ALREADY_EXISTS_CERTIFICATION_ERROR" =>
                    allData.Messages.ALREADY_EXISTS_CERTIFICATION_ERROR,

                "DUPLICATE_CERTIFICATION_ERROR" =>
                    allData.Messages.DUPLICATE_CERTIFICATION_ERROR,

                _ => throw new Exception($"Unknown message key: {messageKey}")
            };
        }
    }
}


