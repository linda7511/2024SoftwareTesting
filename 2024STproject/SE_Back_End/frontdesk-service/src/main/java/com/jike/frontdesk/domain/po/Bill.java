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
@TableName("BILL")
public class Bill implements Serializable {
    private static final long serialVersionUID = 1L;

    /**
     * id
     */
    @TableId(value = "BILL_ID", type = IdType.AUTO)
    private int billId;

    /**
     * 住房开销
     */
    @TableField("ROOM_COST")
    private double roomCost;

    /**
     * 食物开销
     */
    @TableField("FOOD_COST")
    private double foodCost;

    /**
     * 其他开销
     */
    @TableField("OTHER_COST")
    private double otherCost;

    /**
     * 总开销
     */
    @TableField("SUM_COST")
    private double sumCost;

    /**
     * 账单类型
     */
    @TableField("BILL_TYPE")
    private String billType;

    /**
     * 发票
     */
    @TableField("INVOICE_NUMBER")
    private String invoiceNumber;

    /**
     * 顾客id
     */
    @TableField("CUSTOMER_ID")
    private int customerId;
}
