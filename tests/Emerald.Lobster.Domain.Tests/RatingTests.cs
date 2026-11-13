using Emerald.Lobster.Domain.Catalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emerald.Lobster.Domain.Tests;

[TestClass]
public class RatingTests
{
    [TestMethod]
    public void Can_Create_New_Rating()
    {
        // Arrange
        var rating = new Rating(1, "Mike", "Great fit!");

        // Assert
        Assert.AreEqual(1, rating.Stars);
        Assert.AreEqual("Mike", rating.UserName);
        Assert.AreEqual("Great fit!", rating.Review);
    }

    [TestMethod]
    public void Cannot_Create_Rating_With_Invalid_Stars()
    {
        // Act / Assert
        Assert.ThrowsException<ArgumentException>(() => new Rating(0, "Mike", "Great fit!"));
    }
}
