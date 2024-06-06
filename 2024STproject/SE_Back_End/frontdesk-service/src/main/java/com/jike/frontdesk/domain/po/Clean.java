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
@TableName("CLEAN")
public class Clean implements Serializable {
    private static final long serialVersionUID = 1L;

    /**
     * 顾客id
     */
    @MppMultiId
    @TableField("EMPLOYEE_ID")
    private int employeeId;

    /**
     * 房间id
     */
    @MppMultiId
    @TableField(value = "ROOM_ID")
    private int roomId;

    /**
     * 清洁时间
     */
    @TableField("CLEANING_TIME")
    private LocalDateTime cleaningTime;
}
