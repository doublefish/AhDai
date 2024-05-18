using System.Collections.Generic;
using System.Text;

namespace AhDai.Service.Impl;

/// <summary>
/// MyConst
/// </summary>
internal class MyConst
{
    internal class Claim
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public const string ID = "Id";
        /// <summary>
        /// 用户名
        /// </summary>
        public const string USERNAME = "Username";
        /// <summary>
        /// 姓名
        /// </summary>
        public const string NAME = "Name";
        /// <summary>
        /// 角色编码
        /// </summary>
        public const string ROLE_CODES = "RoleCodes";
        /// <summary>
        /// 组织Id
        /// </summary>
        public const string EMPLOYEE_ID = "EmployeeId";
        /// <summary>
        /// 组织Id
        /// </summary>
        public const string ORG_ID = "OrgId";
        /// <summary>
        /// 租户Id
        /// </summary>
        public const string TENANT_ID = "TenantId";
        /// <summary>
        /// 租户名称
        /// </summary>
        public const string TENANT_NAME = "TenantName";
        /// <summary>
        /// 租户类型
        /// </summary>
        public const string TENANT_TYPE = "TenantType";
    }

    /// <summary>
    /// 租户
    /// </summary>
    internal class Tenant
    {
        /// <summary>
        /// 系统
        /// </summary>
        public const long SYSTEM = 10000;
    }

    /// <summary>
    /// 角色
    /// </summary>
    internal class Role
    {
        /// <summary>
        /// 系统管理员
        /// </summary>
        public const string SYSTEM_ADMIN = "sysAdmin";
        /// <summary>
        /// HR
        /// </summary>
        public const string HR = "HR";
        /// <summary>
        /// PM
        /// </summary>
        public const string PM = "PM";
    }

    /// <summary>
    /// Redis
    /// </summary>
    internal class Redis
    {
        /// <summary>
        /// ROOT
        /// </summary>
        public const string ROOT = "AhDai";

        /// <summary>
        /// GenerateKey
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="suffixes"></param>
        /// <returns></returns>
        public static string GenerateKey<T>(params string[] suffixes)
        {
            return GenerateKey<T>(null, suffixes);
        }

        /// <summary>
        /// GenerateKey
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prefix"></param>
        /// <param name="suffixes"></param>
        /// <returns></returns>
        public static string GenerateKey<T>(string? prefix, params string[] suffixes)
        {
            var keys = new List<string>(suffixes);
            keys.Insert(0, typeof(T).Name);
            if (prefix != null) keys.Insert(0, prefix);
            return GenerateKey([.. keys]);
        }

        /// <summary>
        /// GenerateKey
        /// </summary>
        /// <param name="suffixes"></param>
        /// <returns></returns>
        public static string GenerateKey(params string[] suffixes)
        {
            var builder = new StringBuilder(ROOT);
            foreach (var suffix in suffixes)
            {
                builder = builder.Append(':').Append(suffix);
            }
            return builder.ToString();
        }

    }

    /// <summary>
    /// 字典编码
    /// </summary>
    internal class Dict
    {
        /// <summary>
        /// 证书类型
        /// </summary>
        public const string CERTIFICATE_TYPE = "certificate_type";
        /// <summary>
        /// 合同类型
        /// </summary>
        public const string CONTRACT_TYPE = "contract_type";
        /// <summary>
        /// 合同费用类型
        /// </summary>
        public const string CONTRACT_EXPENSE_TYPE = "contract_expense_type";
        /// <summary>
        /// 招标方式
        /// </summary>
        public const string TENDER_METHOD = "tender_method";
        /// <summary>
        /// 成本类型
        /// </summary>
        public const string COST_TYPE = "cost_type";
        /// <summary>
        /// 纳税人类型
        /// </summary>
        public const string TAXPAYER_TYPE = "taxpayer_type";

        /// <summary>
        /// 甲方单位类型
        /// </summary>
        public const string OWNER_TYPE = "owner_type";
        /// <summary>
        /// 供应商类型
        /// </summary>
        public const string SUPPLIER_TYPE = "supplier_type";

        /// <summary>
        /// 标书 - 类型
        /// </summary>
        public const string BID_DOCUMENT_TYPE = "bid_document_type";

        /// <summary>
        /// 项目类型
        /// </summary>
        public const string PROJECT_TYPE = "project_type";
        /// <summary>
        /// 项目来源
        /// </summary>
        public const string PROJECT_SOURCE = "project_source";
        /// <summary>
        /// 项目成员岗位
        /// </summary>
        public const string PROJECT_MEMBER_POST = "project_member_post";

        /// <summary>
        /// 项目变更 - 类型
        /// </summary>
        public const string PROJECT_CHANGE_TYPE = "project_change_type";

        /// <summary>
        /// 保函类型
        /// </summary>
        public const string GUARANTEE_LETTER_TYPE = "guarantee_letter_type";

        /// <summary>
        /// 印章类型
        /// </summary>
        public const string SEAL_TYPE = "seal_type";
        /// <summary>
        /// 用章申请 - 资料类型
        /// </summary>
        public const string SEAL_USE_DOCUMENT_TYPE = "seal_use_document_type";

        /// <summary>
        /// 劳务工种
        /// </summary>
        public const string LABOR_WORK_TYPE = "labor_work_type";
        /// <summary>
        /// 劳务工资类型
        /// </summary>
        public const string LABOR_SALARY_TYPE = "labor_salary_type";

        /// <summary>
        /// 开户申请 - 申请类型
        /// </summary>
        public const string ACCOUNT_OPENING_TYPE = "account_opening_type";
        /// <summary>
        /// 开户申请 - 满足条件
        /// </summary>
        public const string ACCOUNT_OPENING_CONDITION = "account_opening_condition";

        /// <summary>
        /// 监管户内转 - 资金用途
        /// </summary>
        public const string ACCOUNT_TRANSFER_PURPOSE = "account_transfer_purpose";

        /// <summary>
        /// 回款登记 - 收款类型/结算方式
        /// </summary>
        public const string COLLECTION_SOURCE_TYPE = "collection_source_type";
        /// <summary>
        /// 回款登记 - 收款方式/付款方式
        /// </summary>
        public const string COLLECTION_METHOD = "collection_method";
        /// <summary>
        /// 收款单 - 款项来源类型
        /// </summary>
        public const string COLLECTION_RECEIPT_SOURCE_TYPE = "collection_receipt_source_type";
        /// <summary>
        /// 收款单 - 借款类型
        /// </summary>
        public const string COLLECTION_RECEIPT_BORROWING_TYPE = "collection_receipt_borrowing_type";

        /// <summary>
        /// 回款登记 - 扣款资金用途
        /// </summary>
        public const string COLLECTION_DEDUCTION_FUND_PURPOSE = "collection_deduction_fund_purpose";

        /// <summary>
        /// 投标保证金 - 类型
        /// </summary>
        public const string BID_DEPOSIT_TYPE = "bid_deposit_type";
        /// <summary>
        /// 投标保证金退回 - 类型
        /// </summary>
        public const string BID_DEPOSIT_RETURN_METHOD = "bid_deposit_return_method";

        /// <summary>
        /// 费用类型
        /// </summary>
        public const string EXPENSE_TYPE = "expense_type";

        /// <summary>
        /// 发票类型
        /// </summary>
        public const string INVOICE_TYPE = "invoice_type";

        /// <summary>
        /// 跨区域经营方式
        /// </summary>
        public const string OVERSEAS_BUSINESS_MODEL = "overseas_business_model";

        /// <summary>
        /// 付款申请 - 付款类型
        /// </summary>
        public const string PAYMENT_TYPE = "payment_type";
        /// <summary>
        /// 付款申请 - 付款方式
        /// </summary>
        public const string PAYMENT_METHOD = "payment_method";
        /// <summary>
        /// 付款 - 款项来源类型
        /// </summary>
        public const string PAYMENT_SOURCE_TYPE = "payment_source_type";

        /// <summary>
        /// 应收账款类目
        /// </summary>
        public const string RECEIVABLE_CATEGORY = "receivable_category";
        /// <summary>
        /// 应收单位
        /// </summary>
        public const string RECEIVABLE_UNIT = "receivable_unit";

        /// <summary>
        /// 对账类型
        /// </summary>
        public const string RECONCILIATION_TYPE = "reconciliation_type";

        /// <summary>
        /// 安全保证金退回 - 项目情况
        /// </summary>
        public const string SAFETY_DEPOSIT_RETURN_PROJECT_SITUATION = "safety_deposit_return_project_situation";

        /// <summary>
        /// 工作任务类型
        /// </summary>
        public const string WORK_TASK_TYPE = "work_task_type";

        /// <summary>
        /// 预警 - 类型
        /// </summary>
        public const string WARNING_TYPE = "warning_type";
        /// <summary>
        /// 预警 - 处理方式
        /// </summary>
        public const string WARNING_METHOD = "warning_method";

        /// <summary>
        /// 项目检查 - 类型
        /// </summary>
        public const string PROJECT_INSPECTION_TYPE = "project_inspection_type";


        /// <summary>
        /// 员工考勤 - 配置
        /// </summary>
        public const string EMPLOYEE_ATTENDANCE_CONFIG = "employee_attendance_config";
        /// <summary>
        /// 员工福利 - 类型
        /// </summary>
        public const string EMPLOYEE_BENEFIT_TYPE = "employee_benefit_type";
        /// <summary>
        /// 员工请假 - 类型
        /// </summary>
        public const string EMPLOYEE_LEAVE_TYPE = "employee_leave_type";
        /// <summary>
        /// 日常报销 - 费用类型
        /// </summary>
        public const string EMPLOYEE_REIMBURSEMENT_EXPENSE_TYPE = "employee_reimbursement_expense_type";
        /// <summary>
        /// 员工调薪 - 类型
        /// </summary>
        public const string EMPLOYEE_SALARY_ADJUSTMENT_TYPE = "employee_salary_adjustment_type";
        /// <summary>
        /// 办公用品 - 类型
        /// </summary>
        public const string OFFICE_SUPPLY_TYPE = "office_supply_type";
        /// <summary>
        /// 招聘方式
        /// </summary>
        public const string RECRUITMENT_METHOD = "recruitment_method";
        /// <summary>
        /// 交通工具 - 类型
        /// </summary>
        public const string TRANSPORTATION = "transportation";

    }

    /// <summary>
    /// 字典值
    /// </summary>
    internal class DictValue
    {

        public class EmployeeAttendanceConfig
        {
            /// <summary>
            /// 上午上班时间
            /// </summary>
            public const string WORK_START_TIME1 = "work_start_time1";
            /// <summary>
            /// 上午下班时间
            /// </summary>
            public const string WORK_END_TIME1 = "work_end_time1";
            /// <summary>
            /// 下午上班时间
            /// </summary>
            public const string WORK_START_TIME2 = "work_start_time2";
            /// <summary>
            /// 下午下班时间
            /// </summary>
            public const string WORK_END_TIME2 = "work_end_time2";
            /// <summary>
            /// 工作时长（小时）
            /// </summary>
            public const string WORKING_HOURS = "working_hours";
            /// <summary>
            /// 最小加班时长（分钟）
            /// </summary>
            public const string MIN_OVERTIME_DURATION = "min_overtime_duration";
            /// <summary>
            /// 加班时间单位（分钟）
            /// </summary>
            public const string OVERTIME_TIME_UNIT = "overtime_time_unit";
            /// <summary>
            /// 节假日
            /// </summary>
            public const string HOLIDAYS = "holidays";
            /// <summary>
            /// 工作日
            /// </summary>
            public const string WORKDAYS = "workdays";
        }

        /// <summary>
        /// 成本类型
        /// </summary>
        public class CostType
        {
            /// <summary>
            /// 直接费
            /// </summary>
            public const string DIRECT = "直接费";
            /// <summary>
            /// 劳务
            /// </summary>
            public const string LABOR = "劳务";
            /// <summary>
            /// 其他
            /// </summary>
            public const string OTHER = "其他";
        }

        /// <summary>
        /// 回款登记 - 款项来源类型
        /// </summary>
        public class CollectionSourceType
        {
            /// <summary>
            /// 预付款
            /// </summary>
            public const string PREPAYMENT = "预付款";
            /// <summary>
            /// 进度款
            /// </summary>
            public const string PROGRESS = "进度款";
            /// <summary>
            /// 竣工结算款
            /// </summary>
            public const string COMPLETION = "竣工结算款";
            /// <summary>
            /// 质保金
            /// </summary>
            public const string QUALITY_DEPOSIT = "质保金";
        }

        /// <summary>
        /// 收款单 - 款项来源类型
        /// </summary>
        public class CollectionReceiptSourceType
        {
            /// <summary>
            /// 个人
            /// </summary>
            public const string PERSONAL = "个人";
            /// <summary>
            /// 投标/履约保证金退回
            /// </summary>
            public const string DEPOSIT = "投标/履约保证金退回";
            /// <summary>
            /// 借款
            /// </summary>
            public const string BORROWING = "借款";
            /// <summary>
            /// 其他
            /// </summary>
            public const string OTHER = "其他";
        }

        /// <summary>
        /// 付款来源类型
        /// </summary>
        public class PaymentSourceType
        {
            /// <summary>
            /// 工程回款
            /// </summary>
            public const string PROJECT = "工程回款";
            /// <summary>
            /// 退质保金
            /// </summary>
            public const string QUALITY_DEPOSIT = "退质保金";
            /// <summary>
            /// 个人垫资
            /// </summary>
            public const string PERSONAL = "个人垫资";
            /// <summary>
            /// 保证金
            /// </summary>
            public const string DEPOSIT = "保证金";
            /// <summary>
            /// 借款
            /// </summary>
            public const string BORROWING = "借款";
            /// <summary>
            /// 其他退回
            /// </summary>
            public const string OTHER_RETURN = "其他退回";
            /// <summary>
            /// 其他
            /// </summary>
            public const string OTHER = "其他";

            /// <summary>
            /// 获取数据类型
            /// </summary>
            /// <param name="value"></param>
            /// <returns>1-收款登记，2-收款单</returns>
            public static int GetDataType(string value)
            {
                return value switch
                {
                    PROJECT or QUALITY_DEPOSIT => 1,
                    PERSONAL or DEPOSIT or OTHER_RETURN or BORROWING => 2,
                    _ => 0,
                };
            }
        }

        /// <summary>
        /// 付款类型
        /// </summary>
        public class PaymentType
        {
            /// <summary>
            /// 分包费
            /// </summary>
            public const string SUBCONTRACT = "分包费";
            /// <summary>
            /// 劳务费
            /// </summary>
            public const string LABOR = "劳务费";
            /// <summary>
            /// 材料费
            /// </summary>
            public const string MATERIAL = "材料费";
            /// <summary>
            /// 其他
            /// </summary>
            public const string OTHER = "其他";
        }
    }
}
