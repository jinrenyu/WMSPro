<template>
  <div>
    <!-- Dashboard Widgets Placeholder -->
    <div class="dashboard-grid">
      <div class="stat-card">
        <div class="stat-icon is-primary"><Box /></div>
        <div class="stat-info">
          <div class="stat-label">总库存</div>
          <div class="stat-value">12,450</div>
        </div>
      </div>
      <div class="stat-card">
        <div class="stat-icon is-success"><Sort /></div>
        <div class="stat-info">
          <div class="stat-label">今日入库</div>
          <div class="stat-value">156</div>
        </div>
      </div>
      <div class="stat-card">
        <div class="stat-icon is-accent"><SoldOut /></div>
        <div class="stat-info">
          <div class="stat-label">待处理订单</div>
          <div class="stat-value">23</div>
        </div>
      </div>
      <div class="stat-card">
        <div class="stat-icon is-danger"><Warning /></div>
        <div class="stat-info">
          <div class="stat-label">预警物料</div>
          <div class="stat-value warning">5</div>
        </div>
      </div>
    </div>
    
    <div class="chart-placeholder">
      <h3>入库/出库 趋势图 (AI 预测)</h3>
      <div class="chart-mockup">Chart Area</div>
    </div>
  </div>
</template>

<style scoped>
.dashboard-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(260px, 1fr));
  gap: 24px;
  margin-bottom: 32px;
}

.stat-card {
  padding: 22px 24px;
  display: flex;
  align-items: center;
  border-radius: var(--radius-lg);
  background: var(--bg-card);
  border: 1px solid var(--border-color);
  box-shadow: var(--shadow-card);
  transition: transform 0.2s, box-shadow 0.2s;
}

.stat-card:hover {
  transform: translateY(-2px);
  box-shadow: var(--shadow-card-hover);
}

.stat-icon {
  width: 54px;
  height: 54px;
  border-radius: 14px;
  background: var(--primary-soft);
  color: var(--primary-color);
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 26px;
  margin-right: 18px;
  flex-shrink: 0;
}

.stat-icon.is-primary {
  background: var(--el-color-primary-light-9);
  color: var(--primary-color);
}
.stat-icon.is-success {
  background: var(--el-color-success-light-9);
  color: var(--success-color);
}
.stat-icon.is-accent {
  background: var(--accent-soft);
  color: var(--accent-color);
}
.stat-icon.is-danger {
  background: var(--el-color-danger-light-9);
  color: var(--danger-color);
}

.stat-info {
  flex: 1;
}

.stat-label {
  color: var(--text-secondary);
  font-size: 14px;
  margin-bottom: 4px;
  font-weight: 500;
}

.stat-value {
  color: var(--text-primary);
  font-size: 28px;
  font-weight: 700;
  letter-spacing: -0.5px;
  font-variant-numeric: tabular-nums;
}

.stat-value.warning {
  color: var(--danger-color);
}

.chart-placeholder {
  padding: 32px;
  border-radius: 12px;
  background: var(--bg-card);
  border: 1px solid var(--border-color);
  box-shadow: var(--shadow-card);
  min-height: 400px;
}

.chart-placeholder h3 {
  margin-top: 0;
  margin-bottom: 24px;
  color: var(--text-primary);
  font-size: 18px;
}

.chart-mockup {
  height: 300px;
  background: var(--bg-body);
  border-radius: 8px;
  border: 1px dashed var(--border-color);
  display: flex;
  justify-content: center;
  align-items: center;
  color: var(--text-secondary);
}
</style>
