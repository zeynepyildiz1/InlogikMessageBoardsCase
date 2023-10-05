using MessageBoards.Application.Commands;
using MessageBoards.Entities;

namespace MessageBoards.Application.Queries;

public class SendMessageCommandHandler
{
    private List<Message> _messageStore;

    public SendMessageCommandHandler(List<Message> messageStore)
    {
        _messageStore = messageStore;
    }

    public void Handle(SendMessageCommand command)
    {
        var message = new Message
        {
            Content = command.Content,
            Sender = command.Sender,
            Timestamp = DateTime.Now,
            Project = command.Project
        };

        _messageStore.Add(message);
    }
}