using MessageBoards.Entities;

namespace MessageBoards.Application.Commands;

public class SendMessageCommand
{
    public Project Project { get; set; }
    public string Content { get; set; }
    public User Sender { get; set; }
}