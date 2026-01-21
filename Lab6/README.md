# Lab 6: Advanced ASP.NET Core Features

Repository này chứa các bài demo cho Lab 6 môn Lập trình Web Nâng cao.

## Danh sách Project

### 1. [Demo01 - Data Validation](Demo01/README.md)
- **Mục tiêu**: Demo tính năng kiểm tra dữ liệu đầu vào (Validation) sử dụng Data Annotations.
- **Điểm nhấn**: Giao diện Bootstrap 5, Form nhập liệu sinh viên, Validation phía Server và Client.

### 2. [Demo02 - Dependency Injection Lifecycle](Demo02/README.md)
- **Mục tiêu**: Demo cơ chế Dependency Injection (DI) và các vòng đời (Lifetime) của Service.
- **Điểm nhấn**: Giao diện trực quan so sánh Transient, Scoped, Singleton. Minh họa sự khác biệt qua GUID.

## Hướng dẫn chung
Mỗi thư mục con là một project độc lập. Bạn có thể chạy từng project bằng cách `cd` vào thư mục và gõ `dotnet run`.

```bash
# Chạy Demo01
cd Demo01
dotnet run
```

```bash
# Chạy Demo02
cd Demo02
dotnet run
```
