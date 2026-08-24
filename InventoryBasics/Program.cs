// 1. Prompt the user for the product name
Console.WriteLine("Enter the new Product name:");
string productName = Console.ReadLine();

// 2. Prompt for the stock quantity and convert (parse) it
Console.WriteLine($"Enter the stock quantity for {productName}");
string quantityInput = Console.ReadLine();
int stockQuantity = int.Parse(quantityInput);

// 3. Prompt for the price and convert (parse) it
Console.WriteLine($"Enter the price of the {productName}");
string priceInput = Console.ReadLine();
decimal price = decimal.Parse(priceInput);

// 4. Calculate total value
decimal totalValue = stockQuantity * price;

// 5. Print the summary
Console.WriteLine("--- PRODUCT SUMMARY ---");
Console.WriteLine($"Product: {productName}");
Console.WriteLine($"Quantity: {stockQuantity}");
Console.WriteLine($"Total Inventory Value: ${totalValue}");

// NEW DECISION LOGIC
Console.WriteLine("--- STOCK STATUS ---");

if (stockQuantity == 0)
{
    Console.WriteLine("WARNING: This item is completely OUT OF STOCK!");
}
else if (stockQuantity < 10)
{
    Console.WriteLine("NOTICE: Low stock level. Consider reordering soon.");
}
else
{
    Console.WriteLine("STATUS: Stock level is sufficient.");
}