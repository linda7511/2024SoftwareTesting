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
@TableName("APPLICATION")
public class Application implements Serializable {
    private static final long serialVersionUID = 1L;
    /**
     * 申请号
     */
    @TableId(value = "APPLICATION_ID", type = IdType.AUTO)
    private int applicationId;

    /**
     * 申请时间
     */
    @TableField("APPLICATION_TIME")
    private Date applicationTime;

    /**
     * 申请类型
     */
    @TableField("APPLICATION_TYPE")
    private String applicationType;
    /**
     * 申请内容
     */
    @TableField("APPLICATION_CONTENT")
    private String applicationContent;
    /**
     * 是否批准
     */
    @TableField("IF_APPROVE")
    private String ifApprove;
    /**
     * 员工id
     */
    @TableField("EMPLOYEE_ID")
    private int employeeId;
    /**
     * 部门id
     */
    @TableField("DEPARTMENT_ID")
    private int departmentId;

}
