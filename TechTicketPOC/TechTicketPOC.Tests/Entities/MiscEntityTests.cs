using TechTicketPOC.Entities;

namespace TechTicketPOC.Tests.Entities;

[TestFixture]
public class FieldOptionDTOTests
{
    [Test]
    public void FieldOptionDTO_CanSetAndGetId()
    {
        var dto = new FieldOptionDTO { Id = 1 };
        Assert.That(dto.Id, Is.EqualTo(1));
    }

    [Test]
    public void FieldOptionDTO_CanSetAndGetTemplateFieldId()
    {
        var dto = new FieldOptionDTO { TemplateFieldId = 5 };
        Assert.That(dto.TemplateFieldId, Is.EqualTo(5));
    }

    [Test]
    public void FieldOptionDTO_CanSetAndGetDisplayName()
    {
        var dto = new FieldOptionDTO { DisplayName = "Richmond" };
        Assert.That(dto.DisplayName, Is.EqualTo("Richmond"));
    }

    [Test]
    public void FieldOptionDTO_CanSetAndGetValue()
    {
        var dto = new FieldOptionDTO { Value = "richmond_value" };
        Assert.That(dto.Value, Is.EqualTo("richmond_value"));
    }

    [Test]
    public void FieldOptionDTO_ImplementsIAuditable()
    {
        var dto = new FieldOptionDTO
        {
            CreatedBy = "user",
            CreatedDate = DateTime.UtcNow,
            ModifiedBy = "user2",
            ModifiedDate = DateTime.UtcNow
        };
        Assert.That(dto.CreatedBy, Is.EqualTo("user"));
        Assert.That(dto.ModifiedBy, Is.EqualTo("user2"));
    }
}

[TestFixture]
public class EmailTransctionLogDTOTests
{
    [Test]
    public void EmailTransctionLogDTO_CanSetAndGetId()
    {
        var dto = new EmailTransctionLogDTO { Id = 1 };
        Assert.That(dto.Id, Is.EqualTo(1));
    }

    [Test]
    public void EmailTransctionLogDTO_CanSetAndGetEmailTemplateId()
    {
        var dto = new EmailTransctionLogDTO { EmailTemplateId = 3 };
        Assert.That(dto.EmailTemplateId, Is.EqualTo(3));
    }

    [Test]
    public void EmailTransctionLogDTO_CanSetAndGetFrom()
    {
        var dto = new EmailTransctionLogDTO { From = "sender@example.com" };
        Assert.That(dto.From, Is.EqualTo("sender@example.com"));
    }

    [Test]
    public void EmailTransctionLogDTO_CanSetAndGetToList()
    {
        var to = new List<string> { "recipient@example.com" };
        var dto = new EmailTransctionLogDTO { To = to };
        Assert.That(dto.To, Is.EqualTo(to));
    }

    [Test]
    public void EmailTransctionLogDTO_CanSetAndGetEmailBody()
    {
        var dto = new EmailTransctionLogDTO { EmailBody = "Test email body" };
        Assert.That(dto.EmailBody, Is.EqualTo("Test email body"));
    }

    [Test]
    public void EmailTransctionLogDTO_CanSetAndGetSentOn()
    {
        var now = DateTime.UtcNow;
        var dto = new EmailTransctionLogDTO { SentOn = now };
        Assert.That(dto.SentOn, Is.EqualTo(now));
    }

    [Test]
    public void EmailTransctionLogDTO_CanSetAndGetSentBy()
    {
        var dto = new EmailTransctionLogDTO { SentBy = "user1" };
        Assert.That(dto.SentBy, Is.EqualTo("user1"));
    }

    [Test]
    public void EmailTransctionLogDTO_CanSetCCAndBCC()
    {
        var cc = new List<string> { "cc@example.com" };
        var bcc = new List<string> { "bcc@example.com" };
        var dto = new EmailTransctionLogDTO { CC = cc, BCC = bcc };
        Assert.That(dto.CC, Is.EqualTo(cc));
        Assert.That(dto.BCC, Is.EqualTo(bcc));
    }
}

[TestFixture]
public class FieldDataTypeTests
{
    [Test]
    public void FieldDataType_INT_HasExpectedValue()
    {
        Assert.That(TechTicketPOC.Entities.Constants.FieldDataType.INT, Is.EqualTo("int"));
    }

    [Test]
    public void FieldDataType_INT_IsLowercase()
    {
        var value = TechTicketPOC.Entities.Constants.FieldDataType.INT;
        Assert.That(value, Is.EqualTo(value.ToLower()));
    }
}
