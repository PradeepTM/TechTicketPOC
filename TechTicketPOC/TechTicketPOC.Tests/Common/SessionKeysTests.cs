using TechTicketPOC.Common.Constants;

namespace TechTicketPOC.Tests.Common;

[TestFixture]
public class SessionKeysTests
{
    [Test]
    public void EmailTemplateByRequest_HasExpectedPattern()
    {
        Assert.That(SessionKeys.EMAIL_TEMPLATE_BY_REQUEST, Is.EqualTo("EmailTemplate_{0}"));
    }

    [Test]
    public void EmailTemplateByRequest_CanBeFormattedWithRequestId()
    {
        var key = string.Format(SessionKeys.EMAIL_TEMPLATE_BY_REQUEST, 42);
        Assert.That(key, Is.EqualTo("EmailTemplate_42"));
    }

    [Test]
    public void EmailTemplateByRequest_CanBeFormattedWithDifferentIds()
    {
        var key1 = string.Format(SessionKeys.EMAIL_TEMPLATE_BY_REQUEST, 1);
        var key2 = string.Format(SessionKeys.EMAIL_TEMPLATE_BY_REQUEST, 2);
        Assert.That(key1, Is.Not.EqualTo(key2));
    }
}
