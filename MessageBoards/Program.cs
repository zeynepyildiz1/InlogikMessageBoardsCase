using MessageBoards.Business;
using MessageBoards.Helper;

ProcessUserOperations userOperations = new ProcessUserOperations();

 while (true)
 {
     Console.WriteLine("Please, enter a message (format: Alice -> @Moonshot I'm working on the log on screen):");
     string userInput = Console.ReadLine();
     userOperations.ProcessInput(userInput);
 }
     