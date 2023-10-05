
using System.Text.RegularExpressions;
using MessageBoards.Application.Commands;
using MessageBoards.Application.Queries;
using MessageBoards.Entities;
using MessageBoards.Handlers;
using MessageBoards.Helper;

List<User> users = new List<User>();
 List<Project> projects = new List<Project>();
var messageStore = new List<Message>();
var sendMessageCommandHandler = new SendMessageCommandHandler(messageStore);
var readTimelineQueryHandler = new ReadTimelineQueryHandler(messageStore);



 while (1 == 1)
 {
     Console.WriteLine("Please, enter a message (format: Alice -> @Moonshot I'm working on the log on screen):");
    
     string userInput = Console.ReadLine();
 
     if (userInput.Contains("@"))
     {

         string pattern = @"^(.*?) -> @(.*?) (.*)$";

         // split to text
         Match match = Regex.Match(userInput, pattern);

         if (match.Success)
         {
             string senderName = match.Groups[1].Value.Trim();
             string projectName = match.Groups[2].Value.Trim();
             string content = match.Groups[3].Value.Trim();
             User sender = users.FirstOrDefault(user => user.Name == senderName);

             if (sender == null)
             {
                 sender = new User { Name = senderName , JoinedProjects = new List<Project>(), SentMessages = new List<Message>()};
             }


             Project project = projects.FirstOrDefault((project => project.ProjectName == projectName));
             if (project == null)
             {
                 project =  new Project { ProjectName = projectName,Members = new List<User>() , Messages = new List<Message>()};

             }
             // save message
             if (sender != null && project != null)
             {
                 var sendMessageCommand = new SendMessageCommand()
                     { Project = project, Content = content, Sender = sender };
                 sendMessageCommandHandler.Handle(sendMessageCommand);
         
             }

         }
     }
     else if (userInput.StartsWith(">"))
     {
         var projectMessageQueryHandler = new ProjectMessageQueryHandler(messageStore);
         var query = new GetProjectMessagesQuery { ProjectName =  userInput.Substring(2) };
         var projectMessages = projectMessageQueryHandler.Handle(query);


         foreach (var message in projectMessages)
         {
             var calculateTime = FormatTimeStamp.FormatTimestamp(message.Timestamp);
             Console.WriteLine($"{message.Sender.Name}\n{message.Content} ({calculateTime})");
         }
     }
     else if (userInput.Contains("follows"))
     {
         string[] words = userInput.Split(' ');

         var joinCommand = new JoinProjectCommand
         {
             UserName = words[0] ,
             ProjectName = words[1] 
         };
         
         var joinHandler = new JoinProjectCommandHandler(users, projects);
         joinHandler.Handle(joinCommand);
     }
     else if (userInput.EndsWith(" wall"))
     {
     }

 }
     