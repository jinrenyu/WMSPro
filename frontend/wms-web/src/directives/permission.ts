import type { Directive, DirectiveBinding } from 'vue'
import { usePermissionStore } from '../stores/permission'

export const permission: Directive = {
    mounted(el: HTMLElement, binding: DirectiveBinding) {
        const { value } = binding
        const store = usePermissionStore()

        if (value && typeof value === 'string') {
            if (!store.hasPermission(value)) {
                el.parentNode?.removeChild(el)
            }
        } else {
            throw new Error(`need roles! Like v-permission="'user:add'"`)
        }
    }
}
