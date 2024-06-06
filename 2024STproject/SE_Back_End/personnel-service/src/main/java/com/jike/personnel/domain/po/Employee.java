package com.jike.personnel.domain.po;


import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.experimental.Accessors;

import java.io.Serializable;
import java.util.Date;

@Data
@EqualsAndHashCode(callSuper = false)
@Accessors(chain = true)
@TableName("EMPLOYEE")
public class Employee implements Serializable {
    private static final long serialVersionUID = 1L;

    /**
     * 员工id
     */
    @TableId(value = "EMPLOYEE_ID", type = IdType.AUTO)
    private int employeeId;

    /**
     * 员工姓名
     */
    @TableField("NAME")
    private String name;


    /**
     * 员工性别
     */
    @TableField("SEX")
    private String sex;
    /**
     * 员工年龄
     */
    @TableField("AGE")
    private int age;

    /**
     * 所属岗位号
     */
    @TableField("POST_ID")
    private int postId;


    /**
     * 入职时间
     */
    @TableField("ENTRY_TIME")
    private Date entryTime;

    /**
     * 电话
     */
    @TableField("PHONE_NUMBER")
    private String phoneNumber;

    /**
     * 基本工资
     */
    @TableField("BASE_PAY")
    private int basePay;


    /**
     * 密码
     */
    @TableField("PASSWORD")
    private String password;

    /**
     * 银行名称
     */
    @TableField("BANK_NAME")
    private String bankName;

    /**
     * 银行卡号
     */
    @TableField("CREDIT_CARD_NUMBER")
    private String creditCardNumber;

    /**
     * 账号
     */
    @TableField("ACCOUNT")
    private String account;

}
