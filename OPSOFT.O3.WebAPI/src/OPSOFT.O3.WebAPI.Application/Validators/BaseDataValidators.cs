using FluentValidation;
using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Validators;

// ===== 客户 =====

public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator()
    {
        RuleFor(x => x.FNumber).NotEmpty().WithMessage("编码不能为空").MaximumLength(50);
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FShortName).MaximumLength(200);
        RuleFor(x => x.FContact).MaximumLength(100);
        RuleFor(x => x.FPhone).MaximumLength(50);
        RuleFor(x => x.FAddress).MaximumLength(500);
        RuleFor(x => x.FEmail).MaximumLength(200);
        RuleFor(x => x.FNote).MaximumLength(500);
    }
}

public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerRequestValidator()
    {
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FShortName).MaximumLength(200);
        RuleFor(x => x.FContact).MaximumLength(100);
        RuleFor(x => x.FPhone).MaximumLength(50);
        RuleFor(x => x.FAddress).MaximumLength(500);
        RuleFor(x => x.FEmail).MaximumLength(200);
        RuleFor(x => x.FNote).MaximumLength(500);
    }
}

// ===== 供应商 =====

public class CreateSupplierRequestValidator : AbstractValidator<CreateSupplierRequest>
{
    public CreateSupplierRequestValidator()
    {
        RuleFor(x => x.FNumber).NotEmpty().WithMessage("编码不能为空").MaximumLength(50);
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FContact).MaximumLength(100);
        RuleFor(x => x.FPhone).MaximumLength(50);
        RuleFor(x => x.FAddress).MaximumLength(500);
        RuleFor(x => x.FNote).MaximumLength(500);
    }
}

public class UpdateSupplierRequestValidator : AbstractValidator<UpdateSupplierRequest>
{
    public UpdateSupplierRequestValidator()
    {
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FContact).MaximumLength(100);
        RuleFor(x => x.FPhone).MaximumLength(50);
        RuleFor(x => x.FAddress).MaximumLength(500);
        RuleFor(x => x.FNote).MaximumLength(500);
    }
}

// ===== 仓库 =====

public class CreateWarehouseRequestValidator : AbstractValidator<CreateWarehouseRequest>
{
    public CreateWarehouseRequestValidator()
    {
        RuleFor(x => x.FNumber).NotEmpty().WithMessage("编码不能为空").MaximumLength(50);
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FDescription).MaximumLength(500);
        RuleFor(x => x.FPrincipal).MaximumLength(100);
        RuleFor(x => x.FTel).MaximumLength(50);
        RuleFor(x => x.FAddress).MaximumLength(500);
    }
}

public class UpdateWarehouseRequestValidator : AbstractValidator<UpdateWarehouseRequest>
{
    public UpdateWarehouseRequestValidator()
    {
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FDescription).MaximumLength(500);
        RuleFor(x => x.FPrincipal).MaximumLength(100);
        RuleFor(x => x.FTel).MaximumLength(50);
        RuleFor(x => x.FAddress).MaximumLength(500);
    }
}

// ===== 仓位 =====

public class CreateStockPlaceRequestValidator : AbstractValidator<CreateStockPlaceRequest>
{
    public CreateStockPlaceRequestValidator()
    {
        RuleFor(x => x.FNumber).NotEmpty().WithMessage("编码不能为空").MaximumLength(50);
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FStockId).NotEmpty().WithMessage("所属仓库不能为空");
        RuleFor(x => x.FDescription).MaximumLength(500);
        RuleFor(x => x.FMaxCapacity).GreaterThanOrEqualTo(0);
    }
}

public class UpdateStockPlaceRequestValidator : AbstractValidator<UpdateStockPlaceRequest>
{
    public UpdateStockPlaceRequestValidator()
    {
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FDescription).MaximumLength(500);
        RuleFor(x => x.FMaxCapacity).GreaterThanOrEqualTo(0);
    }
}

// ===== 员工 =====

