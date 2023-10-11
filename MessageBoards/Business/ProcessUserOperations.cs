using System.Text.RegularExpressions;
using MessageBoards.Application.Commands;
using MessageBoards.Application.Queries;
using MessageBoards.Entities;
using MessageBoards.Handlers;
using MessageBoards.Helper;

namespace MessageBoards.Business
{
    public class ProcessUserOperations
    {
        private List<User> _users = new List<User>();
        private List<Project> _projects = new List<Project>();
        private List<Message> _messageStore = new List<Message>();
        private readonly SendMessageCommandHandler _sendMessageCommandHandler;
        private readonly GetUserProjectsQueryHandler _getUserProjectsQueryHandler;
        private readonly JoinProjectCommandHandler _joinProjectCommandHandler;

        public ProcessUserOperations()
        {
            _sendMessageCommandHandler = new SendMessageCommandHandler(_messageStore);
            _getUserProjectsQueryHandler = new GetUserProjectsQueryHandler(_users);
            _joinProjectCommandHandler = new JoinProjectCommandHandler(_users, _projects);
        }

        public void ProcessInput(string userInput)
        {
            if (userInput.Contains("@"))
            {
                ProcessSendMessage(userInput);
            }
            else if (userInput.Contains("follows"))
            {
                ProcessFollows(userInput);
            }
            else if (userInput.EndsWith("wall"))
            {
                ProcessWall(userInput);
            }
            else if (!userInput.Contains(" "))
            {
                ProcessProjectMessage(userInput);
            }
        }

        private void ProcessSendMessage(string userInput)
        {
            string pattern = @"^(.*?) -> @(.*?) (.*)$";
            Match match = Regex.Match(userInput, pattern);

            if (match.Success)
            {
                string senderName = match.Groups[1].Value.Trim();
                string projectName = match.Groups[2].Value.Trim();
                string content = match.Groups[3].Value.Trim();
                
                User sender = _users.FirstOrDefault(user => user.Name == senderName) ?? CreateUser(senderName);
                Project project = _projects.FirstOrDefault(project => project.ProjectName == projectName)?? CreateProject(projectName);
                
                // Send message
                SendMessage(sender, project, content);
            }
        }
        private void ProcessFollows(string userInput)
        {
            string[] words = userInput.Split(' ');

            var joinCommand = new JoinProjectCommand
            {
                UserName = words[0] ,
                ProjectName = words[2] 
            };
         
            var joinHandler = new JoinProjectCommandHandler(_users, _projects);
            joinHandler.Handle(joinCommand);
        }

        private void ProcessWall(string userInput)
        {
            var words = userInput.Split(' ');
            var userName = words[0];

            var query = new GetUserProjectsQuery() { UserName = userName };
            var userProjects = _getUserProjectsQueryHandler.Handle(query);
            foreach (var project in userProjects)
            {
                foreach (var message in project.Messages.OrderByDescending(m => m.Timestamp))
                {
                    Console.WriteLine($"{project.ProjectName} - {message.Sender.Name}: {message.Content} ({FormatTimeStamp.FormatTimestamp(message.Timestamp)})");
                }
            }
        }
        private void ProcessProjectMessage(string userInput)
        {
            var projectMessageQueryHandler = new GetProjectMessagesQueryHandler(_messageStore);
            var query = new GetProjectMessagesQuery { ProjectName =  userInput };
            var projectMessages = projectMessageQueryHandler.Handle(query);

            foreach (var message in projectMessages)
            {
                var calculateTime = FormatTimeStamp.FormatTimestamp(message.Timestamp);
                Console.WriteLine($"{message.Sender.Name}\n{message.Content} ({calculateTime})");
            }
        }
        private User CreateUser(string name)
        {
            var newUser = new User { Name = name, JoinedProjects = new List<Project>(), SentMessages = new List<Message>() };
            _users.Add(newUser);
            return newUser;
        }

        private Project CreateProject(string projectName)
        {
            var newProject = new Project { ProjectName = projectName, Members = new List<User>(), Messages = new List<Message>() };
            _projects.Add(newProject);
            return newProject;
        }
        private void SendMessage(User sender, Project project, string content)
        {
            var sendMessageCommand = new SendMessageCommand
            {
                Project = project,
                Content = content,
                Sender = sender
            };

            _sendMessageCommandHandler.Handle(sendMessageCommand);

            if (!sender.JoinedProjects.Contains(project))
            {
                _joinProjectCommandHandler.Handle(new JoinProjectCommand { UserName = sender.Name, ProjectName = project.ProjectName });
            }
        }
    }
}
