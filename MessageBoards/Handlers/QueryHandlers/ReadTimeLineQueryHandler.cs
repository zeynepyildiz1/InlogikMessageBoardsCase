using MessageBoards.Application.Queries;
using MessageBoards.Entities;

namespace MessageBoards.Handlers;

public class ReadTimelineQueryHandler
{
    private List<Message> _messageStore;

    public ReadTimelineQueryHandler(List<Message> messageStore)
    {
        _messageStore = messageStore;
    }

    public ReadTimelineQuery Handle()
    {

        var messages = _messageStore.OrderByDescending(m => m.Timestamp).ToList();

        return new ReadTimelineQuery { Messages = messages };
    }
}