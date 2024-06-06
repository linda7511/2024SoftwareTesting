package com.jike.logistics.domain.po;

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
@TableName("GOODS")
public class Goods implements Serializable {
    private static final long serialVersionUID = 1L;

    /**
     * id
     */
    @TableId(value = "GOODS_ID", type = IdType.AUTO)
    private int goodsId;

    /**
     * 种类
     */
    @TableField("CATEGORY")
    private String category;

    /**
     * 名称
     */
    @TableField("GOODS_NAME")
    private String goodsName;

    /**
     * 库存
     */
    @TableField("INVENTORY")
    private int inventory;
}
