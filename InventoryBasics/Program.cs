// CHANGE THIS: List<string> inventoryList = new List<string>();
// TO THIS:
List<Product> inventoryList = new List<Product>();
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

string? menuChoice = Console.ReadLine();

switch (menuChoice)
{
    case "1":
        AddNewProduct();
        break;
    case "2":
        Console.WriteLine("\n--- CURRENT INVENTORY ---");
        foreach (Product item in inventoryList)
        {
            Console.WriteLine($"- {item.Name} | Qty: {item.Quantity} | Price: ${item.Price}");
        }
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
    string? productName = Console.ReadLine();
    
    // NEW: Add it to our global inventory list
    // inventoryList.Add(productName);

    Console.Write($"Enter the stock quantity for {productName}: ");
    int stockQuantity = int.Parse(Console.ReadLine());

    Console.Write($"Enter the price for {productName}: ");
    decimal price = decimal.Parse(Console.ReadLine());

    // Create the object
    // Product newProduct = new Product();
    // newProduct.Name = productName;
    // newProduct.Quantity = stockQuantity;
    // newProduct.Price = price;

    // Create the object using our new constructor!
    Product newProduct = new Product(productName, stockQuantity, price);
    
    // Add the whole object to the List
    inventoryList.Add(newProduct);

    // USING OUR NEW METHOD HERE!
    // We pass in the quantity and price, and it returns the total.
    decimal totalValue = CalculateTotalValue(stockQuantity, price);

    Console.WriteLine("\n--- SUCCESS ---");
    Console.WriteLine($"Added {stockQuantity}x {productName} @ ${price} each.");
    Console.WriteLine($"Total Value Added: ${totalValue}");
}

decimal CalculateTotalValue(int quantity, decimal cost)
{
    decimal total = quantity * cost;
    return total;
}

class Product
{
    public string Name { get; set; }
    public int Quantity { get; set;}
    public decimal Price { get; set; }

    // THE CONSTRUCTOR
    public Product(string name, int quantity, decimal price)
    {
        Name = name;

        // VALIDATe QUANTITY
        if (quantity < 0)
        {
            Console.WriteLine("\n[ERROR] Quantity cannot be negative. Defaulting to 0.");
            Quantity = 0;
        }
        else
        {
            Quantity = quantity;
        }
        
        // VALIDATE PRICE
        if(price < 0)
        {
            Console.WriteLine("\n[ERROR] Price cannot be negative. Defaulting to $0.00.");
            Price = 0m;
        }
        else
        {
            Price = price;
        }
    }
}