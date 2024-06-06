package com.jike.canteen.domain.dto;


import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@ApiModel(description = "订单菜品")
public class OrderDishDTO {

    @ApiModelProperty("桌位id")
    private int tableId;
    @ApiModelProperty("菜品id")
    private int dishId;
    @ApiModelProperty("消费记录id")
    private int consumptionRecordId;
    @ApiModelProperty("下单时间")
    private LocalDateTime orderTime;
    @ApiModelProperty("订单状态")
    private String orderStatus;
    @ApiModelProperty("菜品名称")
    private String dishName;
    @ApiModelProperty("菜品价格")
    private double dishPrice;
    @ApiModelProperty("菜品口味")
    private String dishTaste;
}
