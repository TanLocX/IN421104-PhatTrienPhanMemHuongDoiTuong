# Môn học: Thiết kế và Phát triển Phần mềm Hướng đối tượng (OOSD)

Repository lưu trữ các bài tập thực hành môn học **Object-Oriented Software Design & Development (OOSD / IN4211)**.

---

## 1. Thông tin sinh viên

- **Họ và tên:** Nguyễn Tấn Lộc
- **Mã số sinh viên (MSSV):** 0024417501
- **GitHub:** [TanLocX](https://github.com/TanLocX)


---

## 2. Cấu trúc dự án

```text
OOSD/
├── .gitignore                   # Cấu hình bỏ qua file build và tài liệu môn học
├── README.md                    # Giới thiệu bản thân và tổng quan dự án
└── BAI_TAP/
    └── CH01/                    # Bài tập Chương 01: Phân tích yêu cầu & Domain Modeling
        ├── Ch01.Console.sln     # Solution file (.NET 10)
        ├── docs/                # Tài liệu phân tích và thiết kế
        │   ├── user-stories.md  # 5 User Stories (3 vai trò, chuẩn Given/When/Then)
        │   └── so_do_lop.png    # Sơ đồ lớp (Class Diagram 5 thực thể)
        └── Ch01.Console/        # Ứng dụng Console minh họa
            ├── Ch01.Console.csproj
            ├── Program.cs       # Luồng demo nghiệp vụ và in báo cáo
            └── Domain/          # 5 Domain Classes (đóng gói, constructor private, Create())
                ├── Enums.cs
                ├── NguoiDung.cs
                ├── Sach.cs
                ├── PhieuMuon.cs
                ├── ChiTietPhieuMuon.cs
                └── PhieuPhat.cs
```

---

## 3. Nội dung bài tập Chương 01 (Quản lý Thư viện)

- **Đề tài:** Hệ thống Quản lý Mượn Trả Sách (Library Management).
- **Yêu cầu 1:** Viết 5 User Stories đủ 3 vai trò (Sinh viên, Thủ thư, Admin) kèm tiêu chí nghiệm thu dạng Given/When/Then.
- **Yêu cầu 2:** Thiết kế sơ đồ lớp gồm 5 Class có khóa ngoại (`[FK]`) và mối quan hệ giữa các thực thể.
- **Yêu cầu 3:** Cài đặt 5 Domain Class tuân thủ nguyên lý Đóng gói (Encapsulation): thuộc tính `private set`, constructor `private`, Factory Method `Create()` có kiểm tra hợp lệ dữ liệu.
- **Yêu cầu 4:** File `Program.cs` chạy mô phỏng đầy đủ các luồng nghiệp vụ và in bảng báo cáo tổng kết ra console.

---

## 4. Hướng dẫn chạy dự án

Yêu cầu máy cài đặt [.NET SDK 10](https://dotnet.microsoft.com/).

```bash
# Di chuyển vào thư mục dự án
cd BAI_TAP/CH01/Ch01.Console

# Chạy ứng dụng console
dotnet run
```

*(Hoặc chạy trực tiếp từ thư mục gốc: `dotnet run --project BAI_TAP/CH01/Ch01.Console`)*
