package com.jike.frontdesk.domain.po;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
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
@TableName("CHECKIN")
public class Checkin implements Serializable {
    private static final long serialVersionUID = 1L;

    /**
     * 顾客id
     */
    @MppMultiId
    @TableField("CUSTOMER_ID")
    private int customerId;

    /**
     * 房间id
     */
    @MppMultiId
    @TableField("ROOM_ID")
    private int roomId;

    /**
     * 入住时间
     */
    @MppMultiId
    @TableField("CHECKIN_TIME")
    private LocalDateTime checkinTime;

    /**
     * 退房时间
     */
    @TableField("CHECKOUT_TIME")
    private LocalDateTime checkoutTime;
}
