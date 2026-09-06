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
