package com.jike.canteen.domain.po;
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
@TableName("BOOK")
public class Book implements Serializable{
    private static final long serialVersionUID = 1L;

    /**
     * 桌位id
     */
    @MppMultiId
    @TableField("TABLE_ID")
    private int tableId;

    /**
     *顾客id
     */
    @MppMultiId
    @TableField("CUSTOMER_ID")
    private int customerId;

    /**
     * 预定时间
     */
    @MppMultiId
    @TableField("BOOK_TIME")
    private LocalDateTime bookTime;

    /**
     * 预定人数
     */
    @TableField("BOOK_NUMBER")
    private int bookNumber;

    /**
     * 预定状态
     */
    @TableField("BOOK_STATUS")
    private String bookStatus;

    /**
     * 特殊需求
     */
    @TableField("BOOK_REQUEMENT")
    private String bookRequement;

    /**
     * 预定备注
     */
    @TableField("BOOK_NOTE")
    private String bookNote;

}
