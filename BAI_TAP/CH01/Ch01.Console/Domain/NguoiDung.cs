namespace OOSD.Ch01.Domain;

/// <summary>
/// Đại diện cho người dùng (Sinh viên/Độc giả, Thủ thư, Admin).
/// Tuân thủ Encapsulation: constructor private, private set, Create() có validation.
/// </summary>
public class NguoiDung
{
    public string MaNguoiDung { get; private set; }
    public string HoTen { get; private set; }
    public string Email { get; private set; }
    public VaiTro VaiTro { get; private set; }
    public bool DangHoatDong { get; private set; }

    // Constructor PRIVATE đảm bảo khởi tạo đúng qua Factory Method
    private NguoiDung(string maNguoiDung, string hoTen, string email, VaiTro vaiTro)
    {
        MaNguoiDung = maNguoiDung;
        HoTen = hoTen;
        Email = email;
        VaiTro = vaiTro;
        DangHoatDong = true;
    }

    /// <summary>
    /// Factory Method Create - kiểm tra tính hợp lệ của dữ liệu đầu vào.
    /// </summary>
    public static NguoiDung Create(string maNguoiDung, string hoTen, string email, VaiTro vaiTro)
    {
        if (string.IsNullOrWhiteSpace(maNguoiDung))
            throw new ArgumentException("Mã người dùng không được để trống.", nameof(maNguoiDung));

        if (string.IsNullOrWhiteSpace(hoTen))
            throw new ArgumentException("Họ tên không được để trống.", nameof(hoTen));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email không hợp lệ (phải chứa ký tự '@').", nameof(email));

        return new NguoiDung(maNguoiDung.Trim(), hoTen.Trim(), email.Trim(), vaiTro);
    }

    /// <summary>
    /// Khóa tài khoản người dùng (US-05).
    /// </summary>
    public void KhoaTaiKhoan()
    {
        DangHoatDong = false;
    }

    /// <summary>
    /// Mở khóa tài khoản người dùng.
    /// </summary>
    public void MoKhoaTaiKhoan()
    {
        DangHoatDong = true;
    }

    public bool IsActive() => DangHoatDong;

    public override string ToString()
    {
        return $"[{MaNguoiDung}] {HoTen} ({VaiTro}) - Trạng thái: {(DangHoatDong ? "Đang hoạt động" : "Đã khóa")}";
    }
}
