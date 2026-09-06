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


