namespace ManagerHelper.Models
{
    /// <summary>
    /// This represents a group of sprints for a developer.
    /// For now, they're grouped by quarter.
    /// </summary>
    public class DeveloperSprintSummaryGroup : List<DeveloperSprintSummary>
    {
        public string Name { get; private set; }

        public DeveloperSprintSummaryGroup(string name) : base(new List<DeveloperSprintSummary>())
        {
            Name = name;
        }

        public DeveloperSprintSummaryGroup(string name, List<DeveloperSprintSummary> list) : base(list)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
