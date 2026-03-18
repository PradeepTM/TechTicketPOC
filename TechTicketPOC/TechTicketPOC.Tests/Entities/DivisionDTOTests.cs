using TechTicketPOC.Entities;

namespace TechTicketPOC.Tests.Entities;

[TestFixture]
public class DivisionDTOTests
{
    [Test]
    public void DivisionDTO_CanSetAndGetId()
    {
        var dto = new DivisionDTO { Id = 5 };
        Assert.That(dto.Id, Is.EqualTo(5));
    }

    [Test]
    public void DivisionDTO_CanSetAndGetDivisionName()
    {
        var dto = new DivisionDTO { DivisionName = "Core" };
        Assert.That(dto.DivisionName, Is.EqualTo("Core"));
    }

    [Test]
    public void DivisionDTO_CanSetAndGetRequests()
    {
        var requests = new List<RequestDTO> { new RequestDTO { RequestName = "Reset" } };
        var dto = new DivisionDTO { Requests = requests };
        Assert.That(dto.Requests, Is.EqualTo(requests));
    }

    [Test]
    public void DivisionDTO_ImplementsIAuditable()
    {
        var dto = new DivisionDTO
        {
            CreatedBy = "user1",
            CreatedDate = new DateTime(2024, 1, 1),
            ModifiedBy = "user2",
            ModifiedDate = new DateTime(2024, 6, 1)
        };
        Assert.That(dto.CreatedBy, Is.EqualTo("user1"));
        Assert.That(dto.CreatedDate, Is.EqualTo(new DateTime(2024, 1, 1)));
        Assert.That(dto.ModifiedBy, Is.EqualTo("user2"));
        Assert.That(dto.ModifiedDate, Is.EqualTo(new DateTime(2024, 6, 1)));
    }

    [Test]
    public void DivisionDTO_DefaultsAreNull()
    {
        var dto = new DivisionDTO();
        Assert.That(dto.DivisionName, Is.Null);
        Assert.That(dto.Requests, Is.Null);
        Assert.That(dto.CreatedBy, Is.Null);
        Assert.That(dto.ModifiedBy, Is.Null);
    }
}
