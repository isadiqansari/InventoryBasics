// CHANGE THIS: List<string> inventoryList = new List<string>();
// TO THIS:
// Delete the List, use the code below insted: List<Product> inventoryList = new List<Product>();
Dictionary<string, Product> inventoryMap = new Dictionary<string, Product>();
bool isRunning = true;

while (isRunning)
{
    Console.WriteLine("===================================");
Console.WriteLine(" INVENTORY & BUSINESS MANAGER v1.0 ");
Console.WriteLine("===================================");
Console.WriteLine("1. Add a New Product");
Console.WriteLine("2. View Inventory");
Console.WriteLine("3. View Low Stock Products");
Console.WriteLine("4. Exit Application");
Console.WriteLine("===================================");
Console.Write("Enter your choice (1-4): ");

string? menuChoice = Console.ReadLine();

switch (menuChoice)
{
    case "1":
        AddNewProduct();
        break;
    case "2":
        Console.WriteLine("\n--- CURRENT INVENTORY ---");
        foreach (Product item in inventoryMap.Values) // Use .Values to get the Product objects from the dictionary
        {
            //Polymorphism in action! C# automatically figures out
            // if it should call the Product version or the DigitalProduct version
            // Console.WriteLine(item.GetDetails());

            Console.WriteLine($"[SKU: {item.SKU}] {item.GetDetails()}");
        }
        break;
    case "3":
        Console.WriteLine("\n--- LOW STOCK ALERT (Under 5 items) ---");

        // USING LINQ TO FILTER THE LIST
        var lowStockItems = inventoryMap.Values.Where(p => p.Quantity < 5).ToList();

        if(lowStockItems.Count == 0)
            {
                Console.WriteLine("All products are sufficiently stocked!");
            }
            else
            {
                foreach (Product item in lowStockItems)
                {
                    Console.WriteLine(item.GetDetails());
                }
            }
        break;
    case "4":
        Console.WriteLine("\n--> You chose to Exit Application.");
        isRunning = false;
        break;
    default:
        Console.WriteLine("\n--> Invalid choice! Please select 1, 2, 3 or 4.");
        break;
}
}

// ==========================================
// METHODS GO BELOW HERE (AT THE BOTTOM OF THE FILE)
// ==========================================

void AddNewProduct()
{
    Console.WriteLine("\n--- ADD NEW PRODUCT ---");
    Console.WriteLine("1. Physical Product");
    Console.WriteLine("2. Digital Product");
    Console.Write("Choice: ");
    string? typeChoice = Console.ReadLine();

    Console.Write("Enter the unique SKU (e.g., KB-001): ");
    string? sku = Console.ReadLine();

    // Prevent duplicates before we even ask for the rest of the details!
    if (inventoryMap.ContainsKey(sku))
    {
        Console.WriteLine("[ERROR] That SKU already exists in the system!");
        return;
    }

    Console.Write("Enter the product name: ");
    string? productName = Console.ReadLine();

    int stockQuantity = 0;
    decimal price = 0m;

    // THE SAFETY NET
    try
    {
    Console.Write($"Enter the stock quantity for {productName}: ");
    stockQuantity = int.Parse(Console.ReadLine());

    Console.Write($"Enter the price for {productName}: ");
    price = decimal.Parse(Console.ReadLine());
    }
    catch (Exception ex)
    {
        Console.WriteLine("\n[ERROR] Invalid Input! Please enter standard numbes only.");
        return; // This immediately exits the AddNewProduct() and goes back to main menu
    }

    if (typeChoice == "2")
    {
        // It's a digital product!
        Console.Write($"Enter the download size in MB: ");
        double size = double.Parse(Console.ReadLine());

        DigitalProduct newDigital = new DigitalProduct(sku, productName, stockQuantity, price, ProductCategory.Software, size);
        // Add to dictionary: The first parameter is the Key, the second is the Object
        inventoryMap.Add(sku, newDigital); // We can add this to the list because a DigitalProduct IS-A Product!

    }
    else
    {
        // It's a standard physical product
        // THIS is the new part
        Console.Write($"Enter the weight in Kg: ");
        double weight = double.Parse(Console.ReadLine());
        
        // We use PhysicalProduct now, NOT the abstract Product!
        PhysicalProduct newPhysical = new PhysicalProduct(sku, productName, stockQuantity, price, ProductCategory.Furniture ,weight);
        // Add to dictionary: The first parameter is the Key, the second is the Object
        inventoryMap.Add(sku, newPhysical);
    }

    Console.WriteLine("\n---> Product added successfully!");
}

decimal CalculateTotalValue(int quantity, decimal cost)
{
    decimal total = quantity * cost;
    return total;
}