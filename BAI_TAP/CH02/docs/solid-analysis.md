# Báo cáo Phân tích Vi phạm SOLID trong ClubManager (EduHub Ch02)

## 1. Giới thiệu tổng quan

Trong dự án `EduHub Ch02`, lớp `ClubManager` là một ví dụ điển hình của một **"God Class" (Anti-pattern)**. Một class duy nhất nhưng ôm đồm quá nhiều trách nhiệm:
- Quản lý vòng đời và danh sách Câu lạc bộ (CLB).
- Quản lý thành viên và kiểm tra giới hạn sĩ số CLB.
- Quản lý sự kiện, ghi nhận và phát thông báo sự kiện mới.
- Trực tiếp thao tác I/O cấp thấp: ghi log vào tệp `club-log.txt` thông qua `File.AppendAllText`.
- Trực tiếp gửi thông báo cố định ra `Console.WriteLine` (Email, Slack).
- Trực tiếp tính toán và định dạng xuất báo cáo ra Console.

Thiết kế này vi phạm nghiêm trọng các nguyên lý thiết kế hướng đối tượng **SOLID**, gây khó khăn cho việc kiểm thử đơn vị (Unit Test), mở rộng tính năng và bảo trì mã nguồn.

---

## 2. Chi tiết các vi phạm SOLID trong `ClubManager`

### 2.1. Vi phạm Single Responsibility Principle (SRP) — Nguyên lý Đơn trách nhiệm

> *"Một lớp chỉ nên có một lý do duy nhất để thay đổi."*

#### Các vị trí vi phạm:
1. **`CreateClub` (Dòng 35 - 64):**
   - Vừa chịu trách nhiệm kiểm tra tính hợp lệ của dữ liệu đầu vào (tên CLB rỗng, độ dài > 100).
   - Vừa chịu trách nhiệm lưu trữ thực thể vào danh sách `_clubs`.
   - Vừa chịu trách nhiệm ghi nhật ký (logging) vào file vật lý.
   - Vừa chịu trách nhiệm phân loại tên miền email và gửi thông báo.
2. **`AddMemberToClub` (Dòng 67 - 84):**
   - Vừa kiểm tra quy tắc nghiệp vụ về giới hạn số lượng thành viên (tối đa 50).
   - Vừa khởi tạo và lưu trữ `Member`.
   - Vừa ghi log vào file.
   - Vừa gửi email chào mừng và gửi thông báo Slack.
3. **`CreateEvent` (Dòng 88 - 99):**
   - Vừa tạo sự kiện cho CLB.
   - Vừa ghi log tạo sự kiện.
   - Vừa tự lặp qua tất cả thành viên của CLB để in thông báo gửi email.
4. **`PrintClubReport` (Dòng 103 - 113):**
   - Vừa truy xuất dữ liệu CLB và đếm thành viên.
   - Vừa chịu trách nhiệm định dạng hiển thị ra Console.
   - Vừa ghi log phát sinh báo cáo vào file.

#### Hậu quả:
- `ClubManager` có quá nhiều lý do để thay đổi: thay đổi luật kiểm tra tên CLB, đổi nơi lưu trữ (từ memory sang database), đổi định dạng file log, đổi nhà cung cấp gửi email, hoặc đổi định dạng báo cáo.
- Mã nguồn có độ ghép nối cao (tight coupling), dễ phát sinh lỗi hồi quy (regression bugs) khi chỉnh sửa.
- Không thể viết unit test độc lập cho từng nghiệp vụ vì các tác vụ I/O (File, Console) bị gắn chặt vào logic nghiệp vụ.

#### Hướng khắc phục:
- Tách quy tắc nghiệp vụ vào các Domain Models: `Club.Create()`, `Club.HasRoom()`, `Event.Register()`, `Event.CheckIn()`.
- Tách việc lưu trữ vào các Repository: `IClubRepository`, `IMemberRepository`, `IEventRepository`.
- Tách việc ghi log sang module `IAppLogger`.
- Tách việc gửi thông báo sang module `INotificationService`.
- Tách điều phối nghiệp vụ thành các service chuyên biệt: `ClubService`, `EventService`, `ReportService`.

---

### 2.2. Vi phạm Open/Closed Principle (OCP) — Nguyên lý Mở/Đóng

> *"Một thực thể phần mềm (class, module, function) nên mở cho việc mở rộng (open for extension), nhưng đóng cho việc sửa đổi (closed for modification)."*

#### Các vị trí vi phạm:
1. **Phân nhánh thông báo trong `CreateClub` (Dòng 52 - 63):**
   ```csharp
   if (presidentEmail.Contains("@gmail.com"))
       Console.WriteLine($"Sending Gmail notification to {presidentEmail}");
   else if (presidentEmail.Contains("@yahoo.com"))
       Console.WriteLine($"Sending Yahoo notification to {presidentEmail}");
   else
       Console.WriteLine($"Sending generic email to {presidentEmail}");
   ```
   Nếu hệ thống muốn hỗ trợ thêm Outlook, gửi qua SMS hoặc Webhook, ta buộc phải mở file `ClubManager.cs` và thêm các khối `else if`.
