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
@TableName("POST")
public class Post implements Serializable {

    private static final long serialVersionUID = 1L;
    /**
     * 岗位ID
     */
    @TableId(value = "POST_ID", type = IdType.AUTO)
    private int postId;

    /**
     * 岗位名称
     */
    @TableField("POST_NAME")
    private String postName;


    /**
     * 所属部门ID
     */
    @TableField("DEPARTMENT_ID")
    private int departmentId;
    /**
     * 岗位等级
     */
    @TableField("POSITION_LEVEL")
    private String positionLevel;

    /**
     * 岗位工资
     */
    @TableField("POSITION_SALARY")
    private int positionSalary;


    /**
     * 缴费类型
     */
    @TableField("PAYMENT_TYPE")
    private String paymentType;

    /**
     * 缴费基数
     */
    @TableField("PAYMENT_BASE")
    private String paymentBase;

    /**
     * 五险
     */
    @TableField("INSURANCE")
    private String insurance;


}
