using System.Text;
using OOSD.Ch01.Domain;

Console.OutputEncoding = Encoding.UTF8;

const string LineSeparator = "==========================================================================================";
const string SectionSeparator = "------------------------------------------------------------------------------------------";

Console.WriteLine(LineSeparator);
Console.WriteLine("          HE THONG QUAN LY THU VIEN (LIBRARY MANAGEMENT) - CONSOLE DEMO           ");
Console.WriteLine("          MSSV: 0024417501 | Bai tap Chuong 01: OOSD Domain Modeling              ");
Console.WriteLine(LineSeparator);
Console.WriteLine();

// ============================================================================
// DU LIEU BAN DAU
// ============================================================================
var danhSachNguoiDung = new List<NguoiDung>
{
    NguoiDung.Create("SV001", "Nguyen Van An", "an.nguyen@eduhub.edu.vn", VaiTro.SinhVien),
    NguoiDung.Create("SV002", "Tran Thi Binh", "binh.tran@eduhub.edu.vn", VaiTro.SinhVien),
    NguoiDung.Create("TT001", "Le Van Cuong (Thu thu)", "cuong.le@eduhub.edu.vn", VaiTro.ThuThu),
    NguoiDung.Create("AD001", "Pham Hoang Dung (Admin)", "admin@eduhub.edu.vn", VaiTro.Admin)
};

var khoSach = new List<Sach>
{
    Sach.Create("BK001", "Nhap mon Lap trinh Huong doi tuong", "Tran Dan Thu", 3),
    Sach.Create("BK002", "Cau truc Du lieu va Giai thuat", "Dinh Manh Tuong", 1),
    Sach.Create("BK003", "Thiet ke Huong doi tuong voi UML", "Martin Fowler", 2)
};

var danhSachPhieuMuon = new List<PhieuMuon>();
var danhSachPhieuPhat = new List<PhieuPhat>();

// ============================================================================
// US-01: Sinh vien: Tim kiem sach
// ============================================================================
Console.WriteLine("[US-01] Sinh vien: Tim kiem sach trong thu vien");
Console.WriteLine(SectionSeparator);

// Tieu chi 1: Tim kiem sach co trong kho
string tuKhoa1 = "Huong doi tuong";
Console.WriteLine($"[1.1] Tim kiem voi tu khoa: \"{tuKhoa1}\"");
var ketQua1 = khoSach.Where(s => s.TieuDe.Contains(tuKhoa1, StringComparison.OrdinalIgnoreCase)).ToList();
if (ketQua1.Any())
{
    Console.WriteLine("    => [Thanh cong] Tim thay cac sach phu hop:");
    foreach (var s in ketQua1)
    {
        Console.WriteLine($"       - {s}");
    }
}
else
{
    Console.WriteLine("    => [Khong tim thay] Khong co sach phu hop.");
}

// Tieu chi 2: Tim kiem sach khong co trong thu vien
string tuKhoa2 = "Tri tue nhan tao nang cao";
Console.WriteLine($"\n[1.2] Tim kiem voi tu khoa: \"{tuKhoa2}\"");
var ketQua2 = khoSach.Where(s => s.TieuDe.Contains(tuKhoa2, StringComparison.OrdinalIgnoreCase)).ToList();
if (ketQua2.Any())
{
    Console.WriteLine("    => [Thanh cong] Tim thay cac sach phu hop.");
}
else
{
    Console.WriteLine("    => [Khong tim thay] Bao ket qua: \"Khong tim thay sach theo yeu cau trong thu vien\".");
}
Console.WriteLine();

// ============================================================================
// US-02: Sinh vien: Dat muon sach
// ============================================================================
Console.WriteLine("[US-02] Sinh vien: Dat muon sach");
Console.WriteLine(SectionSeparator);

var svAn = danhSachNguoiDung.First(u => u.MaNguoiDung == "SV001");
var sachBK002 = khoSach.First(s => s.MaSach == "BK002");

