using MessageBoards.Application.Queries;
using MessageBoards.Entities;

namespace MessageBoards.Handlers;

public class GetProjectMessagesQueryHandler : IQueryHandler<GetProjectMessagesQuery, List<Message>>
{
    private readonly List<Message> _messageStore;

    public GetProjectMessagesQueryHandler(List<Message> messageStore)
    {
        _messageStore = messageStore;
    }

    public List<Message> Handle(GetProjectMessagesQuery query)
    {
        return _messageStore.Where(m => m.Project.ProjectName == query.ProjectName).ToList();
    }
}