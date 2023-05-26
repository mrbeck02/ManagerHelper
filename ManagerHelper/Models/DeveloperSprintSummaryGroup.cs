namespace ManagerHelper.Models
{
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
