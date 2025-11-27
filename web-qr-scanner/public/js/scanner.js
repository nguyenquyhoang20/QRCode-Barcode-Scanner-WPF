// Scanner state
let html5QrcodeScanner = null;
let isScanning = false;

// DOM Elements
const startBtn = document.getElementById('startBtn');
const stopBtn = document.getElementById('stopBtn');
const resultSection = document.getElementById('resultSection');
const resultText = document.getElementById('resultText');
const resultFormat = document.getElementById('resultFormat');
const copyBtn = document.getElementById('copyBtn');
const clearHistoryBtn = document.getElementById('clearHistoryBtn');
const historyList = document.getElementById('historyList');
const scannerStatus = document.getElementById('scanner-status');

// Initialize
document.addEventListener('DOMContentLoaded', () => {
    loadHistory();
    setupEventListeners();
});

// Event Listeners
function setupEventListeners() {
    startBtn.addEventListener('click', startScanning);
    stopBtn.addEventListener('click', stopScanning);
    copyBtn.addEventListener('click', copyToClipboard);
    clearHistoryBtn.addEventListener('click', clearHistory);
}

// Start Scanning
function startScanning() {
    if (isScanning) return;

    const config = {
        fps: 10,
        qrbox: { width: 250, height: 250 },
        aspectRatio: 1.0,
        formatsToSupport: [
            Html5QrcodeSupportedFormats.QR_CODE,
            Html5QrcodeSupportedFormats.EAN_13,
            Html5QrcodeSupportedFormats.EAN_8,
            Html5QrcodeSupportedFormats.CODE_128,
            Html5QrcodeSupportedFormats.CODE_39,
            Html5QrcodeSupportedFormats.UPC_A,
            Html5QrcodeSupportedFormats.UPC_E
        ]
    };

    html5QrcodeScanner = new Html5QrcodeScanner("reader", config, false);

    html5QrcodeScanner.render(onScanSuccess, onScanError);

    isScanning = true;
    startBtn.style.display = 'none';
    stopBtn.style.display = 'flex';
    scannerStatus.style.display = 'none';

    showToast('Camera đã được kích hoạt', 'success');
}

// Stop Scanning
function stopScanning() {
    if (!isScanning || !html5QrcodeScanner) return;

    html5QrcodeScanner.clear().then(() => {
        isScanning = false;
        startBtn.style.display = 'flex';
        stopBtn.style.display = 'none';
        scannerStatus.style.display = 'flex';
        showToast('Đã dừng quét', 'info');
    }).catch(err => {
        console.error('Error stopping scanner:', err);
    });
}

// Scan Success Handler
function onScanSuccess(decodedText, decodedResult) {
    // Display result
    resultText.textContent = decodedText;
    resultFormat.textContent = decodedResult.result.format.formatName || 'QR Code';
    resultSection.style.display = 'block';

    // Save to history
    saveToHistory(decodedText, decodedResult.result.format.formatName);

    // Show notification
    showToast('Quét thành công!', 'success');

    // Vibrate if supported
    if (navigator.vibrate) {
        navigator.vibrate(200);
    }
}

// Scan Error Handler
function onScanError(errorMessage) {
    // Ignore common scanning errors (no QR code in frame)
    // Only log actual errors
    if (!errorMessage.includes('NotFoundException')) {
        console.warn('Scan error:', errorMessage);
    }
}

// Copy to Clipboard
function copyToClipboard() {
    const text = resultText.textContent;

    if (navigator.clipboard && navigator.clipboard.writeText) {
        navigator.clipboard.writeText(text).then(() => {
            showToast('Đã copy vào clipboard!', 'success');
            copyBtn.innerHTML = '<span class="btn-icon">✓</span>Đã copy';

            setTimeout(() => {
                copyBtn.innerHTML = '<span class="btn-icon">📋</span>Copy';
            }, 2000);
        }).catch(err => {
            console.error('Copy failed:', err);
            fallbackCopy(text);
        });
    } else {
        fallbackCopy(text);
    }
}

