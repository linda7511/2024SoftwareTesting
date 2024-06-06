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

@Data
@EqualsAndHashCode(callSuper = false)
@Accessors(chain = true)
@TableName("CONSUMPTIONRECORDS")
public class ConsumptionRecord implements Serializable {
    private static final long serialVersionUID = 1L;

    /**
     * id
     */
    @TableId(value = "CONSUME_ID", type = IdType.AUTO)
    private int consumeId;

    /**
     * 房间id
     */
    @TableField("ROOM_ID")
    private int roomId;

    /**
     * 消费类型
     */
    @TableField("CONSUME_TYPE")
    private String consumeType;

    /**
     * 消费类型
     */
    @TableField("CONSUME_TIME")
    private LocalDateTime consumeTime;

    /**
     * 消费金额
     */
    @TableField("CONSUME_AMOUNT")
    private double consumeAmount;
}
