namespace MessageBoards.Entities;

public class Project
{
    public string ProjectName { get; set; }
    public List<User> Members { get; set; }
    public List<Message> Messages { get; set; }
  //Project has  members
  //Project has a name
  //Project has messages
}