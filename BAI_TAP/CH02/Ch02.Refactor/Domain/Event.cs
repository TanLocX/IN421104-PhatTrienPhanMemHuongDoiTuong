namespace EduHub.Ch02.Domain;

/// <summary>Sự kiện của CLB, kèm danh sách đăng ký và check-in.</summary>
public class Event
{
    public Guid Id { get; set; }
    public Guid ClubId { get; set; }
    public required string Title { get; set; }
    public DateTime Date { get; set; }

    public HashSet<Guid> RegisteredMemberIds { get; } = new();
    //HashSet là tập hợp các phần tử, không được có phần tử trùng lặp
    public HashSet<Guid> CheckedInMemberIds { get; } = new();

    // TODO (Nhiệm vụ 4): đăng ký tham dự; trả về false nếu thành viên đã đăng ký trước đó.
    public bool Register(Guid memberId)
    {
        return RegisteredMemberIds.Add(memberId);
        //.Add cơ chế kiểm tra trùng lặp 
    }

    // TODO (Nhiệm vụ 4): chỉ thành viên đã đăng ký mới được check-in,
    // ngược lại ném InvalidOperationException.
    public void CheckIn(Guid memberId)
    {
        if(!RegisteredMemberIds.Contains(memberId)) // kiểm tra xem thành viên đã đăng ký chưa
        {
            throw new InvalidOperationException("Thành viên chưa đăng ký");
        }
        CheckedInMemberIds.Add(memberId);
    }

    public double AttendanceRate
    {
        get{
            if(RegisteredMemberIds.Count == 0)
            {
                return 0;
            }
            else{
                return (double)CheckedInMemberIds.Count / RegisteredMemberIds.Count;
            }
        }
    }
}
