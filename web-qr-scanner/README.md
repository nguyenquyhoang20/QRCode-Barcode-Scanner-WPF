# Web QR & Barcode Scanner

Ứng dụng web để quét QR code và Barcode trực tuyến, có thể truy cập từ mọi thiết bị có camera.

## ✨ Tính năng

- 📷 Quét QR code và Barcode qua camera
- 🎨 Giao diện đẹp, hiện đại với dark theme
- 📝 Lưu lịch sử quét
- 📋 Copy kết quả vào clipboard
- 📱 Responsive design (mobile-first)
- 🚀 Deploy dễ dàng lên Render

## 🛠️ Công nghệ

- **Frontend**: HTML5, CSS3, Vanilla JavaScript
- **Backend**: Node.js + Express
- **QR/Barcode Library**: html5-qrcode
- **Deployment**: Render

## 📦 Cài đặt

### Chạy local

```bash
# Cài đặt dependencies
npm install

# Chạy server
npm start

# Mở trình duyệt tại http://localhost:3000
```

## 🚀 Deploy lên Render

### Cách 1: Sử dụng Render Dashboard (Khuyến nghị)

1. **Push code lên GitHub**:
   ```bash
   git init
   git add .
   git commit -m "Initial commit"
   git branch -M main
   git remote add origin https://github.com/YOUR_USERNAME/web-qr-scanner.git
   git push -u origin main
   ```

2. **Tạo Web Service trên Render**:
   - Truy cập [Render Dashboard](https://dashboard.render.com/)
   - Click **"New +"** → **"Web Service"**
   - Connect GitHub repository của bạn
   - Chọn repository `web-qr-scanner`

3. **Cấu hình**:
   - **Name**: `web-qr-scanner` (hoặc tên bạn muốn)
   - **Environment**: `Node`
   - **Build Command**: `npm install`
   - **Start Command**: `npm start`
   - **Plan**: Free

4. **Deploy**:
   - Click **"Create Web Service"**
   - Đợi vài phút để Render build và deploy
   - Bạn sẽ nhận được URL: `https://web-qr-scanner.onrender.com`

### Cách 2: Sử dụng Render Blueprint

1. Push code lên GitHub (như trên)

2. Trong repository, đảm bảo có file `render.yaml`

3. Trên Render Dashboard:
   - Click **"New +"** → **"Blueprint"**
   - Connect repository
   - Render sẽ tự động đọc `render.yaml` và deploy

## 📱 Sử dụng

1. Truy cập URL của app (local hoặc Render)
2. Click **"Bắt đầu quét"**
3. Cho phép truy cập camera
4. Đưa QR code hoặc Barcode vào khung hình
5. Kết quả sẽ hiển thị tự động
6. Click **"Copy"** để copy kết quả
7. Xem lịch sử quét ở phía dưới

## 🔒 Lưu ý

- **HTTPS required**: Camera chỉ hoạt động trên HTTPS. Render tự động cung cấp HTTPS.
- **Camera permission**: Lần đầu sử dụng cần cho phép truy cập camera.
- **Mobile**: Hoạt động tốt nhất trên Chrome/Safari mobile.

## 📝 Cấu trúc thư mục

```
web-qr-scanner/
├── public/
│   ├── index.html          # Giao diện chính
│   ├── css/
│   │   └── style.css       # Styling
│   └── js/
│       └── scanner.js      # Logic quét mã
├── server.js               # Express server
├── package.json            # Dependencies
├── render.yaml             # Render config
└── README.md               # Tài liệu này
```

## 🎯 Hỗ trợ định dạng

- QR Code
- EAN-13
- EAN-8
- Code 128
- Code 39
- UPC-A
- UPC-E

## 🐛 Troubleshooting

### Camera không hoạt động
- Đảm bảo đang sử dụng HTTPS
- Kiểm tra quyền truy cập camera trong browser settings
- Thử trình duyệt khác (Chrome/Safari)

### Deploy lỗi trên Render
- Kiểm tra logs trong Render Dashboard
- Đảm bảo `package.json` có đầy đủ dependencies
- Verify build command và start command

## 📄 License

MIT
