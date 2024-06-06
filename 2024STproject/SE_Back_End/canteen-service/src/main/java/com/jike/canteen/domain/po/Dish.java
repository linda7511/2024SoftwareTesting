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
@TableName("DISH")
public class Dish implements Serializable  {
    private static final long serialVersionUID = 1L;


    /**
     * 菜品id
     */
    @TableId(value = "DISH_ID", type = IdType.AUTO)
    private int dishId;

    /**
     * 菜品名称
     */
    @TableField("DISH_NAME")
    private String dishName;


    /**
     * 菜品价格
     */
    @TableField("DISH_PRICE")
    private double dishPrice;

    /**
     * 菜品口味
     */
    @TableField("DISH_TASTE")
    private String dishTaste;

}
