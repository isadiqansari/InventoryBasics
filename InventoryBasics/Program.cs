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
            //Polymorphism in action! C# automatically figures out
            // if it should call the Product version or the DigitalProduct version
            Console.WriteLine(item.GetDetails());
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
    Console.WriteLine("1. Physical Product");
    Console.WriteLine("2. Digital Product");
    Console.Write("Choice: ");
    string? typeChoice = Console.ReadLine();

    Console.Write("Enter the product name: ");
    string? productName = Console.ReadLine();

    Console.Write($"Enter the stock quantity for {productName}: ");
    int stockQuantity = int.Parse(Console.ReadLine());

    Console.Write($"Enter the price for {productName}: ");
    decimal price = decimal.Parse(Console.ReadLine());

    if (typeChoice == "2")
    {
        // It's a digital product!
        Console.Write($"Enter the download size in MB: ");
        double size = double.Parse(Console.ReadLine());

        DigitalProduct newDigital = new DigitalProduct(productName, stockQuantity, price, size);
        inventoryList.Add(newDigital); // We can add this to the list because a DigitalProduct IS-A Product!

    }
    else
    {
        // It's a standard physical product
        Product newProduct = new Product(productName, stockQuantity, price);
        inventoryList.Add(newProduct);
    }

    Console.WriteLine("\n---> Product added successfully!");
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

    // THIS GOES INSIDE THE PRODUCT CLASS
    public decimal GetTotalInventoryValue()
    {
        // Notice how it just directly uses its own properties!
        return Quantity * Price;
    }

    // The default behavior for a standard product
    public virtual string GetDetails()
    {
        return $"- {Name} | Qty: {Quantity} | Price ${Price} | Total: ${GetTotalInventoryValue()}";
    }
}

class DigitalProduct : Product
{
    public double DownloadSizeMB { get; set; }

    public DigitalProduct(string name, int quantity, decimal price, double downloadSizeMB)
        : base(name, quantity, price)
    {
        DownloadSizeMB = downloadSizeMB;
    }

    // The specialized behavior for a digital product
    public override string GetDetails()
    {
        // Notice we still use the basic properties, but we add our new one!
        return $"- [DIGITAL] {Name} | Qty: {Quantity} | Price: ${Price} | Size: {DownloadSizeMB} MB | Total: ${GetTotalInventoryValue()}";
    }
}