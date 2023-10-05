namespace MessageBoards.Entities;

public class Message
{
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public User Sender { get; set; }
    public Project Project { get; set; }
    
    //Content,timestamp, sender, projectname
}