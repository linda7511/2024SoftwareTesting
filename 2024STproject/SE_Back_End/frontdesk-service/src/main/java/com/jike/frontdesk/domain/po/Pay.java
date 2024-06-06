package com.jike.frontdesk.domain.po;


import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableName;
import com.github.jeffreyning.mybatisplus.anno.MppMultiId;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.experimental.Accessors;

import java.io.Serializable;
import java.time.LocalDateTime;

@Data
@EqualsAndHashCode(callSuper = false)
@Accessors(chain = true)
@TableName("PAY")
public class Pay implements Serializable {
    private static final long serialVersionUID = 1L;

    /**
     * 顾客id
     */
    @MppMultiId
    @TableField("CUSTOMER_ID")
    private int customerId;

    /**
     * 账单id
     */
    @MppMultiId
    @TableField(value = "BILL_ID")
    private int billId;

    /**
     * 支付方式
     */
    @TableField("PAY_METHOD")
    private String payMethod;

    /**
     * 支付时间
     */
    @TableField("PAY_TIME")
    private LocalDateTime payTime;

    /**
     * 支付金额
     */
    @TableField("PAY_AMOUNT")
    private double payAmount;

    /**
     * 支付状态
     */
    @TableField("PAY_STATUS")
    private String payStatus;
}
