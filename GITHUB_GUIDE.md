# 🚀 Hướng dẫn Push Project lên GitHub

## Bước 1: Khởi tạo Git Repository

Mở terminal/PowerShell trong thư mục project và chạy:

```bash
cd "c:\Users\QUY HOANG\.gemini\antigravity\playground\magnetic-oort\QRBarcodeScanner"
git init
```

## Bước 2: Add tất cả files

```bash
git add .
```

## Bước 3: Commit lần đầu

```bash
git commit -m "Initial commit: QR Code & Barcode Scanner WPF application"
```

## Bước 4: Tạo Repository trên GitHub

1. Truy cập https://github.com/new
2. Điền thông tin:
   - **Repository name**: `QRCode-Barcode-Scanner-WPF`
   - **Description**: `A modern WPF MVVM desktop application for real-time QR Code and Barcode scanning`
   - **Public** hoặc **Private**: Chọn Public
   - **KHÔNG** check "Add README" (vì đã có rồi)
3. Click **Create repository**

## Bước 5: Link với GitHub Repository

Thay `YOUR_USERNAME` bằng username GitHub của bạn:

```bash
git remote add origin https://github.com/YOUR_USERNAME/QRCode-Barcode-Scanner-WPF.git
git branch -M main
git push -u origin main
```

## Bước 6: Xác thực (nếu cần)

Nếu GitHub yêu cầu đăng nhập:
- **Username**: GitHub username của bạn
- **Password**: Sử dụng **Personal Access Token** (không phải password)

### Tạo Personal Access Token:
1. GitHub → Settings → Developer settings → Personal access tokens → Tokens (classic)
2. Generate new token (classic)
3. Chọn scopes: `repo` (full control)
4. Copy token và dùng làm password

## Bước 7: Verify

Kiểm tra repository trên GitHub:
```
https://github.com/YOUR_USERNAME/QRCode-Barcode-Scanner-WPF
```

## 📝 Các lệnh Git hữu ích

### Xem trạng thái
```bash
git status
```

### Xem lịch sử commit
```bash
git log --oneline
```

### Push thay đổi mới
```bash
git add .
git commit -m "Your commit message"
git push
```

### Tạo branch mới
```bash
git checkout -b feature/new-feature
```

### Merge branch
```bash
git checkout main
git merge feature/new-feature
```

## 🎯 Tên đề xuất cho Repository

**Đã chọn**: `QRCode-Barcode-Scanner-WPF`

**Alternatives**:
- `WPF-Barcode-Scanner`
- `Real-Time-QR-Scanner`
- `OpenCV-Barcode-Reader`
- `Desktop-Barcode-Scanner`

## ✅ Checklist trước khi push

- [x] README.md đã được cập nhật sang tiếng Anh
- [x] .gitignore đã được tạo
- [x] LICENSE đã được thêm (MIT)
- [x] CONTRIBUTING.md đã được tạo
- [x] Code đã build thành công
- [x] Không có file nhạy cảm (passwords, keys)

## 🌟 Sau khi push

### 1. Thêm Topics (Tags)
Trên GitHub repository → Settings → Topics:
- `wpf`
- `mvvm`
- `csharp`
- `opencv`
- `zxing`
- `barcode-scanner`
- `qr-code`
- `dotnet`
- `computer-vision`

### 2. Tạo Release
- GitHub → Releases → Create a new release
- Tag: `v1.0.0`
- Title: `Initial Release`
- Description: Mô tả tính năng

### 3. Add Screenshot
Chụp ảnh màn hình ứng dụng và thêm vào README:
```markdown
## 📸 Screenshots

![Main Interface](screenshots/main-window.png)
```

### 4. Enable GitHub Pages (optional)
Nếu muốn tạo documentation website

## 🔗 Links hữu ích

- [GitHub Docs](https://docs.github.com/)
- [Git Cheat Sheet](https://education.github.com/git-cheat-sheet-education.pdf)
- [Markdown Guide](https://www.markdownguide.org/)

---

**Chúc mừng! 🎉 Project của bạn đã sẵn sàng cho GitHub!**
