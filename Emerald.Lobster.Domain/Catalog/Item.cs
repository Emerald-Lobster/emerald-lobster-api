namespace Emerald.Lobster.Domain.Catalog;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Brand { get; set; }
    public decimal Price { get; set; }
    public List<Rating> Ratings { get; set; } = new();

    public Item(string name, string description, string brand, decimal price)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrEmpty(description))
            throw new ArgumentNullException(nameof(description));
        if (string.IsNullOrEmpty(brand))
            throw new ArgumentNullException(nameof(brand));
        if (price < 0.00m)
            throw new ArgumentException("Price cannot be negative.", nameof(price));

        Name = name;
        Description = description;
        Brand = brand;
        Price = price;
    }

    public void AddRating(Rating rating)
    {
        Ratings.Add(rating);
    }
}
