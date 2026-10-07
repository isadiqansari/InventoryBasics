// Define the class with a generic type parameter <T>.
// <T> acts as a placeholder for whatever entity type this repository will store (e.g., Product, Customer).
class InventoryRepository<T> where T : Product
{
    // A private internal dictionary that actually holds the data in memory.
    // string: represents the lookup key (like a SKU or an ID).
    // T: represents the object itself (matching whatever type T is).
    // readonly: prevents this variable from being reassigned to a different dictionary instance elsewhere.
    private readonly Dictionary<string, T> _storage = new Dictionary<string, T>();

    // Method to add an item to storage.
    // Takes a string key and an item of type T.
    // Returns bool: true if added successfully, false if the key already exists.

    // We no longer need to pass 'string key' as a parameter 
    // if we assume that the Product class has a property that can serve 
    // as a unique identifier (like SKU or ID). The repo reads item.SKU directly.
    public bool Add( T item)
    {
        // Guard check: see if this key is already taken in the dictionary.
        if (_storage.ContainsKey(item.SKU))
        {
            // Exit early and report failure to avoid crashing the program with a duplicate key exception.
            return false;
        }

        // Key is unique, so insert the key-value pair into internal storage.
        _storage.Add(item.SKU, item);

        // Report that the insertion was successful.
        return true;
    }

    // Method to fetch an individual item by its key.
    // Returns an object of type T (or null/default if not found).
    public T GetByKey(string key)
    {
        // Guard check: verify the key exists before trying to read it.
        if (_storage.ContainsKey(key))
        {
            // Key exists; return the corresponding item.
            return _storage[key];
        }

        // If the key is not found, return the default value for type T.
        // For reference types (like classes), default is null.
        return default;
    }

    // Method to fetch all stored items at once.
    // Returns a List<T> containing every object currently held.
    public List<T> GetAll()
    {
        // _storage.Values grabs only the objects (ignoring the dictionary keys).
        // .ToList() converts that collection into a standard List<T>.
        return _storage.Values.ToList();
    }

    // Quick helper method to check whether an item exists without retrieving it.
    // Returns true if present, false if not.
    public bool Exists(string key)
    {
        // Forwards the call directly to Dictionary's built-in, lightning-fast ContainsKey check.
        return _storage.ContainsKey(key);
    }

    // OCT-7-2026: Remove and item by its key. Returns true if the item was found and removed, false if not.
    public bool Remove(string key)
    {
        return _storage.Remove(key);
    }

    // OCT-7-2026: Update quantity of a specific item safely. Returns true if the item was found and updated, false if not.
    public bool UpdateQuantity(string key, int newQuantity)
    {
        if (!_storage.ContainsKey(key))
        {
            return false;
        }

        if (newQuantity < 0)
        {
            Console.WriteLine("[ERROR] Stock quantity cannot be negative.");
            return false;
        }

        _storage[key].Quantity = newQuantity;
        return true;
    }
}