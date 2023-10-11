using MessageBoards.Application.Commands;
using MessageBoards.Entities;

namespace MessageBoards.Application.Queries;

public class SendMessageCommandHandler : ICommandHandler<SendMessageCommand>
{
    private readonly List<Message> _messageStore;
    //readonly make it immuteable. so you can't assigne diffrent value but you can modified the content.
    //you can do add or remove operations.

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
        command.Sender.SentMessages.Add(message);
        command.Project.Messages.Add(message);
    }
}