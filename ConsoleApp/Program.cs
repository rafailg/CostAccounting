using ConsoleApp;
using Controllers;
using DataAccess;

var dataSource = DataSourceRetriever.NewInMemoryDataSource();
TradingController.AddTestTradingData(dataSource);

Commands.ShowStartInfo();
Console.WriteLine("Press any key to start...");
Console.ReadKey();

var stop = false;

while (!stop)
{
    Console.Clear();
    
    Console.WriteLine("Enter 'help' for command details");
    Console.WriteLine("Enter a command:");
    var command = Console.ReadLine();
    if (command is null) continue;
    
    ProcessCommand(command, out stop);

    if (!stop)
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}

void ProcessCommand(string command, out bool stop)
{
    stop = false;
    command = command.ToLower();

    switch (command)
    {
        case "quit":
            stop = true;
            break;
        case "help":
            Commands.ShowHelp();
            break;
        case "sell":
            Commands.SellStock(dataSource);
            break;
        case "overview":
            Commands.Overview(dataSource);
            break;
        default:
            Console.WriteLine($"Unknown command {command}");
            break;
    }
}