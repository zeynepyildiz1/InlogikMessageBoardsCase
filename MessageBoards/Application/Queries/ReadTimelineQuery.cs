using MessageBoards.Entities;

namespace MessageBoards.Application.Queries;

public class ReadTimelineQuery
{
    public List<Message> Messages { get; set; }
}