package com.jike.logistics.domain.po;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.github.jeffreyning.mybatisplus.anno.MppMultiId;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.experimental.Accessors;

import java.io.Serializable;
import java.time.LocalDate;
import java.time.LocalDateTime;

@Data
@EqualsAndHashCode(callSuper = false)
@Accessors(chain = true)
@TableName("CONSUME")
public class Consume implements Serializable {
    private static final long serialVersionUID = 1L;

    /**
     * 部门id
     */

    @TableId(value = "CONSUME_ID", type = IdType.AUTO)
    private int consumeId;


    @TableField("DEPARTMENT_ID")
    private int departmentId;

    /**
     * 物资id
     */

    @TableField("GOODS_ID")
    private int goodsId;

    /**
     * 消耗数量
     */
    @TableField("CONSUME_AMOUNT")
    private int consumeAmount;


}
