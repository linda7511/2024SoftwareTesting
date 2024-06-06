package com.jike.frontdesk.domain.po;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.experimental.Accessors;

import java.io.Serializable;
import java.time.LocalDateTime;
import java.util.Date;

@Data
@EqualsAndHashCode(callSuper = false)
@Accessors(chain = true)
@TableName("ROOMORDER")
public class RoomOrder implements Serializable {
    private static final long serialVersionUID = 1L;

    /**
     * id
     */
    @TableId(value = "ORDER_ID", type = IdType.AUTO)
    private int orderId;

    /**
     * 顾客id
     */
    @TableField("CUSTOMER_ID")
    private int customerId;

    /**
     * 房间类型id
     */
    @TableField("TYPE_ID")
    private int typeId;

    /**
     * 员工id
     */
    @TableField("EMPLOYEE_ID")
    private int employeeId;

    /**
     * 订单状态
     */
    @TableField("ORDER_STATUS")
    private String orderStatus;

    /**
     * 创建时间
     */
    @TableField("CREATE_TIME")
    private LocalDateTime createTime;

    /**
     * 期待入住时间
     */
    @TableField("EXPECT_IN_TIME")
    private LocalDateTime expectInTime;

    /**
     * 期待退房时间
     */
    @TableField("EXPECT_OUT_TIME")
    private LocalDateTime expectOutTime;

    /**
     * 价格
     */
    @TableField("PRICE")
    private double price;

    /**
     * 备注
     */
    @TableField("NOTE")
    private String note;

    /**
     * 顾客人数
     */
    @TableField("CUSTOMER_NUM")
    private int customerNum;
}
