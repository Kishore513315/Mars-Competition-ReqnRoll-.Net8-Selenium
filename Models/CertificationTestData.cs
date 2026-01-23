namespace MarsCompetitionReqnroll.Net8Selenium.Models
{
    public class CertificationTestData
    {
        public string TestCaseId { get; set; } = string.Empty;
        public string Certificate { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
    }

    public class CertificationTestDataRoot
    {
        public CertificationTestData[] CertificationTests { get; set; }
            = Array.Empty<CertificationTestData>();

        public CertificationMessages Messages { get; set; } = new();
    }



    public class CertificationMessages
    {
        public string MANDATORY_FIELDS_ERROR { get; set; } = string.Empty;
        public string ALREADY_EXISTS_CERTIFICATION_ERROR { get; set; } = string.Empty;
        public string DUPLICATE_CERTIFICATION_ERROR { get; set; } = string.Empty;
    }
}


