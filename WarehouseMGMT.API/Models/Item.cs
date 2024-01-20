namespace WarehouseMGMT.Models;

public class Item
{
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public double Weight { get; set; }

    public Item()
    { }
    
    public Item(Guid id)
    {
        Id = id;
    }
}