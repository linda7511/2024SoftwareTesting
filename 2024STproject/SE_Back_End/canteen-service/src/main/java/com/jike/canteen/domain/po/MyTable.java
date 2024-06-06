package com.jike.canteen.domain.po;
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
@TableName("MYTABLE")
public class MyTable implements Serializable{
    private static final long serialVersionUID = 1L;


    /**
     * 桌位id
     */
    @TableId(value = "TABLE_ID", type = IdType.AUTO)
    private int tableId;

    /**
     * 容纳人数
     */
    @TableField("CAPACITY")
    private int capacity;

    /**
     * 桌位类型
     */
    @TableField("TABLE_TYPE")
    private String tableType;


    /**
     * 桌位位置
     */
    @TableField("TABLE_LOCATION")
    private String tableLocation;

    /**
     * 桌位状态
     */
    @TableField("TABLE_STATUS")
    private String tableStatus;

    /**
     * 桌位备注
     */
    @TableField("NOTE")
    private String note;

    /**
     * 是否可预订
     */
    @TableField("BOOKABLE")
    private int bookable;


    /**
     * 是否可用
     */
    @TableField("AVAILABLE")
    private int available;
}
