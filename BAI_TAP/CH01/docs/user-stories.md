# User Stories — Quản lý mượn trả sách (Library Management)

Viết ít nhất 5 User Story, đủ 3 vai trò (Sinh viên, Thủ thư, Admin).
Mỗi story có ít nhất 2 tiêu chí nghiệm thu (Given / When / Then).

---

## US-01 · Sinh viên: Tìm kiếm sách

Là **Sinh viên (Độc giả)**, tôi muốn **tìm kiếm sách theo tên hoặc từ khóa**, để **biết sách có trong thư viện hay không và kiểm tra số lượng còn lại**.

- **1:** Given sách cần tìm tồn tại trong kho thư viện, When sinh viên thực hiện tìm kiếm, Then hệ thống hiển thị thông tin sách và số lượng còn lại khả dụng.
- **2:** Given sách cần tìm không có trong hệ thống thư viện, When sinh viên thực hiện tìm kiếm, Then hệ thống thông báo "Không tìm thấy".

---

## US-02 · Sinh viên: Đặt mượn sách

Là **Sinh viên (Độc giả)**, tôi muốn **đặt mượn sách trực tuyến qua hệ thống**, để **có thể giữ sách và đến nhận tại thư viện**.

- **1:** Given sách còn số lượng khả dụng (> 0) và tài khoản đang hoạt động, When sinh viên bấm xác nhận mượn, Then hệ thống tạo phiếu mượn thành công và giảm số lượng khả dụng của sách đi 1.
- **2:** Given sách đã hết số lượng khả dụng (= 0), When sinh viên bấm mượn sách, Then hệ thống từ chối yêu cầu và thông báo "Hết sách".

---

## US-03 · Thủ thư: Thêm sách mới

Là **Thủ thư**, tôi muốn **thêm sách mới vào kho dữ liệu thư viện**, để **làm phong phú nguồn tài liệu và phục vụ nhu cầu độc giả**.

- **1:** Given thông tin có tên sách hợp lệ và số lượng lớn hơn 0, When thủ thư thực hiện thêm sách, Then sách được lưu thành công vào kho thư viện.
- **2:** Given tên sách để trống hoặc số lượng nhập vào $\le$ 0, When thủ thư bấm thêm, Then hệ thống ngăn chặn lưu và báo lỗi dữ liệu không hợp lệ.

---

## US-04 · Thủ thư: Xác nhận trả sách

Là **Thủ thư**, tôi muốn **xác nhận việc trả sách của sinh viên**, để **cập nhật lại số lượng sách vào kho và xử lý vi phạm quá hạn nếu có**.

- **1:** Given sinh viên trả sách đúng hạn hoặc trước hạn, When thủ thư bấm xác nhận trả, Then phiếu mượn chuyển sang trạng thái "Đã trả" và số lượng sách khả dụng trong kho tăng thêm 1.
- **2:** Given sinh viên trả sách trễ hạn quy định, When thủ thư bấm xác nhận trả, Then hệ thống ghi nhận trả trễ và tự động tính tiền phạt dựa trên số ngày quá hạn để tạo phiếu phạt.

---

## US-05 · Admin: Khóa tài khoản mượn

Là **Quản trị viên (Admin)**, tôi muốn **khóa tài khoản của sinh viên vi phạm quy chế**, để **ngăn không cho sinh viên tiếp tục mượn sách mới mà vẫn giữ nguyên lịch sử giao dịch**.

- **1:** Given tài khoản sinh viên đang ở trạng thái hoạt động bình thường, When Admin chọn thao tác khóa tài khoản, Then tài khoản chuyển sang trạng thái "Đã khóa" và hệ thống chặn mọi lượt mượn sách mới.
- **2:** Given tài khoản sinh viên đã bị khóa, When xem lại thông tin tài khoản, Then toàn bộ lịch sử các phiếu mượn và phiếu phạt trước đó vẫn được lưu trữ nguyên vẹn.
