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
@TableName("ROOM")
public class Room implements Serializable {
    private static final long serialVersionUID = 1L;

    /**
     * id
     */
    @TableId(value = "ROOM_ID", type = IdType.AUTO)
    private int roomId;

    /**
     * 房间类型id
     */
    @TableField("TYPE_ID")
    private int typeId;

    /**
     * 房间号
     */
    @TableField("ROOM_NUMBER")
    private int roomNumber;

    /**
     * 房间电话
     */
    @TableField("ROOM_PHONE")
    private String roomPhone;

    /**
     * 信用等级
     */
    @TableField("STATUS")
    private String status;
}
