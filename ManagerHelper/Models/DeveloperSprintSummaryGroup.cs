using ManagerHelper.Extensions;

namespace ManagerHelper.Models
{
    /// <summary>
    /// This represents a group of sprints for a developer.
    /// For now, they're grouped by quarter.
    /// </summary>
    public class DeveloperSprintSummaryGroup : List<DeveloperSprintSummary>
    {
        public string Name { get; private set; } = "Unknown";
        public DateTime? QuarterStart { get; private set; }

        public DeveloperSprintSummaryGroup(string name) : base(new List<DeveloperSprintSummary>())
        {
            Name = name;
        }

        public DeveloperSprintSummaryGroup(List<DeveloperSprintSummary> list) : base(list)
        {
            if (!list.IsNullOrEmpty())
            {
                Name = list[0].QuarterName;
                QuarterStart = list.OrderBy(s => s.SprintStart).First().SprintStart;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
