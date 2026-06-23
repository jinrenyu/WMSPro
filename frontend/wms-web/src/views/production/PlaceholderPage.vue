<template>
  <div class="placeholder-page">
    <el-empty :description="`${title} —— 功能开发中`">
      <template #image>
        <el-icon class="placeholder-icon"><Tools /></el-icon>
      </template>
    </el-empty>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const title = computed(() => (route.meta?.title as string) || '该功能')
</script>

<style scoped>
.placeholder-page {
  display: flex;
  align-items: center;
  justify-content: center;
  min-height: 60vh;
}

.placeholder-icon {
  font-size: 72px;
  color: var(--el-text-color-placeholder, #c0c4cc);
}
</style>