2. **Gửi thông báo cứng trong `AddMemberToClub` (Dòng 82 - 83):**
   Cố định gửi qua Email và Slack bằng `Console.WriteLine`. Nếu muốn tắt thông báo Slack hoặc chuyển sang gửi qua Telegram, ta buộc phải sửa đổi code của method.
3. **Thông báo sự kiện trong `CreateEvent` (Dòng 96 - 97):**
   Phương thức tự duyệt `_members` và in ra thông báo email. Muốn gửi thêm SMS hay Push Notification cho sinh viên thì phải can thiệp trực tiếp vào logic tạo sự kiện.
4. **Định dạng báo cáo trong `PrintClubReport` (Dòng 103 - 113):**
   Báo cáo chỉ được in trực tiếp ra Console dạng văn bản đơn giản. Nếu người dùng muốn xuất file CSV, JSON hoặc bảng biểu HTML, ta phải sửa lại phương thức này.

#### Hậu quả:
- Mỗi khi có yêu cầu mới (kênh thông báo mới, định dạng báo cáo mới), lập trình viên phải sửa đổi code hiện có, tăng nguy cơ làm hỏng các tính năng cũ đang chạy ổn định.
- Mã nguồn vi phạm cấu trúc mở rộng linh hoạt.

#### Hướng khắc phục:
- **Factory Pattern (`NotificationServiceFactory`):** Tạo ra `INotificationService` tương ứng với `NotificationChannel` (Email, SMS, Slack). Muốn thêm kênh mới chỉ cần thêm một class mới kế thừa `INotificationService` và bổ sung 1 nhánh trong Factory.
- **Observer Pattern (`IEventObserver`):** `EventService` khi tạo sự kiện chỉ cần phát đi thông điệp `OnEventCreated`. Các observer như `EmailNotifier`, `LogNotifier` (hoặc sau này là `SmsNotifier`, `PushNotifier`) độc lập lắng nghe mà không làm thay đổi `EventService`.
- **Strategy Pattern (`IReportStrategy`):** `ReportService` chỉ thu thập số liệu `ClubStat`, sau đó ủy quyền việc định dạng cho strategy (`ConsoleReportStrategy`, `CsvReportStrategy`). Thêm định dạng mới chỉ cần tạo class cài đặt `IReportStrategy`.

---

### 2.3. Vi phạm Dependency Inversion Principle (DIP) — Nguyên lý Đảo ngược Phụ thuộc

> *"1. Các module cấp cao không nên phụ thuộc vào các module cấp thấp. Cả hai nên phụ thuộc vào abstractions (interfaces).*
> *2. Abstractions không nên phụ thuộc vào chi tiết. Chi tiết nên phụ thuộc vào abstractions."*

#### Các vị trí vi phạm:
1. **Ghi log vào file trực tiếp (Dòng 48, 79, 94, 111):**
   ```csharp
   File.AppendAllText(_logFilePath, $"[{DateTime.Now}] Club created: {name}\n");
   ```
   Module nghiệp vụ cấp cao (`ClubManager`) phụ thuộc trực tiếp vào chi tiết cấp thấp (hệ điều hành, tệp tin vật lý `File.AppendAllText`).
2. **Xuất thông tin và thông báo trực tiếp qua Console (Dòng 54, 58, 62, 82, 83, 97, 108):**
   `Console.WriteLine` là cơ chế I/O console cụ thể, bị gắn chặt vào giữa logic xử lý nghiệp vụ.
3. **Phụ thuộc vào kiểu cụ thể `List<Club>` và `List<Member>` (Dòng 30 - 31):**
   Dữ liệu được lưu trong cấu trúc `List<T>` cụ thể, không thông qua abstraction kho dữ liệu.

#### Hậu quả:
- Không thể tráo đổi cơ chế ghi log (ví dụ từ file txt sang Console, Database hoặc Logstash) mà không sửa mã nguồn của `ClubManager`.
- Không thể thay đổi cơ chế lưu trữ (ví dụ từ InMemory sang SQL/Entity Framework/MongoDB).
- Không thể viết Unit Test giả lập (Mocking / Faking) do không thể ngắt kết nối với hệ thống File I/O và Console.

#### Hướng khắc phục:
- Định nghĩa interface trừu tượng `IAppLogger` với phương thức `void Info(string message)`. Các class `ConsoleLogger`, `FileLogger` sẽ hiện thực interface này.
- Định nghĩa interface trừu tượng `INotificationService` và các repository interface `IClubRepository`, `IMemberRepository`, `IEventRepository`.
- Áp dụng **Dependency Injection (DI)**: Các service (`ClubService`, `EventService`, `ReportService`) nhận abstractions thông qua constructor thay vì tự khởi tạo (`new`) hoặc gọi trực tiếp class tĩnh.

---

