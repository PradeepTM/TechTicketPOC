using TechTicketPOC.Common.Extensions;

namespace TechTicketPOC.Tests.Common;

[TestFixture]
public class ObjectExtensionsTests
{
    [Test]
    public void IsNull_WhenObjectIsNull_ReturnsTrue()
    {
        object? obj = null;
        Assert.That(obj.IsNull(), Is.True);
    }

    [Test]
    public void IsNull_WhenObjectIsNotNull_ReturnsFalse()
    {
        object obj = new object();
        Assert.That(obj.IsNull(), Is.False);
    }

    [Test]
    public void IsNull_WhenStringIsNull_ReturnsTrue()
    {
        string? str = null;
        Assert.That(str.IsNull(), Is.True);
    }

    [Test]
    public void IsNull_WhenStringIsEmpty_ReturnsFalse()
    {
        string str = string.Empty;
        Assert.That(str.IsNull(), Is.False);
    }

    [Test]
    public void IsNull_WhenValueTypeIsBoxed_ReturnsFalse()
    {
        object value = 42;
        Assert.That(value.IsNull(), Is.False);
    }

    [Test]
    public void IsNotNull_WhenObjectIsNotNull_ReturnsTrue()
    {
        object obj = new object();
        Assert.That(obj.IsNotNull(), Is.True);
    }

    [Test]
    public void IsNotNull_WhenObjectIsNull_ReturnsFalse()
    {
        object? obj = null;
        Assert.That(obj.IsNotNull(), Is.False);
    }

    [Test]
    public void IsNotNull_WhenStringHasValue_ReturnsTrue()
    {
        string str = "hello";
        Assert.That(str.IsNotNull(), Is.True);
    }

    [Test]
    public void IsNotNull_WhenStringIsNull_ReturnsFalse()
    {
        string? str = null;
        Assert.That(str.IsNotNull(), Is.False);
    }

    [Test]
    public void IsNotNull_IsInverseOfIsNull()
    {
        object obj = new object();
        Assert.That(obj.IsNotNull(), Is.EqualTo(!obj.IsNull()));
    }
}
