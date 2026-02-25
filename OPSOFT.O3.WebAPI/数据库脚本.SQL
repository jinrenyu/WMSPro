/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_BARCODEENTRY 条码编码规则表体档*/ 
 -- DROP TABLE SYS_BARCODEENTRY
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_BARCODEENTRY') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_BARCODEENTRY(
   FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 规则代码
   FNOTE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 描述
   FTYPE     VARCHAR(2) NOT NULL DEFAULT '' ,    -- 类型( 1=常量, 2=文本字段 3= 日期字段 4=流水号 5=数值字段)
   FIELDID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 元素来源字段
   FIELDNAME     NVARCHAR(100) NOT NULL DEFAULT '' ,    -- 元素来源名称
   FVALUE     NVARCHAR(50) NOT NULL DEFAULT '' ,    -- 设置值
   FLENGHT     INT NOT NULL DEFAULT 0 ,    -- 长度
   FMIN     INT NOT NULL DEFAULT 0 ,    -- 起始流水号
   FMAX     INT NOT NULL DEFAULT 0 ,    -- 最大流水号(0表示不限制)
   FSTEP     INT NOT NULL DEFAULT 0 ,    -- 流水号步长
   FORMATSTRING     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 字串时 可选 L=小写 U =大写 及日期型格式
   FCHAR     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 补位符
   FCHARALIGNMENT     VARCHAR(1) NOT NULL DEFAULT '' ,    -- L=左边补位;R=右边补位
   FCODECONTRAST     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 编码对照表
   ISSERIAL     BIT NOT NULL DEFAULT  0 ,    -- 编码依据
   ISMEMBER     BIT NOT NULL DEFAULT  0 ,    -- 编码元素
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_BARCODEENTRY] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FCLASSTYPEID' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 规则代码
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FNOTE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 描述
if(not exists(select name from SysColumns where name ='FTYPE' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FTYPE     VARCHAR(2) NOT NULL DEFAULT ''     -- 类型( 1=常量, 2=文本字段 3= 日期字段 4=流水号 5=数值字段)
if(not exists(select name from SysColumns where name ='FIELDID' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FIELDID     VARCHAR(50) NOT NULL DEFAULT ''     -- 元素来源字段
if(not exists(select name from SysColumns where name ='FIELDNAME' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FIELDNAME     NVARCHAR(100) NOT NULL DEFAULT ''     -- 元素来源名称
if(not exists(select name from SysColumns where name ='FVALUE' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FVALUE     NVARCHAR(50) NOT NULL DEFAULT ''     -- 设置值
if(not exists(select name from SysColumns where name ='FLENGHT' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FLENGHT     INT NOT NULL DEFAULT 0     -- 长度
if(not exists(select name from SysColumns where name ='FMIN' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FMIN     INT NOT NULL DEFAULT 0     -- 起始流水号
if(not exists(select name from SysColumns where name ='FMAX' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FMAX     INT NOT NULL DEFAULT 0     -- 最大流水号(0表示不限制)
if(not exists(select name from SysColumns where name ='FSTEP' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FSTEP     INT NOT NULL DEFAULT 0     -- 流水号步长
if(not exists(select name from SysColumns where name ='FORMATSTRING' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FORMATSTRING     VARCHAR(50) NOT NULL DEFAULT ''     -- 字串时 可选 L=小写 U =大写 及日期型格式
if(not exists(select name from SysColumns where name ='FCHAR' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FCHAR     VARCHAR(1) NOT NULL DEFAULT ''     -- 补位符
if(not exists(select name from SysColumns where name ='FCHARALIGNMENT' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FCHARALIGNMENT     VARCHAR(1) NOT NULL DEFAULT ''     -- L=左边补位;R=右边补位
if(not exists(select name from SysColumns where name ='FCODECONTRAST' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FCODECONTRAST     VARCHAR(50) NOT NULL DEFAULT ''     -- 编码对照表
if(not exists(select name from SysColumns where name ='ISSERIAL' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    ISSERIAL     BIT NOT NULL DEFAULT  0     -- 编码依据
if(not exists(select name from SysColumns where name ='ISMEMBER' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    ISMEMBER     BIT NOT NULL DEFAULT  0     -- 编码元素
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_BARCODEENTRY'))) 
     Alter Table SYS_BARCODEENTRY add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BARCODEENTRY') AND name = N'SYS_BARCODEENTRY_1' ) 
   Drop Index SYS_BARCODEENTRY.SYS_BARCODEENTRY_1
GO
Create  unique Clustered index SYS_BARCODEENTRY_1 on SYS_BARCODEENTRY(FCLASSTYPEID,FENTRYID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_ENANNOU 企业新闻公告*/ 
 -- DROP TABLE SYS_ENANNOU
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_ENANNOU') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_ENANNOU(
   FNAME     NVARCHAR(50) NOT NULL DEFAULT '' ,    -- 公告主题
   FCONTENT     TEXT NOT NULL DEFAULT ''  ,    -- 公告内容
   FISOUTALL     BIT NOT NULL DEFAULT  0 ,    -- 是否外部公开
   FISINALL     BIT NOT NULL DEFAULT  0 ,    -- 是否内部公开
   FNOTE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 备注
   FIMAGE     IMAGE NULL  ,    -- 新闻公告图片
   FCHECKERID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 发布人
   FCHECKDATE     DATE NULL  ,    -- 发布日期
   FOASTATUS     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 审批状态
   FOARESULT     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 审批结果
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FBILLNO	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 单据编号 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_ENANNOU] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_ENANNOU'))) 
     Alter Table SYS_ENANNOU add    FNAME     NVARCHAR(50) NOT NULL DEFAULT ''     -- 公告主题
if(not exists(select name from SysColumns where name ='FCONTENT' and id= OBJECT_ID('SYS_ENANNOU'))) 
     Alter Table SYS_ENANNOU add    FCONTENT     TEXT NOT NULL DEFAULT ''      -- 公告内容
if(not exists(select name from SysColumns where name ='FISOUTALL' and id= OBJECT_ID('SYS_ENANNOU'))) 
     Alter Table SYS_ENANNOU add    FISOUTALL     BIT NOT NULL DEFAULT  0     -- 是否外部公开
if(not exists(select name from SysColumns where name ='FISINALL' and id= OBJECT_ID('SYS_ENANNOU'))) 
     Alter Table SYS_ENANNOU add    FISINALL     BIT NOT NULL DEFAULT  0     -- 是否内部公开
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_ENANNOU'))) 
     Alter Table SYS_ENANNOU add    FNOTE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 备注
if(not exists(select name from SysColumns where name ='FIMAGE' and id= OBJECT_ID('SYS_ENANNOU'))) 
     Alter Table SYS_ENANNOU add    FIMAGE     IMAGE NULL      -- 新闻公告图片
if(not exists(select name from SysColumns where name ='FCHECKERID' and id= OBJECT_ID('SYS_ENANNOU'))) 
     Alter Table SYS_ENANNOU add    FCHECKERID     VARCHAR(50) NOT NULL DEFAULT ''     -- 发布人
if(not exists(select name from SysColumns where name ='FCHECKDATE' and id= OBJECT_ID('SYS_ENANNOU'))) 
     Alter Table SYS_ENANNOU add    FCHECKDATE     DATE NULL      -- 发布日期
if(not exists(select name from SysColumns where name ='FOASTATUS' and id= OBJECT_ID('SYS_ENANNOU'))) 
     Alter Table SYS_ENANNOU add    FOASTATUS     VARCHAR(50) NOT NULL DEFAULT ''     -- 审批状态
if(not exists(select name from SysColumns where name ='FOARESULT' and id= OBJECT_ID('SYS_ENANNOU'))) 
     Alter Table SYS_ENANNOU add    FOARESULT     VARCHAR(50) NOT NULL DEFAULT ''     -- 审批结果
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_ENANNOU'))) 
     Alter Table SYS_ENANNOU add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_ENANNOU'))) 
     Alter Table SYS_ENANNOU add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FBILLNO' and id= OBJECT_ID('SYS_ENANNOU'))) 
     Alter Table SYS_ENANNOU add    FBILLNO     VARCHAR(50) NOT NULL DEFAULT ''      -- 单据编号
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_ENANNOU'))) 
     Alter Table SYS_ENANNOU add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_ENANNOU') AND name = N'SYS_ENANNOU_1' ) 
   Drop Index SYS_ENANNOU.SYS_ENANNOU_1
GO
Create  unique Clustered index SYS_ENANNOU_1 on SYS_ENANNOU(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_ENANNOU') AND name = N'SYS_ENANNOU_2' ) 
   Drop Index SYS_ENANNOU.SYS_ENANNOU_2
GO
Create  unique index SYS_ENANNOU_2 on SYS_ENANNOU(FBILLNO)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_USERLOCKRECORD 用户锁定记录*/ 
 -- DROP TABLE SYS_USERLOCKRECORD
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_USERLOCKRECORD') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_USERLOCKRECORD(
   USERID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 锁定用户ID
   USERNAME     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 用户名称
   UNLOCKTIME     DATETIME NULL  ,    -- 解锁激活时间
   LOCKSTATUS     INT NOT NULL DEFAULT 0 ,    -- 锁定状态
   UNLOCKUSER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 解锁人
   LOCKIP     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 锁定时操作的机器IP
   MACHINENAME     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 锁定时操作的机器名称
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_USERLOCKRECORD] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='USERID' and id= OBJECT_ID('SYS_USERLOCKRECORD'))) 
     Alter Table SYS_USERLOCKRECORD add    USERID     VARCHAR(50) NOT NULL DEFAULT ''     -- 锁定用户ID
if(not exists(select name from SysColumns where name ='USERNAME' and id= OBJECT_ID('SYS_USERLOCKRECORD'))) 
     Alter Table SYS_USERLOCKRECORD add    USERNAME     VARCHAR(50) NOT NULL DEFAULT ''     -- 用户名称
if(not exists(select name from SysColumns where name ='UNLOCKTIME' and id= OBJECT_ID('SYS_USERLOCKRECORD'))) 
     Alter Table SYS_USERLOCKRECORD add    UNLOCKTIME     DATETIME NULL      -- 解锁激活时间
if(not exists(select name from SysColumns where name ='LOCKSTATUS' and id= OBJECT_ID('SYS_USERLOCKRECORD'))) 
     Alter Table SYS_USERLOCKRECORD add    LOCKSTATUS     INT NOT NULL DEFAULT 0     -- 锁定状态
if(not exists(select name from SysColumns where name ='UNLOCKUSER' and id= OBJECT_ID('SYS_USERLOCKRECORD'))) 
     Alter Table SYS_USERLOCKRECORD add    UNLOCKUSER     VARCHAR(50) NOT NULL DEFAULT ''     -- 解锁人
if(not exists(select name from SysColumns where name ='LOCKIP' and id= OBJECT_ID('SYS_USERLOCKRECORD'))) 
     Alter Table SYS_USERLOCKRECORD add    LOCKIP     VARCHAR(50) NOT NULL DEFAULT ''     -- 锁定时操作的机器IP
if(not exists(select name from SysColumns where name ='MACHINENAME' and id= OBJECT_ID('SYS_USERLOCKRECORD'))) 
     Alter Table SYS_USERLOCKRECORD add    MACHINENAME     VARCHAR(50) NOT NULL DEFAULT ''     -- 锁定时操作的机器名称
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_USERLOCKRECORD'))) 
     Alter Table SYS_USERLOCKRECORD add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_USERLOCKRECORD'))) 
     Alter Table SYS_USERLOCKRECORD add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_USERLOCKRECORD'))) 
     Alter Table SYS_USERLOCKRECORD add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO



GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_BILLTEMPLATE 系统表单信息表*/ 
 -- DROP TABLE SYS_BILLTEMPLATE
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_BILLTEMPLATE') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_BILLTEMPLATE(
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 表单代码
   FBILLTITLE     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 单据标题
   FLISTTITLE     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 序时簿标题
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 表单名称
   FDISPLAYCOLUMN     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 表单标识字段
   FCNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 表单名称(第一语言)
   FTNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 表单名称(第二语言)
   FENAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 表单名称(第三语言)
   FTABLEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 主表内码
   FBILLNOFILED     VARCHAR(50) NOT NULL DEFAULT 'FBILLNO' ,    -- 单据编号字段
   FASNAME     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 程序集名称
   FIMAGE     IMAGE NULL  ,    -- 图片
   FASBILLNAME     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 单据类
   FWEBBILL     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 单据URL(WEB)
   FWEBLIST     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 序时簿URL(WEB)
   FASLISTNAME     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 序时簿类
   FMAXROW     INT NOT NULL DEFAULT 0 ,    -- 显示最大行数
   FPAGESIZE     INT NOT NULL DEFAULT 0 ,    -- 每页显示行数
   FISFILTERABLE     BIT NOT NULL DEFAULT  1 ,    -- 是否启用过滤
   FISENABLEOR     BIT NOT NULL DEFAULT  0 ,    -- 启用组织
   FGROUPKEY     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 分组标识
   FISENABLEGROUP     BIT NOT NULL DEFAULT  0 ,    -- 启用分组
   FVERIFYBEFORE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 审核前置状态
   FREVERIFYBEFORE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 反审核前置状态
   FREVERIFYSTATUS     INT NOT NULL DEFAULT 0 ,    -- 反审核后状态
   FSUREBEFORE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 确认前置状态
   FRESUREBEFORE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 反确认前置状态
   FRESURESTATUS     INT NOT NULL DEFAULT 0 ,    -- 反确认后状态
   FSUBMITBEFORE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 提交前置状态
   FRESUBMITBEFORE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 反提交前置状态
   FRESUBMITSTATUS     INT NOT NULL DEFAULT 0 ,    -- 反提交后状态
   FREVIEWBEFORE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 复核前置状态
   FREREVIEWBEFORE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 反复核前置状态
   FREREVIEWSTATUS     INT NOT NULL DEFAULT 0 ,    -- 反复核后状态
   FFINALBEFORE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 结案前置状态
   FREFINALBEFORE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 反结案前置状态
   FREFINALSTATUS     INT NOT NULL DEFAULT 0 ,    -- 反结案后状态
   FCLOSEBEFORE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 关闭前置状态
   FRECLOSEBEFORE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 反关闭前置状态
   FRECLOSESTATUS     INT NOT NULL DEFAULT 0 ,    -- 反关闭后状态
   FFAILEDBEFORE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 作废前置状态
   FREFAILEDBEFORE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 反作废前置状态
   FREFAILEDSTATUS     INT NOT NULL DEFAULT 0 ,    -- 反作废后状态
   FCUSTLISTUI     BIT NOT NULL DEFAULT  0 ,    -- 自定义序时簿界面
   FXMLLISTUI     TEXT NOT NULL DEFAULT ''  ,    -- 序时簿界面代码
   FUXMLLISTUI     TEXT NOT NULL DEFAULT ''  ,    -- 自定义序时簿界面代码
   FCUSTBILLUI     BIT NOT NULL DEFAULT  0 ,    -- 自定义单据界面
   FUXMLUI     TEXT NOT NULL DEFAULT ''  ,    -- 自定义单据界面代码
   FXMLUI     TEXT NOT NULL DEFAULT ''  ,    -- 单据界面代码
   FEXISTLIST     VARCHAR(255) NOT NULL DEFAULT '' ,    -- 判断重复字段
   FHEADKEY     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 序时簿和单据关联字段
   FENABLEAUDITLOG     BIT NOT NULL DEFAULT  0 ,    -- 启用审计追踪
   FENABLESIGNATURE     BIT NOT NULL DEFAULT  0 ,    -- 启用电子签
   FSYSBILLTYPE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 系统单据类型
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_BILLTEMPLATE] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 表单代码
if(not exists(select name from SysColumns where name ='FBILLTITLE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FBILLTITLE     NVARCHAR(200) NOT NULL DEFAULT ''     -- 单据标题
if(not exists(select name from SysColumns where name ='FLISTTITLE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FLISTTITLE     NVARCHAR(200) NOT NULL DEFAULT ''     -- 序时簿标题
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 表单名称
if(not exists(select name from SysColumns where name ='FDISPLAYCOLUMN' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FDISPLAYCOLUMN     VARCHAR(50) NOT NULL DEFAULT ''     -- 表单标识字段
if(not exists(select name from SysColumns where name ='FCNAME' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FCNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 表单名称(第一语言)
if(not exists(select name from SysColumns where name ='FTNAME' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FTNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 表单名称(第二语言)
if(not exists(select name from SysColumns where name ='FENAME' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FENAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 表单名称(第三语言)
if(not exists(select name from SysColumns where name ='FTABLEID' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FTABLEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 主表内码
if(not exists(select name from SysColumns where name ='FBILLNOFILED' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FBILLNOFILED     VARCHAR(50) NOT NULL DEFAULT 'FBILLNO'     -- 单据编号字段
if(not exists(select name from SysColumns where name ='FASNAME' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FASNAME     VARCHAR(200) NOT NULL DEFAULT ''     -- 程序集名称
if(not exists(select name from SysColumns where name ='FIMAGE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FIMAGE     IMAGE NULL      -- 图片
if(not exists(select name from SysColumns where name ='FASBILLNAME' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FASBILLNAME     VARCHAR(200) NOT NULL DEFAULT ''     -- 单据类
if(not exists(select name from SysColumns where name ='FWEBBILL' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FWEBBILL     VARCHAR(200) NOT NULL DEFAULT ''     -- 单据URL(WEB)
if(not exists(select name from SysColumns where name ='FWEBLIST' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FWEBLIST     VARCHAR(200) NOT NULL DEFAULT ''     -- 序时簿URL(WEB)
if(not exists(select name from SysColumns where name ='FASLISTNAME' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FASLISTNAME     VARCHAR(200) NOT NULL DEFAULT ''     -- 序时簿类
if(not exists(select name from SysColumns where name ='FMAXROW' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FMAXROW     INT NOT NULL DEFAULT 0     -- 显示最大行数
if(not exists(select name from SysColumns where name ='FPAGESIZE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FPAGESIZE     INT NOT NULL DEFAULT 0     -- 每页显示行数
if(not exists(select name from SysColumns where name ='FISFILTERABLE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FISFILTERABLE     BIT NOT NULL DEFAULT  1     -- 是否启用过滤
if(not exists(select name from SysColumns where name ='FISENABLEOR' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FISENABLEOR     BIT NOT NULL DEFAULT  0     -- 启用组织
if(not exists(select name from SysColumns where name ='FGROUPKEY' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FGROUPKEY     VARCHAR(50) NOT NULL DEFAULT ''     -- 分组标识
if(not exists(select name from SysColumns where name ='FISENABLEGROUP' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FISENABLEGROUP     BIT NOT NULL DEFAULT  0     -- 启用分组
if(not exists(select name from SysColumns where name ='FVERIFYBEFORE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FVERIFYBEFORE     VARCHAR(50) NOT NULL DEFAULT ''     -- 审核前置状态
if(not exists(select name from SysColumns where name ='FREVERIFYBEFORE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FREVERIFYBEFORE     VARCHAR(50) NOT NULL DEFAULT ''     -- 反审核前置状态
if(not exists(select name from SysColumns where name ='FREVERIFYSTATUS' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FREVERIFYSTATUS     INT NOT NULL DEFAULT 0     -- 反审核后状态
if(not exists(select name from SysColumns where name ='FSUREBEFORE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FSUREBEFORE     VARCHAR(50) NOT NULL DEFAULT ''     -- 确认前置状态
if(not exists(select name from SysColumns where name ='FRESUREBEFORE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FRESUREBEFORE     VARCHAR(50) NOT NULL DEFAULT ''     -- 反确认前置状态
if(not exists(select name from SysColumns where name ='FRESURESTATUS' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FRESURESTATUS     INT NOT NULL DEFAULT 0     -- 反确认后状态
if(not exists(select name from SysColumns where name ='FSUBMITBEFORE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FSUBMITBEFORE     VARCHAR(50) NOT NULL DEFAULT ''     -- 提交前置状态
if(not exists(select name from SysColumns where name ='FRESUBMITBEFORE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FRESUBMITBEFORE     VARCHAR(50) NOT NULL DEFAULT ''     -- 反提交前置状态
if(not exists(select name from SysColumns where name ='FRESUBMITSTATUS' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FRESUBMITSTATUS     INT NOT NULL DEFAULT 0     -- 反提交后状态
if(not exists(select name from SysColumns where name ='FREVIEWBEFORE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FREVIEWBEFORE     VARCHAR(50) NOT NULL DEFAULT ''     -- 复核前置状态
if(not exists(select name from SysColumns where name ='FREREVIEWBEFORE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FREREVIEWBEFORE     VARCHAR(50) NOT NULL DEFAULT ''     -- 反复核前置状态
if(not exists(select name from SysColumns where name ='FREREVIEWSTATUS' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FREREVIEWSTATUS     INT NOT NULL DEFAULT 0     -- 反复核后状态
if(not exists(select name from SysColumns where name ='FFINALBEFORE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FFINALBEFORE     VARCHAR(50) NOT NULL DEFAULT ''     -- 结案前置状态
if(not exists(select name from SysColumns where name ='FREFINALBEFORE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FREFINALBEFORE     VARCHAR(50) NOT NULL DEFAULT ''     -- 反结案前置状态
if(not exists(select name from SysColumns where name ='FREFINALSTATUS' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FREFINALSTATUS     INT NOT NULL DEFAULT 0     -- 反结案后状态
if(not exists(select name from SysColumns where name ='FCLOSEBEFORE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FCLOSEBEFORE     VARCHAR(50) NOT NULL DEFAULT ''     -- 关闭前置状态
if(not exists(select name from SysColumns where name ='FRECLOSEBEFORE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FRECLOSEBEFORE     VARCHAR(50) NOT NULL DEFAULT ''     -- 反关闭前置状态
if(not exists(select name from SysColumns where name ='FRECLOSESTATUS' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FRECLOSESTATUS     INT NOT NULL DEFAULT 0     -- 反关闭后状态
if(not exists(select name from SysColumns where name ='FFAILEDBEFORE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FFAILEDBEFORE     VARCHAR(50) NOT NULL DEFAULT ''     -- 作废前置状态
if(not exists(select name from SysColumns where name ='FREFAILEDBEFORE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FREFAILEDBEFORE     VARCHAR(50) NOT NULL DEFAULT ''     -- 反作废前置状态
if(not exists(select name from SysColumns where name ='FREFAILEDSTATUS' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FREFAILEDSTATUS     INT NOT NULL DEFAULT 0     -- 反作废后状态
if(not exists(select name from SysColumns where name ='FCUSTLISTUI' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FCUSTLISTUI     BIT NOT NULL DEFAULT  0     -- 自定义序时簿界面
if(not exists(select name from SysColumns where name ='FXMLLISTUI' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FXMLLISTUI     TEXT NOT NULL DEFAULT ''      -- 序时簿界面代码
if(not exists(select name from SysColumns where name ='FUXMLLISTUI' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FUXMLLISTUI     TEXT NOT NULL DEFAULT ''      -- 自定义序时簿界面代码
if(not exists(select name from SysColumns where name ='FCUSTBILLUI' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FCUSTBILLUI     BIT NOT NULL DEFAULT  0     -- 自定义单据界面
if(not exists(select name from SysColumns where name ='FUXMLUI' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FUXMLUI     TEXT NOT NULL DEFAULT ''      -- 自定义单据界面代码
if(not exists(select name from SysColumns where name ='FXMLUI' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FXMLUI     TEXT NOT NULL DEFAULT ''      -- 单据界面代码
if(not exists(select name from SysColumns where name ='FEXISTLIST' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FEXISTLIST     VARCHAR(255) NOT NULL DEFAULT ''     -- 判断重复字段
if(not exists(select name from SysColumns where name ='FHEADKEY' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FHEADKEY     VARCHAR(50) NOT NULL DEFAULT ''     -- 序时簿和单据关联字段
if(not exists(select name from SysColumns where name ='FENABLEAUDITLOG' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FENABLEAUDITLOG     BIT NOT NULL DEFAULT  0     -- 启用审计追踪
if(not exists(select name from SysColumns where name ='FENABLESIGNATURE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FENABLESIGNATURE     BIT NOT NULL DEFAULT  0     -- 启用电子签
if(not exists(select name from SysColumns where name ='FSYSBILLTYPE' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FSYSBILLTYPE     VARCHAR(50) NOT NULL DEFAULT ''     -- 系统单据类型
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_BILLTEMPLATE'))) 
     Alter Table SYS_BILLTEMPLATE add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATE') AND name = N'SYS_BILLTEMPLATE_1' ) 
   Drop Index SYS_BILLTEMPLATE.SYS_BILLTEMPLATE_1
GO
Create  unique index SYS_BILLTEMPLATE_1 on SYS_BILLTEMPLATE(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATE') AND name = N'SYS_BILLTEMPLATE_2' ) 
   Drop Index SYS_BILLTEMPLATE.SYS_BILLTEMPLATE_2
GO
Create  unique index SYS_BILLTEMPLATE_2 on SYS_BILLTEMPLATE(FNUMBER)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_USERFILTERINFO 用户过滤方案*/ 
 -- DROP TABLE SYS_USERFILTERINFO
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_USERFILTERINFO') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_USERFILTERINFO(
   FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 功能代码
   FDESCRIPTION     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 方案名称
   FDEFAULT     BIT NOT NULL DEFAULT  0 ,    -- 是否为默认方案
   FACTIVE     BIT NOT NULL DEFAULT  0 ,    -- 是否为当前方案
   FUSER_ID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 用户帐号
   FDATEINDEX     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 第一过滤索引号
   FVERIFYINDEX     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 第二过滤索引号
   FFAILINDEX     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 第三过滤索引号
   FCLOSEINDEX     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 第四过滤索引号
   FWHENSURESAVE     BIT NOT NULL DEFAULT  0 ,    -- 选择确认时是否保存
   FMAXROW     INT NOT NULL DEFAULT 0 ,    -- 最大显示
   FPAGESIZE     INT NOT NULL DEFAULT 0 ,    -- 每页显示
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_USERFILTERINFO] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FCLASSTYPEID' and id= OBJECT_ID('SYS_USERFILTERINFO'))) 
     Alter Table SYS_USERFILTERINFO add    FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 功能代码
if(not exists(select name from SysColumns where name ='FDESCRIPTION' and id= OBJECT_ID('SYS_USERFILTERINFO'))) 
     Alter Table SYS_USERFILTERINFO add    FDESCRIPTION     NVARCHAR(200) NOT NULL DEFAULT ''     -- 方案名称
if(not exists(select name from SysColumns where name ='FDEFAULT' and id= OBJECT_ID('SYS_USERFILTERINFO'))) 
     Alter Table SYS_USERFILTERINFO add    FDEFAULT     BIT NOT NULL DEFAULT  0     -- 是否为默认方案
if(not exists(select name from SysColumns where name ='FACTIVE' and id= OBJECT_ID('SYS_USERFILTERINFO'))) 
     Alter Table SYS_USERFILTERINFO add    FACTIVE     BIT NOT NULL DEFAULT  0     -- 是否为当前方案
if(not exists(select name from SysColumns where name ='FUSER_ID' and id= OBJECT_ID('SYS_USERFILTERINFO'))) 
     Alter Table SYS_USERFILTERINFO add    FUSER_ID     VARCHAR(50) NOT NULL DEFAULT ''     -- 用户帐号
if(not exists(select name from SysColumns where name ='FDATEINDEX' and id= OBJECT_ID('SYS_USERFILTERINFO'))) 
     Alter Table SYS_USERFILTERINFO add    FDATEINDEX     VARCHAR(50) NOT NULL DEFAULT ''     -- 第一过滤索引号
if(not exists(select name from SysColumns where name ='FVERIFYINDEX' and id= OBJECT_ID('SYS_USERFILTERINFO'))) 
     Alter Table SYS_USERFILTERINFO add    FVERIFYINDEX     VARCHAR(50) NOT NULL DEFAULT ''     -- 第二过滤索引号
if(not exists(select name from SysColumns where name ='FFAILINDEX' and id= OBJECT_ID('SYS_USERFILTERINFO'))) 
     Alter Table SYS_USERFILTERINFO add    FFAILINDEX     VARCHAR(50) NOT NULL DEFAULT ''     -- 第三过滤索引号
if(not exists(select name from SysColumns where name ='FCLOSEINDEX' and id= OBJECT_ID('SYS_USERFILTERINFO'))) 
     Alter Table SYS_USERFILTERINFO add    FCLOSEINDEX     VARCHAR(50) NOT NULL DEFAULT ''     -- 第四过滤索引号
if(not exists(select name from SysColumns where name ='FWHENSURESAVE' and id= OBJECT_ID('SYS_USERFILTERINFO'))) 
     Alter Table SYS_USERFILTERINFO add    FWHENSURESAVE     BIT NOT NULL DEFAULT  0     -- 选择确认时是否保存
if(not exists(select name from SysColumns where name ='FMAXROW' and id= OBJECT_ID('SYS_USERFILTERINFO'))) 
     Alter Table SYS_USERFILTERINFO add    FMAXROW     INT NOT NULL DEFAULT 0     -- 最大显示
if(not exists(select name from SysColumns where name ='FPAGESIZE' and id= OBJECT_ID('SYS_USERFILTERINFO'))) 
     Alter Table SYS_USERFILTERINFO add    FPAGESIZE     INT NOT NULL DEFAULT 0     -- 每页显示
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_USERFILTERINFO'))) 
     Alter Table SYS_USERFILTERINFO add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_USERFILTERINFO'))) 
     Alter Table SYS_USERFILTERINFO add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_USERFILTERINFO'))) 
     Alter Table SYS_USERFILTERINFO add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_USERFILTERINFO') AND name = N'SYS_USERFILTERINFO_1' ) 
   Drop Index SYS_USERFILTERINFO.SYS_USERFILTERINFO_1
GO
Create  unique Clustered index SYS_USERFILTERINFO_1 on SYS_USERFILTERINFO(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_USERFILTERINFO') AND name = N'SYS_USERFILTERINFO_2' ) 
   Drop Index SYS_USERFILTERINFO.SYS_USERFILTERINFO_2
GO
Create  index SYS_USERFILTERINFO_2 on SYS_USERFILTERINFO(FCLASSTYPEID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_SECURITYSTRATEGY 系统安全策略设定表*/ 
 -- DROP TABLE SYS_SECURITYSTRATEGY
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_SECURITYSTRATEGY') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_SECURITYSTRATEGY(
   FENABLED     BIT NOT NULL DEFAULT  0 ,    -- 是否启用安全策略
   FPLENGHT     INT NOT NULL DEFAULT 0 ,    -- 登录密码最小长度 <=0 为不限止
   FPCOMBINATION     VARCHAR(10) NOT NULL DEFAULT '' ,    -- 密码组合
   FPREPETITIONTIME     INT NOT NULL DEFAULT 0 ,    -- 密码多少次不可以重复 0 为不控制
   FCPDAYS     INT NOT NULL DEFAULT 0 ,    -- 超过多少天必须修改密码
   FLOGOUTMINUTES     INT NOT NULL DEFAULT 0 ,    -- 超过分钟未使用系统退出
   FENABLEADMIN     BIT NOT NULL DEFAULT  0 ,    -- 管理员可查询业务单据
   FREMINDERDAYS     INT NOT NULL DEFAULT 0 ,    -- 提前提醒天数
   FERRORLIMIT     INT NOT NULL DEFAULT 0 ,    -- 密码锁定允许输入密码错误次数
   FAUTOUNLOCKMINS     INT NOT NULL DEFAULT 0 ,    -- 自动解锁时间，单位分钟
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_SECURITYSTRATEGY] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FENABLED' and id= OBJECT_ID('SYS_SECURITYSTRATEGY'))) 
     Alter Table SYS_SECURITYSTRATEGY add    FENABLED     BIT NOT NULL DEFAULT  0     -- 是否启用安全策略
if(not exists(select name from SysColumns where name ='FPLENGHT' and id= OBJECT_ID('SYS_SECURITYSTRATEGY'))) 
     Alter Table SYS_SECURITYSTRATEGY add    FPLENGHT     INT NOT NULL DEFAULT 0     -- 登录密码最小长度 <=0 为不限止
if(not exists(select name from SysColumns where name ='FPCOMBINATION' and id= OBJECT_ID('SYS_SECURITYSTRATEGY'))) 
     Alter Table SYS_SECURITYSTRATEGY add    FPCOMBINATION     VARCHAR(10) NOT NULL DEFAULT ''     -- 密码组合
if(not exists(select name from SysColumns where name ='FPREPETITIONTIME' and id= OBJECT_ID('SYS_SECURITYSTRATEGY'))) 
     Alter Table SYS_SECURITYSTRATEGY add    FPREPETITIONTIME     INT NOT NULL DEFAULT 0     -- 密码多少次不可以重复 0 为不控制
if(not exists(select name from SysColumns where name ='FCPDAYS' and id= OBJECT_ID('SYS_SECURITYSTRATEGY'))) 
     Alter Table SYS_SECURITYSTRATEGY add    FCPDAYS     INT NOT NULL DEFAULT 0     -- 超过多少天必须修改密码
if(not exists(select name from SysColumns where name ='FLOGOUTMINUTES' and id= OBJECT_ID('SYS_SECURITYSTRATEGY'))) 
     Alter Table SYS_SECURITYSTRATEGY add    FLOGOUTMINUTES     INT NOT NULL DEFAULT 0     -- 超过分钟未使用系统退出
if(not exists(select name from SysColumns where name ='FENABLEADMIN' and id= OBJECT_ID('SYS_SECURITYSTRATEGY'))) 
     Alter Table SYS_SECURITYSTRATEGY add    FENABLEADMIN     BIT NOT NULL DEFAULT  0     -- 管理员可查询业务单据
if(not exists(select name from SysColumns where name ='FREMINDERDAYS' and id= OBJECT_ID('SYS_SECURITYSTRATEGY'))) 
     Alter Table SYS_SECURITYSTRATEGY add    FREMINDERDAYS     INT NOT NULL DEFAULT 0     -- 提前提醒天数
if(not exists(select name from SysColumns where name ='FERRORLIMIT' and id= OBJECT_ID('SYS_SECURITYSTRATEGY'))) 
     Alter Table SYS_SECURITYSTRATEGY add    FERRORLIMIT     INT NOT NULL DEFAULT 0     -- 密码锁定允许输入密码错误次数
if(not exists(select name from SysColumns where name ='FAUTOUNLOCKMINS' and id= OBJECT_ID('SYS_SECURITYSTRATEGY'))) 
     Alter Table SYS_SECURITYSTRATEGY add    FAUTOUNLOCKMINS     INT NOT NULL DEFAULT 0     -- 自动解锁时间，单位分钟
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_SECURITYSTRATEGY'))) 
     Alter Table SYS_SECURITYSTRATEGY add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_SECURITYSTRATEGY'))) 
     Alter Table SYS_SECURITYSTRATEGY add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_SECURITYSTRATEGY'))) 
     Alter Table SYS_SECURITYSTRATEGY add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO



GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_USERLISTTEMPLATE 用户序时簿字段信息设定*/ 
 -- DROP TABLE SYS_USERLISTTEMPLATE
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_USERLISTTEMPLATE') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_USERLISTTEMPLATE(
   FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 功能代码
   FBODYID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 序时簿或是表体内码
   FUSERID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 用户帐号
   FSHOWINDEX     INT NOT NULL DEFAULT 0 ,    -- 显示顺序
   FDETAILID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 此单据内码
   FHEADCAPTION     NVARCHAR(100) NOT NULL DEFAULT '' ,    -- 字段说明
   FFIELDNAME     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 对应的字段实体
   FWIDTH     DECIMAL(24,6) NOT NULL DEFAULT 0 ,    -- 显示宽度
   FISVISIBLE     BIT NOT NULL DEFAULT  0 ,    -- 是否显示
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_USERLISTTEMPLATE] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FCLASSTYPEID' and id= OBJECT_ID('SYS_USERLISTTEMPLATE'))) 
     Alter Table SYS_USERLISTTEMPLATE add    FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 功能代码
if(not exists(select name from SysColumns where name ='FBODYID' and id= OBJECT_ID('SYS_USERLISTTEMPLATE'))) 
     Alter Table SYS_USERLISTTEMPLATE add    FBODYID     VARCHAR(50) NOT NULL DEFAULT ''     -- 序时簿或是表体内码
if(not exists(select name from SysColumns where name ='FUSERID' and id= OBJECT_ID('SYS_USERLISTTEMPLATE'))) 
     Alter Table SYS_USERLISTTEMPLATE add    FUSERID     VARCHAR(50) NOT NULL DEFAULT ''     -- 用户帐号
if(not exists(select name from SysColumns where name ='FSHOWINDEX' and id= OBJECT_ID('SYS_USERLISTTEMPLATE'))) 
     Alter Table SYS_USERLISTTEMPLATE add    FSHOWINDEX     INT NOT NULL DEFAULT 0     -- 显示顺序
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_USERLISTTEMPLATE'))) 
     Alter Table SYS_USERLISTTEMPLATE add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''     -- 此单据内码
if(not exists(select name from SysColumns where name ='FHEADCAPTION' and id= OBJECT_ID('SYS_USERLISTTEMPLATE'))) 
     Alter Table SYS_USERLISTTEMPLATE add    FHEADCAPTION     NVARCHAR(100) NOT NULL DEFAULT ''     -- 字段说明
if(not exists(select name from SysColumns where name ='FFIELDNAME' and id= OBJECT_ID('SYS_USERLISTTEMPLATE'))) 
     Alter Table SYS_USERLISTTEMPLATE add    FFIELDNAME     VARCHAR(50) NOT NULL DEFAULT ''     -- 对应的字段实体
if(not exists(select name from SysColumns where name ='FWIDTH' and id= OBJECT_ID('SYS_USERLISTTEMPLATE'))) 
     Alter Table SYS_USERLISTTEMPLATE add    FWIDTH     DECIMAL(24,6) NOT NULL DEFAULT 0     -- 显示宽度
if(not exists(select name from SysColumns where name ='FISVISIBLE' and id= OBJECT_ID('SYS_USERLISTTEMPLATE'))) 
     Alter Table SYS_USERLISTTEMPLATE add    FISVISIBLE     BIT NOT NULL DEFAULT  0     -- 是否显示
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_USERLISTTEMPLATE'))) 
     Alter Table SYS_USERLISTTEMPLATE add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_USERLISTTEMPLATE'))) 
     Alter Table SYS_USERLISTTEMPLATE add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_USERLISTTEMPLATE'))) 
     Alter Table SYS_USERLISTTEMPLATE add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_USERLISTTEMPLATE') AND name = N'SYS_USERLISTTEMPLATE_1' ) 
   Drop Index SYS_USERLISTTEMPLATE.SYS_USERLISTTEMPLATE_1
GO
Create  unique Clustered index SYS_USERLISTTEMPLATE_1 on SYS_USERLISTTEMPLATE(FDETAILID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_USERLISTTEMPLATE') AND name = N'SYS_USERLISTTEMPLATE_2' ) 
   Drop Index SYS_USERLISTTEMPLATE.SYS_USERLISTTEMPLATE_2
GO
Create  unique index SYS_USERLISTTEMPLATE_2 on SYS_USERLISTTEMPLATE(FCLASSTYPEID,FUSERID,FFIELDNAME)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_FIELDDESCRIPTION 系统字段定义*/ 
 -- DROP TABLE SYS_FIELDDESCRIPTION
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_FIELDDESCRIPTION') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_FIELDDESCRIPTION(
   FDDID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 数据类型内码
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 字段名
   FFIELDTYPE     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 字段类型
   FLENGTH     INT NOT NULL DEFAULT 0 ,    -- 字段长度
   FSCALE     INT NOT NULL DEFAULT 0 ,    -- 小数位数
   FDEFAULTVALUE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 默认值
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 详细描述
   FNOTE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 字段备注
   FCNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 详细描述(第一语言)
   FTNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 详细描述(第二语言)
   FENAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 详细描述(第三语言)
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_FIELDDESCRIPTION] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FDDID' and id= OBJECT_ID('SYS_FIELDDESCRIPTION'))) 
     Alter Table SYS_FIELDDESCRIPTION add    FDDID     VARCHAR(50) NOT NULL DEFAULT ''     -- 数据类型内码
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_FIELDDESCRIPTION'))) 
     Alter Table SYS_FIELDDESCRIPTION add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 字段名
if(not exists(select name from SysColumns where name ='FFIELDTYPE' and id= OBJECT_ID('SYS_FIELDDESCRIPTION'))) 
     Alter Table SYS_FIELDDESCRIPTION add    FFIELDTYPE     VARCHAR(1) NOT NULL DEFAULT ''     -- 字段类型
if(not exists(select name from SysColumns where name ='FLENGTH' and id= OBJECT_ID('SYS_FIELDDESCRIPTION'))) 
     Alter Table SYS_FIELDDESCRIPTION add    FLENGTH     INT NOT NULL DEFAULT 0     -- 字段长度
if(not exists(select name from SysColumns where name ='FSCALE' and id= OBJECT_ID('SYS_FIELDDESCRIPTION'))) 
     Alter Table SYS_FIELDDESCRIPTION add    FSCALE     INT NOT NULL DEFAULT 0     -- 小数位数
if(not exists(select name from SysColumns where name ='FDEFAULTVALUE' and id= OBJECT_ID('SYS_FIELDDESCRIPTION'))) 
     Alter Table SYS_FIELDDESCRIPTION add    FDEFAULTVALUE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 默认值
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_FIELDDESCRIPTION'))) 
     Alter Table SYS_FIELDDESCRIPTION add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 详细描述
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_FIELDDESCRIPTION'))) 
     Alter Table SYS_FIELDDESCRIPTION add    FNOTE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 字段备注
if(not exists(select name from SysColumns where name ='FCNAME' and id= OBJECT_ID('SYS_FIELDDESCRIPTION'))) 
     Alter Table SYS_FIELDDESCRIPTION add    FCNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 详细描述(第一语言)
if(not exists(select name from SysColumns where name ='FTNAME' and id= OBJECT_ID('SYS_FIELDDESCRIPTION'))) 
     Alter Table SYS_FIELDDESCRIPTION add    FTNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 详细描述(第二语言)
if(not exists(select name from SysColumns where name ='FENAME' and id= OBJECT_ID('SYS_FIELDDESCRIPTION'))) 
     Alter Table SYS_FIELDDESCRIPTION add    FENAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 详细描述(第三语言)
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_FIELDDESCRIPTION'))) 
     Alter Table SYS_FIELDDESCRIPTION add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_FIELDDESCRIPTION'))) 
     Alter Table SYS_FIELDDESCRIPTION add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_FIELDDESCRIPTION'))) 
     Alter Table SYS_FIELDDESCRIPTION add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_FIELDDESCRIPTION'))) 
     Alter Table SYS_FIELDDESCRIPTION add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_FIELDDESCRIPTION') AND name = N'SYS_FIELDDESCRIPTION_1' ) 
   Drop Index SYS_FIELDDESCRIPTION.SYS_FIELDDESCRIPTION_1
GO
Create  unique Clustered index SYS_FIELDDESCRIPTION_1 on SYS_FIELDDESCRIPTION(FINTERID,FNUMBER)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_FIELDDESCRIPTION') AND name = N'SYS_FIELDDESCRIPTION_2' ) 
   Drop Index SYS_FIELDDESCRIPTION.SYS_FIELDDESCRIPTION_2
GO
Create  unique index SYS_FIELDDESCRIPTION_2 on SYS_FIELDDESCRIPTION(FINTERID,FENTRYID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_FIELDDESCRIPTION') AND name = N'SYS_FIELDDESCRIPTION_3' ) 
   Drop Index SYS_FIELDDESCRIPTION.SYS_FIELDDESCRIPTION_3
GO
Create  index SYS_FIELDDESCRIPTION_3 on SYS_FIELDDESCRIPTION(FINTERID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_INDEXDESCRIPTION 表索引信息*/ 
 -- DROP TABLE SYS_INDEXDESCRIPTION
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_INDEXDESCRIPTION') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_INDEXDESCRIPTION(
   FNUMBER     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 索引代码
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 索引名称
   FTYPE     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 索引类型(1=唯一簇索引;2=唯一索引;3=普通索引)
   FFIELDINFO     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 字段信息
   FNOTE     NVARCHAR(266) NOT NULL DEFAULT '' ,    -- 备注
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_INDEXDESCRIPTION] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_INDEXDESCRIPTION'))) 
     Alter Table SYS_INDEXDESCRIPTION add    FNUMBER     VARCHAR(100) NOT NULL DEFAULT ''     -- 索引代码
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_INDEXDESCRIPTION'))) 
     Alter Table SYS_INDEXDESCRIPTION add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 索引名称
if(not exists(select name from SysColumns where name ='FTYPE' and id= OBJECT_ID('SYS_INDEXDESCRIPTION'))) 
     Alter Table SYS_INDEXDESCRIPTION add    FTYPE     VARCHAR(1) NOT NULL DEFAULT ''     -- 索引类型(1=唯一簇索引;2=唯一索引;3=普通索引)
if(not exists(select name from SysColumns where name ='FFIELDINFO' and id= OBJECT_ID('SYS_INDEXDESCRIPTION'))) 
     Alter Table SYS_INDEXDESCRIPTION add    FFIELDINFO     VARCHAR(200) NOT NULL DEFAULT ''     -- 字段信息
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_INDEXDESCRIPTION'))) 
     Alter Table SYS_INDEXDESCRIPTION add    FNOTE     NVARCHAR(266) NOT NULL DEFAULT ''     -- 备注
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_INDEXDESCRIPTION'))) 
     Alter Table SYS_INDEXDESCRIPTION add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_INDEXDESCRIPTION'))) 
     Alter Table SYS_INDEXDESCRIPTION add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_INDEXDESCRIPTION'))) 
     Alter Table SYS_INDEXDESCRIPTION add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_INDEXDESCRIPTION'))) 
     Alter Table SYS_INDEXDESCRIPTION add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_INDEXDESCRIPTION') AND name = N'SYS_INDEXDESCRIPTION_1' ) 
   Drop Index SYS_INDEXDESCRIPTION.SYS_INDEXDESCRIPTION_1
GO
Create  unique index SYS_INDEXDESCRIPTION_1 on SYS_INDEXDESCRIPTION(FINTERID,FNUMBER)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_INDEXDESCRIPTION') AND name = N'SYS_INDEXDESCRIPTION_2' ) 
   Drop Index SYS_INDEXDESCRIPTION.SYS_INDEXDESCRIPTION_2
GO
Create  unique index SYS_INDEXDESCRIPTION_2 on SYS_INDEXDESCRIPTION(FINTERID,FENTRYID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_USERPERMISSION 角色权限档案*/ 
 -- DROP TABLE SYS_USERPERMISSION
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_USERPERMISSION') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_USERPERMISSION(
   FROLEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 用户角色
   PR_CODE     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 循环还是程式( B=循环, C =模组  P=程式)
   PRG_ID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 程式代号
   PR_MAP     binary(128) NOT NULL DEFAULT  0  ,    -- 用户权限字段
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_USERPERMISSION] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FROLEID' and id= OBJECT_ID('SYS_USERPERMISSION'))) 
     Alter Table SYS_USERPERMISSION add    FROLEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 用户角色
if(not exists(select name from SysColumns where name ='PR_CODE' and id= OBJECT_ID('SYS_USERPERMISSION'))) 
     Alter Table SYS_USERPERMISSION add    PR_CODE     VARCHAR(1) NOT NULL DEFAULT ''     -- 循环还是程式( B=循环, C =模组  P=程式)
if(not exists(select name from SysColumns where name ='PRG_ID' and id= OBJECT_ID('SYS_USERPERMISSION'))) 
     Alter Table SYS_USERPERMISSION add    PRG_ID     VARCHAR(50) NOT NULL DEFAULT ''     -- 程式代号
if(not exists(select name from SysColumns where name ='PR_MAP' and id= OBJECT_ID('SYS_USERPERMISSION'))) 
     Alter Table SYS_USERPERMISSION add    PR_MAP     binary(128) NOT NULL DEFAULT  0      -- 用户权限字段
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_USERPERMISSION'))) 
     Alter Table SYS_USERPERMISSION add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_USERPERMISSION'))) 
     Alter Table SYS_USERPERMISSION add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_USERPERMISSION'))) 
     Alter Table SYS_USERPERMISSION add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_USERPERMISSION') AND name = N'SYS_USERPERMISSION_1' ) 
   Drop Index SYS_USERPERMISSION.SYS_USERPERMISSION_1
GO
Create  unique Clustered index SYS_USERPERMISSION_1 on SYS_USERPERMISSION(FROLEID,PR_CODE,PRG_ID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_PRGFUNCTION 程式其它功能信息*/ 
 -- DROP TABLE SYS_PRGFUNCTION
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_PRGFUNCTION') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_PRGFUNCTION(
   PRG_ID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 程式代号
   PB_FUNC     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 功能代号
   PB_FCNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 功能说明_CN
   PB_FTNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 功能说明_TW
   PB_FENAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 功能说明_EN
   PB_FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 功能说明
   PB_SEQ     VARCHAR(4) NOT NULL DEFAULT '' ,    -- 项次
   PB_BITMAP     INT NOT NULL DEFAULT 0 ,    -- 权限数值
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_PRGFUNCTION] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='PRG_ID' and id= OBJECT_ID('SYS_PRGFUNCTION'))) 
     Alter Table SYS_PRGFUNCTION add    PRG_ID     VARCHAR(50) NOT NULL DEFAULT ''     -- 程式代号
if(not exists(select name from SysColumns where name ='PB_FUNC' and id= OBJECT_ID('SYS_PRGFUNCTION'))) 
     Alter Table SYS_PRGFUNCTION add    PB_FUNC     VARCHAR(50) NOT NULL DEFAULT ''     -- 功能代号
if(not exists(select name from SysColumns where name ='PB_FCNAME' and id= OBJECT_ID('SYS_PRGFUNCTION'))) 
     Alter Table SYS_PRGFUNCTION add    PB_FCNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 功能说明_CN
if(not exists(select name from SysColumns where name ='PB_FTNAME' and id= OBJECT_ID('SYS_PRGFUNCTION'))) 
     Alter Table SYS_PRGFUNCTION add    PB_FTNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 功能说明_TW
if(not exists(select name from SysColumns where name ='PB_FENAME' and id= OBJECT_ID('SYS_PRGFUNCTION'))) 
     Alter Table SYS_PRGFUNCTION add    PB_FENAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 功能说明_EN
if(not exists(select name from SysColumns where name ='PB_FNAME' and id= OBJECT_ID('SYS_PRGFUNCTION'))) 
     Alter Table SYS_PRGFUNCTION add    PB_FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 功能说明
if(not exists(select name from SysColumns where name ='PB_SEQ' and id= OBJECT_ID('SYS_PRGFUNCTION'))) 
     Alter Table SYS_PRGFUNCTION add    PB_SEQ     VARCHAR(4) NOT NULL DEFAULT ''     -- 项次
if(not exists(select name from SysColumns where name ='PB_BITMAP' and id= OBJECT_ID('SYS_PRGFUNCTION'))) 
     Alter Table SYS_PRGFUNCTION add    PB_BITMAP     INT NOT NULL DEFAULT 0     -- 权限数值
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_PRGFUNCTION'))) 
     Alter Table SYS_PRGFUNCTION add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_PRGFUNCTION'))) 
     Alter Table SYS_PRGFUNCTION add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_PRGFUNCTION'))) 
     Alter Table SYS_PRGFUNCTION add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_PRGFUNCTION'))) 
     Alter Table SYS_PRGFUNCTION add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_PRGFUNCTION') AND name = N'SYS_PRGFUNCTION_1' ) 
   Drop Index SYS_PRGFUNCTION.SYS_PRGFUNCTION_1
GO
Create  unique Clustered index SYS_PRGFUNCTION_1 on SYS_PRGFUNCTION(PRG_ID,PB_FUNC)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_PRGFUNCTION') AND name = N'SYS_PRGFUNCTION_2' ) 
   Drop Index SYS_PRGFUNCTION.SYS_PRGFUNCTION_2
GO
Create  unique index SYS_PRGFUNCTION_2 on SYS_PRGFUNCTION(PRG_ID,PB_BITMAP)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_LISTCODE 单据编码规则表头档*/ 
 -- DROP TABLE SYS_LISTCODE
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_LISTCODE') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_LISTCODE(
   FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 规则代码
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 规则名称
   FNOTE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 描述
   ISMODIFY     BIT NOT NULL DEFAULT  0 ,    -- 单据编号是否允许修改
   FCHECKDATE     DATE NULL  ,    -- 审核日期
   FCHECKERID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 审核人
   FHEX     INT NOT NULL DEFAULT 10 ,    -- 流水进制
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_LISTCODE] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FCLASSTYPEID' and id= OBJECT_ID('SYS_LISTCODE'))) 
     Alter Table SYS_LISTCODE add    FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 规则代码
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_LISTCODE'))) 
     Alter Table SYS_LISTCODE add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 规则名称
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_LISTCODE'))) 
     Alter Table SYS_LISTCODE add    FNOTE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 描述
if(not exists(select name from SysColumns where name ='ISMODIFY' and id= OBJECT_ID('SYS_LISTCODE'))) 
     Alter Table SYS_LISTCODE add    ISMODIFY     BIT NOT NULL DEFAULT  0     -- 单据编号是否允许修改
if(not exists(select name from SysColumns where name ='FCHECKDATE' and id= OBJECT_ID('SYS_LISTCODE'))) 
     Alter Table SYS_LISTCODE add    FCHECKDATE     DATE NULL      -- 审核日期
if(not exists(select name from SysColumns where name ='FCHECKERID' and id= OBJECT_ID('SYS_LISTCODE'))) 
     Alter Table SYS_LISTCODE add    FCHECKERID     VARCHAR(50) NOT NULL DEFAULT ''     -- 审核人
if(not exists(select name from SysColumns where name ='FHEX' and id= OBJECT_ID('SYS_LISTCODE'))) 
     Alter Table SYS_LISTCODE add    FHEX     INT NOT NULL DEFAULT 10     -- 流水进制
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_LISTCODE'))) 
     Alter Table SYS_LISTCODE add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_LISTCODE'))) 
     Alter Table SYS_LISTCODE add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_LISTCODE'))) 
     Alter Table SYS_LISTCODE add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_LISTCODE') AND name = N'SYS_LISTCODE_1' ) 
   Drop Index SYS_LISTCODE.SYS_LISTCODE_1
GO
Create  unique Clustered index SYS_LISTCODE_1 on SYS_LISTCODE(FCLASSTYPEID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_LISTCODEENTRY 单据编码规则表体档*/ 
 -- DROP TABLE SYS_LISTCODEENTRY
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_LISTCODEENTRY') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_LISTCODEENTRY(
   FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 规则代码
   FNOTE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 描述
   FTYPE     VARCHAR(2) NOT NULL DEFAULT '' ,    -- 类型( 1=常量, 2=文本字段 3= 日期字段 4=流水号 5=数值字段)
   FIELDID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 元素来源字段
   FIELDNAME     NVARCHAR(100) NOT NULL DEFAULT '' ,    -- 元素来源名称
   FVALUE     NVARCHAR(50) NOT NULL DEFAULT '' ,    -- 设置值
   FLENGHT     INT NOT NULL DEFAULT 0 ,    -- 长度
   FMIN     INT NOT NULL DEFAULT 0 ,    -- 起始流水号
   FMAX     INT NOT NULL DEFAULT 0 ,    -- 最大流水号(0表示不限制)
   FSTEP     INT NOT NULL DEFAULT 0 ,    -- 流水号步长
   FORMATSTRING     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 字串时 可选 L=小写 U =大写 及日期型格式
   FCHAR     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 补位符
   FCHARALIGNMENT     VARCHAR(1) NOT NULL DEFAULT '' ,    -- L=左边补位;R=右边补位
   FCODECONTRAST     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 编码对照表
   ISSERIAL     BIT NOT NULL DEFAULT  0 ,    -- 编码依据
   ISMEMBER     BIT NOT NULL DEFAULT  0 ,    -- 编码元素
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_LISTCODEENTRY] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FCLASSTYPEID' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 规则代码
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FNOTE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 描述
if(not exists(select name from SysColumns where name ='FTYPE' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FTYPE     VARCHAR(2) NOT NULL DEFAULT ''     -- 类型( 1=常量, 2=文本字段 3= 日期字段 4=流水号 5=数值字段)
if(not exists(select name from SysColumns where name ='FIELDID' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FIELDID     VARCHAR(50) NOT NULL DEFAULT ''     -- 元素来源字段
if(not exists(select name from SysColumns where name ='FIELDNAME' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FIELDNAME     NVARCHAR(100) NOT NULL DEFAULT ''     -- 元素来源名称
if(not exists(select name from SysColumns where name ='FVALUE' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FVALUE     NVARCHAR(50) NOT NULL DEFAULT ''     -- 设置值
if(not exists(select name from SysColumns where name ='FLENGHT' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FLENGHT     INT NOT NULL DEFAULT 0     -- 长度
if(not exists(select name from SysColumns where name ='FMIN' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FMIN     INT NOT NULL DEFAULT 0     -- 起始流水号
if(not exists(select name from SysColumns where name ='FMAX' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FMAX     INT NOT NULL DEFAULT 0     -- 最大流水号(0表示不限制)
if(not exists(select name from SysColumns where name ='FSTEP' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FSTEP     INT NOT NULL DEFAULT 0     -- 流水号步长
if(not exists(select name from SysColumns where name ='FORMATSTRING' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FORMATSTRING     VARCHAR(50) NOT NULL DEFAULT ''     -- 字串时 可选 L=小写 U =大写 及日期型格式
if(not exists(select name from SysColumns where name ='FCHAR' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FCHAR     VARCHAR(1) NOT NULL DEFAULT ''     -- 补位符
if(not exists(select name from SysColumns where name ='FCHARALIGNMENT' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FCHARALIGNMENT     VARCHAR(1) NOT NULL DEFAULT ''     -- L=左边补位;R=右边补位
if(not exists(select name from SysColumns where name ='FCODECONTRAST' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FCODECONTRAST     VARCHAR(50) NOT NULL DEFAULT ''     -- 编码对照表
if(not exists(select name from SysColumns where name ='ISSERIAL' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    ISSERIAL     BIT NOT NULL DEFAULT  0     -- 编码依据
if(not exists(select name from SysColumns where name ='ISMEMBER' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    ISMEMBER     BIT NOT NULL DEFAULT  0     -- 编码元素
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_LISTCODEENTRY'))) 
     Alter Table SYS_LISTCODEENTRY add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_LISTCODEENTRY') AND name = N'SYS_LISTCODEENTRY_1' ) 
   Drop Index SYS_LISTCODEENTRY.SYS_LISTCODEENTRY_1
GO
Create  unique Clustered index SYS_LISTCODEENTRY_1 on SYS_LISTCODEENTRY(FCLASSTYPEID,FENTRYID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_LISTCODENO 单据编码流水号档*/ 
 -- DROP TABLE SYS_LISTCODENO
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_LISTCODENO') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_LISTCODENO(
   FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 规码ID
   FBILLCODE     VARCHAR(255) NOT NULL DEFAULT '' ,    -- 编码标识
   FSEQ     INT NOT NULL DEFAULT 0 ,    -- 目前流水号
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_LISTCODENO] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FCLASSTYPEID' and id= OBJECT_ID('SYS_LISTCODENO'))) 
     Alter Table SYS_LISTCODENO add    FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 规码ID
if(not exists(select name from SysColumns where name ='FBILLCODE' and id= OBJECT_ID('SYS_LISTCODENO'))) 
     Alter Table SYS_LISTCODENO add    FBILLCODE     VARCHAR(255) NOT NULL DEFAULT ''     -- 编码标识
if(not exists(select name from SysColumns where name ='FSEQ' and id= OBJECT_ID('SYS_LISTCODENO'))) 
     Alter Table SYS_LISTCODENO add    FSEQ     INT NOT NULL DEFAULT 0     -- 目前流水号
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_LISTCODENO'))) 
     Alter Table SYS_LISTCODENO add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_LISTCODENO'))) 
     Alter Table SYS_LISTCODENO add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_LISTCODENO'))) 
     Alter Table SYS_LISTCODENO add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_LISTCODENO') AND name = N'SYS_LISTCODENO_1' ) 
   Drop Index SYS_LISTCODENO.SYS_LISTCODENO_1
GO
Create  unique Clustered index SYS_LISTCODENO_1 on SYS_LISTCODENO(FCLASSTYPEID,FBILLCODE)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_LISTCODENO') AND name = N'SYS_LISTCODENO_2' ) 
   Drop Index SYS_LISTCODENO.SYS_LISTCODENO_2
GO
Create  index SYS_LISTCODENO_2 on SYS_LISTCODENO(FCLASSTYPEID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_BATCHCODE 单据编码规则表头档*/ 
 -- DROP TABLE SYS_BATCHCODE
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_BATCHCODE') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_BATCHCODE(
   FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 规则代码
   FPRGKEY     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 表单编号
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 规则名称
   FNOTE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 描述
   ISMODIFY     BIT NOT NULL DEFAULT  0 ,    -- 单据编号是否允许修改
   FCHECKDATE     DATE NULL  ,    -- 审核日期
   FCHECKERID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 审核人
   FHEX     INT NOT NULL DEFAULT 10 ,    -- 流水进制
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_BATCHCODE] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FCLASSTYPEID' and id= OBJECT_ID('SYS_BATCHCODE'))) 
     Alter Table SYS_BATCHCODE add    FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 规则代码
if(not exists(select name from SysColumns where name ='FPRGKEY' and id= OBJECT_ID('SYS_BATCHCODE'))) 
     Alter Table SYS_BATCHCODE add    FPRGKEY     VARCHAR(50) NOT NULL DEFAULT ''     -- 表单编号
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_BATCHCODE'))) 
     Alter Table SYS_BATCHCODE add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 规则名称
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_BATCHCODE'))) 
     Alter Table SYS_BATCHCODE add    FNOTE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 描述
if(not exists(select name from SysColumns where name ='ISMODIFY' and id= OBJECT_ID('SYS_BATCHCODE'))) 
     Alter Table SYS_BATCHCODE add    ISMODIFY     BIT NOT NULL DEFAULT  0     -- 单据编号是否允许修改
if(not exists(select name from SysColumns where name ='FCHECKDATE' and id= OBJECT_ID('SYS_BATCHCODE'))) 
     Alter Table SYS_BATCHCODE add    FCHECKDATE     DATE NULL      -- 审核日期
if(not exists(select name from SysColumns where name ='FCHECKERID' and id= OBJECT_ID('SYS_BATCHCODE'))) 
     Alter Table SYS_BATCHCODE add    FCHECKERID     VARCHAR(50) NOT NULL DEFAULT ''     -- 审核人
if(not exists(select name from SysColumns where name ='FHEX' and id= OBJECT_ID('SYS_BATCHCODE'))) 
     Alter Table SYS_BATCHCODE add    FHEX     INT NOT NULL DEFAULT 10     -- 流水进制
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_BATCHCODE'))) 
     Alter Table SYS_BATCHCODE add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_BATCHCODE'))) 
     Alter Table SYS_BATCHCODE add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_BATCHCODE'))) 
     Alter Table SYS_BATCHCODE add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BATCHCODE') AND name = N'SYS_BATCHCODE_1' ) 
   Drop Index SYS_BATCHCODE.SYS_BATCHCODE_1
GO
Create  unique Clustered index SYS_BATCHCODE_1 on SYS_BATCHCODE(FCLASSTYPEID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BATCHCODE') AND name = N'SYS_BATCHCODE_2' ) 
   Drop Index SYS_BATCHCODE.SYS_BATCHCODE_2
GO
Create  index SYS_BATCHCODE_2 on SYS_BATCHCODE(FPRGKEY)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_BATCHCODEENTRY 单据编码规则表体档*/ 
 -- DROP TABLE SYS_BATCHCODEENTRY
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_BATCHCODEENTRY') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_BATCHCODEENTRY(
   FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 规则代码
   FNOTE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 描述
   FTYPE     VARCHAR(2) NOT NULL DEFAULT '' ,    -- 类型( 1=常量, 2=文本字段 3= 日期字段 4=流水号 5=数值字段)
   FIELDID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 元素来源字段
   FIELDNAME     NVARCHAR(100) NOT NULL DEFAULT '' ,    -- 元素来源名称
   FVALUE     NVARCHAR(50) NOT NULL DEFAULT '' ,    -- 设置值
   FLENGHT     INT NOT NULL DEFAULT 0 ,    -- 长度
   FMIN     INT NOT NULL DEFAULT 0 ,    -- 起始流水号
   FMAX     INT NOT NULL DEFAULT 0 ,    -- 最大流水号(0表示不限制)
   FSTEP     INT NOT NULL DEFAULT 0 ,    -- 流水号步长
   FORMATSTRING     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 字串时 可选 L=小写 U =大写 及日期型格式
   FCHAR     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 补位符
   FCHARALIGNMENT     VARCHAR(1) NOT NULL DEFAULT '' ,    -- L=左边补位;R=右边补位
   FCODECONTRAST     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 编码对照表
   ISSERIAL     BIT NOT NULL DEFAULT  0 ,    -- 编码依据
   ISMEMBER     BIT NOT NULL DEFAULT  0 ,    -- 编码元素
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_BATCHCODEENTRY] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FCLASSTYPEID' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 规则代码
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FNOTE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 描述
if(not exists(select name from SysColumns where name ='FTYPE' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FTYPE     VARCHAR(2) NOT NULL DEFAULT ''     -- 类型( 1=常量, 2=文本字段 3= 日期字段 4=流水号 5=数值字段)
if(not exists(select name from SysColumns where name ='FIELDID' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FIELDID     VARCHAR(50) NOT NULL DEFAULT ''     -- 元素来源字段
if(not exists(select name from SysColumns where name ='FIELDNAME' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FIELDNAME     NVARCHAR(100) NOT NULL DEFAULT ''     -- 元素来源名称
if(not exists(select name from SysColumns where name ='FVALUE' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FVALUE     NVARCHAR(50) NOT NULL DEFAULT ''     -- 设置值
if(not exists(select name from SysColumns where name ='FLENGHT' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FLENGHT     INT NOT NULL DEFAULT 0     -- 长度
if(not exists(select name from SysColumns where name ='FMIN' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FMIN     INT NOT NULL DEFAULT 0     -- 起始流水号
if(not exists(select name from SysColumns where name ='FMAX' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FMAX     INT NOT NULL DEFAULT 0     -- 最大流水号(0表示不限制)
if(not exists(select name from SysColumns where name ='FSTEP' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FSTEP     INT NOT NULL DEFAULT 0     -- 流水号步长
if(not exists(select name from SysColumns where name ='FORMATSTRING' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FORMATSTRING     VARCHAR(50) NOT NULL DEFAULT ''     -- 字串时 可选 L=小写 U =大写 及日期型格式
if(not exists(select name from SysColumns where name ='FCHAR' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FCHAR     VARCHAR(1) NOT NULL DEFAULT ''     -- 补位符
if(not exists(select name from SysColumns where name ='FCHARALIGNMENT' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FCHARALIGNMENT     VARCHAR(1) NOT NULL DEFAULT ''     -- L=左边补位;R=右边补位
if(not exists(select name from SysColumns where name ='FCODECONTRAST' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FCODECONTRAST     VARCHAR(50) NOT NULL DEFAULT ''     -- 编码对照表
if(not exists(select name from SysColumns where name ='ISSERIAL' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    ISSERIAL     BIT NOT NULL DEFAULT  0     -- 编码依据
if(not exists(select name from SysColumns where name ='ISMEMBER' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    ISMEMBER     BIT NOT NULL DEFAULT  0     -- 编码元素
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_BATCHCODEENTRY'))) 
     Alter Table SYS_BATCHCODEENTRY add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BATCHCODEENTRY') AND name = N'SYS_BATCHCODEENTRY_1' ) 
   Drop Index SYS_BATCHCODEENTRY.SYS_BATCHCODEENTRY_1
GO
Create  unique Clustered index SYS_BATCHCODEENTRY_1 on SYS_BATCHCODEENTRY(FCLASSTYPEID,FENTRYID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_BATCHCODENO 单据编码流水号档*/ 
 -- DROP TABLE SYS_BATCHCODENO
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_BATCHCODENO') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_BATCHCODENO(
   FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 规码ID
   FBILLCODE     VARCHAR(255) NOT NULL DEFAULT '' ,    -- 编码标识
   FSEQ     INT NOT NULL DEFAULT 0 ,    -- 目前流水号
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_BATCHCODENO] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FCLASSTYPEID' and id= OBJECT_ID('SYS_BATCHCODENO'))) 
     Alter Table SYS_BATCHCODENO add    FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 规码ID
if(not exists(select name from SysColumns where name ='FBILLCODE' and id= OBJECT_ID('SYS_BATCHCODENO'))) 
     Alter Table SYS_BATCHCODENO add    FBILLCODE     VARCHAR(255) NOT NULL DEFAULT ''     -- 编码标识
if(not exists(select name from SysColumns where name ='FSEQ' and id= OBJECT_ID('SYS_BATCHCODENO'))) 
     Alter Table SYS_BATCHCODENO add    FSEQ     INT NOT NULL DEFAULT 0     -- 目前流水号
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_BATCHCODENO'))) 
     Alter Table SYS_BATCHCODENO add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_BATCHCODENO'))) 
     Alter Table SYS_BATCHCODENO add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_BATCHCODENO'))) 
     Alter Table SYS_BATCHCODENO add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BATCHCODENO') AND name = N'SYS_BATCHCODENO_1' ) 
   Drop Index SYS_BATCHCODENO.SYS_BATCHCODENO_1
GO
Create  unique Clustered index SYS_BATCHCODENO_1 on SYS_BATCHCODENO(FCLASSTYPEID,FBILLCODE)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BATCHCODENO') AND name = N'SYS_BATCHCODENO_2' ) 
   Drop Index SYS_BATCHCODENO.SYS_BATCHCODENO_2
GO
Create  index SYS_BATCHCODENO_2 on SYS_BATCHCODENO(FCLASSTYPEID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_BARCODE 条码编码规则表头档*/ 
 -- DROP TABLE SYS_BARCODE
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_BARCODE') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_BARCODE(
   FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 规则代码
   FPRGKEY     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 表单编号
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 规则名称
   FNOTE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 描述
   ISMODIFY     BIT NOT NULL DEFAULT  0 ,    -- 单据编号是否允许修改
   FCHECKDATE     DATE NULL  ,    -- 审核日期
   FCHECKERID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 审核人
   FHEX     INT NOT NULL DEFAULT 10 ,    -- 流水进制
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_BARCODE] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FCLASSTYPEID' and id= OBJECT_ID('SYS_BARCODE'))) 
     Alter Table SYS_BARCODE add    FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 规则代码
if(not exists(select name from SysColumns where name ='FPRGKEY' and id= OBJECT_ID('SYS_BARCODE'))) 
     Alter Table SYS_BARCODE add    FPRGKEY     VARCHAR(50) NOT NULL DEFAULT ''     -- 表单编号
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_BARCODE'))) 
     Alter Table SYS_BARCODE add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 规则名称
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_BARCODE'))) 
     Alter Table SYS_BARCODE add    FNOTE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 描述
if(not exists(select name from SysColumns where name ='ISMODIFY' and id= OBJECT_ID('SYS_BARCODE'))) 
     Alter Table SYS_BARCODE add    ISMODIFY     BIT NOT NULL DEFAULT  0     -- 单据编号是否允许修改
if(not exists(select name from SysColumns where name ='FCHECKDATE' and id= OBJECT_ID('SYS_BARCODE'))) 
     Alter Table SYS_BARCODE add    FCHECKDATE     DATE NULL      -- 审核日期
if(not exists(select name from SysColumns where name ='FCHECKERID' and id= OBJECT_ID('SYS_BARCODE'))) 
     Alter Table SYS_BARCODE add    FCHECKERID     VARCHAR(50) NOT NULL DEFAULT ''     -- 审核人
if(not exists(select name from SysColumns where name ='FHEX' and id= OBJECT_ID('SYS_BARCODE'))) 
     Alter Table SYS_BARCODE add    FHEX     INT NOT NULL DEFAULT 10     -- 流水进制
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_BARCODE'))) 
     Alter Table SYS_BARCODE add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_BARCODE'))) 
     Alter Table SYS_BARCODE add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_BARCODE'))) 
     Alter Table SYS_BARCODE add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BARCODE') AND name = N'SYS_BARCODE_1' ) 
   Drop Index SYS_BARCODE.SYS_BARCODE_1
GO
Create  unique Clustered index SYS_BARCODE_1 on SYS_BARCODE(FCLASSTYPEID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BARCODE') AND name = N'SYS_BARCODE_2' ) 
   Drop Index SYS_BARCODE.SYS_BARCODE_2
GO
Create  index SYS_BARCODE_2 on SYS_BARCODE(FPRGKEY)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_BARCODENO 条码编码流水号档*/ 
 -- DROP TABLE SYS_BARCODENO
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_BARCODENO') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_BARCODENO(
   FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 规码ID
   FBILLCODE     VARCHAR(255) NOT NULL DEFAULT '' ,    -- 编码标识
   FSEQ     INT NOT NULL DEFAULT 0 ,    -- 目前流水号
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_BARCODENO] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FCLASSTYPEID' and id= OBJECT_ID('SYS_BARCODENO'))) 
     Alter Table SYS_BARCODENO add    FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 规码ID
if(not exists(select name from SysColumns where name ='FBILLCODE' and id= OBJECT_ID('SYS_BARCODENO'))) 
     Alter Table SYS_BARCODENO add    FBILLCODE     VARCHAR(255) NOT NULL DEFAULT ''     -- 编码标识
if(not exists(select name from SysColumns where name ='FSEQ' and id= OBJECT_ID('SYS_BARCODENO'))) 
     Alter Table SYS_BARCODENO add    FSEQ     INT NOT NULL DEFAULT 0     -- 目前流水号
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_BARCODENO'))) 
     Alter Table SYS_BARCODENO add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_BARCODENO'))) 
     Alter Table SYS_BARCODENO add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_BARCODENO'))) 
     Alter Table SYS_BARCODENO add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BARCODENO') AND name = N'SYS_BARCODENO_1' ) 
   Drop Index SYS_BARCODENO.SYS_BARCODENO_1
GO
Create  unique Clustered index SYS_BARCODENO_1 on SYS_BARCODENO(FCLASSTYPEID,FBILLCODE)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BARCODENO') AND name = N'SYS_BARCODENO_2' ) 
   Drop Index SYS_BARCODENO.SYS_BARCODENO_2
GO
Create  index SYS_BARCODENO_2 on SYS_BARCODENO(FCLASSTYPEID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_ORGSTRUCTURE 组织架构*/ 
 -- DROP TABLE SYS_ORGSTRUCTURE
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_ORGSTRUCTURE') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_ORGSTRUCTURE(
   FPARAID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 所属组织
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 代号
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 名称
   FMANAGER     NVARCHAR(50) NOT NULL DEFAULT '' ,    -- 负责人
   FPHONE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 负责人电话
   FCHECKERID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 审核人内码
   FCHECKDATE     DATE NULL  ,    -- 审核日期
   FDISABLEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 禁用人
   FDISABLEDATE     DATE NULL  ,    -- 禁用日期
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_ORGSTRUCTURE] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FPARAID' and id= OBJECT_ID('SYS_ORGSTRUCTURE'))) 
     Alter Table SYS_ORGSTRUCTURE add    FPARAID     VARCHAR(50) NOT NULL DEFAULT ''     -- 所属组织
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_ORGSTRUCTURE'))) 
     Alter Table SYS_ORGSTRUCTURE add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 代号
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_ORGSTRUCTURE'))) 
     Alter Table SYS_ORGSTRUCTURE add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 名称
if(not exists(select name from SysColumns where name ='FMANAGER' and id= OBJECT_ID('SYS_ORGSTRUCTURE'))) 
     Alter Table SYS_ORGSTRUCTURE add    FMANAGER     NVARCHAR(50) NOT NULL DEFAULT ''     -- 负责人
if(not exists(select name from SysColumns where name ='FPHONE' and id= OBJECT_ID('SYS_ORGSTRUCTURE'))) 
     Alter Table SYS_ORGSTRUCTURE add    FPHONE     VARCHAR(50) NOT NULL DEFAULT ''     -- 负责人电话
if(not exists(select name from SysColumns where name ='FCHECKERID' and id= OBJECT_ID('SYS_ORGSTRUCTURE'))) 
     Alter Table SYS_ORGSTRUCTURE add    FCHECKERID     VARCHAR(50) NOT NULL DEFAULT ''     -- 审核人内码
if(not exists(select name from SysColumns where name ='FCHECKDATE' and id= OBJECT_ID('SYS_ORGSTRUCTURE'))) 
     Alter Table SYS_ORGSTRUCTURE add    FCHECKDATE     DATE NULL      -- 审核日期
if(not exists(select name from SysColumns where name ='FDISABLEID' and id= OBJECT_ID('SYS_ORGSTRUCTURE'))) 
     Alter Table SYS_ORGSTRUCTURE add    FDISABLEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 禁用人
if(not exists(select name from SysColumns where name ='FDISABLEDATE' and id= OBJECT_ID('SYS_ORGSTRUCTURE'))) 
     Alter Table SYS_ORGSTRUCTURE add    FDISABLEDATE     DATE NULL      -- 禁用日期
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_ORGSTRUCTURE'))) 
     Alter Table SYS_ORGSTRUCTURE add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_ORGSTRUCTURE'))) 
     Alter Table SYS_ORGSTRUCTURE add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_ORGSTRUCTURE'))) 
     Alter Table SYS_ORGSTRUCTURE add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_ORGSTRUCTURE') AND name = N'SYS_ORGSTRUCTURE_1' ) 
   Drop Index SYS_ORGSTRUCTURE.SYS_ORGSTRUCTURE_1
GO
Create  unique Clustered index SYS_ORGSTRUCTURE_1 on SYS_ORGSTRUCTURE(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_ORGSTRUCTURE') AND name = N'SYS_ORGSTRUCTURE_2' ) 
   Drop Index SYS_ORGSTRUCTURE.SYS_ORGSTRUCTURE_2
GO
Create  unique index SYS_ORGSTRUCTURE_2 on SYS_ORGSTRUCTURE(FNUMBER)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_ROLETYPE 角色类别*/ 
 -- DROP TABLE SYS_ROLETYPE
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_ROLETYPE') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_ROLETYPE(
   FROLETYPE     INT NOT NULL DEFAULT 0 ,    -- 类别代码
   FROLENAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 类别名称
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_ROLETYPE] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FROLETYPE' and id= OBJECT_ID('SYS_ROLETYPE'))) 
     Alter Table SYS_ROLETYPE add    FROLETYPE     INT NOT NULL DEFAULT 0     -- 类别代码
if(not exists(select name from SysColumns where name ='FROLENAME' and id= OBJECT_ID('SYS_ROLETYPE'))) 
     Alter Table SYS_ROLETYPE add    FROLENAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 类别名称
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_ROLETYPE'))) 
     Alter Table SYS_ROLETYPE add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_ROLETYPE'))) 
     Alter Table SYS_ROLETYPE add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_ROLETYPE'))) 
     Alter Table SYS_ROLETYPE add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_ROLETYPE') AND name = N'SYS_ROLETYPE_1' ) 
   Drop Index SYS_ROLETYPE.SYS_ROLETYPE_1
GO
Create  unique Clustered index SYS_ROLETYPE_1 on SYS_ROLETYPE(FROLETYPE)
GO

GO
if (not exists(select 1 from SYS_ROLETYPE where FROLETYPE=0)) 
   insert into SYS_ROLETYPE(FROLETYPE,FROLENAME,UID) values(0,'系统管理员','01')
if (not exists(select 1 from SYS_ROLETYPE where FROLETYPE=1)) 
   insert into SYS_ROLETYPE(FROLETYPE,FROLENAME,UID) values(1,'普通角色','02')
if (not exists(select 1 from SYS_ROLETYPE where FROLETYPE=2)) 
   insert into SYS_ROLETYPE(FROLETYPE,FROLENAME,UID) values(2,'开发角色','03')

GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_PRGWORKBENCH 我的收藏*/ 
 -- DROP TABLE SYS_PRGWORKBENCH
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_PRGWORKBENCH') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_PRGWORKBENCH(
   USER_ID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 用户帐号
   FID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 明细功能ID
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FBILLNO	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 单据编号 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_PRGWORKBENCH] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='USER_ID' and id= OBJECT_ID('SYS_PRGWORKBENCH'))) 
     Alter Table SYS_PRGWORKBENCH add    USER_ID     VARCHAR(50) NOT NULL DEFAULT ''     -- 用户帐号
if(not exists(select name from SysColumns where name ='FID' and id= OBJECT_ID('SYS_PRGWORKBENCH'))) 
     Alter Table SYS_PRGWORKBENCH add    FID     VARCHAR(50) NOT NULL DEFAULT ''     -- 明细功能ID
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_PRGWORKBENCH'))) 
     Alter Table SYS_PRGWORKBENCH add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_PRGWORKBENCH'))) 
     Alter Table SYS_PRGWORKBENCH add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FBILLNO' and id= OBJECT_ID('SYS_PRGWORKBENCH'))) 
     Alter Table SYS_PRGWORKBENCH add    FBILLNO     VARCHAR(50) NOT NULL DEFAULT ''      -- 单据编号
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_PRGWORKBENCH'))) 
     Alter Table SYS_PRGWORKBENCH add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_PRGWORKBENCH') AND name = N'SYS_PRGWORKBENCH_1' ) 
   Drop Index SYS_PRGWORKBENCH.SYS_PRGWORKBENCH_1
GO
Create  unique Clustered index SYS_PRGWORKBENCH_1 on SYS_PRGWORKBENCH(USER_ID,FID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_PRGWORKBENCH') AND name = N'SYS_PRGWORKBENCH_2' ) 
   Drop Index SYS_PRGWORKBENCH.SYS_PRGWORKBENCH_2
GO
Create  unique index SYS_PRGWORKBENCH_2 on SYS_PRGWORKBENCH(FBILLNO)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_CODECONTRAST 代码对照表*/ 
 -- DROP TABLE SYS_CODECONTRAST
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_CODECONTRAST') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_CODECONTRAST(
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 代号
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 名称
   FNOTE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 备注
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_CODECONTRAST] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_CODECONTRAST'))) 
     Alter Table SYS_CODECONTRAST add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 代号
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_CODECONTRAST'))) 
     Alter Table SYS_CODECONTRAST add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 名称
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_CODECONTRAST'))) 
     Alter Table SYS_CODECONTRAST add    FNOTE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 备注
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_CODECONTRAST'))) 
     Alter Table SYS_CODECONTRAST add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_CODECONTRAST'))) 
     Alter Table SYS_CODECONTRAST add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_CODECONTRAST'))) 
     Alter Table SYS_CODECONTRAST add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_CODECONTRAST') AND name = N'SYS_CODECONTRAST_1' ) 
   Drop Index SYS_CODECONTRAST.SYS_CODECONTRAST_1
GO
Create  unique Clustered index SYS_CODECONTRAST_1 on SYS_CODECONTRAST(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_CODECONTRAST') AND name = N'SYS_CODECONTRAST_2' ) 
   Drop Index SYS_CODECONTRAST.SYS_CODECONTRAST_2
GO
Create  unique index SYS_CODECONTRAST_2 on SYS_CODECONTRAST(FNUMBER)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_CODECONTRASTENTRY 代码对照表体*/ 
 -- DROP TABLE SYS_CODECONTRASTENTRY
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_CODECONTRASTENTRY') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_CODECONTRASTENTRY(
   FVALUE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 编码值
   FCODE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 对应编码
   FNOTE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 备注
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_CODECONTRASTENTRY] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FVALUE' and id= OBJECT_ID('SYS_CODECONTRASTENTRY'))) 
     Alter Table SYS_CODECONTRASTENTRY add    FVALUE     VARCHAR(50) NOT NULL DEFAULT ''     -- 编码值
if(not exists(select name from SysColumns where name ='FCODE' and id= OBJECT_ID('SYS_CODECONTRASTENTRY'))) 
     Alter Table SYS_CODECONTRASTENTRY add    FCODE     VARCHAR(50) NOT NULL DEFAULT ''     -- 对应编码
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_CODECONTRASTENTRY'))) 
     Alter Table SYS_CODECONTRASTENTRY add    FNOTE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 备注
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_CODECONTRASTENTRY'))) 
     Alter Table SYS_CODECONTRASTENTRY add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_CODECONTRASTENTRY'))) 
     Alter Table SYS_CODECONTRASTENTRY add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_CODECONTRASTENTRY'))) 
     Alter Table SYS_CODECONTRASTENTRY add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_CODECONTRASTENTRY'))) 
     Alter Table SYS_CODECONTRASTENTRY add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_CODECONTRASTENTRY') AND name = N'SYS_CODECONTRASTENTRY_1' ) 
   Drop Index SYS_CODECONTRASTENTRY.SYS_CODECONTRASTENTRY_1
GO
Create  unique index SYS_CODECONTRASTENTRY_1 on SYS_CODECONTRASTENTRY(FINTERID,FENTRYID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_DEPARAMETERS 系统默认参数表*/ 
 -- DROP TABLE SYS_DEPARAMETERS
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_DEPARAMETERS') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_DEPARAMETERS(
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 参数代码
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 参数名称
   FCNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 参数名称(第一语言)
   FTNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 参数名称(第二语言)
   FENAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 参数名称(第三语言)
   FVALUE     VARCHAR(255) NOT NULL DEFAULT '' ,    -- 参数值
   ISDEFAULT     BIT NOT NULL DEFAULT  0 ,    -- 是否系统默认
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_DEPARAMETERS] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_DEPARAMETERS'))) 
     Alter Table SYS_DEPARAMETERS add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 参数代码
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_DEPARAMETERS'))) 
     Alter Table SYS_DEPARAMETERS add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 参数名称
if(not exists(select name from SysColumns where name ='FCNAME' and id= OBJECT_ID('SYS_DEPARAMETERS'))) 
     Alter Table SYS_DEPARAMETERS add    FCNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 参数名称(第一语言)
if(not exists(select name from SysColumns where name ='FTNAME' and id= OBJECT_ID('SYS_DEPARAMETERS'))) 
     Alter Table SYS_DEPARAMETERS add    FTNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 参数名称(第二语言)
if(not exists(select name from SysColumns where name ='FENAME' and id= OBJECT_ID('SYS_DEPARAMETERS'))) 
     Alter Table SYS_DEPARAMETERS add    FENAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 参数名称(第三语言)
if(not exists(select name from SysColumns where name ='FVALUE' and id= OBJECT_ID('SYS_DEPARAMETERS'))) 
     Alter Table SYS_DEPARAMETERS add    FVALUE     VARCHAR(255) NOT NULL DEFAULT ''     -- 参数值
if(not exists(select name from SysColumns where name ='ISDEFAULT' and id= OBJECT_ID('SYS_DEPARAMETERS'))) 
     Alter Table SYS_DEPARAMETERS add    ISDEFAULT     BIT NOT NULL DEFAULT  0     -- 是否系统默认
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_DEPARAMETERS'))) 
     Alter Table SYS_DEPARAMETERS add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_DEPARAMETERS'))) 
     Alter Table SYS_DEPARAMETERS add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_DEPARAMETERS'))) 
     Alter Table SYS_DEPARAMETERS add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_DEPARAMETERS') AND name = N'SYS_DEPARAMETERS_1' ) 
   Drop Index SYS_DEPARAMETERS.SYS_DEPARAMETERS_1
GO
Create  unique Clustered index SYS_DEPARAMETERS_1 on SYS_DEPARAMETERS(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_DEPARAMETERS') AND name = N'SYS_DEPARAMETERS_2' ) 
   Drop Index SYS_DEPARAMETERS.SYS_DEPARAMETERS_2
GO
Create  unique index SYS_DEPARAMETERS_2 on SYS_DEPARAMETERS(FNUMBER)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_TABLERELATION 系统表结构之间的关系*/ 
 -- DROP TABLE SYS_TABLERELATION
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_TABLERELATION') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_TABLERELATION(
   FSTABLEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 子表内码
   FRETYPE     VARCHAR(1) NOT NULL DEFAULT '1' ,    -- 关联类别
   FMTABLEKEY     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 主表关联字段
   FRTABLEKEY     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 子表关联字段
   FNOTE     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 备注
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_TABLERELATION] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FSTABLEID' and id= OBJECT_ID('SYS_TABLERELATION'))) 
     Alter Table SYS_TABLERELATION add    FSTABLEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 子表内码
if(not exists(select name from SysColumns where name ='FRETYPE' and id= OBJECT_ID('SYS_TABLERELATION'))) 
     Alter Table SYS_TABLERELATION add    FRETYPE     VARCHAR(1) NOT NULL DEFAULT '1'     -- 关联类别
if(not exists(select name from SysColumns where name ='FMTABLEKEY' and id= OBJECT_ID('SYS_TABLERELATION'))) 
     Alter Table SYS_TABLERELATION add    FMTABLEKEY     VARCHAR(200) NOT NULL DEFAULT ''     -- 主表关联字段
if(not exists(select name from SysColumns where name ='FRTABLEKEY' and id= OBJECT_ID('SYS_TABLERELATION'))) 
     Alter Table SYS_TABLERELATION add    FRTABLEKEY     VARCHAR(200) NOT NULL DEFAULT ''     -- 子表关联字段
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_TABLERELATION'))) 
     Alter Table SYS_TABLERELATION add    FNOTE     NVARCHAR(200) NOT NULL DEFAULT ''     -- 备注
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_TABLERELATION'))) 
     Alter Table SYS_TABLERELATION add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_TABLERELATION'))) 
     Alter Table SYS_TABLERELATION add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_TABLERELATION'))) 
     Alter Table SYS_TABLERELATION add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_TABLERELATION'))) 
     Alter Table SYS_TABLERELATION add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_TABLERELATION') AND name = N'SYS_TABLERELATION_1' ) 
   Drop Index SYS_TABLERELATION.SYS_TABLERELATION_1
GO
Create  unique Clustered index SYS_TABLERELATION_1 on SYS_TABLERELATION(FDETAILID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_TABLERELATION') AND name = N'SYS_TABLERELATION_2' ) 
   Drop Index SYS_TABLERELATION.SYS_TABLERELATION_2
GO
Create  unique index SYS_TABLERELATION_2 on SYS_TABLERELATION(FINTERID,FENTRYID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_USERORG 用户组织*/ 
 -- DROP TABLE SYS_USERORG
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_USERORG') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_USERORG(
   USER_ID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 用户账号
   FORGID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 组织内码
   FISDEFAULT     BIT NOT NULL DEFAULT  0 ,    -- 默认组织
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_USERORG] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='USER_ID' and id= OBJECT_ID('SYS_USERORG'))) 
     Alter Table SYS_USERORG add    USER_ID     VARCHAR(50) NOT NULL DEFAULT ''     -- 用户账号
if(not exists(select name from SysColumns where name ='FORGID' and id= OBJECT_ID('SYS_USERORG'))) 
     Alter Table SYS_USERORG add    FORGID     VARCHAR(50) NOT NULL DEFAULT ''     -- 组织内码
if(not exists(select name from SysColumns where name ='FISDEFAULT' and id= OBJECT_ID('SYS_USERORG'))) 
     Alter Table SYS_USERORG add    FISDEFAULT     BIT NOT NULL DEFAULT  0     -- 默认组织
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_USERORG'))) 
     Alter Table SYS_USERORG add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_USERORG'))) 
     Alter Table SYS_USERORG add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_USERORG'))) 
     Alter Table SYS_USERORG add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_USERORG') AND name = N'SYS_USERORG_1' ) 
   Drop Index SYS_USERORG.SYS_USERORG_1
GO
Create  unique Clustered index SYS_USERORG_1 on SYS_USERORG(USER_ID,FORGID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_BILLTEMPLATEENTRY1 表单数据源相关表信息*/ 
 -- DROP TABLE SYS_BILLTEMPLATEENTRY1
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY1') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_BILLTEMPLATEENTRY1(
   FPARENTID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 父阶内码
   FTABLEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 子表内码
   FLEV     INT NOT NULL DEFAULT -1 ,    -- 数据表层级
   FREID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 关系内码
   FPATH     VARCHAR(2000) NOT NULL DEFAULT '' ,    -- 关系路径
   FSUBNAME     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 子表代码
   FSUBNAMEAS     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 子表别名
   FPANAME     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 父表代码
   FRETYPE     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 关联类型
   FMTABLEKEY     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 父表关联字段
   FRTABLEKEY     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 子表关联字段
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FBODYID       VARCHAR(50)     NOT NULL DEFAULT '' ,   --父阶表体内码 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_BILLTEMPLATEENTRY1] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FPARENTID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY1'))) 
     Alter Table SYS_BILLTEMPLATEENTRY1 add    FPARENTID     VARCHAR(50) NOT NULL DEFAULT ''     -- 父阶内码
if(not exists(select name from SysColumns where name ='FTABLEID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY1'))) 
     Alter Table SYS_BILLTEMPLATEENTRY1 add    FTABLEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 子表内码
if(not exists(select name from SysColumns where name ='FLEV' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY1'))) 
     Alter Table SYS_BILLTEMPLATEENTRY1 add    FLEV     INT NOT NULL DEFAULT -1     -- 数据表层级
if(not exists(select name from SysColumns where name ='FREID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY1'))) 
     Alter Table SYS_BILLTEMPLATEENTRY1 add    FREID     VARCHAR(50) NOT NULL DEFAULT ''     -- 关系内码
if(not exists(select name from SysColumns where name ='FPATH' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY1'))) 
     Alter Table SYS_BILLTEMPLATEENTRY1 add    FPATH     VARCHAR(2000) NOT NULL DEFAULT ''     -- 关系路径
if(not exists(select name from SysColumns where name ='FSUBNAME' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY1'))) 
     Alter Table SYS_BILLTEMPLATEENTRY1 add    FSUBNAME     VARCHAR(50) NOT NULL DEFAULT ''     -- 子表代码
if(not exists(select name from SysColumns where name ='FSUBNAMEAS' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY1'))) 
     Alter Table SYS_BILLTEMPLATEENTRY1 add    FSUBNAMEAS     VARCHAR(100) NOT NULL DEFAULT ''     -- 子表别名
if(not exists(select name from SysColumns where name ='FPANAME' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY1'))) 
     Alter Table SYS_BILLTEMPLATEENTRY1 add    FPANAME     VARCHAR(50) NOT NULL DEFAULT ''     -- 父表代码
if(not exists(select name from SysColumns where name ='FRETYPE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY1'))) 
     Alter Table SYS_BILLTEMPLATEENTRY1 add    FRETYPE     VARCHAR(1) NOT NULL DEFAULT ''     -- 关联类型
if(not exists(select name from SysColumns where name ='FMTABLEKEY' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY1'))) 
     Alter Table SYS_BILLTEMPLATEENTRY1 add    FMTABLEKEY     VARCHAR(200) NOT NULL DEFAULT ''     -- 父表关联字段
if(not exists(select name from SysColumns where name ='FRTABLEKEY' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY1'))) 
     Alter Table SYS_BILLTEMPLATEENTRY1 add    FRTABLEKEY     VARCHAR(200) NOT NULL DEFAULT ''     -- 子表关联字段
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY1'))) 
     Alter Table SYS_BILLTEMPLATEENTRY1 add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY1'))) 
     Alter Table SYS_BILLTEMPLATEENTRY1 add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY1'))) 
     Alter Table SYS_BILLTEMPLATEENTRY1 add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY1'))) 
     Alter Table SYS_BILLTEMPLATEENTRY1 add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号
if(not exists(select name from SysColumns where name ='FBODYID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY1'))) 
     Alter Table SYS_BILLTEMPLATEENTRY1 add    FBODYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 父阶表体内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY1') AND name = N'SYS_BILLTEMPLATEENTRY1_1' ) 
   Drop Index SYS_BILLTEMPLATEENTRY1.SYS_BILLTEMPLATEENTRY1_1
GO
Create  unique Clustered index SYS_BILLTEMPLATEENTRY1_1 on SYS_BILLTEMPLATEENTRY1(FDETAILID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY1') AND name = N'SYS_BILLTEMPLATEENTRY1_2' ) 
   Drop Index SYS_BILLTEMPLATEENTRY1.SYS_BILLTEMPLATEENTRY1_2
GO
Create  index SYS_BILLTEMPLATEENTRY1_2 on SYS_BILLTEMPLATEENTRY1(FBODYID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY1') AND name = N'SYS_BILLTEMPLATEENTRY1_3' ) 
   Drop Index SYS_BILLTEMPLATEENTRY1.SYS_BILLTEMPLATEENTRY1_3
GO
Create  index SYS_BILLTEMPLATEENTRY1_3 on SYS_BILLTEMPLATEENTRY1(FPARENTID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_BILLTEMPLATEENTRY2 表单数据源字段信息*/ 
 -- DROP TABLE SYS_BILLTEMPLATEENTRY2
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY2') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_BILLTEMPLATEENTRY2(
   FREID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 关系内码
   FFIELDNAME     VARCHAR(500) NOT NULL DEFAULT '' ,    -- 字段栏位
   FFIELDNAMEAS     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 字段别名
   FHEADCAPTION     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 栏位说明
   FSHOWINDEX     INT NOT NULL DEFAULT 0 ,    -- 显示顺序
   FFIELDTYPE     VARCHAR(2) NOT NULL DEFAULT '' ,    -- 字段类型
   FDISPLAYNAME     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 开窗时显示参考字段
   FCAPTION_CHS     NVARCHAR(50) NOT NULL DEFAULT '' ,    -- 第一语言
   FCAPTION_CHT     NVARCHAR(50) NOT NULL DEFAULT '' ,    -- 第二语言
   FCAPTION_EN     NVARCHAR(50) NOT NULL DEFAULT '' ,    -- 第三语言
   FLOOKUPCTLTYPE     VARCHAR(2) NOT NULL DEFAULT '' ,    -- 控件类型
   FLOOKUPCLASSID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 开窗表单编号
   FLOOKUPTYPE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 开窗表单数据源类型
   FLOOKUPLIST     VARCHAR(max) NOT NULL DEFAULT '' ,    -- 开窗数据来源
   FWIDTH     INT NOT NULL DEFAULT 0 ,    -- 显示宽度
   FHEIGHT     INT NOT NULL DEFAULT 0 ,    -- 显示高度
   FISSORT     BIT NOT NULL DEFAULT  0 ,    -- 是否排序
   FISFILTER     BIT NOT NULL DEFAULT  0 ,    -- 是否过滤
   FISSUM     BIT NOT NULL DEFAULT  0 ,    -- 是否合计
   FISVISIBLE     BIT NOT NULL DEFAULT  0 ,    -- 是否显示
   FTEXTALIGNMENT     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 对齐方式
   FFORMAT     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 显示格式
   FMUSTINPUT     BIT NOT NULL DEFAULT  0 ,    -- 允许回车
   FCANTBLANK     BIT NOT NULL DEFAULT  0 ,    -- 是否必录
   FWINSETVAR     VARCHAR(max) NOT NULL DEFAULT '' ,    -- 开窗时需要赋值的字段
   FGROUPKEY     VARCHAR(4) NOT NULL DEFAULT '' ,    -- 分组KEY
   FVALIDATESCRIPT     VARCHAR(1000) NOT NULL DEFAULT '' ,    -- 验证脚本
   FEXSCRIPT     TEXT NOT NULL DEFAULT ''  ,    -- 给值操作
   FBACKGROUNDFIELD     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 背景颜色字段
   FKEYINTYPE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 只读控制
   FCTLREADONLY     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 只读控制字段
   FLEV     INT NOT NULL DEFAULT -1 ,    -- 数据源层级
   FWINKEY     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 开窗主字段
   FWINWHERE     VARCHAR(max) NOT NULL DEFAULT '' ,    -- 开窗条件
   FWINMULTIPLE     BIT NOT NULL DEFAULT  0 ,    -- 开窗时允许多行
   FMAXVALUE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 最大值
   FMINVALUE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 最小值
   FDEFAULTVALUE     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 默认值
   FCOLUMNSPAN     INT NOT NULL DEFAULT 0 ,    -- 合并列数
   FROWSPAN     INT NOT NULL DEFAULT 0 ,    -- 合并行数
   FDECIMALDIGITS     INT NOT NULL DEFAULT 0 ,    -- 小数精度
   FWINTITLE     VARCHAR(255) NOT NULL DEFAULT '' ,    -- 开窗标题
   FWINSQL     TEXT NOT NULL DEFAULT ''  ,    -- 开窗SQL
   FPATH     VARCHAR(2000) NOT NULL DEFAULT '' ,    -- 关系路径
   FENABLEAUDIT     BIT NOT NULL DEFAULT  0 ,    -- 启用审计追踪
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FBODYID       VARCHAR(50)     NOT NULL DEFAULT '' ,   --父阶表体内码 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_BILLTEMPLATEENTRY2] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FREID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FREID     VARCHAR(50) NOT NULL DEFAULT ''     -- 关系内码
if(not exists(select name from SysColumns where name ='FFIELDNAME' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FFIELDNAME     VARCHAR(500) NOT NULL DEFAULT ''     -- 字段栏位
if(not exists(select name from SysColumns where name ='FFIELDNAMEAS' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FFIELDNAMEAS     VARCHAR(100) NOT NULL DEFAULT ''     -- 字段别名
if(not exists(select name from SysColumns where name ='FHEADCAPTION' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FHEADCAPTION     VARCHAR(100) NOT NULL DEFAULT ''     -- 栏位说明
if(not exists(select name from SysColumns where name ='FSHOWINDEX' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FSHOWINDEX     INT NOT NULL DEFAULT 0     -- 显示顺序
if(not exists(select name from SysColumns where name ='FFIELDTYPE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FFIELDTYPE     VARCHAR(2) NOT NULL DEFAULT ''     -- 字段类型
if(not exists(select name from SysColumns where name ='FDISPLAYNAME' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FDISPLAYNAME     VARCHAR(50) NOT NULL DEFAULT ''     -- 开窗时显示参考字段
if(not exists(select name from SysColumns where name ='FCAPTION_CHS' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FCAPTION_CHS     NVARCHAR(50) NOT NULL DEFAULT ''     -- 第一语言
if(not exists(select name from SysColumns where name ='FCAPTION_CHT' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FCAPTION_CHT     NVARCHAR(50) NOT NULL DEFAULT ''     -- 第二语言
if(not exists(select name from SysColumns where name ='FCAPTION_EN' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FCAPTION_EN     NVARCHAR(50) NOT NULL DEFAULT ''     -- 第三语言
if(not exists(select name from SysColumns where name ='FLOOKUPCTLTYPE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FLOOKUPCTLTYPE     VARCHAR(2) NOT NULL DEFAULT ''     -- 控件类型
if(not exists(select name from SysColumns where name ='FLOOKUPCLASSID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FLOOKUPCLASSID     VARCHAR(50) NOT NULL DEFAULT ''     -- 开窗表单编号
if(not exists(select name from SysColumns where name ='FLOOKUPTYPE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FLOOKUPTYPE     VARCHAR(50) NOT NULL DEFAULT ''     -- 开窗表单数据源类型
if(not exists(select name from SysColumns where name ='FLOOKUPLIST' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FLOOKUPLIST     VARCHAR(max) NOT NULL DEFAULT ''     -- 开窗数据来源
if(not exists(select name from SysColumns where name ='FWIDTH' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FWIDTH     INT NOT NULL DEFAULT 0     -- 显示宽度
if(not exists(select name from SysColumns where name ='FHEIGHT' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FHEIGHT     INT NOT NULL DEFAULT 0     -- 显示高度
if(not exists(select name from SysColumns where name ='FISSORT' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FISSORT     BIT NOT NULL DEFAULT  0     -- 是否排序
if(not exists(select name from SysColumns where name ='FISFILTER' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FISFILTER     BIT NOT NULL DEFAULT  0     -- 是否过滤
if(not exists(select name from SysColumns where name ='FISSUM' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FISSUM     BIT NOT NULL DEFAULT  0     -- 是否合计
if(not exists(select name from SysColumns where name ='FISVISIBLE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FISVISIBLE     BIT NOT NULL DEFAULT  0     -- 是否显示
if(not exists(select name from SysColumns where name ='FTEXTALIGNMENT' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FTEXTALIGNMENT     VARCHAR(1) NOT NULL DEFAULT ''     -- 对齐方式
if(not exists(select name from SysColumns where name ='FFORMAT' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FFORMAT     VARCHAR(50) NOT NULL DEFAULT ''     -- 显示格式
if(not exists(select name from SysColumns where name ='FMUSTINPUT' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FMUSTINPUT     BIT NOT NULL DEFAULT  0     -- 允许回车
if(not exists(select name from SysColumns where name ='FCANTBLANK' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FCANTBLANK     BIT NOT NULL DEFAULT  0     -- 是否必录
if(not exists(select name from SysColumns where name ='FWINSETVAR' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FWINSETVAR     VARCHAR(max) NOT NULL DEFAULT ''     -- 开窗时需要赋值的字段
if(not exists(select name from SysColumns where name ='FGROUPKEY' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FGROUPKEY     VARCHAR(4) NOT NULL DEFAULT ''     -- 分组KEY
if(not exists(select name from SysColumns where name ='FVALIDATESCRIPT' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FVALIDATESCRIPT     VARCHAR(1000) NOT NULL DEFAULT ''     -- 验证脚本
if(not exists(select name from SysColumns where name ='FEXSCRIPT' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FEXSCRIPT     TEXT NOT NULL DEFAULT ''      -- 给值操作
if(not exists(select name from SysColumns where name ='FBACKGROUNDFIELD' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FBACKGROUNDFIELD     VARCHAR(50) NOT NULL DEFAULT ''     -- 背景颜色字段
if(not exists(select name from SysColumns where name ='FKEYINTYPE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FKEYINTYPE     VARCHAR(50) NOT NULL DEFAULT ''     -- 只读控制
if(not exists(select name from SysColumns where name ='FCTLREADONLY' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FCTLREADONLY     VARCHAR(100) NOT NULL DEFAULT ''     -- 只读控制字段
if(not exists(select name from SysColumns where name ='FLEV' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FLEV     INT NOT NULL DEFAULT -1     -- 数据源层级
if(not exists(select name from SysColumns where name ='FWINKEY' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FWINKEY     VARCHAR(50) NOT NULL DEFAULT ''     -- 开窗主字段
if(not exists(select name from SysColumns where name ='FWINWHERE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FWINWHERE     VARCHAR(max) NOT NULL DEFAULT ''     -- 开窗条件
if(not exists(select name from SysColumns where name ='FWINMULTIPLE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FWINMULTIPLE     BIT NOT NULL DEFAULT  0     -- 开窗时允许多行
if(not exists(select name from SysColumns where name ='FMAXVALUE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FMAXVALUE     VARCHAR(50) NOT NULL DEFAULT ''     -- 最大值
if(not exists(select name from SysColumns where name ='FMINVALUE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FMINVALUE     VARCHAR(50) NOT NULL DEFAULT ''     -- 最小值
if(not exists(select name from SysColumns where name ='FDEFAULTVALUE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FDEFAULTVALUE     VARCHAR(100) NOT NULL DEFAULT ''     -- 默认值
if(not exists(select name from SysColumns where name ='FCOLUMNSPAN' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FCOLUMNSPAN     INT NOT NULL DEFAULT 0     -- 合并列数
if(not exists(select name from SysColumns where name ='FROWSPAN' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FROWSPAN     INT NOT NULL DEFAULT 0     -- 合并行数
if(not exists(select name from SysColumns where name ='FDECIMALDIGITS' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FDECIMALDIGITS     INT NOT NULL DEFAULT 0     -- 小数精度
if(not exists(select name from SysColumns where name ='FWINTITLE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FWINTITLE     VARCHAR(255) NOT NULL DEFAULT ''     -- 开窗标题
if(not exists(select name from SysColumns where name ='FWINSQL' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FWINSQL     TEXT NOT NULL DEFAULT ''      -- 开窗SQL
if(not exists(select name from SysColumns where name ='FPATH' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FPATH     VARCHAR(2000) NOT NULL DEFAULT ''     -- 关系路径
if(not exists(select name from SysColumns where name ='FENABLEAUDIT' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FENABLEAUDIT     BIT NOT NULL DEFAULT  0     -- 启用审计追踪
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号
if(not exists(select name from SysColumns where name ='FBODYID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY2'))) 
     Alter Table SYS_BILLTEMPLATEENTRY2 add    FBODYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 父阶表体内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY2') AND name = N'SYS_BILLTEMPLATEENTRY2_1' ) 
   Drop Index SYS_BILLTEMPLATEENTRY2.SYS_BILLTEMPLATEENTRY2_1
GO
Create  unique Clustered index SYS_BILLTEMPLATEENTRY2_1 on SYS_BILLTEMPLATEENTRY2(FDETAILID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY2') AND name = N'SYS_BILLTEMPLATEENTRY2_2' ) 
   Drop Index SYS_BILLTEMPLATEENTRY2.SYS_BILLTEMPLATEENTRY2_2
GO
Create  index SYS_BILLTEMPLATEENTRY2_2 on SYS_BILLTEMPLATEENTRY2(FBODYID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_PARAMETERS 系统参数表*/ 
 -- DROP TABLE SYS_PARAMETERS
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_PARAMETERS') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_PARAMETERS(
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 参数代码
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 参数名称
   FCNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 参数名称(第一语言)
   FTNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 参数名称(第二语言)
   FENAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 参数名称(第三语言)
   FVALUE     VARCHAR(255) NOT NULL DEFAULT '' ,    -- 参数值
   FTYPE     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 数据类别
   ISDEFAULT     BIT NOT NULL DEFAULT  0 ,    -- 是否系统默认
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_PARAMETERS] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_PARAMETERS'))) 
     Alter Table SYS_PARAMETERS add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 参数代码
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_PARAMETERS'))) 
     Alter Table SYS_PARAMETERS add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 参数名称
if(not exists(select name from SysColumns where name ='FCNAME' and id= OBJECT_ID('SYS_PARAMETERS'))) 
     Alter Table SYS_PARAMETERS add    FCNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 参数名称(第一语言)
if(not exists(select name from SysColumns where name ='FTNAME' and id= OBJECT_ID('SYS_PARAMETERS'))) 
     Alter Table SYS_PARAMETERS add    FTNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 参数名称(第二语言)
if(not exists(select name from SysColumns where name ='FENAME' and id= OBJECT_ID('SYS_PARAMETERS'))) 
     Alter Table SYS_PARAMETERS add    FENAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 参数名称(第三语言)
if(not exists(select name from SysColumns where name ='FVALUE' and id= OBJECT_ID('SYS_PARAMETERS'))) 
     Alter Table SYS_PARAMETERS add    FVALUE     VARCHAR(255) NOT NULL DEFAULT ''     -- 参数值
if(not exists(select name from SysColumns where name ='FTYPE' and id= OBJECT_ID('SYS_PARAMETERS'))) 
     Alter Table SYS_PARAMETERS add    FTYPE     VARCHAR(1) NOT NULL DEFAULT ''     -- 数据类别
if(not exists(select name from SysColumns where name ='ISDEFAULT' and id= OBJECT_ID('SYS_PARAMETERS'))) 
     Alter Table SYS_PARAMETERS add    ISDEFAULT     BIT NOT NULL DEFAULT  0     -- 是否系统默认
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_PARAMETERS'))) 
     Alter Table SYS_PARAMETERS add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_PARAMETERS'))) 
     Alter Table SYS_PARAMETERS add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_PARAMETERS'))) 
     Alter Table SYS_PARAMETERS add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_PARAMETERS') AND name = N'SYS_PARAMETERS_1' ) 
   Drop Index SYS_PARAMETERS.SYS_PARAMETERS_1
GO
Create  unique Clustered index SYS_PARAMETERS_1 on SYS_PARAMETERS(FNUMBER)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_TASK 智能调度平台*/ 
 -- DROP TABLE SYS_TASK
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_TASK') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_TASK(
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 调度代码
   FNAME     VARCHAR(255) NOT NULL DEFAULT '' ,    -- 调度名称
   FJOBNAME     VARCHAR(300) NOT NULL DEFAULT '' ,    -- 作业名称
   FJOBGROUP     NVARCHAR(300) NOT NULL DEFAULT '' ,    -- 作业分组
   FJOBDES     NVARCHAR(1000) NOT NULL DEFAULT '' ,    -- 作业描述
   FJOBPARAM     NVARCHAR(1000) NOT NULL DEFAULT '' ,    -- 作业参数
   FJOBTYPE     NVARCHAR(300) NOT NULL DEFAULT '' ,    -- 作业程序集
   FTRIGGERNAME     NVARCHAR(300) NOT NULL DEFAULT '' ,    -- 触发器名称
   FTRIGGERGROUP     NVARCHAR(300) NOT NULL DEFAULT '' ,    -- 触发器分组
   FTRIGGERDES     NVARCHAR(1000) NOT NULL DEFAULT '' ,    -- 触发器描述
   FTRIGGERCRON     NVARCHAR(1000) NOT NULL DEFAULT '' ,    -- Cron表达式
   FCRONREMARK     NVARCHAR(1000) NOT NULL DEFAULT '' ,    -- Cron描述
   FSDTIME     DATETIME NULL  ,    -- 开始时间
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FBILLNO	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 单据编号 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_TASK] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 调度代码
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FNAME     VARCHAR(255) NOT NULL DEFAULT ''     -- 调度名称
if(not exists(select name from SysColumns where name ='FJOBNAME' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FJOBNAME     VARCHAR(300) NOT NULL DEFAULT ''     -- 作业名称
if(not exists(select name from SysColumns where name ='FJOBGROUP' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FJOBGROUP     NVARCHAR(300) NOT NULL DEFAULT ''     -- 作业分组
if(not exists(select name from SysColumns where name ='FJOBDES' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FJOBDES     NVARCHAR(1000) NOT NULL DEFAULT ''     -- 作业描述
if(not exists(select name from SysColumns where name ='FJOBPARAM' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FJOBPARAM     NVARCHAR(1000) NOT NULL DEFAULT ''     -- 作业参数
if(not exists(select name from SysColumns where name ='FJOBTYPE' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FJOBTYPE     NVARCHAR(300) NOT NULL DEFAULT ''     -- 作业程序集
if(not exists(select name from SysColumns where name ='FTRIGGERNAME' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FTRIGGERNAME     NVARCHAR(300) NOT NULL DEFAULT ''     -- 触发器名称
if(not exists(select name from SysColumns where name ='FTRIGGERGROUP' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FTRIGGERGROUP     NVARCHAR(300) NOT NULL DEFAULT ''     -- 触发器分组
if(not exists(select name from SysColumns where name ='FTRIGGERDES' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FTRIGGERDES     NVARCHAR(1000) NOT NULL DEFAULT ''     -- 触发器描述
if(not exists(select name from SysColumns where name ='FTRIGGERCRON' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FTRIGGERCRON     NVARCHAR(1000) NOT NULL DEFAULT ''     -- Cron表达式
if(not exists(select name from SysColumns where name ='FCRONREMARK' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FCRONREMARK     NVARCHAR(1000) NOT NULL DEFAULT ''     -- Cron描述
if(not exists(select name from SysColumns where name ='FSDTIME' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FSDTIME     DATETIME NULL      -- 开始时间
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FBILLNO' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FBILLNO     VARCHAR(50) NOT NULL DEFAULT ''      -- 单据编号
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_TASK'))) 
     Alter Table SYS_TASK add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_TASK') AND name = N'SYS_TASK_1' ) 
   Drop Index SYS_TASK.SYS_TASK_1
GO
Create  unique Clustered index SYS_TASK_1 on SYS_TASK(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_TASK') AND name = N'SYS_TASK_2' ) 
   Drop Index SYS_TASK.SYS_TASK_2
GO
Create  unique index SYS_TASK_2 on SYS_TASK(FNUMBER)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_BILLTEMPLATEENTRY4 单据报表模板*/ 
 -- DROP TABLE SYS_BILLTEMPLATEENTRY4
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY4') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_BILLTEMPLATEENTRY4(
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 模板代码
   FCAPTION     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 模板描述
   FCAPTIONGB     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 模板描述
   FCAPTIONTW     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 模板描述
   FCAPTIONEN     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 模板描述
   FTEMPLATE     IMAGE NULL  ,    -- 报表模板
   FISDEFAULT     BIT NOT NULL DEFAULT  0 ,    -- 是否默认模板
   FISPDF     BIT NOT NULL DEFAULT  0 ,    -- 是否打印成PDF
   FSQL     VARCHAR(max) NOT NULL DEFAULT '' ,    -- 自定义SQL
   FISCUSTOMSQL     BIT NOT NULL DEFAULT  0 ,    -- 是否自定义SQL
   FSTRWHERE     VARCHAR(max) NOT NULL DEFAULT '' ,    -- 选择模板条件
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FBODYID       VARCHAR(50)     NOT NULL DEFAULT '' ,   --父阶表体内码 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_BILLTEMPLATEENTRY4] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY4'))) 
     Alter Table SYS_BILLTEMPLATEENTRY4 add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 模板代码
if(not exists(select name from SysColumns where name ='FCAPTION' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY4'))) 
     Alter Table SYS_BILLTEMPLATEENTRY4 add    FCAPTION     NVARCHAR(255) NOT NULL DEFAULT ''     -- 模板描述
if(not exists(select name from SysColumns where name ='FCAPTIONGB' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY4'))) 
     Alter Table SYS_BILLTEMPLATEENTRY4 add    FCAPTIONGB     NVARCHAR(255) NOT NULL DEFAULT ''     -- 模板描述
if(not exists(select name from SysColumns where name ='FCAPTIONTW' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY4'))) 
     Alter Table SYS_BILLTEMPLATEENTRY4 add    FCAPTIONTW     NVARCHAR(255) NOT NULL DEFAULT ''     -- 模板描述
if(not exists(select name from SysColumns where name ='FCAPTIONEN' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY4'))) 
     Alter Table SYS_BILLTEMPLATEENTRY4 add    FCAPTIONEN     NVARCHAR(255) NOT NULL DEFAULT ''     -- 模板描述
if(not exists(select name from SysColumns where name ='FTEMPLATE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY4'))) 
     Alter Table SYS_BILLTEMPLATEENTRY4 add    FTEMPLATE     IMAGE NULL      -- 报表模板
if(not exists(select name from SysColumns where name ='FISDEFAULT' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY4'))) 
     Alter Table SYS_BILLTEMPLATEENTRY4 add    FISDEFAULT     BIT NOT NULL DEFAULT  0     -- 是否默认模板
if(not exists(select name from SysColumns where name ='FISPDF' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY4'))) 
     Alter Table SYS_BILLTEMPLATEENTRY4 add    FISPDF     BIT NOT NULL DEFAULT  0     -- 是否打印成PDF
if(not exists(select name from SysColumns where name ='FSQL' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY4'))) 
     Alter Table SYS_BILLTEMPLATEENTRY4 add    FSQL     VARCHAR(max) NOT NULL DEFAULT ''     -- 自定义SQL
if(not exists(select name from SysColumns where name ='FISCUSTOMSQL' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY4'))) 
     Alter Table SYS_BILLTEMPLATEENTRY4 add    FISCUSTOMSQL     BIT NOT NULL DEFAULT  0     -- 是否自定义SQL
if(not exists(select name from SysColumns where name ='FSTRWHERE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY4'))) 
     Alter Table SYS_BILLTEMPLATEENTRY4 add    FSTRWHERE     VARCHAR(max) NOT NULL DEFAULT ''     -- 选择模板条件
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY4'))) 
     Alter Table SYS_BILLTEMPLATEENTRY4 add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY4'))) 
     Alter Table SYS_BILLTEMPLATEENTRY4 add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY4'))) 
     Alter Table SYS_BILLTEMPLATEENTRY4 add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY4'))) 
     Alter Table SYS_BILLTEMPLATEENTRY4 add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号
if(not exists(select name from SysColumns where name ='FBODYID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY4'))) 
     Alter Table SYS_BILLTEMPLATEENTRY4 add    FBODYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 父阶表体内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY4') AND name = N'SYS_BILLTEMPLATEENTRY4_1' ) 
   Drop Index SYS_BILLTEMPLATEENTRY4.SYS_BILLTEMPLATEENTRY4_1
GO
Create  unique Clustered index SYS_BILLTEMPLATEENTRY4_1 on SYS_BILLTEMPLATEENTRY4(FDETAILID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY4') AND name = N'SYS_BILLTEMPLATEENTRY4_2' ) 
   Drop Index SYS_BILLTEMPLATEENTRY4.SYS_BILLTEMPLATEENTRY4_2
GO
Create  index SYS_BILLTEMPLATEENTRY4_2 on SYS_BILLTEMPLATEENTRY4(FINTERID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_BILLTEMPLATEENTRY5 序时簿报表模板*/ 
 -- DROP TABLE SYS_BILLTEMPLATEENTRY5
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY5') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_BILLTEMPLATEENTRY5(
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 模板代码
   FCAPTION     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 模板描述
   FCAPTIONGB     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 模板描述
   FCAPTIONTW     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 模板描述
   FCAPTIONEN     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 模板描述
   FTEMPLATE     IMAGE NULL  ,    -- 报表模板
   FISDEFAULT     BIT NOT NULL DEFAULT  0 ,    -- 是否默认模板
   FISPDF     BIT NOT NULL DEFAULT  0 ,    -- 是否打印成PDF
   FSQL     VARCHAR(max) NOT NULL DEFAULT '' ,    -- 自定义SQL
   FISCUSTOMSQL     BIT NOT NULL DEFAULT  0 ,    -- 是否自定义SQL
   FSTRWHERE     VARCHAR(max) NOT NULL DEFAULT '' ,    -- 选择模板条件
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FBODYID       VARCHAR(50)     NOT NULL DEFAULT '' ,   --父阶表体内码 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_BILLTEMPLATEENTRY5] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY5'))) 
     Alter Table SYS_BILLTEMPLATEENTRY5 add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 模板代码
if(not exists(select name from SysColumns where name ='FCAPTION' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY5'))) 
     Alter Table SYS_BILLTEMPLATEENTRY5 add    FCAPTION     NVARCHAR(255) NOT NULL DEFAULT ''     -- 模板描述
if(not exists(select name from SysColumns where name ='FCAPTIONGB' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY5'))) 
     Alter Table SYS_BILLTEMPLATEENTRY5 add    FCAPTIONGB     NVARCHAR(255) NOT NULL DEFAULT ''     -- 模板描述
if(not exists(select name from SysColumns where name ='FCAPTIONTW' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY5'))) 
     Alter Table SYS_BILLTEMPLATEENTRY5 add    FCAPTIONTW     NVARCHAR(255) NOT NULL DEFAULT ''     -- 模板描述
if(not exists(select name from SysColumns where name ='FCAPTIONEN' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY5'))) 
     Alter Table SYS_BILLTEMPLATEENTRY5 add    FCAPTIONEN     NVARCHAR(255) NOT NULL DEFAULT ''     -- 模板描述
if(not exists(select name from SysColumns where name ='FTEMPLATE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY5'))) 
     Alter Table SYS_BILLTEMPLATEENTRY5 add    FTEMPLATE     IMAGE NULL      -- 报表模板
if(not exists(select name from SysColumns where name ='FISDEFAULT' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY5'))) 
     Alter Table SYS_BILLTEMPLATEENTRY5 add    FISDEFAULT     BIT NOT NULL DEFAULT  0     -- 是否默认模板
if(not exists(select name from SysColumns where name ='FISPDF' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY5'))) 
     Alter Table SYS_BILLTEMPLATEENTRY5 add    FISPDF     BIT NOT NULL DEFAULT  0     -- 是否打印成PDF
if(not exists(select name from SysColumns where name ='FSQL' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY5'))) 
     Alter Table SYS_BILLTEMPLATEENTRY5 add    FSQL     VARCHAR(max) NOT NULL DEFAULT ''     -- 自定义SQL
if(not exists(select name from SysColumns where name ='FISCUSTOMSQL' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY5'))) 
     Alter Table SYS_BILLTEMPLATEENTRY5 add    FISCUSTOMSQL     BIT NOT NULL DEFAULT  0     -- 是否自定义SQL
if(not exists(select name from SysColumns where name ='FSTRWHERE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY5'))) 
     Alter Table SYS_BILLTEMPLATEENTRY5 add    FSTRWHERE     VARCHAR(max) NOT NULL DEFAULT ''     -- 选择模板条件
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY5'))) 
     Alter Table SYS_BILLTEMPLATEENTRY5 add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY5'))) 
     Alter Table SYS_BILLTEMPLATEENTRY5 add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY5'))) 
     Alter Table SYS_BILLTEMPLATEENTRY5 add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY5'))) 
     Alter Table SYS_BILLTEMPLATEENTRY5 add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号
if(not exists(select name from SysColumns where name ='FBODYID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY5'))) 
     Alter Table SYS_BILLTEMPLATEENTRY5 add    FBODYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 父阶表体内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY5') AND name = N'SYS_BILLTEMPLATEENTRY5_1' ) 
   Drop Index SYS_BILLTEMPLATEENTRY5.SYS_BILLTEMPLATEENTRY5_1
GO
Create  unique Clustered index SYS_BILLTEMPLATEENTRY5_1 on SYS_BILLTEMPLATEENTRY5(FDETAILID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY5') AND name = N'SYS_BILLTEMPLATEENTRY5_2' ) 
   Drop Index SYS_BILLTEMPLATEENTRY5.SYS_BILLTEMPLATEENTRY5_2
GO
Create  index SYS_BILLTEMPLATEENTRY5_2 on SYS_BILLTEMPLATEENTRY5(FINTERID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_BILLTEMPLATEDEL 单据删除约束*/ 
 -- DROP TABLE SYS_BILLTEMPLATEDEL
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_BILLTEMPLATEDEL') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_BILLTEMPLATEDEL(
   FTABLENAME     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 引用表名
   FFIELDNAME     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 引用表字段
   FBILLFIELDNAME     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 单据字段
   FWHERE     VARCHAR(max) NOT NULL DEFAULT '' ,    -- 其他Where
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_BILLTEMPLATEDEL] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FTABLENAME' and id= OBJECT_ID('SYS_BILLTEMPLATEDEL'))) 
     Alter Table SYS_BILLTEMPLATEDEL add    FTABLENAME     VARCHAR(100) NOT NULL DEFAULT ''     -- 引用表名
if(not exists(select name from SysColumns where name ='FFIELDNAME' and id= OBJECT_ID('SYS_BILLTEMPLATEDEL'))) 
     Alter Table SYS_BILLTEMPLATEDEL add    FFIELDNAME     VARCHAR(100) NOT NULL DEFAULT ''     -- 引用表字段
if(not exists(select name from SysColumns where name ='FBILLFIELDNAME' and id= OBJECT_ID('SYS_BILLTEMPLATEDEL'))) 
     Alter Table SYS_BILLTEMPLATEDEL add    FBILLFIELDNAME     VARCHAR(100) NOT NULL DEFAULT ''     -- 单据字段
if(not exists(select name from SysColumns where name ='FWHERE' and id= OBJECT_ID('SYS_BILLTEMPLATEDEL'))) 
     Alter Table SYS_BILLTEMPLATEDEL add    FWHERE     VARCHAR(max) NOT NULL DEFAULT ''     -- 其他Where
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_BILLTEMPLATEDEL'))) 
     Alter Table SYS_BILLTEMPLATEDEL add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_BILLTEMPLATEDEL'))) 
     Alter Table SYS_BILLTEMPLATEDEL add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_BILLTEMPLATEDEL'))) 
     Alter Table SYS_BILLTEMPLATEDEL add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_BILLTEMPLATEDEL'))) 
     Alter Table SYS_BILLTEMPLATEDEL add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATEDEL') AND name = N'SYS_BILLTEMPLATEDEL_1' ) 
   Drop Index SYS_BILLTEMPLATEDEL.SYS_BILLTEMPLATEDEL_1
GO
Create  unique Clustered index SYS_BILLTEMPLATEDEL_1 on SYS_BILLTEMPLATEDEL(FTABLENAME,FFIELDNAME)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATEDEL') AND name = N'SYS_BILLTEMPLATEDEL_2' ) 
   Drop Index SYS_BILLTEMPLATEDEL.SYS_BILLTEMPLATEDEL_2
GO
Create  unique index SYS_BILLTEMPLATEDEL_2 on SYS_BILLTEMPLATEDEL(FINTERID,FENTRYID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_KBSET 看板配置表-表头*/ 
 -- DROP TABLE SYS_KBSET
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_KBSET') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_KBSET(
   FNAME     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 看板说明
   FTITLEHEIGHT     DECIMAL(24,6) NOT NULL DEFAULT 0 ,    -- 看板标题高度
   FHASTITLE     BIT NOT NULL DEFAULT  0 ,    -- 是否显示主标题
   FSHOWSUBTITLE     BIT NOT NULL DEFAULT  0 ,    -- 是否显示副标题
   FSHOWTIME     BIT NOT NULL DEFAULT  0 ,    -- 是否显示时间
   FTITLE     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 主标题
   FSUBTITLE     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 副标题
   FSFONTSIZE     INT NOT NULL DEFAULT 0 ,    -- 副标题字体大小
   FMFONTSIZE     INT NOT NULL DEFAULT 0 ,    -- 主标题字体大小
   FPERIOD     INT NOT NULL DEFAULT 0 ,    -- 看板刷新频率(秒)
   FBARMAXWIDTH     DECIMAL(24,6) NOT NULL DEFAULT 0 ,    -- 看板内柱状图最大宽度
   FROW     INT NOT NULL DEFAULT 0 ,    -- 看板表格布局行数
   FCOLUMN     INT NOT NULL DEFAULT 0 ,    -- 看板表格布局列数
   FLeftControl     VARCHAR(max) NOT NULL DEFAULT '' ,    -- 标题左边控件
   FRightControl     VARCHAR(max) NOT NULL DEFAULT '' ,    -- 标题右边控件
   FISPHONE     BIT NOT NULL DEFAULT  0 ,    -- 手机展示
   FISLANDSCAPE     BIT NOT NULL DEFAULT  0 ,    -- 横屏展示
   FSUBTITLEWIDHT     DECIMAL(24,6) NOT NULL DEFAULT 0 ,    -- 副标题宽
   FTITLEWIDHT     DECIMAL(24,6) NOT NULL DEFAULT 0 ,    -- 主标题宽
   FSHOWLINE     BIT NOT NULL DEFAULT  0 ,    -- 显示分隔线
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FBILLNO	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 单据编号 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_KBSET] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FNAME     NVARCHAR(255) NOT NULL DEFAULT ''     -- 看板说明
if(not exists(select name from SysColumns where name ='FTITLEHEIGHT' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FTITLEHEIGHT     DECIMAL(24,6) NOT NULL DEFAULT 0     -- 看板标题高度
if(not exists(select name from SysColumns where name ='FHASTITLE' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FHASTITLE     BIT NOT NULL DEFAULT  0     -- 是否显示主标题
if(not exists(select name from SysColumns where name ='FSHOWSUBTITLE' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FSHOWSUBTITLE     BIT NOT NULL DEFAULT  0     -- 是否显示副标题
if(not exists(select name from SysColumns where name ='FSHOWTIME' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FSHOWTIME     BIT NOT NULL DEFAULT  0     -- 是否显示时间
if(not exists(select name from SysColumns where name ='FTITLE' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FTITLE     NVARCHAR(200) NOT NULL DEFAULT ''     -- 主标题
if(not exists(select name from SysColumns where name ='FSUBTITLE' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FSUBTITLE     NVARCHAR(200) NOT NULL DEFAULT ''     -- 副标题
if(not exists(select name from SysColumns where name ='FSFONTSIZE' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FSFONTSIZE     INT NOT NULL DEFAULT 0     -- 副标题字体大小
if(not exists(select name from SysColumns where name ='FMFONTSIZE' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FMFONTSIZE     INT NOT NULL DEFAULT 0     -- 主标题字体大小
if(not exists(select name from SysColumns where name ='FPERIOD' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FPERIOD     INT NOT NULL DEFAULT 0     -- 看板刷新频率(秒)
if(not exists(select name from SysColumns where name ='FBARMAXWIDTH' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FBARMAXWIDTH     DECIMAL(24,6) NOT NULL DEFAULT 0     -- 看板内柱状图最大宽度
if(not exists(select name from SysColumns where name ='FROW' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FROW     INT NOT NULL DEFAULT 0     -- 看板表格布局行数
if(not exists(select name from SysColumns where name ='FCOLUMN' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FCOLUMN     INT NOT NULL DEFAULT 0     -- 看板表格布局列数
if(not exists(select name from SysColumns where name ='FLeftControl' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FLeftControl     VARCHAR(max) NOT NULL DEFAULT ''     -- 标题左边控件
if(not exists(select name from SysColumns where name ='FRightControl' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FRightControl     VARCHAR(max) NOT NULL DEFAULT ''     -- 标题右边控件
if(not exists(select name from SysColumns where name ='FISPHONE' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FISPHONE     BIT NOT NULL DEFAULT  0     -- 手机展示
if(not exists(select name from SysColumns where name ='FISLANDSCAPE' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FISLANDSCAPE     BIT NOT NULL DEFAULT  0     -- 横屏展示
if(not exists(select name from SysColumns where name ='FSUBTITLEWIDHT' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FSUBTITLEWIDHT     DECIMAL(24,6) NOT NULL DEFAULT 0     -- 副标题宽
if(not exists(select name from SysColumns where name ='FTITLEWIDHT' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FTITLEWIDHT     DECIMAL(24,6) NOT NULL DEFAULT 0     -- 主标题宽
if(not exists(select name from SysColumns where name ='FSHOWLINE' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FSHOWLINE     BIT NOT NULL DEFAULT  0     -- 显示分隔线
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FBILLNO' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FBILLNO     VARCHAR(50) NOT NULL DEFAULT ''      -- 单据编号
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_KBSET'))) 
     Alter Table SYS_KBSET add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_KBSET') AND name = N'SYS_KBSET_1' ) 
   Drop Index SYS_KBSET.SYS_KBSET_1
GO
Create  unique index SYS_KBSET_1 on SYS_KBSET(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_KBSET') AND name = N'SYS_KBSET_2' ) 
   Drop Index SYS_KBSET.SYS_KBSET_2
GO
Create  unique Clustered index SYS_KBSET_2 on SYS_KBSET(FBILLNO)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_KBSETENTRY 看板配置表-数据源*/ 
 -- DROP TABLE SYS_KBSETENTRY
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_KBSETENTRY') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_KBSETENTRY(
   FSQL     VARCHAR(max) NOT NULL DEFAULT '' ,    -- SQL语句
   FNOTE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 备注
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_KBSETENTRY] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FSQL' and id= OBJECT_ID('SYS_KBSETENTRY'))) 
     Alter Table SYS_KBSETENTRY add    FSQL     VARCHAR(max) NOT NULL DEFAULT ''     -- SQL语句
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_KBSETENTRY'))) 
     Alter Table SYS_KBSETENTRY add    FNOTE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 备注
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_KBSETENTRY'))) 
     Alter Table SYS_KBSETENTRY add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_KBSETENTRY'))) 
     Alter Table SYS_KBSETENTRY add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_KBSETENTRY'))) 
     Alter Table SYS_KBSETENTRY add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_KBSETENTRY'))) 
     Alter Table SYS_KBSETENTRY add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_KBSETENTRY') AND name = N'SYS_KBSETENTRY_1' ) 
   Drop Index SYS_KBSETENTRY.SYS_KBSETENTRY_1
GO
Create  unique Clustered index SYS_KBSETENTRY_1 on SYS_KBSETENTRY(FINTERID,FENTRYID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_KBSETENTRY') AND name = N'SYS_KBSETENTRY_2' ) 
   Drop Index SYS_KBSETENTRY.SYS_KBSETENTRY_2
GO
Create  unique index SYS_KBSETENTRY_2 on SYS_KBSETENTRY(FDETAILID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_KBSETENTRY1 看板配置表-面板信息*/ 
 -- DROP TABLE SYS_KBSETENTRY1
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_KBSETENTRY1') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_KBSETENTRY1(
   FROW     INT NOT NULL DEFAULT 0 ,    -- 面板所在行(从零开始)
   FCOLUMN     INT NOT NULL DEFAULT 0 ,    -- 面板所在列(从零开始)
   FROWSPAN     INT NOT NULL DEFAULT 0 ,    -- 合并行数
   FCOLUMNSPAN     INT NOT NULL DEFAULT 0 ,    -- 合并列数
   FHASTITLE     BIT NOT NULL DEFAULT  0 ,    -- 是否显示标题栏
   FTITLE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 面板标题
   FORIENTATION     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 显示方向(0=水平;1=垂直)
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_KBSETENTRY1] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FROW' and id= OBJECT_ID('SYS_KBSETENTRY1'))) 
     Alter Table SYS_KBSETENTRY1 add    FROW     INT NOT NULL DEFAULT 0     -- 面板所在行(从零开始)
if(not exists(select name from SysColumns where name ='FCOLUMN' and id= OBJECT_ID('SYS_KBSETENTRY1'))) 
     Alter Table SYS_KBSETENTRY1 add    FCOLUMN     INT NOT NULL DEFAULT 0     -- 面板所在列(从零开始)
if(not exists(select name from SysColumns where name ='FROWSPAN' and id= OBJECT_ID('SYS_KBSETENTRY1'))) 
     Alter Table SYS_KBSETENTRY1 add    FROWSPAN     INT NOT NULL DEFAULT 0     -- 合并行数
if(not exists(select name from SysColumns where name ='FCOLUMNSPAN' and id= OBJECT_ID('SYS_KBSETENTRY1'))) 
     Alter Table SYS_KBSETENTRY1 add    FCOLUMNSPAN     INT NOT NULL DEFAULT 0     -- 合并列数
if(not exists(select name from SysColumns where name ='FHASTITLE' and id= OBJECT_ID('SYS_KBSETENTRY1'))) 
     Alter Table SYS_KBSETENTRY1 add    FHASTITLE     BIT NOT NULL DEFAULT  0     -- 是否显示标题栏
if(not exists(select name from SysColumns where name ='FTITLE' and id= OBJECT_ID('SYS_KBSETENTRY1'))) 
     Alter Table SYS_KBSETENTRY1 add    FTITLE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 面板标题
if(not exists(select name from SysColumns where name ='FORIENTATION' and id= OBJECT_ID('SYS_KBSETENTRY1'))) 
     Alter Table SYS_KBSETENTRY1 add    FORIENTATION     VARCHAR(1) NOT NULL DEFAULT ''     -- 显示方向(0=水平;1=垂直)
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_KBSETENTRY1'))) 
     Alter Table SYS_KBSETENTRY1 add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_KBSETENTRY1'))) 
     Alter Table SYS_KBSETENTRY1 add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_KBSETENTRY1'))) 
     Alter Table SYS_KBSETENTRY1 add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_KBSETENTRY1'))) 
     Alter Table SYS_KBSETENTRY1 add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_KBSETENTRY1') AND name = N'SYS_KBSETENTRY1_1' ) 
   Drop Index SYS_KBSETENTRY1.SYS_KBSETENTRY1_1
GO
Create  unique Clustered index SYS_KBSETENTRY1_1 on SYS_KBSETENTRY1(FINTERID,FENTRYID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_KBSETENTRY1') AND name = N'SYS_KBSETENTRY1_2' ) 
   Drop Index SYS_KBSETENTRY1.SYS_KBSETENTRY1_2
GO
Create  unique index SYS_KBSETENTRY1_2 on SYS_KBSETENTRY1(FDETAILID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_KBSETENTRY2 看板配置表-图表信息*/ 
 -- DROP TABLE SYS_KBSETENTRY2
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_KBSETENTRY2') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_KBSETENTRY2(
   FSOURCE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 看板数据源
   FKBTYPE     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 看板类别
   FFULLROWCOLOR     BIT NOT NULL DEFAULT  0 ,    -- 同行统一前景色
   FHASHEADER     BIT NOT NULL DEFAULT  0 ,    -- 是否表头
   FHASBKGROUP     BIT NOT NULL DEFAULT  0 ,    -- 是否有背景颜色
   FXAML     VARCHAR(max) NOT NULL DEFAULT '' ,    -- 自定义时的脚本
   FTIME     INT NOT NULL DEFAULT 3 ,    -- 看板滚动时间(秒)
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FBODYID       VARCHAR(50)     NOT NULL DEFAULT '' ,   --父阶表体内码 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_KBSETENTRY2] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FSOURCE' and id= OBJECT_ID('SYS_KBSETENTRY2'))) 
     Alter Table SYS_KBSETENTRY2 add    FSOURCE     VARCHAR(50) NOT NULL DEFAULT ''     -- 看板数据源
if(not exists(select name from SysColumns where name ='FKBTYPE' and id= OBJECT_ID('SYS_KBSETENTRY2'))) 
     Alter Table SYS_KBSETENTRY2 add    FKBTYPE     VARCHAR(1) NOT NULL DEFAULT ''     -- 看板类别
if(not exists(select name from SysColumns where name ='FFULLROWCOLOR' and id= OBJECT_ID('SYS_KBSETENTRY2'))) 
     Alter Table SYS_KBSETENTRY2 add    FFULLROWCOLOR     BIT NOT NULL DEFAULT  0     -- 同行统一前景色
if(not exists(select name from SysColumns where name ='FHASHEADER' and id= OBJECT_ID('SYS_KBSETENTRY2'))) 
     Alter Table SYS_KBSETENTRY2 add    FHASHEADER     BIT NOT NULL DEFAULT  0     -- 是否表头
if(not exists(select name from SysColumns where name ='FHASBKGROUP' and id= OBJECT_ID('SYS_KBSETENTRY2'))) 
     Alter Table SYS_KBSETENTRY2 add    FHASBKGROUP     BIT NOT NULL DEFAULT  0     -- 是否有背景颜色
if(not exists(select name from SysColumns where name ='FXAML' and id= OBJECT_ID('SYS_KBSETENTRY2'))) 
     Alter Table SYS_KBSETENTRY2 add    FXAML     VARCHAR(max) NOT NULL DEFAULT ''     -- 自定义时的脚本
if(not exists(select name from SysColumns where name ='FTIME' and id= OBJECT_ID('SYS_KBSETENTRY2'))) 
     Alter Table SYS_KBSETENTRY2 add    FTIME     INT NOT NULL DEFAULT 3     -- 看板滚动时间(秒)
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_KBSETENTRY2'))) 
     Alter Table SYS_KBSETENTRY2 add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_KBSETENTRY2'))) 
     Alter Table SYS_KBSETENTRY2 add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_KBSETENTRY2'))) 
     Alter Table SYS_KBSETENTRY2 add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_KBSETENTRY2'))) 
     Alter Table SYS_KBSETENTRY2 add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号
if(not exists(select name from SysColumns where name ='FBODYID' and id= OBJECT_ID('SYS_KBSETENTRY2'))) 
     Alter Table SYS_KBSETENTRY2 add    FBODYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 父阶表体内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_KBSETENTRY2') AND name = N'SYS_KBSETENTRY2_1' ) 
   Drop Index SYS_KBSETENTRY2.SYS_KBSETENTRY2_1
GO
Create  unique Clustered index SYS_KBSETENTRY2_1 on SYS_KBSETENTRY2(FBODYID,FENTRYID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_KBSETENTRY2') AND name = N'SYS_KBSETENTRY2_2' ) 
   Drop Index SYS_KBSETENTRY2.SYS_KBSETENTRY2_2
GO
Create  unique index SYS_KBSETENTRY2_2 on SYS_KBSETENTRY2(FDETAILID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_KBSETENTRY3 看板配置表-图表信息-系列明细*/ 
 -- DROP TABLE SYS_KBSETENTRY3
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_KBSETENTRY3') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_KBSETENTRY3(
   FPANELID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 面板内码
   FCAPTION     NVARCHAR(100) NOT NULL DEFAULT '' ,    -- 系列标题
   FXCATEGORY     VARCHAR(50) NOT NULL DEFAULT '' ,    -- X坐标字段
   FYVALUE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- Y坐标字段
   FYMAX     DECIMAL(24,6) NOT NULL DEFAULT 0 ,    -- Y坐标最大值
   FYMIN     DECIMAL(24,6) NOT NULL DEFAULT 0 ,    -- Y坐标最小值
   FYFORMAT     VARCHAR(20) NOT NULL DEFAULT '' ,    -- Y坐标格式
   FMAPPINTTYPE     VARCHAR(2) NOT NULL DEFAULT '' ,    -- 系列图类别
   FWIDTH     DECIMAL(24,6) NOT NULL DEFAULT 0 ,    -- 显示宽度
   FCOLOR     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 元素颜色
   FFONTSIZE     DECIMAL(24,6) NOT NULL DEFAULT 20 ,    -- 字体大小
   FFORMAT     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 显示格式
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FBODYID       VARCHAR(50)     NOT NULL DEFAULT '' ,   --父阶表体内码 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_KBSETENTRY3] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FPANELID' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FPANELID     VARCHAR(50) NOT NULL DEFAULT ''     -- 面板内码
if(not exists(select name from SysColumns where name ='FCAPTION' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FCAPTION     NVARCHAR(100) NOT NULL DEFAULT ''     -- 系列标题
if(not exists(select name from SysColumns where name ='FXCATEGORY' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FXCATEGORY     VARCHAR(50) NOT NULL DEFAULT ''     -- X坐标字段
if(not exists(select name from SysColumns where name ='FYVALUE' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FYVALUE     VARCHAR(50) NOT NULL DEFAULT ''     -- Y坐标字段
if(not exists(select name from SysColumns where name ='FYMAX' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FYMAX     DECIMAL(24,6) NOT NULL DEFAULT 0     -- Y坐标最大值
if(not exists(select name from SysColumns where name ='FYMIN' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FYMIN     DECIMAL(24,6) NOT NULL DEFAULT 0     -- Y坐标最小值
if(not exists(select name from SysColumns where name ='FYFORMAT' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FYFORMAT     VARCHAR(20) NOT NULL DEFAULT ''     -- Y坐标格式
if(not exists(select name from SysColumns where name ='FMAPPINTTYPE' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FMAPPINTTYPE     VARCHAR(2) NOT NULL DEFAULT ''     -- 系列图类别
if(not exists(select name from SysColumns where name ='FWIDTH' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FWIDTH     DECIMAL(24,6) NOT NULL DEFAULT 0     -- 显示宽度
if(not exists(select name from SysColumns where name ='FCOLOR' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FCOLOR     VARCHAR(50) NOT NULL DEFAULT ''     -- 元素颜色
if(not exists(select name from SysColumns where name ='FFONTSIZE' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FFONTSIZE     DECIMAL(24,6) NOT NULL DEFAULT 20     -- 字体大小
if(not exists(select name from SysColumns where name ='FFORMAT' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FFORMAT     VARCHAR(50) NOT NULL DEFAULT ''     -- 显示格式
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号
if(not exists(select name from SysColumns where name ='FBODYID' and id= OBJECT_ID('SYS_KBSETENTRY3'))) 
     Alter Table SYS_KBSETENTRY3 add    FBODYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 父阶表体内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_KBSETENTRY3') AND name = N'SYS_KBSETENTRY3_1' ) 
   Drop Index SYS_KBSETENTRY3.SYS_KBSETENTRY3_1
GO
Create  unique Clustered index SYS_KBSETENTRY3_1 on SYS_KBSETENTRY3(FBODYID,FENTRYID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_KBSETENTRY3') AND name = N'SYS_KBSETENTRY3_2' ) 
   Drop Index SYS_KBSETENTRY3.SYS_KBSETENTRY3_2
GO
Create  unique index SYS_KBSETENTRY3_2 on SYS_KBSETENTRY3(FDETAILID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_BILLTEMPLATEENTRY6 表单插件*/ 
 -- DROP TABLE SYS_BILLTEMPLATEENTRY6
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY6') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_BILLTEMPLATEENTRY6(
   FINDEX     INT NOT NULL DEFAULT 0 ,    -- 插件优先顺序
   FPLUGTYPE     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 插件类别
   FASSEMBLY     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 插件程序集
   FNOTE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 插件描述
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_BILLTEMPLATEENTRY6] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FINDEX' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY6'))) 
     Alter Table SYS_BILLTEMPLATEENTRY6 add    FINDEX     INT NOT NULL DEFAULT 0     -- 插件优先顺序
if(not exists(select name from SysColumns where name ='FPLUGTYPE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY6'))) 
     Alter Table SYS_BILLTEMPLATEENTRY6 add    FPLUGTYPE     VARCHAR(1) NOT NULL DEFAULT ''     -- 插件类别
if(not exists(select name from SysColumns where name ='FASSEMBLY' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY6'))) 
     Alter Table SYS_BILLTEMPLATEENTRY6 add    FASSEMBLY     VARCHAR(200) NOT NULL DEFAULT ''     -- 插件程序集
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY6'))) 
     Alter Table SYS_BILLTEMPLATEENTRY6 add    FNOTE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 插件描述
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY6'))) 
     Alter Table SYS_BILLTEMPLATEENTRY6 add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY6'))) 
     Alter Table SYS_BILLTEMPLATEENTRY6 add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY6'))) 
     Alter Table SYS_BILLTEMPLATEENTRY6 add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY6'))) 
     Alter Table SYS_BILLTEMPLATEENTRY6 add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY6') AND name = N'SYS_BILLTEMPLATEENTRY6_1' ) 
   Drop Index SYS_BILLTEMPLATEENTRY6.SYS_BILLTEMPLATEENTRY6_1
GO
Create  unique Clustered index SYS_BILLTEMPLATEENTRY6_1 on SYS_BILLTEMPLATEENTRY6(FINTERID,FENTRYID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY6') AND name = N'SYS_BILLTEMPLATEENTRY6_2' ) 
   Drop Index SYS_BILLTEMPLATEENTRY6.SYS_BILLTEMPLATEENTRY6_2
GO
Create  unique index SYS_BILLTEMPLATEENTRY6_2 on SYS_BILLTEMPLATEENTRY6(FDETAILID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_ENANNOUENTRY 企业新闻公告接收人*/ 
 -- DROP TABLE SYS_ENANNOUENTRY
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_ENANNOUENTRY') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_ENANNOUENTRY(
   FTYPE     VARCHAR(1) NOT NULL DEFAULT '2' ,    -- 接收对象类别(1=内部组织;2=用户;3=客户;4=厂商)
   FPERSONID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 接收对象内码或帐号
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_ENANNOUENTRY] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FTYPE' and id= OBJECT_ID('SYS_ENANNOUENTRY'))) 
     Alter Table SYS_ENANNOUENTRY add    FTYPE     VARCHAR(1) NOT NULL DEFAULT '2'     -- 接收对象类别(1=内部组织;2=用户;3=客户;4=厂商)
if(not exists(select name from SysColumns where name ='FPERSONID' and id= OBJECT_ID('SYS_ENANNOUENTRY'))) 
     Alter Table SYS_ENANNOUENTRY add    FPERSONID     VARCHAR(50) NOT NULL DEFAULT ''     -- 接收对象内码或帐号
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_ENANNOUENTRY'))) 
     Alter Table SYS_ENANNOUENTRY add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_ENANNOUENTRY'))) 
     Alter Table SYS_ENANNOUENTRY add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_ENANNOUENTRY'))) 
     Alter Table SYS_ENANNOUENTRY add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_ENANNOUENTRY'))) 
     Alter Table SYS_ENANNOUENTRY add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_ENANNOUENTRY') AND name = N'SYS_ENANNOUENTRY_1' ) 
   Drop Index SYS_ENANNOUENTRY.SYS_ENANNOUENTRY_1
GO
Create  unique index SYS_ENANNOUENTRY_1 on SYS_ENANNOUENTRY(FDETAILID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_ENANNOUENTRY') AND name = N'SYS_ENANNOUENTRY_2' ) 
   Drop Index SYS_ENANNOUENTRY.SYS_ENANNOUENTRY_2
GO
Create  index SYS_ENANNOUENTRY_2 on SYS_ENANNOUENTRY(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_ENANNOUENTRY') AND name = N'SYS_ENANNOUENTRY_3' ) 
   Drop Index SYS_ENANNOUENTRY.SYS_ENANNOUENTRY_3
GO
Create  index SYS_ENANNOUENTRY_3 on SYS_ENANNOUENTRY(FPERSONID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_ORGANIZATION 客户档案*/ 
 -- DROP TABLE SYS_ORGANIZATION
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_ORGANIZATION') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_ORGANIZATION(
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 代码
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 名称
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_ORGANIZATION] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_ORGANIZATION'))) 
     Alter Table SYS_ORGANIZATION add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 代码
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_ORGANIZATION'))) 
     Alter Table SYS_ORGANIZATION add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 名称
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_ORGANIZATION'))) 
     Alter Table SYS_ORGANIZATION add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_ORGANIZATION'))) 
     Alter Table SYS_ORGANIZATION add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_ORGANIZATION'))) 
     Alter Table SYS_ORGANIZATION add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_ORGANIZATION') AND name = N'SYS_ORGANIZATION_1' ) 
   Drop Index SYS_ORGANIZATION.SYS_ORGANIZATION_1
GO
Create  unique index SYS_ORGANIZATION_1 on SYS_ORGANIZATION(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_ORGANIZATION') AND name = N'SYS_ORGANIZATION_2' ) 
   Drop Index SYS_ORGANIZATION.SYS_ORGANIZATION_2
GO
Create  Clustered index SYS_ORGANIZATION_2 on SYS_ORGANIZATION(FNUMBER)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_SUPPLIER 供应商档案*/ 
 -- DROP TABLE SYS_SUPPLIER
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_SUPPLIER') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_SUPPLIER(
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 代码
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 名称
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_SUPPLIER] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_SUPPLIER'))) 
     Alter Table SYS_SUPPLIER add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 代码
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_SUPPLIER'))) 
     Alter Table SYS_SUPPLIER add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 名称
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_SUPPLIER'))) 
     Alter Table SYS_SUPPLIER add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_SUPPLIER'))) 
     Alter Table SYS_SUPPLIER add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_SUPPLIER'))) 
     Alter Table SYS_SUPPLIER add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_SUPPLIER') AND name = N'SYS_SUPPLIER_1' ) 
   Drop Index SYS_SUPPLIER.SYS_SUPPLIER_1
GO
Create  unique Clustered index SYS_SUPPLIER_1 on SYS_SUPPLIER(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_SUPPLIER') AND name = N'SYS_SUPPLIER_2' ) 
   Drop Index SYS_SUPPLIER.SYS_SUPPLIER_2
GO
Create  unique index SYS_SUPPLIER_2 on SYS_SUPPLIER(FNUMBER)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_USERMESSAGE 用户间信息档*/ 
 -- DROP TABLE SYS_USERMESSAGE
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_USERMESSAGE') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_USERMESSAGE(
   FTITLE     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 信息主题
   FINFO     TEXT NOT NULL DEFAULT ''  ,    -- 信息内容
   FDATETIME     DATETIME NULL  ,    -- 信息发送时间
   FFROMUSER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 信息发送人
   FTOUSER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 信息接收人
   FISALL     BIT NOT NULL DEFAULT  0 ,    -- 是否发送所有人
   FISSYS     BIT NOT NULL DEFAULT  0 ,    -- 是否系统信息
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_USERMESSAGE] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FTITLE' and id= OBJECT_ID('SYS_USERMESSAGE'))) 
     Alter Table SYS_USERMESSAGE add    FTITLE     NVARCHAR(200) NOT NULL DEFAULT ''     -- 信息主题
if(not exists(select name from SysColumns where name ='FINFO' and id= OBJECT_ID('SYS_USERMESSAGE'))) 
     Alter Table SYS_USERMESSAGE add    FINFO     TEXT NOT NULL DEFAULT ''      -- 信息内容
if(not exists(select name from SysColumns where name ='FDATETIME' and id= OBJECT_ID('SYS_USERMESSAGE'))) 
     Alter Table SYS_USERMESSAGE add    FDATETIME     DATETIME NULL      -- 信息发送时间
if(not exists(select name from SysColumns where name ='FFROMUSER' and id= OBJECT_ID('SYS_USERMESSAGE'))) 
     Alter Table SYS_USERMESSAGE add    FFROMUSER     VARCHAR(50) NOT NULL DEFAULT ''     -- 信息发送人
if(not exists(select name from SysColumns where name ='FTOUSER' and id= OBJECT_ID('SYS_USERMESSAGE'))) 
     Alter Table SYS_USERMESSAGE add    FTOUSER     VARCHAR(50) NOT NULL DEFAULT ''     -- 信息接收人
if(not exists(select name from SysColumns where name ='FISALL' and id= OBJECT_ID('SYS_USERMESSAGE'))) 
     Alter Table SYS_USERMESSAGE add    FISALL     BIT NOT NULL DEFAULT  0     -- 是否发送所有人
if(not exists(select name from SysColumns where name ='FISSYS' and id= OBJECT_ID('SYS_USERMESSAGE'))) 
     Alter Table SYS_USERMESSAGE add    FISSYS     BIT NOT NULL DEFAULT  0     -- 是否系统信息
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_USERMESSAGE'))) 
     Alter Table SYS_USERMESSAGE add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_USERMESSAGE'))) 
     Alter Table SYS_USERMESSAGE add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_USERMESSAGE'))) 
     Alter Table SYS_USERMESSAGE add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_USERMESSAGE') AND name = N'SYS_USERMESSAGE_1' ) 
   Drop Index SYS_USERMESSAGE.SYS_USERMESSAGE_1
GO
Create  unique Clustered index SYS_USERMESSAGE_1 on SYS_USERMESSAGE(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_USERMESSAGE') AND name = N'SYS_USERMESSAGE_2' ) 
   Drop Index SYS_USERMESSAGE.SYS_USERMESSAGE_2
GO
Create  index SYS_USERMESSAGE_2 on SYS_USERMESSAGE(FFROMUSER)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_USERMESSAGE') AND name = N'SYS_USERMESSAGE_3' ) 
   Drop Index SYS_USERMESSAGE.SYS_USERMESSAGE_3
GO
Create  index SYS_USERMESSAGE_3 on SYS_USERMESSAGE(FTOUSER)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_USERMESSAGEENTRY 信息是否已读标记档*/ 
 -- DROP TABLE SYS_USERMESSAGEENTRY
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_USERMESSAGEENTRY') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_USERMESSAGEENTRY(
   FDETAILID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 表体内码
   FDATETIME     DATETIME NULL  ,    -- 已读时间
   FREADUSER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 信息读取人
   FISREAD     BIT NOT NULL DEFAULT  0 ,    -- 是否已读
   FISDEL     BIT NOT NULL DEFAULT  0 ,    -- 是否删除
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_USERMESSAGEENTRY] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_USERMESSAGEENTRY'))) 
     Alter Table SYS_USERMESSAGEENTRY add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''     -- 表体内码
if(not exists(select name from SysColumns where name ='FDATETIME' and id= OBJECT_ID('SYS_USERMESSAGEENTRY'))) 
     Alter Table SYS_USERMESSAGEENTRY add    FDATETIME     DATETIME NULL      -- 已读时间
if(not exists(select name from SysColumns where name ='FREADUSER' and id= OBJECT_ID('SYS_USERMESSAGEENTRY'))) 
     Alter Table SYS_USERMESSAGEENTRY add    FREADUSER     VARCHAR(50) NOT NULL DEFAULT ''     -- 信息读取人
if(not exists(select name from SysColumns where name ='FISREAD' and id= OBJECT_ID('SYS_USERMESSAGEENTRY'))) 
     Alter Table SYS_USERMESSAGEENTRY add    FISREAD     BIT NOT NULL DEFAULT  0     -- 是否已读
if(not exists(select name from SysColumns where name ='FISDEL' and id= OBJECT_ID('SYS_USERMESSAGEENTRY'))) 
     Alter Table SYS_USERMESSAGEENTRY add    FISDEL     BIT NOT NULL DEFAULT  0     -- 是否删除
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_USERMESSAGEENTRY'))) 
     Alter Table SYS_USERMESSAGEENTRY add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_USERMESSAGEENTRY'))) 
     Alter Table SYS_USERMESSAGEENTRY add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_USERMESSAGEENTRY'))) 
     Alter Table SYS_USERMESSAGEENTRY add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_USERMESSAGEENTRY') AND name = N'SYS_USERMESSAGEENTRY_1' ) 
   Drop Index SYS_USERMESSAGEENTRY.SYS_USERMESSAGEENTRY_1
GO
Create  unique Clustered index SYS_USERMESSAGEENTRY_1 on SYS_USERMESSAGEENTRY(FDETAILID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_USERMESSAGEENTRY') AND name = N'SYS_USERMESSAGEENTRY_2' ) 
   Drop Index SYS_USERMESSAGEENTRY.SYS_USERMESSAGEENTRY_2
GO
Create  unique index SYS_USERMESSAGEENTRY_2 on SYS_USERMESSAGEENTRY(FINTERID,FREADUSER)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_AUDITLOG 审计追踪日志*/ 
 -- DROP TABLE SYS_AUDITLOG
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_AUDITLOG') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_AUDITLOG(
   FIP     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 客户端IP地址
   FUID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 单据UID
   FPRGKEY     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 单据类型
   FDESCRIPTION     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 描述
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FBILLNO	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 单据编号 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_AUDITLOG] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FIP' and id= OBJECT_ID('SYS_AUDITLOG'))) 
     Alter Table SYS_AUDITLOG add    FIP     VARCHAR(50) NOT NULL DEFAULT ''     -- 客户端IP地址
if(not exists(select name from SysColumns where name ='FUID' and id= OBJECT_ID('SYS_AUDITLOG'))) 
     Alter Table SYS_AUDITLOG add    FUID     VARCHAR(50) NOT NULL DEFAULT ''     -- 单据UID
if(not exists(select name from SysColumns where name ='FPRGKEY' and id= OBJECT_ID('SYS_AUDITLOG'))) 
     Alter Table SYS_AUDITLOG add    FPRGKEY     VARCHAR(50) NOT NULL DEFAULT ''     -- 单据类型
if(not exists(select name from SysColumns where name ='FDESCRIPTION' and id= OBJECT_ID('SYS_AUDITLOG'))) 
     Alter Table SYS_AUDITLOG add    FDESCRIPTION     VARCHAR(50) NOT NULL DEFAULT ''     -- 描述
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_AUDITLOG'))) 
     Alter Table SYS_AUDITLOG add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_AUDITLOG'))) 
     Alter Table SYS_AUDITLOG add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FBILLNO' and id= OBJECT_ID('SYS_AUDITLOG'))) 
     Alter Table SYS_AUDITLOG add    FBILLNO     VARCHAR(50) NOT NULL DEFAULT ''      -- 单据编号
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_AUDITLOG'))) 
     Alter Table SYS_AUDITLOG add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_AUDITLOG') AND name = N'SYS_AUDITLOG_1' ) 
   Drop Index SYS_AUDITLOG.SYS_AUDITLOG_1
GO
Create  index SYS_AUDITLOG_1 on SYS_AUDITLOG(FPRGKEY,FBILLNO)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_AUDITLOG') AND name = N'SYS_AUDITLOG_2' ) 
   Drop Index SYS_AUDITLOG.SYS_AUDITLOG_2
GO
Create  index SYS_AUDITLOG_2 on SYS_AUDITLOG(FUID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_BILLTEMPLATEENTRY 表单数据源信息*/ 
 -- DROP TABLE SYS_BILLTEMPLATEENTRY
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_BILLTEMPLATEENTRY(
   FSTYPE     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 数据源类型
   FNAME     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 数据源名称
   FPARENTID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 父阶内码
   FKEY     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 数据源标识
   FTABLEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 主表内码
   FTABLEKEY     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 数据表标识
   FISUSERDEFINED     BIT NOT NULL DEFAULT  0 ,    -- 是否用户定义
   FUSQL     TEXT NOT NULL DEFAULT ''  ,    -- 用户定义SQL
   FSQL     TEXT NOT NULL DEFAULT ''  ,    -- 系统自动SQL
   FBARLIST     VARCHAR(255) NOT NULL DEFAULT '' ,    -- 条码编码字段
   FWHERE     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 其他条件
   FSWHERE     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 保存时过滤条件
   FISALL     BIT NOT NULL DEFAULT  0 ,    -- 是否抓所有资料
   FLOTLIST     VARCHAR(255) NOT NULL DEFAULT '' ,    -- 批号编码字段
   FORDERKEY     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 排序字段
   FISMAIN     BIT NOT NULL DEFAULT  0 ,    -- 是否主数据源
   FISSAVE     BIT NOT NULL DEFAULT  0 ,    -- 是否保存
   FHEADKEY     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 表头关系字段-表头字段
   FBODYKEY     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 表头关系字段-本表字段
   FPARENTKEY     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 父阶关系字段-父阶表字段
   FSONKEY     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 父阶关系字段-本表字段
   FPYTHON1     TEXT NOT NULL DEFAULT ''  ,    -- 只读Python脚本
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_BILLTEMPLATEENTRY] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FSTYPE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FSTYPE     VARCHAR(1) NOT NULL DEFAULT ''     -- 数据源类型
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FNAME     VARCHAR(200) NOT NULL DEFAULT ''     -- 数据源名称
if(not exists(select name from SysColumns where name ='FPARENTID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FPARENTID     VARCHAR(50) NOT NULL DEFAULT ''     -- 父阶内码
if(not exists(select name from SysColumns where name ='FKEY' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FKEY     VARCHAR(50) NOT NULL DEFAULT ''     -- 数据源标识
if(not exists(select name from SysColumns where name ='FTABLEID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FTABLEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 主表内码
if(not exists(select name from SysColumns where name ='FTABLEKEY' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FTABLEKEY     VARCHAR(50) NOT NULL DEFAULT ''     -- 数据表标识
if(not exists(select name from SysColumns where name ='FISUSERDEFINED' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FISUSERDEFINED     BIT NOT NULL DEFAULT  0     -- 是否用户定义
if(not exists(select name from SysColumns where name ='FUSQL' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FUSQL     TEXT NOT NULL DEFAULT ''      -- 用户定义SQL
if(not exists(select name from SysColumns where name ='FSQL' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FSQL     TEXT NOT NULL DEFAULT ''      -- 系统自动SQL
if(not exists(select name from SysColumns where name ='FBARLIST' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FBARLIST     VARCHAR(255) NOT NULL DEFAULT ''     -- 条码编码字段
if(not exists(select name from SysColumns where name ='FWHERE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FWHERE     VARCHAR(200) NOT NULL DEFAULT ''     -- 其他条件
if(not exists(select name from SysColumns where name ='FSWHERE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FSWHERE     VARCHAR(200) NOT NULL DEFAULT ''     -- 保存时过滤条件
if(not exists(select name from SysColumns where name ='FISALL' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FISALL     BIT NOT NULL DEFAULT  0     -- 是否抓所有资料
if(not exists(select name from SysColumns where name ='FLOTLIST' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FLOTLIST     VARCHAR(255) NOT NULL DEFAULT ''     -- 批号编码字段
if(not exists(select name from SysColumns where name ='FORDERKEY' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FORDERKEY     VARCHAR(200) NOT NULL DEFAULT ''     -- 排序字段
if(not exists(select name from SysColumns where name ='FISMAIN' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FISMAIN     BIT NOT NULL DEFAULT  0     -- 是否主数据源
if(not exists(select name from SysColumns where name ='FISSAVE' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FISSAVE     BIT NOT NULL DEFAULT  0     -- 是否保存
if(not exists(select name from SysColumns where name ='FHEADKEY' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FHEADKEY     VARCHAR(100) NOT NULL DEFAULT ''     -- 表头关系字段-表头字段
if(not exists(select name from SysColumns where name ='FBODYKEY' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FBODYKEY     VARCHAR(100) NOT NULL DEFAULT ''     -- 表头关系字段-本表字段
if(not exists(select name from SysColumns where name ='FPARENTKEY' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FPARENTKEY     VARCHAR(100) NOT NULL DEFAULT ''     -- 父阶关系字段-父阶表字段
if(not exists(select name from SysColumns where name ='FSONKEY' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FSONKEY     VARCHAR(100) NOT NULL DEFAULT ''     -- 父阶关系字段-本表字段
if(not exists(select name from SysColumns where name ='FPYTHON1' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FPYTHON1     TEXT NOT NULL DEFAULT ''      -- 只读Python脚本
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_BILLTEMPLATEENTRY'))) 
     Alter Table SYS_BILLTEMPLATEENTRY add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY') AND name = N'SYS_BILLTEMPLATEENTRY_1' ) 
   Drop Index SYS_BILLTEMPLATEENTRY.SYS_BILLTEMPLATEENTRY_1
GO
Create  unique Clustered index SYS_BILLTEMPLATEENTRY_1 on SYS_BILLTEMPLATEENTRY(FDETAILID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY') AND name = N'SYS_BILLTEMPLATEENTRY_2' ) 
   Drop Index SYS_BILLTEMPLATEENTRY.SYS_BILLTEMPLATEENTRY_2
GO
Create  unique index SYS_BILLTEMPLATEENTRY_2 on SYS_BILLTEMPLATEENTRY(FINTERID,FENTRYID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_BILLTEMPLATEENTRY') AND name = N'SYS_BILLTEMPLATEENTRY_3' ) 
   Drop Index SYS_BILLTEMPLATEENTRY.SYS_BILLTEMPLATEENTRY_3
GO
Create  index SYS_BILLTEMPLATEENTRY_3 on SYS_BILLTEMPLATEENTRY(FPARENTID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_ROTATEINFO 系统主模块信息*/ 
 -- DROP TABLE SYS_ROTATEINFO
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_ROTATEINFO') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_ROTATEINFO(
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 模块代码
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 模块名称
   FCNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 模块名称(第一语言)
   FTNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 模块名称(第二语言)
   FENAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 模块名称(第三语言)
   RO_CSEQ     VARCHAR(4) NOT NULL DEFAULT '' ,    -- 显示序号
   SHOW_YN     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 是否可见
   RO_BITMAP     INT NOT NULL DEFAULT 0 ,    -- 权限数值
   ISDEFAULT     BIT NOT NULL DEFAULT  0 ,    -- 是否系统默认
   FIMAGE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 图标信息
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_ROTATEINFO] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_ROTATEINFO'))) 
     Alter Table SYS_ROTATEINFO add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 模块代码
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_ROTATEINFO'))) 
     Alter Table SYS_ROTATEINFO add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 模块名称
if(not exists(select name from SysColumns where name ='FCNAME' and id= OBJECT_ID('SYS_ROTATEINFO'))) 
     Alter Table SYS_ROTATEINFO add    FCNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 模块名称(第一语言)
if(not exists(select name from SysColumns where name ='FTNAME' and id= OBJECT_ID('SYS_ROTATEINFO'))) 
     Alter Table SYS_ROTATEINFO add    FTNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 模块名称(第二语言)
if(not exists(select name from SysColumns where name ='FENAME' and id= OBJECT_ID('SYS_ROTATEINFO'))) 
     Alter Table SYS_ROTATEINFO add    FENAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 模块名称(第三语言)
if(not exists(select name from SysColumns where name ='RO_CSEQ' and id= OBJECT_ID('SYS_ROTATEINFO'))) 
     Alter Table SYS_ROTATEINFO add    RO_CSEQ     VARCHAR(4) NOT NULL DEFAULT ''     -- 显示序号
if(not exists(select name from SysColumns where name ='SHOW_YN' and id= OBJECT_ID('SYS_ROTATEINFO'))) 
     Alter Table SYS_ROTATEINFO add    SHOW_YN     VARCHAR(1) NOT NULL DEFAULT ''     -- 是否可见
if(not exists(select name from SysColumns where name ='RO_BITMAP' and id= OBJECT_ID('SYS_ROTATEINFO'))) 
     Alter Table SYS_ROTATEINFO add    RO_BITMAP     INT NOT NULL DEFAULT 0     -- 权限数值
if(not exists(select name from SysColumns where name ='ISDEFAULT' and id= OBJECT_ID('SYS_ROTATEINFO'))) 
     Alter Table SYS_ROTATEINFO add    ISDEFAULT     BIT NOT NULL DEFAULT  0     -- 是否系统默认
if(not exists(select name from SysColumns where name ='FIMAGE' and id= OBJECT_ID('SYS_ROTATEINFO'))) 
     Alter Table SYS_ROTATEINFO add    FIMAGE     VARCHAR(50) NOT NULL DEFAULT ''     -- 图标信息
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_ROTATEINFO'))) 
     Alter Table SYS_ROTATEINFO add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_ROTATEINFO'))) 
     Alter Table SYS_ROTATEINFO add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_ROTATEINFO'))) 
     Alter Table SYS_ROTATEINFO add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_ROTATEINFO') AND name = N'SYS_ROTATEINFO_1' ) 
   Drop Index SYS_ROTATEINFO.SYS_ROTATEINFO_1
GO
Create  unique Clustered index SYS_ROTATEINFO_1 on SYS_ROTATEINFO(FNUMBER)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_ROTATEINFO') AND name = N'SYS_ROTATEINFO_2' ) 
   Drop Index SYS_ROTATEINFO.SYS_ROTATEINFO_2
GO
Create  unique index SYS_ROTATEINFO_2 on SYS_ROTATEINFO(RO_BITMAP)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_CYCLEINFO 系统子模块信息*/ 
 -- DROP TABLE SYS_CYCLEINFO
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_CYCLEINFO') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_CYCLEINFO(
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 子模块代码
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 子模块名称
   FCNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 子模块名称(第一语言)
   FTNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 子模块名称(第二语言)
   FENAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 子模块名称(第三语言)
   FPARENT     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 父阶菜单
   RO_CSEQ     VARCHAR(4) NOT NULL DEFAULT '' ,    -- 显示序号
   SHOW_YN     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 是否可见
   RO_BITMAP     INT NOT NULL DEFAULT 0 ,    -- 权限数值
   ISDEFAULT     BIT NOT NULL DEFAULT  0 ,    -- 是否系统默认
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_CYCLEINFO] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_CYCLEINFO'))) 
     Alter Table SYS_CYCLEINFO add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 子模块代码
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_CYCLEINFO'))) 
     Alter Table SYS_CYCLEINFO add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 子模块名称
if(not exists(select name from SysColumns where name ='FCNAME' and id= OBJECT_ID('SYS_CYCLEINFO'))) 
     Alter Table SYS_CYCLEINFO add    FCNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 子模块名称(第一语言)
if(not exists(select name from SysColumns where name ='FTNAME' and id= OBJECT_ID('SYS_CYCLEINFO'))) 
     Alter Table SYS_CYCLEINFO add    FTNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 子模块名称(第二语言)
if(not exists(select name from SysColumns where name ='FENAME' and id= OBJECT_ID('SYS_CYCLEINFO'))) 
     Alter Table SYS_CYCLEINFO add    FENAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 子模块名称(第三语言)
if(not exists(select name from SysColumns where name ='FPARENT' and id= OBJECT_ID('SYS_CYCLEINFO'))) 
     Alter Table SYS_CYCLEINFO add    FPARENT     VARCHAR(50) NOT NULL DEFAULT ''     -- 父阶菜单
if(not exists(select name from SysColumns where name ='RO_CSEQ' and id= OBJECT_ID('SYS_CYCLEINFO'))) 
     Alter Table SYS_CYCLEINFO add    RO_CSEQ     VARCHAR(4) NOT NULL DEFAULT ''     -- 显示序号
if(not exists(select name from SysColumns where name ='SHOW_YN' and id= OBJECT_ID('SYS_CYCLEINFO'))) 
     Alter Table SYS_CYCLEINFO add    SHOW_YN     VARCHAR(1) NOT NULL DEFAULT ''     -- 是否可见
if(not exists(select name from SysColumns where name ='RO_BITMAP' and id= OBJECT_ID('SYS_CYCLEINFO'))) 
     Alter Table SYS_CYCLEINFO add    RO_BITMAP     INT NOT NULL DEFAULT 0     -- 权限数值
if(not exists(select name from SysColumns where name ='ISDEFAULT' and id= OBJECT_ID('SYS_CYCLEINFO'))) 
     Alter Table SYS_CYCLEINFO add    ISDEFAULT     BIT NOT NULL DEFAULT  0     -- 是否系统默认
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_CYCLEINFO'))) 
     Alter Table SYS_CYCLEINFO add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_CYCLEINFO'))) 
     Alter Table SYS_CYCLEINFO add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_CYCLEINFO'))) 
     Alter Table SYS_CYCLEINFO add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_CYCLEINFO') AND name = N'SYS_CYCLEINFO_1' ) 
   Drop Index SYS_CYCLEINFO.SYS_CYCLEINFO_1
GO
Create  unique Clustered index SYS_CYCLEINFO_1 on SYS_CYCLEINFO(FNUMBER)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_CYCLEINFO') AND name = N'SYS_CYCLEINFO_2' ) 
   Drop Index SYS_CYCLEINFO.SYS_CYCLEINFO_2
GO
Create  unique index SYS_CYCLEINFO_2 on SYS_CYCLEINFO(RO_BITMAP)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_PROGRAMCYCLE 明细功能信息*/ 
 -- DROP TABLE SYS_PROGRAMCYCLE
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_PROGRAMCYCLE') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_PROGRAMCYCLE(
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 功能代码
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 功能名称
   FPARENT     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 父阶菜单
   FPARENTNA     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 父阶菜单名称
   FPARENTSEQ     VARCHAR(4) NOT NULL DEFAULT '' ,    -- 父阶菜单序号
   RO_CSEQ     VARCHAR(4) NOT NULL DEFAULT '' ,    -- 显示序号
   SHOW_YN     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 是否可见
   FTYPE     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 功能类别
   ISDEFAULT     BIT NOT NULL DEFAULT  0 ,    -- 是否系统默认
   FSHOWTYPE     VARCHAR(1) NOT NULL DEFAULT 1 ,    -- 窗口显示模式
   FGNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 分组名称
   FPRTYPE     VARCHAR(1) NOT NULL DEFAULT 1 ,    -- 程序类型
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_PROGRAMCYCLE] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_PROGRAMCYCLE'))) 
     Alter Table SYS_PROGRAMCYCLE add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 功能代码
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_PROGRAMCYCLE'))) 
     Alter Table SYS_PROGRAMCYCLE add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 功能名称
if(not exists(select name from SysColumns where name ='FPARENT' and id= OBJECT_ID('SYS_PROGRAMCYCLE'))) 
     Alter Table SYS_PROGRAMCYCLE add    FPARENT     VARCHAR(50) NOT NULL DEFAULT ''     -- 父阶菜单
if(not exists(select name from SysColumns where name ='FPARENTNA' and id= OBJECT_ID('SYS_PROGRAMCYCLE'))) 
     Alter Table SYS_PROGRAMCYCLE add    FPARENTNA     VARCHAR(200) NOT NULL DEFAULT ''     -- 父阶菜单名称
if(not exists(select name from SysColumns where name ='FPARENTSEQ' and id= OBJECT_ID('SYS_PROGRAMCYCLE'))) 
     Alter Table SYS_PROGRAMCYCLE add    FPARENTSEQ     VARCHAR(4) NOT NULL DEFAULT ''     -- 父阶菜单序号
if(not exists(select name from SysColumns where name ='RO_CSEQ' and id= OBJECT_ID('SYS_PROGRAMCYCLE'))) 
     Alter Table SYS_PROGRAMCYCLE add    RO_CSEQ     VARCHAR(4) NOT NULL DEFAULT ''     -- 显示序号
if(not exists(select name from SysColumns where name ='SHOW_YN' and id= OBJECT_ID('SYS_PROGRAMCYCLE'))) 
     Alter Table SYS_PROGRAMCYCLE add    SHOW_YN     VARCHAR(1) NOT NULL DEFAULT ''     -- 是否可见
if(not exists(select name from SysColumns where name ='FTYPE' and id= OBJECT_ID('SYS_PROGRAMCYCLE'))) 
     Alter Table SYS_PROGRAMCYCLE add    FTYPE     VARCHAR(1) NOT NULL DEFAULT ''     -- 功能类别
if(not exists(select name from SysColumns where name ='ISDEFAULT' and id= OBJECT_ID('SYS_PROGRAMCYCLE'))) 
     Alter Table SYS_PROGRAMCYCLE add    ISDEFAULT     BIT NOT NULL DEFAULT  0     -- 是否系统默认
if(not exists(select name from SysColumns where name ='FSHOWTYPE' and id= OBJECT_ID('SYS_PROGRAMCYCLE'))) 
     Alter Table SYS_PROGRAMCYCLE add    FSHOWTYPE     VARCHAR(1) NOT NULL DEFAULT 1     -- 窗口显示模式
if(not exists(select name from SysColumns where name ='FGNAME' and id= OBJECT_ID('SYS_PROGRAMCYCLE'))) 
     Alter Table SYS_PROGRAMCYCLE add    FGNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 分组名称
if(not exists(select name from SysColumns where name ='FPRTYPE' and id= OBJECT_ID('SYS_PROGRAMCYCLE'))) 
     Alter Table SYS_PROGRAMCYCLE add    FPRTYPE     VARCHAR(1) NOT NULL DEFAULT 1     -- 程序类型
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_PROGRAMCYCLE'))) 
     Alter Table SYS_PROGRAMCYCLE add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_PROGRAMCYCLE'))) 
     Alter Table SYS_PROGRAMCYCLE add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_PROGRAMCYCLE'))) 
     Alter Table SYS_PROGRAMCYCLE add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_PROGRAMCYCLE') AND name = N'SYS_PROGRAMCYCLE_1' ) 
   Drop Index SYS_PROGRAMCYCLE.SYS_PROGRAMCYCLE_1
GO
Create  unique Clustered index SYS_PROGRAMCYCLE_1 on SYS_PROGRAMCYCLE(FINTERID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_PRESONINFO 个人信息公告*/ 
 -- DROP TABLE SYS_PRESONINFO
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_PRESONINFO') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_PRESONINFO(
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 消息编号
   FNAME     NVARCHAR(50) NOT NULL DEFAULT '' ,    -- 消息推送名称
   FCONTENT     TEXT NOT NULL DEFAULT ''  ,    -- 消息内容
   FINFOSTATUS     VARCHAR(2) NOT NULL DEFAULT '' ,    -- 消息状态
   FRECEIVER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 收件人
   FMESSAGETYPE     VARCHAR(2) NOT NULL DEFAULT '' ,    -- 消息类型
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_PRESONINFO] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_PRESONINFO'))) 
     Alter Table SYS_PRESONINFO add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 消息编号
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_PRESONINFO'))) 
     Alter Table SYS_PRESONINFO add    FNAME     NVARCHAR(50) NOT NULL DEFAULT ''     -- 消息推送名称
if(not exists(select name from SysColumns where name ='FCONTENT' and id= OBJECT_ID('SYS_PRESONINFO'))) 
     Alter Table SYS_PRESONINFO add    FCONTENT     TEXT NOT NULL DEFAULT ''      -- 消息内容
if(not exists(select name from SysColumns where name ='FINFOSTATUS' and id= OBJECT_ID('SYS_PRESONINFO'))) 
     Alter Table SYS_PRESONINFO add    FINFOSTATUS     VARCHAR(2) NOT NULL DEFAULT ''     -- 消息状态
if(not exists(select name from SysColumns where name ='FRECEIVER' and id= OBJECT_ID('SYS_PRESONINFO'))) 
     Alter Table SYS_PRESONINFO add    FRECEIVER     VARCHAR(50) NOT NULL DEFAULT ''     -- 收件人
if(not exists(select name from SysColumns where name ='FMESSAGETYPE' and id= OBJECT_ID('SYS_PRESONINFO'))) 
     Alter Table SYS_PRESONINFO add    FMESSAGETYPE     VARCHAR(2) NOT NULL DEFAULT ''     -- 消息类型
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_PRESONINFO'))) 
     Alter Table SYS_PRESONINFO add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_PRESONINFO'))) 
     Alter Table SYS_PRESONINFO add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_PRESONINFO'))) 
     Alter Table SYS_PRESONINFO add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_PRESONINFO') AND name = N'SYS_PRESONINFO_1' ) 
   Drop Index SYS_PRESONINFO.SYS_PRESONINFO_1
GO
Create  unique Clustered index SYS_PRESONINFO_1 on SYS_PRESONINFO(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_PRESONINFO') AND name = N'SYS_PRESONINFO_2' ) 
   Drop Index SYS_PRESONINFO.SYS_PRESONINFO_2
GO
Create  unique index SYS_PRESONINFO_2 on SYS_PRESONINFO(FNUMBER)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_AUDITLOGENTRY 审计追踪日志表体*/ 
 -- DROP TABLE SYS_AUDITLOGENTRY
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_AUDITLOGENTRY') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_AUDITLOGENTRY(
   FTYPE     INT NOT NULL DEFAULT 1 ,    -- 类型
   FTABLENAME     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 表名称
   FCOLUMNNAME     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 字段名称
   FBEFOREDATA     VARCHAR(500) NOT NULL DEFAULT '' ,    -- 修改前数据
   FAFTERDATA     VARCHAR(500) NOT NULL DEFAULT '' ,    -- 修改后数据
   FDATA     TEXT NOT NULL DEFAULT ''  ,    -- 数据
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_AUDITLOGENTRY] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FTYPE' and id= OBJECT_ID('SYS_AUDITLOGENTRY'))) 
     Alter Table SYS_AUDITLOGENTRY add    FTYPE     INT NOT NULL DEFAULT 1     -- 类型
if(not exists(select name from SysColumns where name ='FTABLENAME' and id= OBJECT_ID('SYS_AUDITLOGENTRY'))) 
     Alter Table SYS_AUDITLOGENTRY add    FTABLENAME     VARCHAR(50) NOT NULL DEFAULT ''     -- 表名称
if(not exists(select name from SysColumns where name ='FCOLUMNNAME' and id= OBJECT_ID('SYS_AUDITLOGENTRY'))) 
     Alter Table SYS_AUDITLOGENTRY add    FCOLUMNNAME     VARCHAR(50) NOT NULL DEFAULT ''     -- 字段名称
if(not exists(select name from SysColumns where name ='FBEFOREDATA' and id= OBJECT_ID('SYS_AUDITLOGENTRY'))) 
     Alter Table SYS_AUDITLOGENTRY add    FBEFOREDATA     VARCHAR(500) NOT NULL DEFAULT ''     -- 修改前数据
if(not exists(select name from SysColumns where name ='FAFTERDATA' and id= OBJECT_ID('SYS_AUDITLOGENTRY'))) 
     Alter Table SYS_AUDITLOGENTRY add    FAFTERDATA     VARCHAR(500) NOT NULL DEFAULT ''     -- 修改后数据
if(not exists(select name from SysColumns where name ='FDATA' and id= OBJECT_ID('SYS_AUDITLOGENTRY'))) 
     Alter Table SYS_AUDITLOGENTRY add    FDATA     TEXT NOT NULL DEFAULT ''      -- 数据
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_AUDITLOGENTRY'))) 
     Alter Table SYS_AUDITLOGENTRY add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_AUDITLOGENTRY'))) 
     Alter Table SYS_AUDITLOGENTRY add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_AUDITLOGENTRY'))) 
     Alter Table SYS_AUDITLOGENTRY add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_AUDITLOGENTRY'))) 
     Alter Table SYS_AUDITLOGENTRY add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_AUDITLOGENTRY') AND name = N'SYS_AUDITLOGENTRY_1' ) 
   Drop Index SYS_AUDITLOGENTRY.SYS_AUDITLOGENTRY_1
GO
Create  index SYS_AUDITLOGENTRY_1 on SYS_AUDITLOGENTRY(FINTERID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:T_SFC_SYSTEMFILE 系统文件管理表*/ 
 -- DROP TABLE T_SFC_SYSTEMFILE
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'T_SFC_SYSTEMFILE') AND type in (N'U'))
BEGIN
CREATE TABLE T_SFC_SYSTEMFILE(
   FBILLID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 单据内码
   FFILENAME     VARCHAR(255) NOT NULL DEFAULT '' ,    -- 系统文件名称
   FPATH     VARCHAR(255) NOT NULL DEFAULT '' ,    -- 名称路径
   FVERDESC     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 版本描述
   FREMK     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 关键信息描述
   FEFFECTIVEDATE     DATE NULL  ,    -- 生效日期
   FEXPIRYDATE     DATE NULL  ,    -- 关闭日期
   FTYPE     INT NOT NULL DEFAULT 0 ,    -- 文件类型
   FLOADCOUNT     INT NOT NULL DEFAULT 0 ,    -- 下载次数
   FNOTE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 备注
   FCHECKERID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 审核人
   FCHECKDATE     DATETIME NULL  ,    -- 审核日期
   FDISABLEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 禁用人
   FDISABLEDATE     DATETIME NULL  ,    -- 禁用日期
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FBILLNO	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 单据编号 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_T_SFC_SYSTEMFILE] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FBILLID' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FBILLID     VARCHAR(50) NOT NULL DEFAULT ''     -- 单据内码
if(not exists(select name from SysColumns where name ='FFILENAME' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FFILENAME     VARCHAR(255) NOT NULL DEFAULT ''     -- 系统文件名称
if(not exists(select name from SysColumns where name ='FPATH' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FPATH     VARCHAR(255) NOT NULL DEFAULT ''     -- 名称路径
if(not exists(select name from SysColumns where name ='FVERDESC' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FVERDESC     NVARCHAR(255) NOT NULL DEFAULT ''     -- 版本描述
if(not exists(select name from SysColumns where name ='FREMK' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FREMK     NVARCHAR(255) NOT NULL DEFAULT ''     -- 关键信息描述
if(not exists(select name from SysColumns where name ='FEFFECTIVEDATE' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FEFFECTIVEDATE     DATE NULL      -- 生效日期
if(not exists(select name from SysColumns where name ='FEXPIRYDATE' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FEXPIRYDATE     DATE NULL      -- 关闭日期
if(not exists(select name from SysColumns where name ='FTYPE' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FTYPE     INT NOT NULL DEFAULT 0     -- 文件类型
if(not exists(select name from SysColumns where name ='FLOADCOUNT' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FLOADCOUNT     INT NOT NULL DEFAULT 0     -- 下载次数
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FNOTE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 备注
if(not exists(select name from SysColumns where name ='FCHECKERID' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FCHECKERID     VARCHAR(50) NOT NULL DEFAULT ''     -- 审核人
if(not exists(select name from SysColumns where name ='FCHECKDATE' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FCHECKDATE     DATETIME NULL      -- 审核日期
if(not exists(select name from SysColumns where name ='FDISABLEID' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FDISABLEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 禁用人
if(not exists(select name from SysColumns where name ='FDISABLEDATE' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FDISABLEDATE     DATETIME NULL      -- 禁用日期
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FBILLNO' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FBILLNO     VARCHAR(50) NOT NULL DEFAULT ''      -- 单据编号
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('T_SFC_SYSTEMFILE'))) 
     Alter Table T_SFC_SYSTEMFILE add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO



GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_STATUS 单据状态基本资料*/ 
 -- DROP TABLE SYS_STATUS
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_STATUS') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_STATUS(
   FITEMID     INT NOT NULL DEFAULT 0 ,    -- 状态代码
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 状态名称
   ISDEFAULT     BIT NOT NULL DEFAULT  0 ,    -- 是否系统默认
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_STATUS] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FITEMID' and id= OBJECT_ID('SYS_STATUS'))) 
     Alter Table SYS_STATUS add    FITEMID     INT NOT NULL DEFAULT 0     -- 状态代码
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_STATUS'))) 
     Alter Table SYS_STATUS add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 状态名称
if(not exists(select name from SysColumns where name ='ISDEFAULT' and id= OBJECT_ID('SYS_STATUS'))) 
     Alter Table SYS_STATUS add    ISDEFAULT     BIT NOT NULL DEFAULT  0     -- 是否系统默认
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_STATUS'))) 
     Alter Table SYS_STATUS add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_STATUS'))) 
     Alter Table SYS_STATUS add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_STATUS'))) 
     Alter Table SYS_STATUS add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_STATUS') AND name = N'SYS_STATUS_1' ) 
   Drop Index SYS_STATUS.SYS_STATUS_1
GO
Create  unique Clustered index SYS_STATUS_1 on SYS_STATUS(FITEMID)
GO

GO
delete from  SYS_STATUS 
insert into  SYS_STATUS(FINTERID,FITEMID,FNAME,ISDEFAULT,UID) values('10',10,'暂存',1,'01') 
insert into  SYS_STATUS(FINTERID,FITEMID,FNAME,ISDEFAULT,UID) values('20',20,'提交',1,'02') 
insert into  SYS_STATUS(FINTERID,FITEMID,FNAME,ISDEFAULT,UID) values('30',30,'确认',1,'03') 
insert into  SYS_STATUS(FINTERID,FITEMID,FNAME,ISDEFAULT,UID) values('40',40,'审核',1,'04') 
insert into  SYS_STATUS(FINTERID,FITEMID,FNAME,ISDEFAULT,UID) values('50',50,'复核',1,'05') 
insert into  SYS_STATUS(FINTERID,FITEMID,FNAME,ISDEFAULT,UID) values('60',60,'结案',1,'06') 
insert into  SYS_STATUS(FINTERID,FITEMID,FNAME,ISDEFAULT,UID) values('70',70,'关闭',1,'07') 
insert into  SYS_STATUS(FINTERID,FITEMID,FNAME,ISDEFAULT,UID) values('80',80,'作废',1,'08') 

GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_LOGINUSER 登录帐号表结构*/ 
 -- DROP TABLE SYS_LOGINUSER
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_LOGINUSER') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_LOGINUSER(
   USER_ID     VARCHAR(20) NOT NULL DEFAULT '' ,    -- 用户帐号
   USER_NAME     NVARCHAR(50) NOT NULL DEFAULT '' ,    -- 用户姓名
   PR_PASSWORD     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 用户密码
   IS_PDA     BIT NOT NULL DEFAULT  0 ,    -- 是否为智能终端
   PATYPE     VARCHAR(2) NOT NULL DEFAULT '' ,    -- 关联类别(1=职员,2=厂商,3=客户)
   EMAIL     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 电子邮箱
   PA_ID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 职员/厂商/客户内码
   ERPID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- ERP中的用户帐号
   ISDEFAULT     BIT NOT NULL DEFAULT  0 ,    -- 是否系统默认
   CARDID     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 用户ID卡
   DOMAINUSER     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 域用户名
   LASTLOGINTIME     DATETIME NULL  ,    -- 最后一次登录时间
   LASTCPTIME     DATETIME NULL  ,    -- 最后修改密码的时间
   PHOTO     IMAGE NULL  ,    -- 用户照片
   PWDERRTIMES     INT NOT NULL DEFAULT 0 ,    -- 密码输错次数
   LOCKSTATUS     INT NOT NULL DEFAULT 0 ,    -- 锁定状态
   LASTLOCKTIME     DATETIME NULL  ,    -- 最后一次锁定解锁时间
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_LOGINUSER] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='USER_ID' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    USER_ID     VARCHAR(20) NOT NULL DEFAULT ''     -- 用户帐号
if(not exists(select name from SysColumns where name ='USER_NAME' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    USER_NAME     NVARCHAR(50) NOT NULL DEFAULT ''     -- 用户姓名
if(not exists(select name from SysColumns where name ='PR_PASSWORD' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    PR_PASSWORD     VARCHAR(50) NOT NULL DEFAULT ''     -- 用户密码
if(not exists(select name from SysColumns where name ='IS_PDA' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    IS_PDA     BIT NOT NULL DEFAULT  0     -- 是否为智能终端
if(not exists(select name from SysColumns where name ='PATYPE' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    PATYPE     VARCHAR(2) NOT NULL DEFAULT ''     -- 关联类别(1=职员,2=厂商,3=客户)
if(not exists(select name from SysColumns where name ='EMAIL' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    EMAIL     VARCHAR(200) NOT NULL DEFAULT ''     -- 电子邮箱
if(not exists(select name from SysColumns where name ='PA_ID' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    PA_ID     VARCHAR(50) NOT NULL DEFAULT ''     -- 职员/厂商/客户内码
if(not exists(select name from SysColumns where name ='ERPID' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    ERPID     VARCHAR(50) NOT NULL DEFAULT ''     -- ERP中的用户帐号
if(not exists(select name from SysColumns where name ='ISDEFAULT' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    ISDEFAULT     BIT NOT NULL DEFAULT  0     -- 是否系统默认
if(not exists(select name from SysColumns where name ='CARDID' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    CARDID     VARCHAR(100) NOT NULL DEFAULT ''     -- 用户ID卡
if(not exists(select name from SysColumns where name ='DOMAINUSER' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    DOMAINUSER     VARCHAR(200) NOT NULL DEFAULT ''     -- 域用户名
if(not exists(select name from SysColumns where name ='LASTLOGINTIME' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    LASTLOGINTIME     DATETIME NULL      -- 最后一次登录时间
if(not exists(select name from SysColumns where name ='LASTCPTIME' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    LASTCPTIME     DATETIME NULL      -- 最后修改密码的时间
if(not exists(select name from SysColumns where name ='PHOTO' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    PHOTO     IMAGE NULL      -- 用户照片
if(not exists(select name from SysColumns where name ='PWDERRTIMES' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    PWDERRTIMES     INT NOT NULL DEFAULT 0     -- 密码输错次数
if(not exists(select name from SysColumns where name ='LOCKSTATUS' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    LOCKSTATUS     INT NOT NULL DEFAULT 0     -- 锁定状态
if(not exists(select name from SysColumns where name ='LASTLOCKTIME' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    LASTLOCKTIME     DATETIME NULL      -- 最后一次锁定解锁时间
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_LOGINUSER'))) 
     Alter Table SYS_LOGINUSER add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_LOGINUSER') AND name = N'SYS_LOGINUSER_1' ) 
   Drop Index SYS_LOGINUSER.SYS_LOGINUSER_1
GO
Create  unique Clustered index SYS_LOGINUSER_1 on SYS_LOGINUSER(USER_ID)
GO

GO
if (not exists(select 1 from SYS_LOGINUSER where USER_ID='Admin')) 
   insert into SYS_LOGINUSER(USER_ID,USER_NAME,ISDEFAULT,UID) values('Admin','系统管理员',1,'01') 
if (not exists(select 1 from SYS_LOGINUSER where USER_ID='TEST')) 
   insert into SYS_LOGINUSER(USER_ID,USER_NAME,ISDEFAULT,UID) values('TEST','测试用户',0,'02') 
GO

GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_USERFILTERINFOENTRY 用户过滤方案信息表体*/ 
 -- DROP TABLE SYS_USERFILTERINFOENTRY
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_USERFILTERINFOENTRY') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_USERFILTERINFOENTRY(
   FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 功能代码
   FUSER_ID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 用户帐号
   FLEFT     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 左边括号
   FDATAMEMBER     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 字段
   FFILTEROPERATOR     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 过滤条件
   FVALUE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 值
   FRIGHT     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 右边括号
   FLOGICALOPERATORS     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 逻辑
   FISCASESENSITIVE     BIT NOT NULL DEFAULT  0 ,    -- 是否区分大小写
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FENTRYID		INT             NOT NULL DEFAULT 0 ,	-- 行号 
   FDETAILID     VARCHAR(50)     NOT NULL DEFAULT '' ,   --表体内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_USERFILTERINFOENTRY] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FCLASSTYPEID' and id= OBJECT_ID('SYS_USERFILTERINFOENTRY'))) 
     Alter Table SYS_USERFILTERINFOENTRY add    FCLASSTYPEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 功能代码
if(not exists(select name from SysColumns where name ='FUSER_ID' and id= OBJECT_ID('SYS_USERFILTERINFOENTRY'))) 
     Alter Table SYS_USERFILTERINFOENTRY add    FUSER_ID     VARCHAR(50) NOT NULL DEFAULT ''     -- 用户帐号
if(not exists(select name from SysColumns where name ='FLEFT' and id= OBJECT_ID('SYS_USERFILTERINFOENTRY'))) 
     Alter Table SYS_USERFILTERINFOENTRY add    FLEFT     VARCHAR(100) NOT NULL DEFAULT ''     -- 左边括号
if(not exists(select name from SysColumns where name ='FDATAMEMBER' and id= OBJECT_ID('SYS_USERFILTERINFOENTRY'))) 
     Alter Table SYS_USERFILTERINFOENTRY add    FDATAMEMBER     VARCHAR(100) NOT NULL DEFAULT ''     -- 字段
if(not exists(select name from SysColumns where name ='FFILTEROPERATOR' and id= OBJECT_ID('SYS_USERFILTERINFOENTRY'))) 
     Alter Table SYS_USERFILTERINFOENTRY add    FFILTEROPERATOR     VARCHAR(50) NOT NULL DEFAULT ''     -- 过滤条件
if(not exists(select name from SysColumns where name ='FVALUE' and id= OBJECT_ID('SYS_USERFILTERINFOENTRY'))) 
     Alter Table SYS_USERFILTERINFOENTRY add    FVALUE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 值
if(not exists(select name from SysColumns where name ='FRIGHT' and id= OBJECT_ID('SYS_USERFILTERINFOENTRY'))) 
     Alter Table SYS_USERFILTERINFOENTRY add    FRIGHT     VARCHAR(100) NOT NULL DEFAULT ''     -- 右边括号
if(not exists(select name from SysColumns where name ='FLOGICALOPERATORS' and id= OBJECT_ID('SYS_USERFILTERINFOENTRY'))) 
     Alter Table SYS_USERFILTERINFOENTRY add    FLOGICALOPERATORS     VARCHAR(100) NOT NULL DEFAULT ''     -- 逻辑
if(not exists(select name from SysColumns where name ='FISCASESENSITIVE' and id= OBJECT_ID('SYS_USERFILTERINFOENTRY'))) 
     Alter Table SYS_USERFILTERINFOENTRY add    FISCASESENSITIVE     BIT NOT NULL DEFAULT  0     -- 是否区分大小写
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_USERFILTERINFOENTRY'))) 
     Alter Table SYS_USERFILTERINFOENTRY add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_USERFILTERINFOENTRY'))) 
     Alter Table SYS_USERFILTERINFOENTRY add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FDETAILID' and id= OBJECT_ID('SYS_USERFILTERINFOENTRY'))) 
     Alter Table SYS_USERFILTERINFOENTRY add    FDETAILID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表体内码
if(not exists(select name from SysColumns where name ='FENTRYID' and id= OBJECT_ID('SYS_USERFILTERINFOENTRY'))) 
     Alter Table SYS_USERFILTERINFOENTRY add    FENTRYID     INT             NOT NULL DEFAULT 0      -- 行号

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_USERFILTERINFOENTRY') AND name = N'SYS_USERFILTERINFOENTRY_1' ) 
   Drop Index SYS_USERFILTERINFOENTRY.SYS_USERFILTERINFOENTRY_1
GO
Create  unique Clustered index SYS_USERFILTERINFOENTRY_1 on SYS_USERFILTERINFOENTRY(FINTERID,FENTRYID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_USERROLERELATION 帐号及角色对照表*/ 
 -- DROP TABLE SYS_USERROLERELATION
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_USERROLERELATION') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_USERROLERELATION(
   USER_ID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 用户帐号
   FROLEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 用户角色
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_USERROLERELATION] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='USER_ID' and id= OBJECT_ID('SYS_USERROLERELATION'))) 
     Alter Table SYS_USERROLERELATION add    USER_ID     VARCHAR(50) NOT NULL DEFAULT ''     -- 用户帐号
if(not exists(select name from SysColumns where name ='FROLEID' and id= OBJECT_ID('SYS_USERROLERELATION'))) 
     Alter Table SYS_USERROLERELATION add    FROLEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 用户角色
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_USERROLERELATION'))) 
     Alter Table SYS_USERROLERELATION add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_USERROLERELATION'))) 
     Alter Table SYS_USERROLERELATION add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_USERROLERELATION'))) 
     Alter Table SYS_USERROLERELATION add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_USERROLERELATION') AND name = N'SYS_USERROLERELATION_1' ) 
   Drop Index SYS_USERROLERELATION.SYS_USERROLERELATION_1
GO
Create  unique Clustered index SYS_USERROLERELATION_1 on SYS_USERROLERELATION(USER_ID,FROLEID)
GO

GO
if (not exists(select 1 from SYS_USERROLERELATION where FROLEID='SYS001' and USER_ID ='Admin'))
   insert into SYS_USERROLERELATION(FROLEID,USER_ID,UID) values('SYS001','Admin','01')
if (not exists(select 1 from SYS_USERROLERELATION where FROLEID='SYS002' and USER_ID='TEST')) 
   insert into SYS_USERROLERELATION(FROLEID,USER_ID,UID) values('SYS002','TEST','02')

GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_FIELDTYPE 字段类型表*/ 
 -- DROP TABLE SYS_FIELDTYPE
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_FIELDTYPE') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_FIELDTYPE(
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 类型代码
   FNAME     NVARCHAR(50) NOT NULL DEFAULT '' ,    -- 类型名称
   FCNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 类型名称(第一语言)
   FTNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 类型名称(第二语言)
   FENAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 类型名称(第三语言)
   FFIELDTYPE     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 字段类型
   FLENGTH     INT NOT NULL DEFAULT 0 ,    -- 字段长度
   FSCALE     INT NOT NULL DEFAULT 0 ,    -- 小数位数
   FFIELDNOTE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 字段备注
   FDISABLEDATE     DATE NULL  ,    -- 禁用日期
   FDISABLEID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 禁用人员
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_FIELDTYPE] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_FIELDTYPE'))) 
     Alter Table SYS_FIELDTYPE add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 类型代码
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_FIELDTYPE'))) 
     Alter Table SYS_FIELDTYPE add    FNAME     NVARCHAR(50) NOT NULL DEFAULT ''     -- 类型名称
if(not exists(select name from SysColumns where name ='FCNAME' and id= OBJECT_ID('SYS_FIELDTYPE'))) 
     Alter Table SYS_FIELDTYPE add    FCNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 类型名称(第一语言)
if(not exists(select name from SysColumns where name ='FTNAME' and id= OBJECT_ID('SYS_FIELDTYPE'))) 
     Alter Table SYS_FIELDTYPE add    FTNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 类型名称(第二语言)
if(not exists(select name from SysColumns where name ='FENAME' and id= OBJECT_ID('SYS_FIELDTYPE'))) 
     Alter Table SYS_FIELDTYPE add    FENAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 类型名称(第三语言)
if(not exists(select name from SysColumns where name ='FFIELDTYPE' and id= OBJECT_ID('SYS_FIELDTYPE'))) 
     Alter Table SYS_FIELDTYPE add    FFIELDTYPE     VARCHAR(1) NOT NULL DEFAULT ''     -- 字段类型
if(not exists(select name from SysColumns where name ='FLENGTH' and id= OBJECT_ID('SYS_FIELDTYPE'))) 
     Alter Table SYS_FIELDTYPE add    FLENGTH     INT NOT NULL DEFAULT 0     -- 字段长度
if(not exists(select name from SysColumns where name ='FSCALE' and id= OBJECT_ID('SYS_FIELDTYPE'))) 
     Alter Table SYS_FIELDTYPE add    FSCALE     INT NOT NULL DEFAULT 0     -- 小数位数
if(not exists(select name from SysColumns where name ='FFIELDNOTE' and id= OBJECT_ID('SYS_FIELDTYPE'))) 
     Alter Table SYS_FIELDTYPE add    FFIELDNOTE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 字段备注
if(not exists(select name from SysColumns where name ='FDISABLEDATE' and id= OBJECT_ID('SYS_FIELDTYPE'))) 
     Alter Table SYS_FIELDTYPE add    FDISABLEDATE     DATE NULL      -- 禁用日期
if(not exists(select name from SysColumns where name ='FDISABLEID' and id= OBJECT_ID('SYS_FIELDTYPE'))) 
     Alter Table SYS_FIELDTYPE add    FDISABLEID     VARCHAR(50) NOT NULL DEFAULT ''     -- 禁用人员
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_FIELDTYPE'))) 
     Alter Table SYS_FIELDTYPE add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_FIELDTYPE'))) 
     Alter Table SYS_FIELDTYPE add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_FIELDTYPE'))) 
     Alter Table SYS_FIELDTYPE add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_FIELDTYPE') AND name = N'SYS_FIELDTYPE_1' ) 
   Drop Index SYS_FIELDTYPE.SYS_FIELDTYPE_1
GO
Create  unique Clustered index SYS_FIELDTYPE_1 on SYS_FIELDTYPE(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_FIELDTYPE') AND name = N'SYS_FIELDTYPE_2' ) 
   Drop Index SYS_FIELDTYPE.SYS_FIELDTYPE_2
GO
Create  unique index SYS_FIELDTYPE_2 on SYS_FIELDTYPE(FNUMBER)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_USERROLE 用户角色信息*/ 
 -- DROP TABLE SYS_USERROLE
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_USERROLE') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_USERROLE(
   FROLENUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 角色代码
   FROLENAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 角色名称
   FROLETYPE     INT NOT NULL DEFAULT 0 ,    -- 角色类别
   ISDEFAULT     BIT NOT NULL DEFAULT  0 ,    -- 是否系统默认
   FNOTE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 备注
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_USERROLE] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FROLENUMBER' and id= OBJECT_ID('SYS_USERROLE'))) 
     Alter Table SYS_USERROLE add    FROLENUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 角色代码
if(not exists(select name from SysColumns where name ='FROLENAME' and id= OBJECT_ID('SYS_USERROLE'))) 
     Alter Table SYS_USERROLE add    FROLENAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 角色名称
if(not exists(select name from SysColumns where name ='FROLETYPE' and id= OBJECT_ID('SYS_USERROLE'))) 
     Alter Table SYS_USERROLE add    FROLETYPE     INT NOT NULL DEFAULT 0     -- 角色类别
if(not exists(select name from SysColumns where name ='ISDEFAULT' and id= OBJECT_ID('SYS_USERROLE'))) 
     Alter Table SYS_USERROLE add    ISDEFAULT     BIT NOT NULL DEFAULT  0     -- 是否系统默认
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_USERROLE'))) 
     Alter Table SYS_USERROLE add    FNOTE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 备注
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_USERROLE'))) 
     Alter Table SYS_USERROLE add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_USERROLE'))) 
     Alter Table SYS_USERROLE add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_USERROLE'))) 
     Alter Table SYS_USERROLE add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO


GO
if (not exists(select 1 from SYS_USERROLE where FROLENUMBER='SYS001')) 
   insert into SYS_USERROLE(FINTERID,FROLENUMBER,FROLENAME,FROLETYPE,ISDEFAULT,UID) values('1','SYS001','Administrators',0,1,'01')
if (not exists(select 1 from SYS_USERROLE where FROLENUMBER='SYS002')) 
   insert into SYS_USERROLE(FINTERID,FROLENUMBER,FROLENAME,FROLETYPE,ISDEFAULT,UID) values('2','SYS002','普通角色',1,1,'02')

GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_OPERATIONLOG 系统日志表*/ 
 -- DROP TABLE SYS_OPERATIONLOG
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_OPERATIONLOG') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_OPERATIONLOG(
   FDATE     DATETIME NULL  ,    -- 操作日期
   FUSERID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 操作用户
   FPRGKEY     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 系统功能
   FSTATEMENT     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 日志状态信息(扩展用)
   FDESCRIPTION     NVARCHAR(max) NOT NULL DEFAULT '' ,    -- 日志描述
   FTABLENAME     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 操作实体表名
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_OPERATIONLOG] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FDATE' and id= OBJECT_ID('SYS_OPERATIONLOG'))) 
     Alter Table SYS_OPERATIONLOG add    FDATE     DATETIME NULL      -- 操作日期
if(not exists(select name from SysColumns where name ='FUSERID' and id= OBJECT_ID('SYS_OPERATIONLOG'))) 
     Alter Table SYS_OPERATIONLOG add    FUSERID     VARCHAR(50) NOT NULL DEFAULT ''     -- 操作用户
if(not exists(select name from SysColumns where name ='FPRGKEY' and id= OBJECT_ID('SYS_OPERATIONLOG'))) 
     Alter Table SYS_OPERATIONLOG add    FPRGKEY     VARCHAR(50) NOT NULL DEFAULT ''     -- 系统功能
if(not exists(select name from SysColumns where name ='FSTATEMENT' and id= OBJECT_ID('SYS_OPERATIONLOG'))) 
     Alter Table SYS_OPERATIONLOG add    FSTATEMENT     VARCHAR(1) NOT NULL DEFAULT ''     -- 日志状态信息(扩展用)
if(not exists(select name from SysColumns where name ='FDESCRIPTION' and id= OBJECT_ID('SYS_OPERATIONLOG'))) 
     Alter Table SYS_OPERATIONLOG add    FDESCRIPTION     NVARCHAR(max) NOT NULL DEFAULT ''     -- 日志描述
if(not exists(select name from SysColumns where name ='FTABLENAME' and id= OBJECT_ID('SYS_OPERATIONLOG'))) 
     Alter Table SYS_OPERATIONLOG add    FTABLENAME     VARCHAR(50) NOT NULL DEFAULT ''     -- 操作实体表名
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_OPERATIONLOG'))) 
     Alter Table SYS_OPERATIONLOG add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_OPERATIONLOG'))) 
     Alter Table SYS_OPERATIONLOG add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_OPERATIONLOG'))) 
     Alter Table SYS_OPERATIONLOG add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_OPERATIONLOG') AND name = N'SYS_OPERATIONLOG_1' ) 
   Drop Index SYS_OPERATIONLOG.SYS_OPERATIONLOG_1
GO
Create  unique Clustered index SYS_OPERATIONLOG_1 on SYS_OPERATIONLOG(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_OPERATIONLOG') AND name = N'SYS_OPERATIONLOG_2' ) 
   Drop Index SYS_OPERATIONLOG.SYS_OPERATIONLOG_2
GO
Create  index SYS_OPERATIONLOG_2 on SYS_OPERATIONLOG(FDATE)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_LOGININFO 用户登录信息表*/ 
 -- DROP TABLE SYS_LOGININFO
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_LOGININFO') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_LOGININFO(
   FCOMPUTER     VARCHAR(100) NOT NULL DEFAULT '' ,    -- 机器名
   FUSERID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 登录用户
   FLOGINTIME     DATETIME NULL  ,    -- 登录时间
   FLASTTIME     DATETIME NULL  ,    -- 最后异动时间
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_LOGININFO] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FCOMPUTER' and id= OBJECT_ID('SYS_LOGININFO'))) 
     Alter Table SYS_LOGININFO add    FCOMPUTER     VARCHAR(100) NOT NULL DEFAULT ''     -- 机器名
if(not exists(select name from SysColumns where name ='FUSERID' and id= OBJECT_ID('SYS_LOGININFO'))) 
     Alter Table SYS_LOGININFO add    FUSERID     VARCHAR(50) NOT NULL DEFAULT ''     -- 登录用户
if(not exists(select name from SysColumns where name ='FLOGINTIME' and id= OBJECT_ID('SYS_LOGININFO'))) 
     Alter Table SYS_LOGININFO add    FLOGINTIME     DATETIME NULL      -- 登录时间
if(not exists(select name from SysColumns where name ='FLASTTIME' and id= OBJECT_ID('SYS_LOGININFO'))) 
     Alter Table SYS_LOGININFO add    FLASTTIME     DATETIME NULL      -- 最后异动时间
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_LOGININFO'))) 
     Alter Table SYS_LOGININFO add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_LOGININFO'))) 
     Alter Table SYS_LOGININFO add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_LOGININFO'))) 
     Alter Table SYS_LOGININFO add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_LOGININFO') AND name = N'SYS_LOGININFO_1' ) 
   Drop Index SYS_LOGININFO.SYS_LOGININFO_1
GO
Create  unique Clustered index SYS_LOGININFO_1 on SYS_LOGININFO(FINTERID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_USERPWHISTORY 用户密码历史记录*/ 
 -- DROP TABLE SYS_USERPWHISTORY
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_USERPWHISTORY') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_USERPWHISTORY(
   FUSERID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 用户帐号
   FDATE     DATETIME NULL  ,    -- 修改日期
   FPASSWORD     NVARCHAR(100) NOT NULL DEFAULT '' ,    -- 密码
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_USERPWHISTORY] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FUSERID' and id= OBJECT_ID('SYS_USERPWHISTORY'))) 
     Alter Table SYS_USERPWHISTORY add    FUSERID     VARCHAR(50) NOT NULL DEFAULT ''     -- 用户帐号
if(not exists(select name from SysColumns where name ='FDATE' and id= OBJECT_ID('SYS_USERPWHISTORY'))) 
     Alter Table SYS_USERPWHISTORY add    FDATE     DATETIME NULL      -- 修改日期
if(not exists(select name from SysColumns where name ='FPASSWORD' and id= OBJECT_ID('SYS_USERPWHISTORY'))) 
     Alter Table SYS_USERPWHISTORY add    FPASSWORD     NVARCHAR(100) NOT NULL DEFAULT ''     -- 密码
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_USERPWHISTORY'))) 
     Alter Table SYS_USERPWHISTORY add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_USERPWHISTORY'))) 
     Alter Table SYS_USERPWHISTORY add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_USERPWHISTORY'))) 
     Alter Table SYS_USERPWHISTORY add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_USERPWHISTORY') AND name = N'SYS_USERPWHISTORY_1' ) 
   Drop Index SYS_USERPWHISTORY.SYS_USERPWHISTORY_1
GO
Create  unique Clustered index SYS_USERPWHISTORY_1 on SYS_USERPWHISTORY(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_USERPWHISTORY') AND name = N'SYS_USERPWHISTORY_2' ) 
   Drop Index SYS_USERPWHISTORY.SYS_USERPWHISTORY_2
GO
Create  index SYS_USERPWHISTORY_2 on SYS_USERPWHISTORY(FUSERID)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_BASEDATAGROUP 基本资料分组共用表结构*/ 
 -- DROP TABLE SYS_BASEDATAGROUP
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_BASEDATAGROUP') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_BASEDATAGROUP(
   FPRGKEY     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 系统功能
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 分组代码
   FNAME     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 分组名称
   FCNAME     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 分组名称(第一语言)
   FTNAME     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 分组名称(第二语言)
   FENAME     VARCHAR(200) NOT NULL DEFAULT '' ,    -- 分组名称(第三语言)
   FPARENTID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 父阶内码
   FFULLNUMBER     VARCHAR(max) NOT NULL DEFAULT '' ,    -- 长编码
   FNOTE     VARCHAR(max) NOT NULL DEFAULT '' ,    -- 备注
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_BASEDATAGROUP] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FPRGKEY' and id= OBJECT_ID('SYS_BASEDATAGROUP'))) 
     Alter Table SYS_BASEDATAGROUP add    FPRGKEY     VARCHAR(50) NOT NULL DEFAULT ''     -- 系统功能
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_BASEDATAGROUP'))) 
     Alter Table SYS_BASEDATAGROUP add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 分组代码
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_BASEDATAGROUP'))) 
     Alter Table SYS_BASEDATAGROUP add    FNAME     VARCHAR(200) NOT NULL DEFAULT ''     -- 分组名称
if(not exists(select name from SysColumns where name ='FCNAME' and id= OBJECT_ID('SYS_BASEDATAGROUP'))) 
     Alter Table SYS_BASEDATAGROUP add    FCNAME     VARCHAR(200) NOT NULL DEFAULT ''     -- 分组名称(第一语言)
if(not exists(select name from SysColumns where name ='FTNAME' and id= OBJECT_ID('SYS_BASEDATAGROUP'))) 
     Alter Table SYS_BASEDATAGROUP add    FTNAME     VARCHAR(200) NOT NULL DEFAULT ''     -- 分组名称(第二语言)
if(not exists(select name from SysColumns where name ='FENAME' and id= OBJECT_ID('SYS_BASEDATAGROUP'))) 
     Alter Table SYS_BASEDATAGROUP add    FENAME     VARCHAR(200) NOT NULL DEFAULT ''     -- 分组名称(第三语言)
if(not exists(select name from SysColumns where name ='FPARENTID' and id= OBJECT_ID('SYS_BASEDATAGROUP'))) 
     Alter Table SYS_BASEDATAGROUP add    FPARENTID     VARCHAR(50) NOT NULL DEFAULT ''     -- 父阶内码
if(not exists(select name from SysColumns where name ='FFULLNUMBER' and id= OBJECT_ID('SYS_BASEDATAGROUP'))) 
     Alter Table SYS_BASEDATAGROUP add    FFULLNUMBER     VARCHAR(max) NOT NULL DEFAULT ''     -- 长编码
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_BASEDATAGROUP'))) 
     Alter Table SYS_BASEDATAGROUP add    FNOTE     VARCHAR(max) NOT NULL DEFAULT ''     -- 备注
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_BASEDATAGROUP'))) 
     Alter Table SYS_BASEDATAGROUP add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_BASEDATAGROUP'))) 
     Alter Table SYS_BASEDATAGROUP add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_BASEDATAGROUP'))) 
     Alter Table SYS_BASEDATAGROUP add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO



GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:SYS_TABLEDESCRIPTION 系统表结构定义*/ 
 -- DROP TABLE SYS_TABLEDESCRIPTION
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'SYS_TABLEDESCRIPTION') AND type in (N'U'))
BEGIN
CREATE TABLE SYS_TABLEDESCRIPTION(
   FNUMBER     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 实体表名
   FNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 表描述
   FNOTE     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 备注
   FCNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 详细描述(第一语言)
   FTNAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 详细描述(第二语言)
   FENAME     NVARCHAR(200) NOT NULL DEFAULT '' ,    -- 详细描述(第三语言)
   FTYPE     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 表属性(1=系统表结构;2=普通表结构)
   FTTYPE     VARCHAR(1) NOT NULL DEFAULT '' ,    -- 表类型(1=基本资料，2=单据表头，3=单据表体,4=单据表体的表体,5=其他)
   FENABLEAUDITLOG     BIT NOT NULL DEFAULT  0 ,    -- 否打开审计日志
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_SYS_TABLEDESCRIPTION] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FNUMBER' and id= OBJECT_ID('SYS_TABLEDESCRIPTION'))) 
     Alter Table SYS_TABLEDESCRIPTION add    FNUMBER     VARCHAR(50) NOT NULL DEFAULT ''     -- 实体表名
if(not exists(select name from SysColumns where name ='FNAME' and id= OBJECT_ID('SYS_TABLEDESCRIPTION'))) 
     Alter Table SYS_TABLEDESCRIPTION add    FNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 表描述
if(not exists(select name from SysColumns where name ='FNOTE' and id= OBJECT_ID('SYS_TABLEDESCRIPTION'))) 
     Alter Table SYS_TABLEDESCRIPTION add    FNOTE     NVARCHAR(255) NOT NULL DEFAULT ''     -- 备注
if(not exists(select name from SysColumns where name ='FCNAME' and id= OBJECT_ID('SYS_TABLEDESCRIPTION'))) 
     Alter Table SYS_TABLEDESCRIPTION add    FCNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 详细描述(第一语言)
if(not exists(select name from SysColumns where name ='FTNAME' and id= OBJECT_ID('SYS_TABLEDESCRIPTION'))) 
     Alter Table SYS_TABLEDESCRIPTION add    FTNAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 详细描述(第二语言)
if(not exists(select name from SysColumns where name ='FENAME' and id= OBJECT_ID('SYS_TABLEDESCRIPTION'))) 
     Alter Table SYS_TABLEDESCRIPTION add    FENAME     NVARCHAR(200) NOT NULL DEFAULT ''     -- 详细描述(第三语言)
if(not exists(select name from SysColumns where name ='FTYPE' and id= OBJECT_ID('SYS_TABLEDESCRIPTION'))) 
     Alter Table SYS_TABLEDESCRIPTION add    FTYPE     VARCHAR(1) NOT NULL DEFAULT ''     -- 表属性(1=系统表结构;2=普通表结构)
if(not exists(select name from SysColumns where name ='FTTYPE' and id= OBJECT_ID('SYS_TABLEDESCRIPTION'))) 
     Alter Table SYS_TABLEDESCRIPTION add    FTTYPE     VARCHAR(1) NOT NULL DEFAULT ''     -- 表类型(1=基本资料，2=单据表头，3=单据表体,4=单据表体的表体,5=其他)
if(not exists(select name from SysColumns where name ='FENABLEAUDITLOG' and id= OBJECT_ID('SYS_TABLEDESCRIPTION'))) 
     Alter Table SYS_TABLEDESCRIPTION add    FENABLEAUDITLOG     BIT NOT NULL DEFAULT  0     -- 否打开审计日志
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('SYS_TABLEDESCRIPTION'))) 
     Alter Table SYS_TABLEDESCRIPTION add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('SYS_TABLEDESCRIPTION'))) 
     Alter Table SYS_TABLEDESCRIPTION add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('SYS_TABLEDESCRIPTION'))) 
     Alter Table SYS_TABLEDESCRIPTION add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO

-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_TABLEDESCRIPTION') AND name = N'SYS_TABLEDESCRIPTION_1' ) 
   Drop Index SYS_TABLEDESCRIPTION.SYS_TABLEDESCRIPTION_1
GO
Create  unique index SYS_TABLEDESCRIPTION_1 on SYS_TABLEDESCRIPTION(FINTERID)
GO
-- 
IF EXISTS (SELECT * FROM sysindexes WHERE id = OBJECT_ID(N'SYS_TABLEDESCRIPTION') AND name = N'SYS_TABLEDESCRIPTION_2' ) 
   Drop Index SYS_TABLEDESCRIPTION.SYS_TABLEDESCRIPTION_2
GO
Create  unique index SYS_TABLEDESCRIPTION_2 on SYS_TABLEDESCRIPTION(FNUMBER)
GO


GO

/* 脚本导出时间:2024/01/22 15:22:24 */ 
/* 脚本导出人员:系统管理员 */ 
/* 脚本    说明:ODK_SRM_SYSMessage 系统消息*/ 
 -- DROP TABLE ODK_SRM_SYSMessage
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ODK_SRM_SYSMessage') AND type in (N'U'))
BEGIN
CREATE TABLE ODK_SRM_SYSMessage(
   FDATE     DATE NULL  ,    -- 日期
   FUSERID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 用户内码
   FKEY     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 单据内码
   FPRGKEY     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 写入功能编号
   FFUNCTION     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 功能按钮
   FREADPRGKEY     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 读取功能编号
   FBILLTYPE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 读取单据类型
   FVISIBLTYPE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 可看类型
   FISSKIP     BIT NOT NULL DEFAULT  0 ,    -- 是否跳转
   FDESC     NVARCHAR(255) NOT NULL DEFAULT '' ,    -- 描述
   FSUPPLYID     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 供应商
   FTYPE     VARCHAR(50) NOT NULL DEFAULT '' ,    -- 类型
   FHASREAD     INT NOT NULL DEFAULT 0 ,    -- 已读状态
   FHASREADTIME     DATE NULL  ,    -- 已读时间
   FINTERID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 表内码 
   FBILLNO	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 单据编号 
   FGROUPID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 分组内码 
   FSTATUS		    INT NOT NULL DEFAULT 0 ,	 -- 状态 
   FDELETED          BIT NOT NULL DEFAULT  0 ,    --禁用状态 0 = 正常 1 = 禁用 
   FCOMPANYID	    VARCHAR(50)		NOT NULL DEFAULT '',	-- 组织代码 
   C_YMD				DATETIME		NULL,		            -- 资料创建日期
   C_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 资料创建帐号
   M_YMD				DATETIME		NULL,		            -- 最后异动日期
   M_USER				VARCHAR(20)		NOT NULL DEFAULT '',	-- 最后异动帐号
   UID VARCHAR(50) NOT NULL CONSTRAINT [PK_ODK_SRM_SYSMessage] PRIMARY KEY NONCLUSTERED
 ) 
END
ELSE
BEGIN
if(not exists(select name from SysColumns where name ='FDATE' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FDATE     DATE NULL      -- 日期
if(not exists(select name from SysColumns where name ='FUSERID' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FUSERID     VARCHAR(50) NOT NULL DEFAULT ''     -- 用户内码
if(not exists(select name from SysColumns where name ='FKEY' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FKEY     VARCHAR(50) NOT NULL DEFAULT ''     -- 单据内码
if(not exists(select name from SysColumns where name ='FPRGKEY' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FPRGKEY     VARCHAR(50) NOT NULL DEFAULT ''     -- 写入功能编号
if(not exists(select name from SysColumns where name ='FFUNCTION' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FFUNCTION     VARCHAR(50) NOT NULL DEFAULT ''     -- 功能按钮
if(not exists(select name from SysColumns where name ='FREADPRGKEY' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FREADPRGKEY     VARCHAR(50) NOT NULL DEFAULT ''     -- 读取功能编号
if(not exists(select name from SysColumns where name ='FBILLTYPE' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FBILLTYPE     VARCHAR(50) NOT NULL DEFAULT ''     -- 读取单据类型
if(not exists(select name from SysColumns where name ='FVISIBLTYPE' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FVISIBLTYPE     VARCHAR(50) NOT NULL DEFAULT ''     -- 可看类型
if(not exists(select name from SysColumns where name ='FISSKIP' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FISSKIP     BIT NOT NULL DEFAULT  0     -- 是否跳转
if(not exists(select name from SysColumns where name ='FDESC' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FDESC     NVARCHAR(255) NOT NULL DEFAULT ''     -- 描述
if(not exists(select name from SysColumns where name ='FSUPPLYID' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FSUPPLYID     VARCHAR(50) NOT NULL DEFAULT ''     -- 供应商
if(not exists(select name from SysColumns where name ='FTYPE' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FTYPE     VARCHAR(50) NOT NULL DEFAULT ''     -- 类型
if(not exists(select name from SysColumns where name ='FHASREAD' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FHASREAD     INT NOT NULL DEFAULT 0     -- 已读状态
if(not exists(select name from SysColumns where name ='FHASREADTIME' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FHASREADTIME     DATE NULL      -- 已读时间
if(not exists(select name from SysColumns where name ='FCOMPANYID' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FCOMPANYID     VARCHAR(50) NOT NULL DEFAULT ''      -- 组织代码
if(not exists(select name from SysColumns where name ='FINTERID' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FINTERID     VARCHAR(50) NOT NULL DEFAULT ''      -- 表内码
if(not exists(select name from SysColumns where name ='FBILLNO' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FBILLNO     VARCHAR(50) NOT NULL DEFAULT ''      -- 单据编号
if(not exists(select name from SysColumns where name ='FGROUPID' and id= OBJECT_ID('ODK_SRM_SYSMessage'))) 
     Alter Table ODK_SRM_SYSMessage add    FGROUPID     VARCHAR(50) NOT NULL DEFAULT ''      -- 分组内码

END
GO



GO