### 2.4. Vi phạm Interface Segregation Principle (ISP) — Nguyên lý Phân tách Giao diện

> *"Không nên ép buộc một client phải phụ thuộc vào những interface mà nó không sử dụng."*

#### Các vị trí vi phạm:
- Lớp `ClubManager` đóng vai trò là một interface "nguyên khối" (fat interface / monolithic class) cho tất cả các hoạt động: quản lý CLB, quản lý thành viên, quản lý sự kiện, gửi thông báo, in báo cáo.
- Bất kỳ client nào cần dùng một chức năng nhỏ (ví dụ chỉ muốn xem danh sách CLB) cũng phải khởi tạo toàn bộ `ClubManager` và chịu ảnh hưởng nếu các phần sự kiện hay thông báo thay đổi.

#### Hậu quả:
- Khó tái sử dụng, coupling cao, khó phân chia công việc trong nhóm phát triển.
- Các interface quá lớn dẫn đến việc cài đặt thừa thãi và dễ vi phạm khi mock dữ liệu kiểm thử.

#### Hướng khắc phục:
- Phân rã thành các interface nhỏ, tập trung đúng ngữ cảnh nghiệp vụ:
  - `IClubRepository`: chỉ chứa `GetAll()`, `GetById()`, `Add()`.
  - `IMemberRepository`: chỉ chứa `GetByClub()`, `GetById()`, `Add()`.
  - `IEventRepository`: chỉ chứa `GetByClub()`, `GetById()`, `Add()`.
  - `IClubService`, `IEventService`, `IReportService`.
- Áp dụng **Decorator Pattern (`LoggingClubRepository`)**: bọc `IClubRepository` để ghi log mà không làm phình to interface hay can thiệp vào mã nguồn gốc.

---

### 2.5. Vi phạm Liskov Substitution Principle (LSP) & Mô hình Anemic Domain

#### Chi tiết vi phạm:
- Các class trong `Domain/` (`Club.cs`, `Event.cs`) ban đầu chỉ là các đối tượng chứa thuộc tính thuần túy (Anemic Domain Model), thiếu vắng hành vi nội tại và tính bao đóng.
- Logic kiểm tra tính toàn vẹn (tên CLB bắt buộc, độ dài tối đa 100, số lượng thành viên tối đa 50, kiểm tra đăng ký trước khi check-in) bị đẩy hết ra ngoài `ClubManager`.

#### Hướng khắc phục:
- Đóng gói toàn bộ quy tắc bất biến (invariants) vào chính Domain Model:
  - `Club.Create(name, description)` chịu trách nhiệm validate và đảm bảo đối tượng luôn ở trạng thái hợp lệ khi được tạo.
  - `Club.HasRoom(currentMemberCount)` xác định tính sẵn sàng tiếp nhận thành viên.
  - `Event.Register(memberId)` và `Event.CheckIn(memberId)` kiểm soát trạng thái tham dự sự kiện.

---

## 3. Tổng kết kiến trúc sau khi Refactor

| Thành phần | Trước khi Refactor (`starter`) | Sau khi Refactor (`solution`) | Nguyên lý / Pattern đạt được |
|---|---|---|---|
| **Domain Model** | Anemic classes (`Club`, `Event`), logic validate nằm rải rác trong `ClubManager` | `Club.Create`, `Club.HasRoom`, `Event.Register`, `Event.CheckIn`, `AttendanceRate` | **Encapsulation**, **Domain-Driven Validation** |
| **Quản lý dữ liệu** | `List<Club>`, `List<Member>` nằm trực tiếp trong `ClubManager` | `IClubRepository`, `IMemberRepository`, `IEventRepository` + `InMemory*Repository` | **ISP**, **DIP** |
| **Ghi log** | `File.AppendAllText("club-log.txt", ...)` | `IAppLogger`, `ConsoleLogger`, `FileLogger`, `LoggingClubRepository` | **DIP**, **Decorator Pattern** |
| **Thông báo** | `if/else` kiểm tra email và `Console.WriteLine` | `INotificationService`, `Email/Sms/SlackNotificationService`, `NotificationServiceFactory` | **OCP**, **Factory Pattern** |
| **Báo cáo** | `PrintClubReport()` in cứng ra Console | `IReportStrategy`, `ConsoleReportStrategy`, `CsvReportStrategy`, `ReportService` | **OCP**, **Strategy Pattern** |
| **Sự kiện** | Lặp qua thành viên gửi email trực tiếp | `IEventObserver`, `LogNotifier`, `EmailNotifier`, `EventService` | **OCP**, **Observer Pattern** |
| **Điều phối nghiệp vụ** | "God class" `ClubManager` (118 dòng) | `ClubService`, `EventService`, `ReportService` | **SRP**, **DIP** |
| **Khởi chạy (Entry)** | `new ClubManager()` trong `Program.cs` | **Composition Root** trong `Program.cs`: nơi duy nhất `new` các class cụ thể và ráp nối qua Constructor Injection | **DIP** (IoC) |
