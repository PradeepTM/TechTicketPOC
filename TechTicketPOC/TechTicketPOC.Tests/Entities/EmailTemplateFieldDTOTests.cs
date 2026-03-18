using TechTicketPOC.Entities;

namespace TechTicketPOC.Tests.Entities;

[TestFixture]
public class EmailTemplateFieldDTOTests
{
    [Test]
    public void EmailTemplateFieldDTO_CanSetAndGetId()
    {
        var dto = new EmailTemplateFieldDTO { Id = 1 };
        Assert.That(dto.Id, Is.EqualTo(1));
    }

    [Test]
    public void EmailTemplateFieldDTO_CanSetAndGetEmailTemplateId()
    {
        var dto = new EmailTemplateFieldDTO { EmailTemplateId = 3 };
        Assert.That(dto.EmailTemplateId, Is.EqualTo(3));
    }

    [Test]
    public void EmailTemplateFieldDTO_CanSetAndGetFieldName()
    {
        var dto = new EmailTemplateFieldDTO { FieldName = "ClaimNumber" };
        Assert.That(dto.FieldName, Is.EqualTo("ClaimNumber"));
    }

    [Test]
    public void EmailTemplateFieldDTO_CanSetAndGetDisplayName()
    {
        var dto = new EmailTemplateFieldDTO { DisplayName = "Claim Number" };
        Assert.That(dto.DisplayName, Is.EqualTo("Claim Number"));
    }

    [Test]
    public void EmailTemplateFieldDTO_CanSetAndGetDataType()
    {
        var dto = new EmailTemplateFieldDTO { DataType = "Int" };
        Assert.That(dto.DataType, Is.EqualTo("Int"));
    }

    [Test]
    public void EmailTemplateFieldDTO_CanSetAndGetFieldType()
    {
        var dto = new EmailTemplateFieldDTO { FieldType = "TextBox" };
        Assert.That(dto.FieldType, Is.EqualTo("TextBox"));
    }

    [Test]
    public void EmailTemplateFieldDTO_CanSetAndGetIsAllowBlank()
    {
        var dto = new EmailTemplateFieldDTO { IsAllowBlank = true };
        Assert.That(dto.IsAllowBlank, Is.True);
    }

    [Test]
    public void EmailTemplateFieldDTO_CanSetAndGetFieldOrder()
    {
        var dto = new EmailTemplateFieldDTO { FieldOrder = 2 };
        Assert.That(dto.FieldOrder, Is.EqualTo(2));
    }

    [Test]
    public void EmailTemplateFieldDTO_CanSetAndGetDefaultValue()
    {
        var dto = new EmailTemplateFieldDTO { DefaultValue = "N/A" };
        Assert.That(dto.DefaultValue, Is.EqualTo("N/A"));
    }

    [Test]
    public void EmailTemplateFieldDTO_CanSetAndGetFieldOptions()
    {
        var options = new List<FieldOptionDTO>
        {
            new FieldOptionDTO { DisplayName = "Richmond", Value = "Richmond" }
        };
        var dto = new EmailTemplateFieldDTO { FieldOptions = options };
        Assert.That(dto.FieldOptions, Has.Count.EqualTo(1));
    }

    [Test]
    public void EmailTemplateFieldDTO_CanSetAndGetMaxLength()
    {
        var dto = new EmailTemplateFieldDTO { MaxLength = 100 };
        Assert.That(dto.MaxLength, Is.EqualTo(100));
    }

    [Test]
    public void EmailTemplateFieldDTO_CanSetAndGetMinMaxValues()
    {
        var dto = new EmailTemplateFieldDTO { MinValue = "0", MaxValue = "999" };
        Assert.That(dto.MinValue, Is.EqualTo("0"));
        Assert.That(dto.MaxValue, Is.EqualTo("999"));
    }

    [Test]
    public void EmailTemplateFieldDTO_CanSetAndGetFormatRegEx()
    {
        var dto = new EmailTemplateFieldDTO { FormatRegEx = "/[0-9]/" };
        Assert.That(dto.FormatRegEx, Is.EqualTo("/[0-9]/"));
    }

    [Test]
    public void EmailTemplateFieldDTO_ImplementsIAuditable()
    {
        var dto = new EmailTemplateFieldDTO
        {
            CreatedBy = "admin",
            CreatedDate = new DateTime(2024, 1, 1),
            ModifiedBy = "admin",
            ModifiedDate = new DateTime(2024, 2, 1)
        };
        Assert.That(dto.CreatedBy, Is.EqualTo("admin"));
    }
}
