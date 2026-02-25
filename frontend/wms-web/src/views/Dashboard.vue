<template>
  <div>
    <!-- Dashboard Widgets Placeholder -->
    <div class="dashboard-grid">
      <div class="stat-card">
        <div class="stat-icon"><Box /></div>
        <div class="stat-info">
          <div class="stat-label">总库存</div>
          <div class="stat-value">12,450</div>
        </div>
      </div>
      <div class="stat-card">
        <div class="stat-icon"><Sort /></div>
        <div class="stat-info">
          <div class="stat-label">今日入库</div>
          <div class="stat-value">156</div>
        </div>
      </div>
      <div class="stat-card">
        <div class="stat-icon"><SoldOut /></div>
        <div class="stat-info">
          <div class="stat-label">待处理订单</div>
          <div class="stat-value">23</div>
        </div>
      </div>
      <div class="stat-card">
        <div class="stat-icon"><Warning /></div>
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
  padding: 24px;
  display: flex;
  align-items: center;
  border-radius: 12px;
  background: var(--bg-card);
  border: 1px solid var(--border-color);
  box-shadow: var(--shadow-card);
  transition: transform 0.2s, box-shadow 0.2s;
}

.stat-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1);
}

.stat-icon {
  width: 56px;
  height: 56px;
  border-radius: 16px;
  background: var(--bg-body);
  color: var(--primary-color);
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 28px;
  margin-right: 20px;
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
}

.stat-value.warning {
  color: #ef4444;
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
