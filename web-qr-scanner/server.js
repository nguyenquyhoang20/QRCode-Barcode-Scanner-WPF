const express = require('express');
const cors = require('cors');
const path = require('path');

const app = express();
const PORT = process.env.PORT || 3000;

// Middleware
app.use(cors());
app.use(express.json());
app.use(express.static('public'));

// In-memory storage for scan history
let scanHistory = [];

// API Routes
app.get('/api/history', (req, res) => {
  res.json(scanHistory);
});

app.post('/api/history', (req, res) => {
  const { text, format, timestamp } = req.body;
  const newScan = {
    id: Date.now(),
    text,
    format,
    timestamp: timestamp || new Date().toISOString()
  };
  scanHistory.unshift(newScan);
  
  // Keep only last 100 scans
  if (scanHistory.length > 100) {
    scanHistory = scanHistory.slice(0, 100);
  }
  
  res.json(newScan);
});

app.delete('/api/history', (req, res) => {
  scanHistory = [];
  res.json({ message: 'History cleared' });
});

// Serve index.html for root route
app.get('/', (req, res) => {
  res.sendFile(path.join(__dirname, 'public', 'index.html'));
});

// Start server
app.listen(PORT, () => {
  console.log(`🚀 QR Scanner server running on port ${PORT}`);
  console.log(`📱 Open http://localhost:${PORT} in your browser`);
});
