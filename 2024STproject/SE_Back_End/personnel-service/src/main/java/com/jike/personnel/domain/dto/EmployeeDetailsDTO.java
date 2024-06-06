package com.jike.personnel.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.util.Date;

@Data
@ApiModel(description = "新增员工实体")
public class EmployeeDetailsDTO {
    @ApiModelProperty("员工id")
    private int employeeId;

    @ApiModelProperty("员工姓名")
    private String name;

    @ApiModelProperty("性别")
    private String sex;

    @ApiModelProperty("年龄")
    private int age;

    @ApiModelProperty("所属岗位号")
    private int postId;

    @ApiModelProperty("入职时间")
    private Date entryTime;

    @ApiModelProperty("电话")
    private String phoneNumber;

    @ApiModelProperty("基本工资")
    private int basePay;

    @ApiModelProperty("密码")
    private String password;

    @ApiModelProperty("银行名称")
    private String bankName;

    @ApiModelProperty("银行卡号")
    private String creditCardNumber;

    @ApiModelProperty("账号")
    private String account;

    @ApiModelProperty("岗位名称")
    private String postName;

    @ApiModelProperty("部门名称")
    private String departmentName;
}
