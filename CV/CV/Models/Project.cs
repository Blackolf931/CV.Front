namespace CV.Models
{
    public record class Project(int Id, string Name, 
        DateTime StartProject, string Description, 
        string Environment, DateTime? EndProject);
}
