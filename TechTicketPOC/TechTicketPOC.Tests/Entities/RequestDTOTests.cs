using TechTicketPOC.Entities;

namespace TechTicketPOC.Tests.Entities;

[TestFixture]
public class RequestDTOTests
{
    [Test]
    public void RequestDTO_CanSetAndGetId()
    {
        var dto = new RequestDTO { Id = "requests/1" };
        Assert.That(dto.Id, Is.EqualTo("requests/1"));
    }

    [Test]
    public void RequestDTO_CanSetAndGetRequestName()
    {
        var dto = new RequestDTO { RequestName = "Reset" };
        Assert.That(dto.RequestName, Is.EqualTo("Reset"));
    }

    [Test]
    public void RequestDTO_CanSetAndGetDivisionId()
    {
        var dto = new RequestDTO { DivisionId = "divisions/1" };
        Assert.That(dto.DivisionId, Is.EqualTo("divisions/1"));
    }

    [Test]
    public void RequestDTO_CanSetAndGetDivision()
    {
        var division = new DivisionDTO { Id = 1, DivisionName = "Core" };
        var dto = new RequestDTO { Division = division };
        Assert.That(dto.Division, Is.EqualTo(division));
    }

    [Test]
    public void RequestDTO_CanSetAndGetParentRequest()
    {
        var parent = new RequestDTO { RequestName = "Parent" };
        var child = new RequestDTO { RequestName = "Child", ParentRequest = parent };
        Assert.That(child.ParentRequest, Is.EqualTo(parent));
    }

    [Test]
    public void RequestDTO_CanSetAndGetParentRequestId()
    {
        var dto = new RequestDTO { ParentRequestId = "requests/5" };
        Assert.That(dto.ParentRequestId, Is.EqualTo("requests/5"));
    }

    [Test]
    public void RequestDTO_CanSetAndGetChildRequests()
    {
        var children = new List<RequestDTO>
        {
            new RequestDTO { RequestName = "Child1" },
            new RequestDTO { RequestName = "Child2" }
        };
        var dto = new RequestDTO { ChildRequests = children };
        Assert.That(dto.ChildRequests, Has.Count.EqualTo(2));
    }

    [Test]
    public void RequestDTO_ImplementsIAuditable()
    {
        var dto = new RequestDTO
        {
            CreatedBy = "admin",
            CreatedDate = new DateTime(2023, 1, 1),
            ModifiedBy = "admin2",
            ModifiedDate = new DateTime(2023, 6, 1)
        };
        Assert.That(dto.CreatedBy, Is.EqualTo("admin"));
        Assert.That(dto.CreatedDate, Is.EqualTo(new DateTime(2023, 1, 1)));
    }

    [Test]
    public void RequestDTO_DefaultsAreNull()
    {
        var dto = new RequestDTO();
        Assert.That(dto.Id, Is.Null);
        Assert.That(dto.RequestName, Is.Null);
        Assert.That(dto.DivisionId, Is.Null);
        Assert.That(dto.ChildRequests, Is.Null);
    }
}
