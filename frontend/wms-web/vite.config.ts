import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [vue()],
    server: {
        port: 3000,
        strictPort: true,
        host: true,
        proxy: {
            '/api': {
                target: 'http://127.0.0.1:5180',
                changeOrigin: true,
                secure: false,
                configure: (proxy: any) => {
                    proxy.on('error', (err: any) => {
                        console.log('proxy error', err)
                    })
                    proxy.on('proxyReq', (_proxyReq: any, req: any) => {
                        console.log('Proxy request:', req.method, req.url, '-> http://127.0.0.1:5180')
                    })
                    proxy.on('proxyRes', (proxyRes: any, req: any) => {
                        console.log('Proxy response:', proxyRes.statusCode, req.url)
                    })
                }
            }
        }
    }
})
