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
