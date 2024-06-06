package com.jike.personnel.domain.po;


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
@TableName("DEPARTMENT")
public class Department implements Serializable {
    private static final long serialVersionUID = 1L;
    /**
     * 部门id
     */
    @TableId(value = "DEPARTMENT_ID", type = IdType.AUTO)
    private int departmentId;

    /**
     * 部门名称
     */
    @TableField("DEPARTMENT_NAME")
    private String departmentName;


    /**
     * 部门人数
     */
    @TableField("NUMBER_OF_PEOPLE")
    private int numberOfPeople;



}
