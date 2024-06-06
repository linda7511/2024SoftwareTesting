package com.jike.performance.domain.po;
import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.experimental.Accessors;

import java.io.Serializable;
@Data
@EqualsAndHashCode(callSuper = false)
@Accessors(chain = true)
@TableName("SALARY")
public class Salary implements Serializable{
    private static final long serialVersionUID = 1L;
    /**
     * id
     */
    @TableId(value = "Salary_ID", type = IdType.AUTO)
    private int salaryId;

    /**
     * 员工ID
     */
    @TableField("EMPLOYEE_ID")
    private int employeeId;

    /**
     * 年份
     */
    @TableField("YEAR")
    private int year;

    /**
     * 月份
     */
    @TableField("MONTH")
    private int month;

    /**
     * 绩效工资
     */
    @TableField("BONUS")
    private double bonus;

    /**
     * 节假日补贴
     */
    @TableField("HOLIDAY_ALLOWANCE")
    private double holidayAllowance;

    /**
     * 其它补贴
     */
    @TableField("OTHER_ALLOWANCE")
    private double otherAllowance;

    /**
     * 提成
     */
    @TableField("COMMISSION")
    private double commission;

    /**
     * 年终奖
     */
    @TableField("YEAR_END_BONUS")
    private double yearEndBonus;

    /**
     * 加班费
     */
    @TableField("OVERTIME_PAY")
    private double overtimePay;

    /**
     * 奖励类型
     */
    @TableField("REWARD_TYPE")
    private String rewardType;

    /**
     * 奖励金额
     */
    @TableField("REWARD_AMOUNT")
    private double rewardAmount;

    /**
     * 迟到扣款
     */
    @TableField("LATE_DEDUCTION")
    private double lateDeduction;

    /**
     * 早退扣款
     */
    @TableField("EARLY_DEPARTURE_DEDUCTION")
    private double earlyDepartureDeduction;

    /**
     * 个人所得税
     */
    @TableField("INCOME_TAX")
    private double incomeTax;


    /**
     * 五险一金
     */
    @TableField("SOCIAL_INSURANCE")
    private double socialInsurance;

    /**
     * 实发工资
     */
    @TableField("GROSS_SALARY")
    private double grossSalary;

    /**
     * 应发工资
     */
    @TableField("NET_SALARY")
    private double netSalary;

}