public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
{
    public CreateEmployeeRequestValidator()
    {
        RuleFor(x => x.FNumber).NotEmpty().WithMessage("编码不能为空").MaximumLength(50);
        RuleFor(x => x.FName).NotEmpty().WithMessage("姓名不能为空").MaximumLength(200);
        RuleFor(x => x.FTel).MaximumLength(50);
        RuleFor(x => x.FMail).MaximumLength(200);
        RuleFor(x => x.FNote).MaximumLength(500);
        RuleFor(x => x.FAddress).MaximumLength(500);
    }
}

public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
{
    public UpdateEmployeeRequestValidator()
    {
        RuleFor(x => x.FName).NotEmpty().WithMessage("姓名不能为空").MaximumLength(200);
        RuleFor(x => x.FTel).MaximumLength(50);
        RuleFor(x => x.FMail).MaximumLength(200);
        RuleFor(x => x.FNote).MaximumLength(500);
        RuleFor(x => x.FAddress).MaximumLength(500);
    }
}

// ===== 物料 =====

public class CreateMaterialRequestValidator : AbstractValidator<CreateMaterialRequest>
{
    public CreateMaterialRequestValidator()
    {
        RuleFor(x => x.FNumber).NotEmpty().WithMessage("编码不能为空").MaximumLength(50);
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FSpecification).MaximumLength(200);
        RuleFor(x => x.FDescription).MaximumLength(500);
        RuleFor(x => x.FBarcode).MaximumLength(100);
        RuleFor(x => x.FMaxQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FSafeQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FNetWeight).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FGrossWeight).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FMinPoQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FIncreaseQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FKfPeriod).GreaterThanOrEqualTo(0);
    }
}

public class UpdateMaterialRequestValidator : AbstractValidator<UpdateMaterialRequest>
{
    public UpdateMaterialRequestValidator()
    {
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FSpecification).MaximumLength(200);
        RuleFor(x => x.FDescription).MaximumLength(500);
        RuleFor(x => x.FBarcode).MaximumLength(100);
        RuleFor(x => x.FMaxQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FSafeQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FNetWeight).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FGrossWeight).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FMinPoQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FIncreaseQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FKfPeriod).GreaterThanOrEqualTo(0);
    }
}

// ===== 物料条码类型 =====

public class CreateMaterialBarTypeRequestValidator : AbstractValidator<CreateMaterialBarTypeRequest>
{
    public CreateMaterialBarTypeRequestValidator()
    {
        RuleFor(x => x.Fmaterialnumber).NotEmpty().WithMessage("物料编码不能为空").MaximumLength(50);
        RuleFor(x => x.Fmaterialname).MaximumLength(200);
    }
}

public class UpdateMaterialBarTypeRequestValidator : AbstractValidator<UpdateMaterialBarTypeRequest>
{
    public UpdateMaterialBarTypeRequestValidator()
    {
        RuleFor(x => x.Fmaterialnumber).MaximumLength(50);
        RuleFor(x => x.Fmaterialname).MaximumLength(200);
    }
}

// ===== 单位 =====

public class CreateUnitRequestValidator : AbstractValidator<CreateUnitRequest>
{
    public CreateUnitRequestValidator()
    {
        RuleFor(x => x.FNumber).NotEmpty().WithMessage("编码不能为空").MaximumLength(50);
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FDescription).MaximumLength(500);
        RuleFor(x => x.FCoefficient).GreaterThan(0).WithMessage("换算系数必须大于0");
        RuleFor(x => x.FPrecision).GreaterThanOrEqualTo(0);
    }
}

public class UpdateUnitRequestValidator : AbstractValidator<UpdateUnitRequest>
{
    public UpdateUnitRequestValidator()
    {
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FDescription).MaximumLength(500);
        RuleFor(x => x.FCoefficient).GreaterThan(0).WithMessage("换算系数必须大于0");
        RuleFor(x => x.FPrecision).GreaterThanOrEqualTo(0);
    }
}

// ===== 币别 =====

