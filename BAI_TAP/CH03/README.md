# Ch03 — Clean Architecture + REST API: EduHub

Bài tập Chương 03: Clean Architecture 4 lớp + REST API (Controller-based) cho Club, Swagger UI, unit test lớp Application bằng Moq trên nền tảng .NET 10.

---

## 1. Cấu trúc dự án

```text
EduHub.Domain/          Entity (Club, Member) + Interface Repository
EduHub.Application/     CQRS Handlers (GetClubs, GetClubById, CreateClub, DeleteClub)
EduHub.Infrastructure/  InMemoryClubRepository (lưu tạm trong bộ nhớ, thread-safe)
EduHub.API/             ClubsController (RESTful API), Swagger UI, Program.cs (DI container)
EduHub.Tests/           Unit test lớp Application với xUnit và Moq (5 tests passed)
```

---

## 2. Các yêu cầu đã hoàn thành (TODOs)

- **TODO 1a–1e (`InMemoryClubRepository.cs`)**: Cài đặt 5 phương thức lưu trữ bộ nhớ an toàn luồng với `lock`: `GetAllAsync`, `GetByIdAsync`, `AddAsync`, `UpdateAsync`, `DeleteAsync`.
- **TODO 2 (`GetClubsQuery.cs`)**: Handler lấy toàn bộ danh sách câu lạc bộ.
- **TODO 3 (`GetClubByIdQuery.cs`)**: Handler lấy thông tin chi tiết một câu lạc bộ theo Id.
- **TODO 4a–4d (`CreateClubCommand.cs`)**: Handler kiểm tra tên hợp lệ (không rỗng, không quá 50 ký tự), tạo mới và lưu trữ câu lạc bộ.
- **TODO 5 (`DeleteClubCommand.cs`)**: Handler xoá câu lạc bộ theo Id.
- **TODO 6a–6c (`Program.cs`)**: Đăng ký Dependency Injection scoped cho các repository và handler.
- **TODO 7 (`Program.cs`)**: Cấu hình SwaggerGen với tài liệu SwaggerDoc v1 ("EduHub API").
- **TODO 8a–8i (`ClubsController.cs`)**: Cài đặt đầy đủ 4 RESTful endpoints (`GET /api/clubs`, `GET /api/clubs/{id}`, `POST /api/clubs`, `DELETE /api/clubs/{id}`) trả về các HTTP status code chuẩn:
  - `200 OK`
  - `201 Created` kèm header `Location`
  - `400 Bad Request` khi validation thất bại
  - `404 Not Found` khi không tìm thấy Id
  - `204 No Content` khi xoá thành công
- **Bổ sung Unit Test (`CreateClubCommandTests.cs`)**: Thêm `GetClubByIdQueryTests` mock repository bằng Moq, kiểm thử lấy câu lạc bộ thành công theo Id.

---

## 3. Hướng dẫn chạy và kiểm thử

### Build dự án
```bash
dotnet build EduHub.sln
```

### Chạy Unit Tests
```bash
dotnet test
```

### Chạy ứng dụng API
```bash
dotnet run --project EduHub.API
```
> Swagger UI có sẵn tại: `http://localhost:5000/swagger`

---

## 4. Demo nhanh qua cURL

```bash
# 1. Lấy danh sách CLB (ban đầu rỗng)
curl -i http://localhost:5000/api/clubs

# 2. Tạo mới CLB hợp lệ (201 Created + Location)
curl -i -X POST http://localhost:5000/api/clubs -H "Content-Type: application/json" \
     -d '{"name":"CLB Lập trình","description":"Học C# và .NET"}'

# 3. Lấy thông tin CLB theo ID (200 OK)
curl -i http://localhost:5000/api/clubs/{id}

# 4. Tạo mới CLB lỗi dữ liệu (400 Bad Request)
curl -i -X POST http://localhost:5000/api/clubs -H "Content-Type: application/json" \
     -d '{"name":"","description":"Test"}'

# 5. Xóa CLB (204 No Content)
curl -i -X DELETE http://localhost:5000/api/clubs/{id}
```
