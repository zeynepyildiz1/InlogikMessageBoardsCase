using MessageBoards.Application.Queries;
using MessageBoards.Entities;

namespace MessageBoards.Handlers;

public class GetUserProjectsQueryHandler : IQueryHandler<GetUserProjectsQuery, List<Project>>
{
    private readonly List<User> _users;

    public GetUserProjectsQueryHandler(List<User> users)
    {
        _users = users;
    }

    public List<Project> Handle(GetUserProjectsQuery query)
    {
        var user = _users.FirstOrDefault(u => u.Name == query.UserName);

        return user?.JoinedProjects;
    }
}