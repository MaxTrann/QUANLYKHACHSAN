# 📘 QUANLYKHACHSAN - Hotel Management System

> Dự án phần mềm quản lý khách sạn được xây dựng bằng C# WinForms, hỗ trợ đặt phòng, quản lý dịch vụ, in báo cáo và phân quyền người dùng.

---

## 🏨 Chức năng chính

- 📅 **Đặt phòng khách lẻ & theo đoàn**
  - Kéo thả phòng trực quan
  - Tự động cập nhật trạng thái phòng (trống, đã đặt, đang sử dụng)
  - Tính tổng tiền, thêm dịch vụ đi kèm

- 🧾 **In hóa đơn / phiếu đặt phòng**
  - Sử dụng Crystal Reports
  - Hỗ trợ in trực tiếp hoặc xuất file

- 🔐 **Phân quyền người dùng**
  - Tài khoản đăng nhập với quyền Admin / Nhân viên
  - Giới hạn quyền thao tác theo từng user

- 📊 **Báo cáo thống kê**
  - Báo cáo doanh thu theo ngày, công ty, đơn vị
  - Lọc dữ liệu động theo tham số đầu vào

---


## 🧱 Kiến trúc phần mềm

Dự án được tổ chức theo mô hình **3 lớp (3-tier architecture)** rõ ràng:

- **1. Data Layer (Tầng dữ liệu):**
  - Chứa các lớp entity, context (`DbContext`) và truy xuất dữ liệu bằng Entity Framework.
  - Ví dụ: `tb_KHACHHANG`, `tb_PHONG`, `HOTELSContext.cs`

- **2. Business Layer (Tầng nghiệp vụ):**
  - Chứa các lớp xử lý logic nghiệp vụ, trung gian giữa UI và Data Layer.
  - Ví dụ: `PHONG.cs`, `DICHVU.cs`, `HOADON.cs`

- **3. Presentation Layer (Tầng giao diện):**
  - Giao diện người dùng viết bằng WinForms, phân chia theo chức năng như đặt phòng, phân quyền...
  - Các form nằm trong thư mục `THUEPHONG` và `USERMANAGEMENT`

---
## 🛠 Công nghệ sử dụng

| Công nghệ         | Mô tả |
|------------------|-------|
| 💻 **C# WinForms**       | Giao diện người dùng |
| 🎨 **DevExpress UI**     | Các control nâng cao (grid, drag & drop, button...) |
| 🗄 **Entity Framework**  | ORM làm việc với SQL Server |
| 📊 **Crystal Reports**   | In và thiết kế báo cáo động |
| 🧠 **LINQ**              | Truy vấn dữ liệu động |

---

## 🚀 Hướng dẫn chạy project

> ⚠ **Yêu cầu cài đặt trước**:  
> - Visual Studio 2022 hoặc mới hơn  
> - .NET Framework 4.8  
> - SQL Server hoặc LocalDB  
> - Crystal Reports Runtime

### Các bước:

1. **Clone repo**:
   ```bash
   git clone https://github.com/MaxTrann/QUANLYKHACHSAN.git
   ```

2. **Mở project trong Visual Studio**:
   - Mở file `QUANLYKHACHSAN.sln`

3. **Khôi phục thư viện NuGet**:
   - VS sẽ tự động restore
   - Hoặc vào menu:  
     `Tools` → `NuGet Package Manager` → `Manage NuGet Packages for Solution…` → tab **Restore**

4. **Cập nhật chuỗi kết nối cơ sở dữ liệu**:
   - Mở file `App.config`
   - Cập nhật chuỗi sau theo SQL Server của bạn:
     ```xml
     <connectionStrings>
         <add name="DBContext" 
              connectionString="Data Source=localhost;Initial Catalog=HOTELS;Integrated Security=True" 
              providerName="System.Data.SqlClient" />
     </connectionStrings>
     ```

5. **Tạo database** (nếu chưa có):
   - Mở SQL Server Management Studio (SSMS)
   - Restore từ file `HOTELS.bak` có sẵn trong repo (nếu có)
   - Hoặc tạo database thủ công rồi chạy migration / script SQL tương ứng

6. **Chạy ứng dụng**:
   - Nhấn `F5` trong Visual Studio để chạy

---

## 📁 Cấu trúc thư mục

```
├── BusinessLayer          // Xử lý nghiệp vụ
├── DataLayer              // Mô hình dữ liệu, Entity Framework
├── THUEPHONG              // Giao diện đặt phòng
├── USERMANAGEMENT         // Giao diện phân quyền người dùng
├── img                    // Hình ảnh sử dụng trong giao diện
├── HOTELS.bak             // File backup cơ sở dữ liệu
├── QUANLYKHACHSAN.sln     // File mở solution
├── .gitignore             // File cấu hình Git
└── README.md              // Tài liệu mô tả dự án
```

---

## 👨‍💻 Tác giả

- **MaxTrann**  
  📧 [Facebook](https://www.facebook.com/tran.le.quoc.ai.149118)  
  💼 [Instagram](https://www.instagram.com/_maxtrann)

---

## ⭐ Ghi chú

- Dự án phục vụ học tập, đồ án môn **Phân tích thiết kế hệ thống** / **Lập trình Windows Forms**
- Bạn có thể fork và cải tiến nếu muốn nâng cấp thêm tính năng!
