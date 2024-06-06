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
@TableName("MYORDER")
public class MyOrder implements Serializable {
    private static final long serialVersionUID = 1L;

    /**
     * 桌位id
     */
    @MppMultiId
    @TableField("TABLE_ID")
    private int tableId;

    /**
     *
     */
    @MppMultiId
    @TableField("DISH_ID")
    private int dishId;


    /**
     * 订单id菜品id
     */
    @TableField("CONSUMPTION_RECORD_ID")
    private int consumptionRecordId;



    /**
     * 订餐时间
     */
    @MppMultiId
    @TableField("ORDER_TIME")
    private LocalDateTime orderTime;



    /**
     * 订单状态
     */
    @TableField("ORDER_STATUS")
    private String orderStatus;


}
