namespace Corwords.Web.Models.Configuration
{
    public class AppSettings
    {
        public string BuildNumber { get; set; }
        public bool FirstRun { get; set; }
        public GeneralSettings GeneralSettings { get; set; }
    }
}