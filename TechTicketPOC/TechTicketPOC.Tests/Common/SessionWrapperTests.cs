using TechTicketPOC.Common;

namespace TechTicketPOC.Tests.Common;

[TestFixture]
public class SessionWrapperTests
{
    private MockSessionProvider _sessionProvider = null!;

    [SetUp]
    public void SetUp()
    {
        _sessionProvider = new MockSessionProvider();
        SessionWrapper.Initialize(_sessionProvider);
    }

    [Test]
    public void Initialize_WithNullProvider_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => SessionWrapper.Initialize(null!));
    }

    [Test]
    public void Provider_WhenNotInitialized_ThrowsInvalidOperationException()
    {
        // Use reflection to reset the static provider field
        var field = typeof(SessionWrapper).GetField("_provider",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        var original = field!.GetValue(null);
        field.SetValue(null, null);

        try
        {
            Assert.Throws<InvalidOperationException>(() => _ = SessionWrapper.Provider);
        }
        finally
        {
            field.SetValue(null, original);
        }
    }

    [Test]
    public void Set_StoresValueInSession()
    {
        SessionWrapper.Set("myKey", "myValue");

        Assert.That(_sessionProvider.Store["myKey"], Is.EqualTo("myValue"));
    }

    [Test]
    public void Set_OverwritesExistingValue()
    {
        SessionWrapper.Set("key", "value1");
        SessionWrapper.Set("key", "value2");

        Assert.That(_sessionProvider.Store["key"], Is.EqualTo("value2"));
    }

    [Test]
    public void Get_ReturnsStoredValue()
    {
        _sessionProvider.Store["stringKey"] = "hello";

        var result = SessionWrapper.Get<string>("stringKey");

        Assert.That(result, Is.EqualTo("hello"));
    }

    [Test]
    public void Get_WhenKeyNotPresent_ReturnsDefault()
    {
        var result = SessionWrapper.Get<string>("nonExistentKey");

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Get_WhenIntValueStored_ReturnsCorrectValue()
    {
        _sessionProvider.Store["intKey"] = 42;

        var result = SessionWrapper.Get<int>("intKey");

        Assert.That(result, Is.EqualTo(42));
    }

    [Test]
    public void Get_WhenNullValueStored_ReturnsDefault()
    {
        _sessionProvider.Store["nullKey"] = null!;

        var result = SessionWrapper.Get<string>("nullKey");

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Set_ThenGet_RoundTripsValue()
    {
        var expected = new List<string> { "a", "b" };
        SessionWrapper.Set("listKey", expected);

        var result = SessionWrapper.Get<List<string>>("listKey");

        Assert.That(result, Is.EqualTo(expected));
    }

    private class MockSessionProvider : ISessionProvider
    {
        public Dictionary<string, object?> Store { get; } = new Dictionary<string, object?>();

        public bool ContainsKey(string key) => Store.ContainsKey(key);

        public object? Get(string key)
        {
            Store.TryGetValue(key, out var value);
            return value;
        }

        public void Set(string key, object value)
        {
            Store[key] = value;
        }
    }
}
