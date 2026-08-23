// 1. Declare our variables
string productName = "Mechanical Keyboard";
int stockQuantity = 150;
decimal price = 89.99m; 
bool isInStock = true;

// 2. Calculate the total inventory value
decimal totalValue = stockQuantity * price;

// 3. Print the formatted output using String Interpolation
Console.WriteLine($"Product: {productName} | Price: ${price} | In Stock: {isInStock}");
Console.WriteLine($"Total Inventory Value: ${totalValue}");