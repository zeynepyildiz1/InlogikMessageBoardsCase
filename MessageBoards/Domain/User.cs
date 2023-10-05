namespace MessageBoards.Entities;

public class User
{
    public string Name { get; set; }
    public List<Project> JoinedProjects { get; set; }
    public List<Message> SentMessages { get; set; }
}