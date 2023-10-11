using MessageBoards.Application.Commands;
using MessageBoards.Entities;

namespace MessageBoards.Application.Queries;

public class JoinProjectCommandHandler : ICommandHandler<JoinProjectCommand>
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
        
        if (user == null)
        {
            user = new User { Name = command.UserName , JoinedProjects = new List<Project>(), SentMessages = new List<Message>()};
            _users.Add(user);
        }
        
        Project project = _projects.FirstOrDefault(project => project.ProjectName == command.ProjectName);
        
        if (project == null)
        {
            project =  new Project { ProjectName = command.ProjectName,Members = new List<User>() , Messages = new List<Message>()};
            _projects.Add(project);
        }
        project.Members.Add(user);
        user.JoinedProjects.Add(project);
    }
}