public class CreateCurrencyRequestValidator : AbstractValidator<CreateCurrencyRequest>
{
    public CreateCurrencyRequestValidator()
    {
        RuleFor(x => x.FNumber).NotEmpty().WithMessage("编码不能为空").MaximumLength(50);
        RuleFor(x => x.FCode).NotEmpty().WithMessage("货币代码不能为空").MaximumLength(10);
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FDescription).MaximumLength(500);
        RuleFor(x => x.FExchangeRate).GreaterThan(0).WithMessage("汇率必须大于0");
        RuleFor(x => x.FPriceDigits).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FAmountDigits).GreaterThanOrEqualTo(0);
    }
}

public class UpdateCurrencyRequestValidator : AbstractValidator<UpdateCurrencyRequest>
{
    public UpdateCurrencyRequestValidator()
    {
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FCode).MaximumLength(10);
        RuleFor(x => x.FDescription).MaximumLength(500);
        RuleFor(x => x.FExchangeRate).GreaterThan(0).WithMessage("汇率必须大于0");
        RuleFor(x => x.FPriceDigits).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FAmountDigits).GreaterThanOrEqualTo(0);
    }
}

// ===== 部门 =====

public class CreateDepartmentRequestValidator : AbstractValidator<CreateDepartmentRequest>
{
    public CreateDepartmentRequestValidator()
    {
        RuleFor(x => x.FNumber).NotEmpty().WithMessage("编码不能为空").MaximumLength(50);
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FFullName).MaximumLength(500);
        RuleFor(x => x.FDescription).MaximumLength(500);
        RuleFor(x => x.FHelpCode).MaximumLength(50);
    }
}

public class UpdateDepartmentRequestValidator : AbstractValidator<UpdateDepartmentRequest>
{
    public UpdateDepartmentRequestValidator()
    {
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FFullName).MaximumLength(500);
        RuleFor(x => x.FDescription).MaximumLength(500);
        RuleFor(x => x.FHelpCode).MaximumLength(50);
    }
}

// ===== 辅助资料类别 =====

public class CreateAssistantDataRequestValidator : AbstractValidator<CreateAssistantDataRequest>
{
    public CreateAssistantDataRequestValidator()
    {
        RuleFor(x => x.Fnumber).NotEmpty().WithMessage("编码不能为空").MaximumLength(50);
        RuleFor(x => x.Fname).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.Fdescription).MaximumLength(500);
    }
}

public class UpdateAssistantDataRequestValidator : AbstractValidator<UpdateAssistantDataRequest>
{
    public UpdateAssistantDataRequestValidator()
    {
        RuleFor(x => x.Fname).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.Fdescription).MaximumLength(500);
    }
}

// ===== 辅助资料明细 =====

public class CreateAssistantDataEntryRequestValidator : AbstractValidator<CreateAssistantDataEntryRequest>
{
    public CreateAssistantDataEntryRequestValidator()
    {
        RuleFor(x => x.Fnumber).NotEmpty().WithMessage("编码不能为空").MaximumLength(50);
        RuleFor(x => x.Fdatavalue).NotEmpty().WithMessage("辅助资料名称不能为空").MaximumLength(200);
        RuleFor(x => x.Fid).NotEmpty().WithMessage("辅助资料类别不能为空");
        RuleFor(x => x.Fdescription).MaximumLength(500);
    }
}

public class UpdateAssistantDataEntryRequestValidator : AbstractValidator<UpdateAssistantDataEntryRequest>
{
    public UpdateAssistantDataEntryRequestValidator()
    {
        RuleFor(x => x.Fdatavalue).NotEmpty().WithMessage("辅助资料名称不能为空").MaximumLength(200);
        RuleFor(x => x.Fdescription).MaximumLength(500);
    }
}

// ===== 基础资料分组 =====

public class CreateBaseDataGroupRequestValidator : AbstractValidator<CreateBaseDataGroupRequest>
{
    public CreateBaseDataGroupRequestValidator()
    {
        RuleFor(x => x.FNumber).NotEmpty().WithMessage("编码不能为空").MaximumLength(50);
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FCName).MaximumLength(200);
        RuleFor(x => x.FTName).MaximumLength(200);
        RuleFor(x => x.FEName).MaximumLength(200);
        RuleFor(x => x.FNote).MaximumLength(500);
    }
}

