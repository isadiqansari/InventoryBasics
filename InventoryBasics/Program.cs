// Instantiate our generic repository specifically configured to hold 'Product' objects.
// 'T' becomes 'Product' throughout the repository instance.
InventoryRepository<Product> productRepo = new InventoryRepository<Product>();

// Loop control variable. While true, the application stays open.
bool isRunning = true;

// The main application loop: keeps running until isRunning is set to false.
while (isRunning)
{
    // Display the console UI menu options to the user.
    Console.WriteLine("\n===================================");
    Console.WriteLine(" INVENTORY & BUSINESS MANAGER v1.0 ");
    Console.WriteLine("===================================");
    Console.WriteLine("1. Add a New Product");
    Console.WriteLine("2. View All Inventory");
    Console.WriteLine("3. View Low Stock Products");
    Console.WriteLine("4. Exit Application");
    Console.WriteLine("===================================");
    Console.Write("Enter your choice (1-4): ");

    // Read the user's menu choice from the console as text.
    string menuChoice = Console.ReadLine();

    // Inspect the input and route execution to the matching case.
    switch (menuChoice)
    {
        // User picked option 1: Add a product.
        case "1":
            // Call the dedicated method defined lower down in the file.
            AddProduct();
            // Exit the switch statement and let the loop repeat.
            break;

        // User picked option 2: View full inventory.
        case "2":
            Console.WriteLine("\n--- CURRENT INVENTORY ---");

            // Ask our generic repository for all products currently held in storage.
            var allProducts = productRepo.GetAll();

            // Iterate over each product returned from the repository.
            foreach (Product item in allProducts)
            {
                // Print the SKU alongside the details formatted by the product's own GetDetails() method.
                Console.WriteLine($"[SKU: {item.SKU}] {item.GetDetails()}");
            }
            // Exit the switch statement and return to the main menu.
            break;

        // User picked option 3: View low stock items.
        case "3":
            Console.WriteLine("\n--- LOW STOCK ALERT (Under 5 items) ---");

            // LINQ query: fetch all products from the repo, then filter where Quantity is under 5.
            var lowStockItems = productRepo.GetAll().Where(p => p.Quantity < 5).ToList();

            // Check if our filtered list is empty.
            if (lowStockItems.Count == 0)
            {
                // Inform the user there are no low-stock alerts.
                Console.WriteLine("All products are sufficiently stocked!");
            }
            else
            {
                // If items were found, loop through and display each one.
                foreach (Product item in lowStockItems)
                {
                    Console.WriteLine($"[SKU: {item.SKU}] {item.GetDetails()}");
                }
            }
            // Exit the switch statement and return to the main menu.
            break;

        // User picked option 4: Exit.
        case "4":
            Console.WriteLine("\n--> Exiting application. Goodbye!");
            // Setting isRunning to false terminates the while loop on the next evaluation.
            isRunning = false;
            break;

        // Fallback case: user typed something other than 1, 2, 3, or 4.
        default:
            Console.WriteLine("\n--> INVALID CHOICE! Please select 1, 2, 3, or 4.");
            break;
    }
}

// Method handling the creation and registration of new products.
void AddProduct()
{
    Console.WriteLine("\n--- ADD NEW PRODUCT ---");
    Console.WriteLine("1. Physical Product");
    Console.WriteLine("2. Digital Product");
    Console.Write("Choice: ");

    // Read whether the product is physical or digital.
    string typeChoice = Console.ReadLine();

    Console.Write("Enter the unique SKU (e.g., KB-001): ");
    // Read the SKU identifier.
    string sku = Console.ReadLine();

    // Query our repository helper to ensure this SKU isn't already taken.
    if (productRepo.Exists(sku))
    {
        Console.WriteLine("[ERROR] That SKU already exists in the system!");
        // Early return exits AddProduct immediately and returns to the menu loop.
        return;
    }

    Console.Write("Enter the product name: ");
    // Read product name.
    string productName = Console.ReadLine();

    // Default numeric variables before parsing.
    int stockQuantity = 0;
    decimal price = 0;

    // Safety net: catch parsing errors if the user inputs letters instead of numbers.
    try
    {
        Console.Write($"Enter the stock quantity for {productName}: ");
        // Parse the text quantity into an integer.
        stockQuantity = int.Parse(Console.ReadLine());

        Console.Write($"Enter the price for {productName}: ");
        // Parse the text price into a decimal.
        price = decimal.Parse(Console.ReadLine());
    }
    catch (Exception)
    {
        // Runs only if int.Parse or decimal.Parse throws a FormatException.
        Console.WriteLine("\n[ERROR] Invalid input! Please enter standard numbers only.");
        // Exit method safely so the application does not terminate.
        return;
    }

    // Branch based on whether this is a digital or physical product.
    if (typeChoice == "2")
    {
        Console.Write("Enter the download size in MB: ");
        // Read and parse the file size for digital products.
        double size = double.Parse(Console.ReadLine());

        // Construct a DigitalProduct using its specific constructor.
        DigitalProduct newDigital = new DigitalProduct(sku, productName, stockQuantity, price, ProductCategory.Software, size);

        // Store the newly created digital product in our generic repository.
        productRepo.Add(sku, newDigital);
    }
    else
    {
        Console.Write("Enter the weight in Kg: ");
        // Read and parse weight for physical products.
        double weight = double.Parse(Console.ReadLine());

        // Construct a PhysicalProduct using its specific constructor.
        PhysicalProduct newPhysical = new PhysicalProduct(sku, productName, stockQuantity, price, ProductCategory.Furniture, weight);

        // Store the newly created physical product in our generic repository.
        productRepo.Add(sku, newPhysical);
    }

    // Confirmation message shown after successful insertion.
    Console.WriteLine("\n--> Product added successfully!");
}