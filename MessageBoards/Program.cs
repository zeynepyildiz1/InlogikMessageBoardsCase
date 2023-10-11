using MessageBoards.Business;

ProcessUserOperations userOperations = new ProcessUserOperations();

 while (true)
 {
     Console.WriteLine("Please, enter a message (format: Alice -> @Moonshot I'm working on the log on screen):");
     var userInput = Console.ReadLine();
     userOperations.ProcessInput(userInput);
 }
     