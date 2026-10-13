namespace OOSD.Ch01.Domain;

/// <summary>
/// Đại diện cho phiếu phạt tiền khi trả sách trễ hạn (US-04).
/// Chứa khóa ngoại MaPhieuMuon [FK] tham chiếu tới PhieuMuon.
/// </summary>
public class PhieuPhat
{
    public string MaPhieuPhat { get; private set; }
    public string MaPhieuMuon { get; private set; }
    public int SoNgayTre { get; private set; }
    public decimal TienPhatMoiNgay { get; private set; }
    public decimal TongTienPhat { get; private set; }
    public bool DaNopPhat { get; private set; }

    private PhieuPhat(string maPhieuPhat, string maPhieuMuon, int soNgayTre, decimal tienPhatMoiNgay)
    {
        MaPhieuPhat = maPhieuPhat;
        MaPhieuMuon = maPhieuMuon;
        SoNgayTre = soNgayTre;
        TienPhatMoiNgay = tienPhatMoiNgay;
        TongTienPhat = soNgayTre * tienPhatMoiNgay;
        DaNopPhat = false;
    }

    /// <summary>
    /// Factory Method Create - kiểm tra số ngày trễ > 0 và tiền phạt > 0.
    /// </summary>
    public static PhieuPhat Create(string maPhieuPhat, string maPhieuMuon, int soNgayTre, decimal tienPhatMoiNgay)
    {
        if (string.IsNullOrWhiteSpace(maPhieuPhat))
            throw new ArgumentException("Mã phiếu phạt không được để trống.", nameof(maPhieuPhat));

        if (string.IsNullOrWhiteSpace(maPhieuMuon))
            throw new ArgumentException("Mã phiếu mượn không được để trống.", nameof(maPhieuMuon));

        if (soNgayTre <= 0)
            throw new ArgumentException("Số ngày trễ phải lớn hơn 0 để tính phạt.", nameof(soNgayTre));

        if (tienPhatMoiNgay <= 0)
            throw new ArgumentException("Mức phạt mỗi ngày phải lớn hơn 0.", nameof(tienPhatMoiNgay));

        return new PhieuPhat(maPhieuPhat.Trim(), maPhieuMuon.Trim(), soNgayTre, tienPhatMoiNgay);
    }

    /// <summary>
    /// Độc giả nộp phạt.
    /// </summary>
    public void ThanhToanPhat()
    {
        DaNopPhat = true;
    }

    public override string ToString()
    {
        return $"[Phiếu phạt {MaPhieuPhat}] Gắn với phiếu mượn: {MaPhieuMuon} | Trễ: {SoNgayTre} ngày | Phạt: {TongTienPhat:N0} VNĐ (Đơn giá: {TienPhatMoiNgay:N0}/ngày) | Tình trạng: {(DaNopPhat ? "Đã nộp" : "Chưa nộp")}";
    }
}
