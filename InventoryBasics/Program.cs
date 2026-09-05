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

abstract class Product
{
    public string SKU { get; set; } // Stock Keeping Unit, a unique identifier for the product
    public string Name { get; set; }
    public int Quantity { get; set;}
    public decimal Price { get; set; }

    // OUR NEW ENUM PROPERTY
    public ProductCategory Category { get; set; }

    // THE CONSTRUCTOR
    // Update the constructor to require the category
    public Product(string sku, string name, int quantity, decimal price, ProductCategory category)
    {
        SKU = sku;
        Name = name;
        Category = category;

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

class PhysicalProduct : Product, ITaxable
{
    public double WeightInKg { get; set; }

    // Add ProductCategory to the parameters, and pass it to base()
    public PhysicalProduct(string sku, string name, int quantity, decimal price, ProductCategory category, double weightinKg)
        : base(sku, name, quantity, price, category)
    {
        WeightInKg = weightinKg;
    }

    // Fulfilling the ITaxable contract
    public decimal CalculateTax()
    {
        // 10% tax rate based on the base price
        return Price * 0.10m;
    }

    public override string GetDetails()
    {
        decimal tax = CalculateTax();
        decimal finalPrice = Price + tax;
        return $"- [PHYSICAL] {Name} | Qty: {Quantity} | Base Price: ${Price} | Tax: ${tax} | Weight: {WeightInKg}kg | Total: ${GetTotalInventoryValue()}";
    }

}

class DigitalProduct : Product
{
    public double DownloadSizeMB { get; set; }

    public DigitalProduct(string sku, string name, int quantity, decimal price, ProductCategory category, double downloadSizeMB)
        : base(sku, name, quantity, price, category)
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

interface ITaxable
{
    decimal CalculateTax();
}

enum ProductCategory
{
    Electronics,
    Clothing,
    Food,
    Books,
    Furniture,
    Software,
    Other
}