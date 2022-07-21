namespace CV.Models
{
    public class Project
    {
#nullable disable
        public string Name { get; set; }
        public DateTime StartProject { get; set; }
        public string Description { get; set; }
        public string Environment { get; set; }
#nullable enable

        public DateTime? EndProject { get; set; }
    }
}
