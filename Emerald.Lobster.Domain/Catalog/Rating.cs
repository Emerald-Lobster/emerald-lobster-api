namespace Emerald.Lobster.Domain.Catalog;

public class Rating
{
    public int Stars { get; set; }
    public string UserName { get; set; }
    public string? Review { get; set; }

    public Rating(int stars, string userName, string? review)
    {
        if (stars < 1 || stars > 5)
            throw new ArgumentException("Star rating must be an integer of: 1, 2, 3, 4, 5.", nameof(stars));
        if (string.IsNullOrEmpty(userName))
            throw new ArgumentException("UserName cannot be empty.", nameof(userName));

        Stars = stars;
        UserName = userName;
        Review = review;
    }
}
