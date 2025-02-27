import express from "express";
import morgan from "morgan";
import { fileURLToPath } from "url";
import { dirname, join } from "path";

const app = express();
const PORT = process.env.PORT || 4000;

// Get __dirname equivalent in ES modules
const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

// Use morgan middleware to log requests
app.use(morgan("combined"));

// Serve static files from the entire "dist" folder
app.use(express.static(join(__dirname, "dist")));

// The "catchall" handler for React routing
app.get("*", (req, res) => {
    res.sendFile(join(__dirname, "dist", "index.html"));
});

// Start the server
app.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`);
});