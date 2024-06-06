package com.jike.frontdesk.domain.po;

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
@TableName("CUSTOMER")
public class Customer implements Serializable {
    private static final long serialVersionUID = 1L;

    /**
     * id
     */
    @TableId(value = "CUSTOMER_ID", type = IdType.AUTO)
    private int customerId;

    /**
     * 身份证号
     */
    @TableField("ID")
    private String ID;

    /**
     * 性别
     */
    @TableField("GENDER")
    private String gender;

    /**
     * 电话
     */
    @TableField("PHONE")
    private String phone;

    /**
     * 信用等级
     */
    @TableField("CREDIT_GRADE")
    private String creditGrade;

    /**
     * 会员等级
     */
    @TableField("MEMBER_GRADE")
    private String memberGrade;

    /**
     * 姓名
     */
    @TableField("NAME")
    private String name;
}