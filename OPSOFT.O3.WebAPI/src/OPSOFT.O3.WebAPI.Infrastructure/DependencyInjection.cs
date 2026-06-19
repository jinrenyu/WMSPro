using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Application.Services;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using OPSOFT.O3.WebAPI.Infrastructure.Repositories;
using OPSOFT.O3.WebAPI.Infrastructure.Services;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString, DbType dbType)
    {
        var connectionConfig = new ConnectionConfig
        {
            ConnectionString = connectionString,
            DbType = dbType,
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute
        };

        // CodeFirst: auto-create tables for all entities with [SugarTable] attribute
        using (var db = new SqlSugarClient(connectionConfig))
        {
            var domainAssembly = Assembly.Load("OPSOFT.O3.WebAPI.Domain");
            var entityTypes = domainAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract
                    && t.GetCustomAttributes(typeof(SugarTable), true).Length > 0)
                .ToArray();

            db.CodeFirst.InitTables(entityTypes);
        }

        services.AddScoped<ISqlSugarClient>(sp =>
        {
            var db = new SqlSugarClient(connectionConfig);
            var logger = sp.GetRequiredService<ILoggerFactory>().CreateLogger("SqlSugar");
            var sqlLoggingConfig = sp.GetRequiredService<IConfiguration>().GetSection("SqlLogging");
            var enabled = sqlLoggingConfig.GetValue<bool>("Enabled", true);

            if (enabled)
            {
                var slowThreshold = sqlLoggingConfig.GetValue<int>("SlowQueryThresholdMs", 200);
                var criticalThreshold = sqlLoggingConfig.GetValue<int>("CriticalQueryThresholdMs", 1000);

                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    logger.LogDebug("Executing SQL: {Sql}, Parameters: {@Parameters}", sql, pars?.Select(p => new { p.ParameterName, p.Value }));
                };

                db.Aop.OnLogExecuted = (sql, pars) =>
                {
                    var elapsed = db.Ado.SqlExecutionTime.TotalMilliseconds;
                    if (elapsed > criticalThreshold)
                    {
                        logger.LogError("慢查询 (Critical) [{ElapsedMs}ms]: {Sql}", elapsed, sql);
                    }
                    else if (elapsed > slowThreshold)
                    {
                        logger.LogWarning("慢查询 [{ElapsedMs}ms]: {Sql}", elapsed, sql);
                    }
                };

                db.Aop.OnError = (ex) =>
                {
                    logger.LogError(ex, "SQL执行错误: {Sql}", ex.Sql);
                };
            }

            return db;
        });

        // 通用仓储
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // 基础服务
        services.AddSingleton<IRsaKeyService, RsaKeyService>();
        services.AddScoped<ILoginUserRepository, LoginUserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ICorrelationIdProvider, HttpCorrelationIdProvider>();
        services.AddScoped<IOperationLogService, OperationLogService>();
        services.AddScoped<IRequestLogService, RequestLogService>();
        services.AddScoped<IAuditLogService, AuditLogService>();
        services.AddScoped<ILogExportService, LogExportService>();

        // Phase 1: 登录权限
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ILoginInfoService, LoginInfoService>();

        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IOrgService, OrgService>();

        // Phase 2: 基础资料
        services.AddScoped<DepartmentService>();
        services.AddScoped<ICrudService<TBdDepartment, DepartmentListDto, DepartmentDetailDto, CreateDepartmentRequest, UpdateDepartmentRequest>, DepartmentService>();
        services.AddScoped<IApprovableDisableableService<TBdCustomer, CustomerListDto, CustomerDetailDto, CreateCustomerRequest, UpdateCustomerRequest>, CustomerService>();
        services.AddScoped<ICrudService<TBdCustomer, CustomerListDto, CustomerDetailDto, CreateCustomerRequest, UpdateCustomerRequest>>(sp => sp.GetRequiredService<IApprovableDisableableService<TBdCustomer, CustomerListDto, CustomerDetailDto, CreateCustomerRequest, UpdateCustomerRequest>>());
        services.AddScoped<IApprovableDisableableService<TBdSupplier, SupplierListDto, SupplierDetailDto, CreateSupplierRequest, UpdateSupplierRequest>, SupplierService>();
        services.AddScoped<ICrudService<TBdSupplier, SupplierListDto, SupplierDetailDto, CreateSupplierRequest, UpdateSupplierRequest>>(sp => sp.GetRequiredService<IApprovableDisableableService<TBdSupplier, SupplierListDto, SupplierDetailDto, CreateSupplierRequest, UpdateSupplierRequest>>());
        services.AddScoped<IApprovableDisableableService<TBdCurrency, CurrencyListDto, CurrencyDetailDto, CreateCurrencyRequest, UpdateCurrencyRequest>, CurrencyService>();
        services.AddScoped<ICrudService<TBdCurrency, CurrencyListDto, CurrencyDetailDto, CreateCurrencyRequest, UpdateCurrencyRequest>>(sp => sp.GetRequiredService<IApprovableDisableableService<TBdCurrency, CurrencyListDto, CurrencyDetailDto, CreateCurrencyRequest, UpdateCurrencyRequest>>());
        services.AddScoped<IApprovableDisableableService<TBdStock, WarehouseListDto, WarehouseDetailDto, CreateWarehouseRequest, UpdateWarehouseRequest>, WarehouseService>();
        services.AddScoped<ICrudService<TBdStock, WarehouseListDto, WarehouseDetailDto, CreateWarehouseRequest, UpdateWarehouseRequest>>(sp => sp.GetRequiredService<IApprovableDisableableService<TBdStock, WarehouseListDto, WarehouseDetailDto, CreateWarehouseRequest, UpdateWarehouseRequest>>());
        services.AddScoped<IMaterialService, MaterialService>();
        services.AddScoped<ICrudService<TBdMaterial, MaterialListDto, MaterialDetailDto, CreateMaterialRequest, UpdateMaterialRequest>>(sp => sp.GetRequiredService<IMaterialService>());
        services.AddScoped<IApprovableDisableableService<TBdUnit, UnitListDto, UnitDetailDto, CreateUnitRequest, UpdateUnitRequest>, UnitService>();
        services.AddScoped<ICrudService<TBdUnit, UnitListDto, UnitDetailDto, CreateUnitRequest, UpdateUnitRequest>>(sp => sp.GetRequiredService<IApprovableDisableableService<TBdUnit, UnitListDto, UnitDetailDto, CreateUnitRequest, UpdateUnitRequest>>());
        services.AddScoped<IApprovableDisableableService<TBdStockPlace, StockPlaceListDto, StockPlaceDetailDto, CreateStockPlaceRequest, UpdateStockPlaceRequest>, StockPlaceService>();
        services.AddScoped<ICrudService<TBdStockPlace, StockPlaceListDto, StockPlaceDetailDto, CreateStockPlaceRequest, UpdateStockPlaceRequest>>(sp => sp.GetRequiredService<IApprovableDisableableService<TBdStockPlace, StockPlaceListDto, StockPlaceDetailDto, CreateStockPlaceRequest, UpdateStockPlaceRequest>>());
        services.AddScoped<IApprovableDisableableService<TBasFlexvalues, FlexValuesListDto, FlexValuesDetailDto, CreateFlexValuesRequest, UpdateFlexValuesRequest>, FlexValuesService>();
        services.AddScoped<ICrudService<TBasFlexvalues, FlexValuesListDto, FlexValuesDetailDto, CreateFlexValuesRequest, UpdateFlexValuesRequest>>(sp => sp.GetRequiredService<IApprovableDisableableService<TBasFlexvalues, FlexValuesListDto, FlexValuesDetailDto, CreateFlexValuesRequest, UpdateFlexValuesRequest>>());
        services.AddScoped<IApprovableDisableableService<TBasAssistantdata, AssistantDataListDto, AssistantDataDetailDto, CreateAssistantDataRequest, UpdateAssistantDataRequest>, AssistantDataService>();
        services.AddScoped<ICrudService<TBasAssistantdata, AssistantDataListDto, AssistantDataDetailDto, CreateAssistantDataRequest, UpdateAssistantDataRequest>>(sp => sp.GetRequiredService<IApprovableDisableableService<TBasAssistantdata, AssistantDataListDto, AssistantDataDetailDto, CreateAssistantDataRequest, UpdateAssistantDataRequest>>());
        services.AddScoped<IApprovableDisableableService<TBasAssistantdataentry, AssistantDataEntryListDto, AssistantDataEntryDetailDto, CreateAssistantDataEntryRequest, UpdateAssistantDataEntryRequest>, AssistantDataEntryService>();
        services.AddScoped<ICrudService<TBasAssistantdataentry, AssistantDataEntryListDto, AssistantDataEntryDetailDto, CreateAssistantDataEntryRequest, UpdateAssistantDataEntryRequest>>(sp => sp.GetRequiredService<IApprovableDisableableService<TBasAssistantdataentry, AssistantDataEntryListDto, AssistantDataEntryDetailDto, CreateAssistantDataEntryRequest, UpdateAssistantDataEntryRequest>>());
        services.AddScoped<IApprovableDisableableService<THrEmpinfo, EmployeeListDto, EmployeeDetailDto, CreateEmployeeRequest, UpdateEmployeeRequest>, EmployeeService>();
        services.AddScoped<ICrudService<THrEmpinfo, EmployeeListDto, EmployeeDetailDto, CreateEmployeeRequest, UpdateEmployeeRequest>>(sp => sp.GetRequiredService<IApprovableDisableableService<THrEmpinfo, EmployeeListDto, EmployeeDetailDto, CreateEmployeeRequest, UpdateEmployeeRequest>>());
        services.AddScoped<IApprovableDisableableService<TBdMaterialbartype, MaterialBarTypeListDto, MaterialBarTypeDetailDto, CreateMaterialBarTypeRequest, UpdateMaterialBarTypeRequest>, MaterialBarTypeService>();
        services.AddScoped<ICrudService<TBdMaterialbartype, MaterialBarTypeListDto, MaterialBarTypeDetailDto, CreateMaterialBarTypeRequest, UpdateMaterialBarTypeRequest>>(sp => sp.GetRequiredService<IApprovableDisableableService<TBdMaterialbartype, MaterialBarTypeListDto, MaterialBarTypeDetailDto, CreateMaterialBarTypeRequest, UpdateMaterialBarTypeRequest>>());
        services.AddScoped<ICrudService<TBdPrintTemplate, PrintTemplateListDto, PrintTemplateDetailDto, CreatePrintTemplateRequest, UpdatePrintTemplateRequest>, PrintTemplateService>();
        services.AddScoped<IBaseDataGroupService, BaseDataGroupService>();

        // Phase 3: 业务单据
        services.AddScoped<IDocumentService<TPurPoOrder, TPurPoOrderEntry, PurchaseOrderListDto, PurchaseOrderDetailDto, CreatePurchaseOrderRequest, UpdatePurchaseOrderRequest>, PurchaseOrderService>();
        services.AddScoped<IDocumentService<TPurReceive, TPurReceiveEntry, ReceiveNoticeListDto, ReceiveNoticeDetailDto, CreateReceiveNoticeRequest, UpdateReceiveNoticeRequest>, ReceiveNoticeService>();
        // 退料申请单（一主一从 T_PUR_MRAPP / T_PUR_MRAPPENTRY）
        services.AddScoped<IDocumentService<TPurMrApp, TPurMrAppEntry, MaterialReturnApplyListDto, MaterialReturnApplyDetailDto, CreateMaterialReturnApplyRequest, UpdateMaterialReturnApplyRequest>, MaterialReturnApplyService>();
        // 采购入库单（一主三从 ENTRY/ENTRY1/ENTRY2 + 扫码 + 源单类型）：暴露专有接口（含扫码/源单类型），其本身亦实现通用单据契约
        services.AddScoped<IInStockService, InStockService>();
        services.AddScoped<IDocumentService<TSalOrder, TSalOrderentry, SalesOrderListDto, SalesOrderDetailDto, CreateSalesOrderRequest, UpdateSalesOrderRequest>, SalesOrderService>();
        services.AddScoped<IDocumentService<OdkSrmDelivery, OdkSrmDeliveryEntry, DeliveryListDto, DeliveryDetailDto, CreateDeliveryRequest, UpdateDeliveryRequest>, DeliveryService>();
        services.AddScoped<IDocumentService<OdkSrmInvoice, OdkSrmInvoiceEntry, InvoiceListDto, InvoiceDetailDto, CreateInvoiceRequest, UpdateInvoiceRequest>, InvoiceService>();

        // 采购标签打印（采购订单 / 收料通知单）：非单据 CRUD，独立服务（条码生成/作废/查询）
        services.AddScoped<IPurchaseOrderLabelService, PurchaseOrderLabelService>();
        services.AddScoped<IReceiveNoticeLabelService, ReceiveNoticeLabelService>();

        // 编码规则：业务表单目录（数据驱动）+ 取号引擎（单据编号/条码编号统一取号）+ 配置维护（系统管理→编码规则）
        services.AddScoped<IBillCodeFormCatalog, BillCodeFormCatalog>();
        services.AddScoped<IBillCodeService, BillCodeService>();
        services.AddScoped<IBillCodeRuleService, BillCodeRuleService>();

        // 数据种子服务
        services.AddScoped<DataSeedService>();

        // 演示数据种子服务（可拆卸，通过配置开关控制）
        services.AddScoped<DemoDataSeedService>();

        return services;
    }
}
