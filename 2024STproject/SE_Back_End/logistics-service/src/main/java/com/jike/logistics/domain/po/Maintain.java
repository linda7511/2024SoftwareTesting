package com.jike.logistics.domain.po;

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
@TableName("MAINTAIN")
public class Maintain implements Serializable {
    private static final long serialVersionUID = 1L;

    /**
     * 房间id
     */
    @MppMultiId
    @TableField("ROOM_ID")
    private int roomId;

    /**
     * 员工id
     */
    @MppMultiId
    @TableField("EMPLOYEE_ID")
    private int employeeId;

    /**
     * 维修时间
     */
    @MppMultiId
    @TableField("MAINTAINING_TIME")
    private LocalDateTime maintainingTime;

    /**
     * 维修项
     */
    @TableField("MAINTAINING_ITEM")
    private String maintainingItem;

    /**
     * 维修结果
     */
    @TableField("MAINTAINING_RESULT")
    private String maintainingResult;
}
