using Moq;
using TechTicketPOC.BLL;
using TechTicketPOC.BLL.Interfaces;
using TechTicketPOC.Entities;

namespace TechTicketPOC.Tests.BLL;

[TestFixture]
public class DivisionBLLTests
{
    private Mock<IDivisionRepository> _mockRepository = null!;
    private DivisionBLL _sut = null!;

    [SetUp]
    public void SetUp()
    {
        _mockRepository = new Mock<IDivisionRepository>();
        _sut = new DivisionBLL(_mockRepository.Object);
    }

    [Test]
    public void Constructor_WithNullRepository_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new DivisionBLL(null!));
    }

    [Test]
    public void GetDivisions_WhenRepositoryReturnsNull_ReturnsNull()
    {
        _mockRepository.Setup(r => r.GetDivisions()).Returns((List<DivisionDTO>?)null!);

        var result = _sut.GetDivisions();

        Assert.That(result, Is.Null);
    }

    [Test]
    public void GetDivisions_WhenRepositoryReturnsDivisions_ReturnsSameDivisions()
    {
        var divisions = new List<DivisionDTO>
        {
            new DivisionDTO { Id = 1, DivisionName = "Core" },
            new DivisionDTO { Id = 2, DivisionName = "Raiser" }
        };
        _mockRepository.Setup(r => r.GetDivisions()).Returns(divisions);

        var result = _sut.GetDivisions();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result![0].DivisionName, Is.EqualTo("Core"));
        Assert.That(result[1].DivisionName, Is.EqualTo("Raiser"));
    }

    [Test]
    public void GetDivisions_WhenRepositoryReturnsEmptyList_ReturnsEmptyList()
    {
        _mockRepository.Setup(r => r.GetDivisions()).Returns(new List<DivisionDTO>());

        var result = _sut.GetDivisions();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetDivisions_CallsRepositoryExactlyOnce()
    {
        _mockRepository.Setup(r => r.GetDivisions()).Returns(new List<DivisionDTO>());

        _sut.GetDivisions();

        _mockRepository.Verify(r => r.GetDivisions(), Times.Once);
    }

    [Test]
    public void GetDivisions_WhenRepositoryReturnsSingleDivision_ReturnsSingleDivision()
    {
        var divisions = new List<DivisionDTO> { new DivisionDTO { Id = 1, DivisionName = "Core" } };
        _mockRepository.Setup(r => r.GetDivisions()).Returns(divisions);

        var result = _sut.GetDivisions();

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result![0].Id, Is.EqualTo(1));
    }
}
