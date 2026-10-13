namespace OOSD.Ch01.Domain;

/// <summary>
/// Đại diện cho giao dịch mượn sách của độc giả.
/// Chứa khóa ngoại MaDocGia [FK] tham chiếu tới NguoiDung.
/// </summary>
public class PhieuMuon
{
    public string MaPhieuMuon { get; private set; }
    public string MaDocGia { get; private set; }
    public DateTime NgayMuon { get; private set; }
    public DateTime HanTra { get; private set; }
    public DateTime? NgayTraThucTe { get; private set; }
    public TrangThaiPhieu TrangThai { get; private set; }

    private readonly List<ChiTietPhieuMuon> _danhSachChiTiet = new();
    public IReadOnlyList<ChiTietPhieuMuon> DanhSachChiTiet => _danhSachChiTiet.AsReadOnly();

    private PhieuMuon(string maPhieuMuon, string maDocGia, DateTime ngayMuon, DateTime hanTra)
    {
        MaPhieuMuon = maPhieuMuon;
        MaDocGia = maDocGia;
        NgayMuon = ngayMuon;
        HanTra = hanTra;
        TrangThai = TrangThaiPhieu.DangMuon;
        NgayTraThucTe = null;
    }

    /// <summary>
    /// Factory Method Create - kiểm tra dữ liệu và tự động tính HanTra.
    /// </summary>
    public static PhieuMuon Create(string maPhieuMuon, string maDocGia, int soNgayMuon, DateTime? ngayMuon = null)
    {
        if (string.IsNullOrWhiteSpace(maPhieuMuon))
            throw new ArgumentException("Mã phiếu mượn không được để trống.", nameof(maPhieuMuon));

        if (string.IsNullOrWhiteSpace(maDocGia))
            throw new ArgumentException("Mã độc giả không được để trống.", nameof(maDocGia));

        if (soNgayMuon <= 0)
            throw new ArgumentException("Số ngày mượn phải lớn hơn 0.", nameof(soNgayMuon));

        var ngayBatDau = ngayMuon ?? DateTime.Now;
        var ngayHan = ngayBatDau.AddDays(soNgayMuon);

        return new PhieuMuon(maPhieuMuon.Trim(), maDocGia.Trim(), ngayBatDau, ngayHan);
    }

    /// <summary>
    /// Thêm chi tiết sách mượn vào phiếu.
    /// </summary>
    public void ThemChiTiet(ChiTietPhieuMuon chiTiet)
    {
        if (chiTiet == null)
            throw new ArgumentNullException(nameof(chiTiet));

        if (chiTiet.MaPhieuMuon != MaPhieuMuon)
            throw new InvalidOperationException("Mã phiếu mượn trong chi tiết không khớp với phiếu hiện tại.");

        _danhSachChiTiet.Add(chiTiet);
    }

    /// <summary>
    /// Xác nhận trả sách (US-04). Chuyển trạng thái sang DaTra hoặc QuaHan.
    /// </summary>
    public void XacNhanTra(DateTime ngayTraThucTe)
    {
        if (TrangThai == TrangThaiPhieu.DaTra)
            throw new InvalidOperationException("Phiếu này đã được trả trước đó.");

        NgayTraThucTe = ngayTraThucTe;

        if (ngayTraThucTe.Date > HanTra.Date)
        {
            TrangThai = TrangThaiPhieu.QuaHan;
        }
        else
        {
            TrangThai = TrangThaiPhieu.DaTra;
        }
    }

    /// <summary>
    /// Tính số ngày trễ hạn so với HanTra.
    /// </summary>
    public int TinhSoNgayTreHan(DateTime ngayTraThucTe)
    {
        if (ngayTraThucTe.Date <= HanTra.Date)
            return 0;

        return (int)(ngayTraThucTe.Date - HanTra.Date).TotalDays;
    }

    public override string ToString()
    {
        var traStr = NgayTraThucTe.HasValue ? NgayTraThucTe.Value.ToString("dd/MM/yyyy") : "Chưa trả";
        return $"[Phiếu {MaPhieuMuon}] Độc giả: {MaDocGia} | Mượn: {NgayMuon:dd/MM/yyyy} | Hạn: {HanTra:dd/MM/yyyy} | Ngày trả: {traStr} | Trạng thái: {TrangThai}";
    }
}