// Fallback copy method
function fallbackCopy(text) {
    const textArea = document.createElement('textarea');
    textArea.value = text;
    textArea.style.position = 'fixed';
    textArea.style.left = '-999999px';
    document.body.appendChild(textArea);
    textArea.select();

    try {
        document.execCommand('copy');
        showToast('Đã copy vào clipboard!', 'success');
    } catch (err) {
        console.error('Fallback copy failed:', err);
        showToast('Không thể copy', 'error');
    }

    document.body.removeChild(textArea);
}

// Save to History
async function saveToHistory(text, format) {
    const scanData = {
        text,
        format: format || 'QR Code',
        timestamp: new Date().toISOString()
    };

    try {
        const response = await fetch('/api/history', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(scanData)
        });

        if (response.ok) {
            loadHistory();
        }
    } catch (error) {
        console.error('Error saving to history:', error);
        // Fallback to localStorage
        saveToLocalStorage(scanData);
        loadHistory();
    }
}

// Save to LocalStorage (fallback)
function saveToLocalStorage(scanData) {
    let history = JSON.parse(localStorage.getItem('scanHistory') || '[]');
    history.unshift({ ...scanData, id: Date.now() });
    history = history.slice(0, 100); // Keep last 100
    localStorage.setItem('scanHistory', JSON.stringify(history));
}

// Load History
async function loadHistory() {
    try {
        const response = await fetch('/api/history');
        const history = await response.json();
        displayHistory(history);
    } catch (error) {
        console.error('Error loading history:', error);
        // Fallback to localStorage
        const localHistory = JSON.parse(localStorage.getItem('scanHistory') || '[]');
        displayHistory(localHistory);
    }
}

// Display History
function displayHistory(history) {
    if (!history || history.length === 0) {
        historyList.innerHTML = `
            <div class="empty-state">
                <span class="empty-icon">📝</span>
                <p>Chưa có lịch sử quét</p>
            </div>
        `;
        return;
    }

    historyList.innerHTML = history.map(item => `
        <div class="history-item">
            <div class="history-item-header">
                <span class="history-format">${item.format}</span>
                <span class="history-time">${formatTime(item.timestamp)}</span>
            </div>
            <p class="history-text">${escapeHtml(item.text)}</p>
        </div>
    `).join('');
}

// Clear History
async function clearHistory() {
    if (!confirm('Bạn có chắc muốn xóa toàn bộ lịch sử?')) {
        return;
    }

    try {
        await fetch('/api/history', { method: 'DELETE' });
        localStorage.removeItem('scanHistory');
        loadHistory();
        showToast('Đã xóa lịch sử', 'success');
    } catch (error) {
        console.error('Error clearing history:', error);
        localStorage.removeItem('scanHistory');
        loadHistory();
        showToast('Đã xóa lịch sử', 'success');
    }
}

// Format Time
function formatTime(timestamp) {
    const date = new Date(timestamp);
    const now = new Date();
    const diff = now - date;

    // Less than 1 minute
    if (diff < 60000) {
        return 'Vừa xong';
    }

    // Less than 1 hour
    if (diff < 3600000) {
        const minutes = Math.floor(diff / 60000);
        return `${minutes} phút trước`;
    }

    // Less than 1 day
    if (diff < 86400000) {
        const hours = Math.floor(diff / 3600000);
        return `${hours} giờ trước`;
    }

    // Format as date
    return date.toLocaleDateString('vi-VN', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
    });
}

// Escape HTML
function escapeHtml(text) {
    const div = document.createElement('div');
    div.textContent = text;
    return div.innerHTML;
}

// Show Toast Notification
function showToast(message, type = 'success') {
    const toast = document.getElementById('toast');
    toast.textContent = message;
    toast.className = 'toast show';

    if (type === 'error') {
        toast.style.background = '#ef4444';
    } else if (type === 'info') {
        toast.style.background = '#6366f1';
    } else {
        toast.style.background = '#10b981';
    }

    setTimeout(() => {
        toast.classList.remove('show');
    }, 3000);
}
