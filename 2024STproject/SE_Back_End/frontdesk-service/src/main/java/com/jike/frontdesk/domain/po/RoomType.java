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
@TableName("ROOMTYPE")
public class RoomType implements Serializable {
    private static final long serialVersionUID = 1L;

    /**
     * id
     */
    @TableId(value = "TYPE_ID", type = IdType.AUTO)
    private int typeId;

    /**
     * 类型名称
     */
    @TableField("TYPE_NAME")
    private String typeName;

    /**
     * 价格
     */
    @TableField("PRICE")
    private double price;

    /**
     * 剩余量
     */
    @TableField("REMAINING")
    private int remaining;

    /**
     * 备注
     */
    @TableField("NOTE")
    private String note;
}
