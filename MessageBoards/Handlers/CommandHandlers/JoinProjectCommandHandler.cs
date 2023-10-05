using MessageBoards.Application.Commands;
using MessageBoards.Entities;

namespace MessageBoards.Application.Queries;

public class JoinProjectCommandHandler
{
    private readonly List<User> _users;
    private readonly List<Project> _projects;

    public JoinProjectCommandHandler(List<User> users, List<Project> projects)
    {
        _users = users;
        _projects = projects;
    }

    public void Handle(JoinProjectCommand command)
    {
        User user = _users.FirstOrDefault(u => u.Name == command.UserName);
        Project project = _projects.FirstOrDefault(p => p.ProjectName == command.ProjectName);
            project.Members.Add(user);
            user.JoinedProjects.Add(project);
    }
}