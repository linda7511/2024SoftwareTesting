package com.jike.performance.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

@Data
@ApiModel(description = "工资实体")
public class NewSalaryDTO {
    @ApiModelProperty("员工id")
    private int employeeId;
    @ApiModelProperty("年份")
    private int year;
    @ApiModelProperty("月份")
    private int month;
    @ApiModelProperty("绩效工资")
    private double bonus;
    @ApiModelProperty("节假日补贴")
    private double holidayAllowance;
    @ApiModelProperty("其它补贴")
    private double otherAllowance;
    @ApiModelProperty("提成")
    private double commission;
    @ApiModelProperty("年终奖")
    private double yearEndBonus;
    @ApiModelProperty("加班费")
    private double overtimePay;
    @ApiModelProperty("奖励类型")
    private String rewardType;
    @ApiModelProperty("奖励金额")
    private double rewardAmount;
    @ApiModelProperty("迟到扣款")
    private double lateDeduction;
    @ApiModelProperty("早退扣款")
    private double earlyDepartureDeduction;
    @ApiModelProperty("个人所得税")
    private double incomeTax;
    @ApiModelProperty("五险一金")
    private double socialInsurance;
    @ApiModelProperty("应发工资")
    private double grossSalary;
    @ApiModelProperty("实发工资")
    private double netSalary;
}
