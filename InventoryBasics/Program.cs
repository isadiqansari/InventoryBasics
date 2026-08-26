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
        AddNewProduct();
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

// ==========================================
// METHODS GO BELOW HERE (AT THE BOTTOM OF THE FILE)
// ==========================================

void AddNewProduct()
{
    Console.WriteLine("\n--- ADD NEW PRODUCT ---");

    Console.Write("Enter the product name: ");
    string productName = Console.ReadLine();

    Console.Write($"Enter the stock quantity for {productName}: ");
    int stockQuantity = int.Parse(Console.ReadLine());

    Console.Write($"Enter the price for {productName}: ");
    decimal price = decimal.Parse(Console.ReadLine());

    decimal totalValue = stockQuantity * price;

    Console.WriteLine("\n--- SUCCESS ---");
    Console.WriteLine($"Added {stockQuantity}x {productName} @ ${price} each.");
    Console.WriteLine($"Total Value Added: ${totalValue}");
}