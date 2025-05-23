import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export default defineConfig({
  base: "/",
  plugins: [react()],
  preview: {
    port: 4000,
    strictPort: true,
  },
  server: {
    port: 4000,
    strictPort: true,
    host: true,
    origin: "http://0.0.0.0:4000",
  },
  build: {
    outDir: '../Server/dist',
    emptyOutDir: true, // also necessary
  }
});