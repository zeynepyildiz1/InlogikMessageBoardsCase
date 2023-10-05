using MessageBoards.Application.Queries;
using MessageBoards.Entities;

namespace MessageBoards.Handlers;

public class ProjectMessageQueryHandler
{
    private readonly List<Message> _messageStore;

    public ProjectMessageQueryHandler(List<Message> messageStore)
    {
        _messageStore = messageStore;
    }

    public List<Message> Handle(GetProjectMessagesQuery query)
    {
        return _messageStore.Where(m => m.Project.ProjectName == query.ProjectName).ToList();
    }
}