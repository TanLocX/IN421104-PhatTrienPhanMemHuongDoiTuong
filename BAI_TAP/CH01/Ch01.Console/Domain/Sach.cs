namespace OOSD.Ch01.Domain;

/// <summary>
/// Đại diện cho sách trong thư viện.
/// Quản lý số lượng tổng và số lượng khả dụng khi mượn/trả.
/// </summary>
public class Sach
{
    public string MaSach { get; private set; }
    public string TieuDe { get; private set; }
    public string TacGia { get; private set; }
    public int TongSoLuong { get; private set; }
    public int SoLuongKhaDung { get; private set; }

    private Sach(string maSach, string tieuDe, string tacGia, int tongSoLuong)
    {
        MaSach = maSach;
        TieuDe = tieuDe;
        TacGia = tacGia;
        TongSoLuong = tongSoLuong;
        SoLuongKhaDung = tongSoLuong;
    }

    /// <summary>
    /// Factory Method Create - kiểm tra tính hợp lệ dữ liệu (US-03).
    /// </summary>
    public static Sach Create(string maSach, string tieuDe, string tacGia, int tongSoLuong)
    {
        if (string.IsNullOrWhiteSpace(maSach))
            throw new ArgumentException("Mã sách không được để trống.", nameof(maSach));

        if (string.IsNullOrWhiteSpace(tieuDe))
            throw new ArgumentException("Tên sách không được để trống.", nameof(tieuDe));

        if (string.IsNullOrWhiteSpace(tacGia))
            throw new ArgumentException("Tên tác giả không được để trống.", nameof(tacGia));

        if (tongSoLuong <= 0)
            throw new ArgumentException("Số lượng sách phải lớn hơn 0.", nameof(tongSoLuong));

        return new Sach(maSach.Trim(), tieuDe.Trim(), tacGia.Trim(), tongSoLuong);
    }

    /// <summary>
    /// Giảm số lượng sách khả dụng khi có người mượn (US-02).
    /// Trả về true nếu mượn thành công, false nếu đã hết sách.
    /// </summary>
    public bool GiamSoLuongKhiMuon()
    {
        if (SoLuongKhaDung <= 0)
            return false;

        SoLuongKhaDung--;
        return true;
    }

    /// <summary>
    /// Tăng lại số lượng sách khả dụng khi độc giả trả sách (US-04).
    /// </summary>
    public void TangSoLuongKhiTra()
    {
        if (SoLuongKhaDung < TongSoLuong)
        {
            SoLuongKhaDung++;
        }
    }

    public bool ConSach() => SoLuongKhaDung > 0;

    public override string ToString()
    {
        return $"[{MaSach}] \"{TieuDe}\" - Tác giả: {TacGia} | Tồn kho khả dụng: {SoLuongKhaDung}/{TongSoLuong}";
    }
}
