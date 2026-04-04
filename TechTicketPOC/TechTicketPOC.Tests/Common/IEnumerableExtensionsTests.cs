using TechTicketPOC.Common.Extensions;

namespace TechTicketPOC.Tests.Common;

[TestFixture]
public class IEnumerableExtensionsTests
{
    [Test]
    public void IsCollectionValid_WhenCollectionIsNull_ReturnsFalse()
    {
        IEnumerable<int>? collection = null;
        Assert.That(collection!.IsCollectionValid(), Is.False);
    }

    [Test]
    public void IsCollectionValid_WhenCollectionIsEmpty_ReturnsFalse()
    {
        var collection = Enumerable.Empty<int>();
        Assert.That(collection.IsCollectionValid(), Is.False);
    }

    [Test]
    public void IsCollectionValid_WhenCollectionHasOneElement_ReturnsTrue()
    {
        var collection = new List<int> { 1 };
        Assert.That(collection.IsCollectionValid(), Is.True);
    }

    [Test]
    public void IsCollectionValid_WhenCollectionHasMultipleElements_ReturnsTrue()
    {
        var collection = new List<string> { "a", "b", "c" };
        Assert.That(collection.IsCollectionValid(), Is.True);
    }

    [Test]
    public void IsCollectionValid_WhenStringCollectionIsEmpty_ReturnsFalse()
    {
        var collection = new List<string>();
        Assert.That(collection.IsCollectionValid(), Is.False);
    }

    [Test]
    public void IsCollectionValid_WhenObjectCollectionHasItems_ReturnsTrue()
    {
        var collection = new List<object> { new object(), new object() };
        Assert.That(collection.IsCollectionValid(), Is.True);
    }
}
