using System.ComponentModel.DataAnnotations;

namespace ManagerHelper.Data.Entities
{
    public enum ProductEnum
    {
        CARA = 1, CM, CRT, EPMM, PPS, PFS, SMARTonFHIR, AvailabilityAPI, ReferralAPI, Cognito, Launcher, Dynatrace, Other, CRAPI, Research, JI
    };

    /// <summary>
    /// This represents the product that a Jira issue affects.
    /// </summary>
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = "";

        #region Relationships

        public virtual ICollection<JiraIssue> JiraIssues { get; set; } = new List<JiraIssue>();

        #endregion
    }
}
