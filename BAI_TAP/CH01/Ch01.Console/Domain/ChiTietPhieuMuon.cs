namespace OOSD.Ch01.Domain;

/// <summary>
/// Chi tiết cuốn sách được mượn trong phiếu mượn (bảng nối M:N giữa PhieuMuon và Sach).
/// Chứa khóa ngoại MaPhieuMuon [FK] và MaSach [FK].
/// </summary>
public class ChiTietPhieuMuon
{
    public string MaChiTiet { get; private set; }
    public string MaPhieuMuon { get; private set; }
    public string MaSach { get; private set; }
    public int SoLuong { get; private set; }

    private ChiTietPhieuMuon(string maChiTiet, string maPhieuMuon, string maSach, int soLuong)
    {
        MaChiTiet = maChiTiet;
        MaPhieuMuon = maPhieuMuon;
        MaSach = maSach;
        SoLuong = soLuong;
    }

    /// <summary>
    /// Factory Method Create - kiểm tra tính hợp lệ dữ liệu.
    /// </summary>
    public static ChiTietPhieuMuon Create(string maChiTiet, string maPhieuMuon, string maSach, int soLuong)
    {
        if (string.IsNullOrWhiteSpace(maChiTiet))
            throw new ArgumentException("Mã chi tiết không được để trống.", nameof(maChiTiet));

        if (string.IsNullOrWhiteSpace(maPhieuMuon))
            throw new ArgumentException("Mã phiếu mượn không được để trống.", nameof(maPhieuMuon));

        if (string.IsNullOrWhiteSpace(maSach))
            throw new ArgumentException("Mã sách không được để trống.", nameof(maSach));

        if (soLuong <= 0)
            throw new ArgumentException("Số lượng mượn phải lớn hơn 0.", nameof(soLuong));

        return new ChiTietPhieuMuon(maChiTiet.Trim(), maPhieuMuon.Trim(), maSach.Trim(), soLuong);
    }

    public override string ToString()
    {
        return $"  - Chi tiết [{MaChiTiet}]: Sách {MaSach}, Số lượng: {SoLuong}";
    }
}
