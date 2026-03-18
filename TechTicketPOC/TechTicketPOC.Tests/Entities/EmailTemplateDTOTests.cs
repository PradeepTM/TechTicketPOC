using TechTicketPOC.Entities;

namespace TechTicketPOC.Tests.Entities;

[TestFixture]
public class EmailTemplateDTOTests
{
    [Test]
    public void EmailTemplateDTO_CanSetAndGetId()
    {
        var dto = new EmailTemplateDTO { Id = 10 };
        Assert.That(dto.Id, Is.EqualTo(10));
    }

    [Test]
    public void EmailTemplateDTO_CanSetAndGetRequestId()
    {
        var dto = new EmailTemplateDTO { RequestId = 5 };
        Assert.That(dto.RequestId, Is.EqualTo(5));
    }

    [Test]
    public void EmailTemplateDTO_CanSetAndGetTemplateName()
    {
        var dto = new EmailTemplateDTO { TemplateName = "Core-Reset" };
        Assert.That(dto.TemplateName, Is.EqualTo("Core-Reset"));
    }

    [Test]
    public void EmailTemplateDTO_CanSetAndGetDescription()
    {
        var dto = new EmailTemplateDTO { Description = "Core reset template" };
        Assert.That(dto.Description, Is.EqualTo("Core reset template"));
    }

    [Test]
    public void EmailTemplateDTO_CanSetAndGetEmailTemplateBody()
    {
        var dto = new EmailTemplateDTO { EmailTemplateBody = "<html>...</html>" };
        Assert.That(dto.EmailTemplateBody, Is.EqualTo("<html>...</html>"));
    }

    [Test]
    public void EmailTemplateDTO_CanSetAndGetToList()
    {
        var to = new List<string> { "user@example.com" };
        var dto = new EmailTemplateDTO { To = to };
        Assert.That(dto.To, Is.EqualTo(to));
    }

    [Test]
    public void EmailTemplateDTO_CanSetAndGetCCList()
    {
        var cc = new List<string> { "cc@example.com" };
        var dto = new EmailTemplateDTO { CC = cc };
        Assert.That(dto.CC, Is.EqualTo(cc));
    }

    [Test]
    public void EmailTemplateDTO_CanSetAndGetBCCList()
    {
        var bcc = new List<string> { "bcc@example.com" };
        var dto = new EmailTemplateDTO { BCC = bcc };
        Assert.That(dto.BCC, Is.EqualTo(bcc));
    }

    [Test]
    public void EmailTemplateDTO_CanSetAndGetFields()
    {
        var fields = new List<EmailTemplateFieldDTO>
        {
            new EmailTemplateFieldDTO { FieldName = "ClaimNumber", DisplayName = "Claim Number" }
        };
        var dto = new EmailTemplateDTO { Fields = fields };
        Assert.That(dto.Fields, Has.Count.EqualTo(1));
        Assert.That(dto.Fields[0].FieldName, Is.EqualTo("ClaimNumber"));
    }

    [Test]
    public void EmailTemplateDTO_ImplementsIAuditable()
    {
        var dto = new EmailTemplateDTO
        {
            CreatedBy = "user",
            CreatedDate = new DateTime(2024, 1, 1),
            ModifiedBy = "user2",
            ModifiedDate = new DateTime(2024, 2, 1)
        };
        Assert.That(dto.CreatedBy, Is.EqualTo("user"));
        Assert.That(dto.CreatedDate, Is.EqualTo(new DateTime(2024, 1, 1)));
        Assert.That(dto.ModifiedBy, Is.EqualTo("user2"));
        Assert.That(dto.ModifiedDate, Is.EqualTo(new DateTime(2024, 2, 1)));
    }
}
