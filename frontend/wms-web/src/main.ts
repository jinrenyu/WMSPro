import { createApp } from 'vue'
import App from './App.vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import 'element-plus/theme-chalk/dark/css-vars.css'
// 全局主题置于 Element Plus 样式之后加载，确保设计令牌（--el-* 覆写）权威生效
import './style.css'
import { createPinia } from 'pinia'
import router from './router'

import * as ElementPlusIconsVue from '@element-plus/icons-vue'

import { permission } from './directives/permission'

const app = createApp(App)
const pinia = createPinia()

app.use(ElementPlus)
app.use(pinia)
app.use(router)
app.directive('permission', permission)

for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
}

app.mount('#app')