public class UpdateBaseDataGroupRequestValidator : AbstractValidator<UpdateBaseDataGroupRequest>
{
    public UpdateBaseDataGroupRequestValidator()
    {
        RuleFor(x => x.FName).NotEmpty().WithMessage("名称不能为空").MaximumLength(200);
        RuleFor(x => x.FCName).MaximumLength(200);
        RuleFor(x => x.FTName).MaximumLength(200);
        RuleFor(x => x.FEName).MaximumLength(200);
        RuleFor(x => x.FNote).MaximumLength(500);
    }
}

// ===== 用户 =====

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("用户ID不能为空").MaximumLength(50);
        RuleFor(x => x.UserName).NotEmpty().WithMessage("用户名不能为空").MaximumLength(200);
        RuleFor(x => x.Password).NotEmpty().WithMessage("密码不能为空").MinimumLength(6).WithMessage("密码长度不能少于6位");
        RuleFor(x => x.Email).MaximumLength(200);
    }
}

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("用户名不能为空").MaximumLength(200);
        RuleFor(x => x.Email).MaximumLength(200);
    }
}

// ===== 角色 =====

public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
{
    public CreateRoleRequestValidator()
    {
        RuleFor(x => x.RoleNumber).NotEmpty().WithMessage("角色代码不能为空").MaximumLength(50);
        RuleFor(x => x.RoleName).NotEmpty().WithMessage("角色名称不能为空").MaximumLength(200);
        RuleFor(x => x.Note).MaximumLength(500);
    }
}

public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
{
    public UpdateRoleRequestValidator()
    {
        RuleFor(x => x.RoleName).NotEmpty().WithMessage("角色名称不能为空").MaximumLength(200);
        RuleFor(x => x.Note).MaximumLength(500);
    }
}

// ===== 菜单 =====

public class CreateMenuRequestValidator : AbstractValidator<CreateMenuRequest>
{
    public CreateMenuRequestValidator()
    {
        RuleFor(x => x.MenuName).NotEmpty().WithMessage("菜单名称不能为空").MaximumLength(200);
        RuleFor(x => x.MenuType).NotEmpty().WithMessage("菜单类型不能为空").MaximumLength(50);
        RuleFor(x => x.RoutePath).MaximumLength(500);
        RuleFor(x => x.Icon).MaximumLength(100);
        RuleFor(x => x.PermCode).MaximumLength(100);
    }
}

public class UpdateMenuRequestValidator : AbstractValidator<UpdateMenuRequest>
{
    public UpdateMenuRequestValidator()
    {
        RuleFor(x => x.MenuName).NotEmpty().WithMessage("菜单名称不能为空").MaximumLength(200);
        RuleFor(x => x.MenuType).NotEmpty().WithMessage("菜单类型不能为空").MaximumLength(50);
        RuleFor(x => x.RoutePath).MaximumLength(500);
        RuleFor(x => x.Icon).MaximumLength(100);
        RuleFor(x => x.PermCode).MaximumLength(100);
    }
}

// ===== 登录 =====

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("用户ID不能为空").MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().WithMessage("密码不能为空");
    }
}

// ===== 修改密码 =====

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.OldPassword).NotEmpty().WithMessage("旧密码不能为空");
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage("新密码不能为空").MinimumLength(6).WithMessage("密码长度不能少于6位");
    }
}

// ===== 重置密码 =====

public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("用户ID不能为空");
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage("新密码不能为空").MinimumLength(6).WithMessage("密码长度不能少于6位");
    }
}

// ===== 分配角色 =====

public class AssignRolesRequestValidator : AbstractValidator<AssignRolesRequest>
{
    public AssignRolesRequestValidator()
    {
        RuleFor(x => x.RoleIds).NotEmpty().WithMessage("角色ID列表不能为空");
    }
}

// ===== 分配权限 =====

public class AssignPermissionsRequestValidator : AbstractValidator<AssignPermissionsRequest>
{
    public AssignPermissionsRequestValidator()
    {
        RuleFor(x => x.PermissionCodes).NotEmpty().WithMessage("权限代码列表不能为空");
    }
}

// ===== 刷新Token =====

public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("RefreshToken不能为空");
    }
}
