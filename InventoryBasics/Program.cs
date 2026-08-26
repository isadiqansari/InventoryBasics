bool isRunning = true;

while (isRunning)
{
    Console.WriteLine("===================================");
Console.WriteLine(" INVENTORY & BUSINESS MANAGER v1.0 ");
Console.WriteLine("===================================");
Console.WriteLine("1. Add a New Product");
Console.WriteLine("2. View Inventory");
Console.WriteLine("3. Exit Application");
Console.WriteLine("===================================");
Console.Write("Enter your choice (1-3): ");

string menuChoice = Console.ReadLine();

switch (menuChoice)
{
    case "1":
        Console.WriteLine("\n--> You chose to Add a New Product.");
        break;
    case "2":
        Console.WriteLine("\n--> You chose to View Inventory.");
        break;
    case "3":
        Console.WriteLine("\n--> You chose to Exit Application.");
        isRunning = false;
        break;
    default:
        Console.WriteLine("\n--> Invalid choice! Please select 1, 2, or 3.");
        break;
}
}

