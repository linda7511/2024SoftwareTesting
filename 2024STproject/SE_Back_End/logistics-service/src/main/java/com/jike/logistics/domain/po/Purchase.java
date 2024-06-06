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
import java.util.Date;

@Data
@EqualsAndHashCode(callSuper = false)
@Accessors(chain = true)
@TableName("PURCHASE")
public class Purchase implements Serializable {
    private static final long serialVersionUID = 1L;

    /**
     * 物资id
     */

    @TableId (value = "PURCHASE_ID" ,type = IdType.AUTO)
    private int purchaseId;


    @TableField("GOODS_ID")
    private int goodsId;

    /**
     * 员工id
     */

    @TableField("EMPLOYEE_ID")
    private int employeeId;

    /**
     * 采购时间
     */
    @TableField("PURCHASE_DATE")
    private Date purchaseDate;

    /**
     * 采购数量
     */
    @TableField("PURCHASE_QUANTITY")
    private int purchaseQuantity;

    /**
     * 单价
     */
    @TableField("UNIT_PRICE")
    private double unitPrice;
}
