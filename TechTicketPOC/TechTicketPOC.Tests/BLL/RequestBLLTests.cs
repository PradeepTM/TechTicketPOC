using Moq;
using TechTicketPOC.BLL;
using TechTicketPOC.BLL.Interfaces;
using TechTicketPOC.Entities;

namespace TechTicketPOC.Tests.BLL;

[TestFixture]
public class RequestBLLTests
{
    private Mock<IRequestRepository> _mockRepository = null!;
    private RequestBLL _sut = null!;

    [SetUp]
    public void SetUp()
    {
        _mockRepository = new Mock<IRequestRepository>();
        _sut = new RequestBLL(_mockRepository.Object);
    }

    [Test]
    public void Constructor_WithNullRepository_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new RequestBLL(null!));
    }

    [Test]
    public void GetRequests_WhenRepositoryReturnsNull_ReturnsNull()
    {
        _mockRepository.Setup(r => r.GetRequests(It.IsAny<int>())).Returns((List<RequestDTO>?)null!);

        var result = _sut.GetRequests(1);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetRequests_WhenRepositoryReturnsRequests_ReturnsSameRequests()
    {
        var requests = new List<RequestDTO>
        {
            new RequestDTO { Id = "requests/1", RequestName = "Reset", DivisionId = "divisions/1" },
            new RequestDTO { Id = "requests/2", RequestName = "Spit", DivisionId = "divisions/1" }
        };
        _mockRepository.Setup(r => r.GetRequests(1)).Returns(requests);

        var result = _sut.GetRequests(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result![0].RequestName, Is.EqualTo("Reset"));
        Assert.That(result[1].RequestName, Is.EqualTo("Spit"));
    }

    [Test]
    public void GetRequests_WhenRepositoryReturnsEmptyList_ReturnsEmptyList()
    {
        _mockRepository.Setup(r => r.GetRequests(It.IsAny<int>())).Returns(new List<RequestDTO>());

        var result = _sut.GetRequests(99);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetRequests_PassesDivisionIdToRepository()
    {
        const int divisionId = 7;
        _mockRepository.Setup(r => r.GetRequests(divisionId)).Returns(new List<RequestDTO>());

        _sut.GetRequests(divisionId);

        _mockRepository.Verify(r => r.GetRequests(divisionId), Times.Once);
    }

    [Test]
    public void GetRequests_WithDifferentDivisionIds_CallsRepositoryWithCorrectId()
    {
        var requestsDiv1 = new List<RequestDTO> { new RequestDTO { RequestName = "Reset" } };
        var requestsDiv2 = new List<RequestDTO> { new RequestDTO { RequestName = "Collision" } };
        _mockRepository.Setup(r => r.GetRequests(1)).Returns(requestsDiv1);
        _mockRepository.Setup(r => r.GetRequests(2)).Returns(requestsDiv2);

        var result1 = _sut.GetRequests(1);
        var result2 = _sut.GetRequests(2);

        Assert.That(result1![0].RequestName, Is.EqualTo("Reset"));
        Assert.That(result2![0].RequestName, Is.EqualTo("Collision"));
    }

    [Test]
    public void GetRequests_WhenCalledMultipleTimes_CallsRepositoryEachTime()
    {
        _mockRepository.Setup(r => r.GetRequests(It.IsAny<int>())).Returns(new List<RequestDTO>());

        _sut.GetRequests(1);
        _sut.GetRequests(1);
        _sut.GetRequests(1);

        _mockRepository.Verify(r => r.GetRequests(1), Times.Exactly(3));
    }
}
