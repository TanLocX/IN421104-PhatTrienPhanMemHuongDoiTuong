using EduHub.Ch02;
using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests;

// Mẫu AAA: Arrange (chuẩn bị) - Act (thực hiện) - Assert (kiểm tra).
// Mỗi test chỉ kiểm tra MỘT hành vi. Đặt tên: TênMethod_TìnhHuống_KếtQuảMongĐợi.
public class DomainTests
{
    // Test mẫu đã viết sẵn: chạy được ngay trên ClubManager hiện tại.
    [Fact]
    public void CreateClub_WithEmptyName_ThrowsArgumentException()
    {
        // Arrange
        var manager = new ClubManager();

        // Act
        var act = () => manager.CreateClub("", "mô tả", "p@gmail.com");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    // TODO (Nhiệm vụ 4): bỏ Skip và viết thân test cho từng trường hợp dưới đây.
    // Cần ≥ 6 test AAA cho lớp Domain (Club, Event), trong đó có cả trường hợp lỗi.

    //[Fact(Skip = "TODO: viết test Club.Create với tên hợp lệ")]
    [Fact]
    public void Club_Create_WithValidName_ReturnsClub()
    {
        // Arrange
        const string name = "CLB ABCD";

        // Act
        var club = Club.Create(name,"CLB jj đó");

        // Assert
        Assert.Equal(name, club.Name);
        Assert.NotEqual(Guid.Empty,club.Id);
    }

    //[Fact(Skip = "TODO: viết test Club.Create với tên rỗng hoặc dài hơn 100 ký tự")]
    [Fact]
    public void Club_Create_WithInvalidName_ThrowsArgumentException()
    {
        // Arrange
        // var longName = "";
        var longName = new string('a', Club.MaxNameLength + 1);

        // Act

        // Assert
        Assert.Throws<ArgumentException>(() => Club.Create("", "mô tả"));
        Assert.Throws<ArgumentException>(() => Club.Create("   ", "mô tả"));
        Assert.Throws<ArgumentException>(() => Club.Create(longName, "mô tả"));
    }

    //[Fact(Skip = "TODO: viết test Club.HasRoom ở biên 49 và 50 thành viên")]
    [Fact]
    public void Club_HasRoom_AtMaxMembers_ReturnsFalse()
    {
        // Arrange
        var club = Club.Create("CLB ABCD", "mô tả");
        // Act

        // Assert
        Assert.True(club.HasRoom(49));
        Assert.False(club.HasRoom(50));
    }

    //[Fact(Skip = "TODO: viết test Event.Register thành viên mới")]
    [Fact]
    public void Event_Register_NewMember_ReturnsTrue()
    {
        // Arrange
        var ev = new Event { 
            Id = Guid.NewGuid(), 
            ClubId = Guid.NewGuid(), 
            Title = "hihi haha", 
            Date = DateTime.Now 
        };
        var memberId = Guid.NewGuid();
        // Act
        var result = ev.Register(memberId);
        // Assert
        Assert.True(result);
        Assert.Contains(memberId, ev.RegisteredMemberIds);
    }

    //[Fact(Skip = "TODO: viết test Event.Register cùng một thành viên hai lần")]
    [Fact]
    public void Event_Register_SameMemberTwice_ReturnsFalse()
    {
        // Arrange
        var ev = new Event { 
            Id = Guid.NewGuid(), 
            ClubId = Guid.NewGuid(), 
            Title = "hihi haha", 
            Date = DateTime.Now 
        };
        var memberId = Guid.NewGuid();
        ev.Register(memberId);

        // Act
        var resultAgain = ev.Register(memberId); 

        // Assert
        Assert.False(resultAgain);
        Assert.Single(ev.RegisteredMemberIds);
    }

    //[Fact(Skip = "TODO: viết test Event.CheckIn khi chưa đăng ký")]
    [Fact]
    public void Event_CheckIn_UnregisteredMember_ThrowsInvalidOperationException()
    {
        // Arrange
        var ev = new Event { 
            Id = Guid.NewGuid(), 
            ClubId = Guid.NewGuid(), 
            Title = "hihi haha", 
            Date = DateTime.Now 
        };
        // Act
        var act = () => ev.CheckIn(Guid.NewGuid());
        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }
}
