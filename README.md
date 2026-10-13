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