// Tieu chi 1: Sach con -> muon thanh cong
Console.WriteLine($"[2.1] Sinh vien {svAn.HoTen} muon cuon: \"{sachBK002.TieuDe}\" (So luong kha dung: {sachBK002.SoLuongKhaDung})");
if (svAn.DangHoatDong && sachBK002.GiamSoLuongKhiMuon())
{
    var phieu1 = PhieuMuon.Create("PM001", svAn.MaNguoiDung, soNgayMuon: 7, DateTime.Now.AddDays(-10)); // Gia lap muon cach day 10 ngay
    var ct1 = ChiTietPhieuMuon.Create("CT001", phieu1.MaPhieuMuon, sachBK002.MaSach, 1);
    phieu1.ThemChiTiet(ct1);
    danhSachPhieuMuon.Add(phieu1);

    Console.WriteLine("    => [Thanh cong] Da tao phieu muon:");
    Console.WriteLine($"       - {phieu1}");
    Console.WriteLine($"       - Ton kho con lai cua \"{sachBK002.TieuDe}\": {sachBK002.SoLuongKhaDung}/{sachBK002.TongSoLuong}");
}

// Tieu chi 2: Sach da het -> bam muon -> bao het sach
var svBinh = danhSachNguoiDung.First(u => u.MaNguoiDung == "SV002");
Console.WriteLine($"\n[2.2] Sinh vien {svBinh.HoTen} tiep tuc muon cuon: \"{sachBK002.TieuDe}\" (So luong kha dung: {sachBK002.SoLuongKhaDung})");
if (sachBK002.GiamSoLuongKhiMuon())
{
    Console.WriteLine("    => [Thanh cong] Dat muon thanh cong.");
}
else
{
    Console.WriteLine("    => [Tu choi] Bao loi: \"Sach da het so luong kha dung, khong the dat muon luc nay!\".");
}
Console.WriteLine();

// ============================================================================
// US-03: Thu thu: Them sach moi
// ============================================================================
Console.WriteLine("[US-03] Thu thu: Them sach moi vao kho");
Console.WriteLine(SectionSeparator);

// Tieu chi 1: Ten hop le va so luong > 0 -> Luu thanh cong
Console.WriteLine("[3.1] Thu thu them sach moi hop le:");
try
{
    var sachMoi = Sach.Create("BK004", "Clean Architecture", "Robert C. Martin", 5);
    khoSach.Add(sachMoi);
    Console.WriteLine($"    => [Thanh cong] Da them sach vao kho: {sachMoi}");
}
catch (Exception ex)
{
    Console.WriteLine($"    => [Loi] {ex.Message}");
}

// Tieu chi 2: Ten trong hoac so luong <= 0 -> Bao loi du lieu khong hop le
Console.WriteLine("\n[3.2] Thu thu them sach voi so luong <= 0:");
try
{
    var sachLoi = Sach.Create("BK005", "Sach Loi", "Tac gia", 0);
    khoSach.Add(sachLoi);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"    => [Bat loi hop le] He thong ngan chan thanh cong: \"{ex.Message}\"");
}
Console.WriteLine();

// ============================================================================
// US-04: Thu thu: Xac nhan tra sach va tinh tien phat
// ============================================================================
Console.WriteLine("[US-04] Thu thu: Xac nhan tra sach");
Console.WriteLine(SectionSeparator);

// Gia lap phieu 2: Sinh vien Binh muon BK001 va tra dung han
var sachBK001 = khoSach.First(s => s.MaSach == "BK001");
sachBK001.GiamSoLuongKhiMuon();
var phieu2 = PhieuMuon.Create("PM002", svBinh.MaNguoiDung, soNgayMuon: 7, DateTime.Now.AddDays(-5));
phieu2.ThemChiTiet(ChiTietPhieuMuon.Create("CT002", phieu2.MaPhieuMuon, sachBK001.MaSach, 1));
danhSachPhieuMuon.Add(phieu2);

// Tieu chi 1: Tra dung han -> Phieu "Da tra", ton kho tang 1
Console.WriteLine($"[4.1] Sinh vien {svBinh.HoTen} tra sach \"{sachBK001.TieuDe}\" dung han (Han: {phieu2.HanTra:dd/MM/yyyy}, Ngay tra: {DateTime.Now:dd/MM/yyyy}):");
phieu2.XacNhanTra(DateTime.Now);
sachBK001.TangSoLuongKhiTra();
Console.WriteLine($"    => [Thanh cong] {phieu2}");
Console.WriteLine($"    => Ton kho sau khi hoan tra: {sachBK001.SoLuongKhaDung}/{sachBK001.TongSoLuong}");

