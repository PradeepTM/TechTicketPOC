using Moq;
using TechTicketPOC.BLL;
using TechTicketPOC.BLL.Interfaces;
using TechTicketPOC.Entities;

namespace TechTicketPOC.Tests.BLL;

[TestFixture]
public class EmailTemplateBLLTests
{
    private Mock<IEmailTemplateRepository> _mockRepository = null!;
    private EmailTemplateBLL _sut = null!;

    [SetUp]
    public void SetUp()
    {
        _mockRepository = new Mock<IEmailTemplateRepository>();
        _sut = new EmailTemplateBLL(_mockRepository.Object);
    }

    [Test]
    public void Constructor_WithNullRepository_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new EmailTemplateBLL(null!));
    }

    [Test]
    public void GetEmailTemplate_WhenRepositoryReturnsNull_ReturnsNull()
    {
        _mockRepository.Setup(r => r.GetEmailTemplate(It.IsAny<int>())).Returns((EmailTemplateDTO?)null);

        var result = _sut.GetEmailTemplate(1);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetEmailTemplate_WhenRepositoryReturnsTemplate_ReturnsTemplate()
    {
        var template = new EmailTemplateDTO
        {
            Id = 1,
            RequestId = 5,
            TemplateName = "Core-Reset",
            Description = "Core Reset email template",
            To = new List<string> { "user@example.com" },
            EmailTemplateBody = "Hello {{Name}}",
            Fields = new List<EmailTemplateFieldDTO>
            {
                new EmailTemplateFieldDTO { FieldName = "ClaimNumber", FieldType = "TextBox" }
            }
        };
        _mockRepository.Setup(r => r.GetEmailTemplate(5)).Returns(template);

        var result = _sut.GetEmailTemplate(5);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.TemplateName, Is.EqualTo("Core-Reset"));
        Assert.That(result.RequestId, Is.EqualTo(5));
        Assert.That(result.Fields, Has.Count.EqualTo(1));
    }

    [Test]
    public void GetEmailTemplate_PassesRequestIdToRepository()
    {
        const int requestId = 42;
        _mockRepository.Setup(r => r.GetEmailTemplate(requestId)).Returns((EmailTemplateDTO?)null);

        _sut.GetEmailTemplate(requestId);

        _mockRepository.Verify(r => r.GetEmailTemplate(requestId), Times.Once);
    }

    [Test]
    public void GetEmailTemplate_CallsRepositoryExactlyOnce()
    {
        _mockRepository.Setup(r => r.GetEmailTemplate(It.IsAny<int>())).Returns((EmailTemplateDTO?)null);

        _sut.GetEmailTemplate(1);

        _mockRepository.Verify(r => r.GetEmailTemplate(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public void GetEmailTemplate_WhenTemplateHasToListAndCCList_ReturnsTemplateWithLists()
    {
        var template = new EmailTemplateDTO
        {
            Id = 2,
            RequestId = 10,
            To = new List<string> { "to@example.com" },
            CC = new List<string> { "cc@example.com" },
            BCC = new List<string> { "bcc@example.com" }
        };
        _mockRepository.Setup(r => r.GetEmailTemplate(10)).Returns(template);

        var result = _sut.GetEmailTemplate(10);

        Assert.That(result!.To, Has.Count.EqualTo(1));
        Assert.That(result.CC, Has.Count.EqualTo(1));
        Assert.That(result.BCC, Has.Count.EqualTo(1));
    }

    [Test]
    public void GetEmailTemplate_WithDifferentRequestIds_CallsRepositoryWithCorrectIds()
    {
        var template1 = new EmailTemplateDTO { Id = 1, RequestId = 1, TemplateName = "Template1" };
        var template2 = new EmailTemplateDTO { Id = 2, RequestId = 2, TemplateName = "Template2" };
        _mockRepository.Setup(r => r.GetEmailTemplate(1)).Returns(template1);
        _mockRepository.Setup(r => r.GetEmailTemplate(2)).Returns(template2);

        var result1 = _sut.GetEmailTemplate(1);
        var result2 = _sut.GetEmailTemplate(2);

        Assert.That(result1!.TemplateName, Is.EqualTo("Template1"));
        Assert.That(result2!.TemplateName, Is.EqualTo("Template2"));
    }
}