// Tieu chi 2: Tra tre han -> Tu dong tinh tien phat
var phieu1TreHan = danhSachPhieuMuon.First(p => p.MaPhieuMuon == "PM001");
DateTime ngayTraTre = DateTime.Now; // Han la 7 ngay ke tu -10 ngay -> tre 3 ngay
Console.WriteLine($"\n[4.2] Sinh vien {svAn.HoTen} tra sach tre han (Han: {phieu1TreHan.HanTra:dd/MM/yyyy}, Ngay tra: {ngayTraTre:dd/MM/yyyy}):");
phieu1TreHan.XacNhanTra(ngayTraTre);
sachBK002.TangSoLuongKhiTra();
int soNgayTre = phieu1TreHan.TinhSoNgayTreHan(ngayTraTre);

Console.WriteLine($"    => Trang thai phieu: {phieu1TreHan.TrangThai} (Tre {soNgayTre} ngay)");
if (soNgayTre > 0)
{
    decimal donGiaPhat = 5000m; // 5.000 VND / ngay
    var phieuPhat = PhieuPhat.Create("PP001", phieu1TreHan.MaPhieuMuon, soNgayTre, donGiaPhat);
    danhSachPhieuPhat.Add(phieuPhat);
    Console.WriteLine($"    => [Tu dong tinh phat] {phieuPhat}");
}
Console.WriteLine();

// ============================================================================
// US-05: Admin: Khoa tai khoan vi pham
// ============================================================================
Console.WriteLine("[US-05] Admin: Khoa tai khoan vi pham");
Console.WriteLine(SectionSeparator);

// Tieu chi 1: Tai khoan dang hoat dong -> chuyen sang Da khoa
Console.WriteLine($"[5.1] Admin thuc hien khoa tai khoan vi pham cua {svAn.HoTen} ({svAn.MaNguoiDung}):");
svAn.KhoaTaiKhoan();
Console.WriteLine($"    => [Thanh cong] Trang thai tai khoan hien tai: {svAn}");

// Tieu chi 2: Lich su muon tra truoc do van duoc luu tru nguyen ven
Console.WriteLine($"\n[5.2] Kiem tra lich su muon tra cua tai khoan da khoa ({svAn.MaNguoiDung}):");
var lichSuCuaAn = danhSachPhieuMuon.Where(p => p.MaDocGia == svAn.MaNguoiDung).ToList();
Console.WriteLine($"    => Tim thay {lichSuCuaAn.Count} phieu muon trong lich su (du lieu van bao toan nguyen ven):");
foreach (var ls in lichSuCuaAn)
{
    Console.WriteLine($"       - {ls}");
    foreach (var ct in ls.DanhSachChiTiet)
    {
        Console.WriteLine($"         {ct}");
    }
}
Console.WriteLine();

// ============================================================================
// BAO CAO TONG KET HE THONG
// ============================================================================
Console.WriteLine(LineSeparator);
Console.WriteLine("                    BAO CAO TONG KET HOAT DONG THU VIEN                   ");
Console.WriteLine(LineSeparator);

Console.WriteLine("\n[1. TONG KHO SACH HIEN TAI]");
foreach (var s in khoSach)
{
    Console.WriteLine($" - {s}");
}

Console.WriteLine("\n[2. DANH SACH TAT CA PHIEU MUON]");
foreach (var p in danhSachPhieuMuon)
{
    Console.WriteLine($" - {p}");
}

Console.WriteLine("\n[3. DANH SACH PHIEU PHAT]");
if (danhSachPhieuPhat.Any())
{
    foreach (var f in danhSachPhieuPhat)
    {
        Console.WriteLine($" - {f}");
    }
}
else
{
    Console.WriteLine(" - Khong co phieu phat nao.");
}

Console.WriteLine("\n[4. TRANG THAI TAI KHOAN NGUOI DUNG]");
foreach (var u in danhSachNguoiDung)
{
    Console.WriteLine($" - {u}");
}

Console.WriteLine(LineSeparator);
Console.WriteLine("=> HOAN THANH KIEM THU TAT CA CAC LUONG NGHIEP VU!");
Console.WriteLine(LineSeparator);
