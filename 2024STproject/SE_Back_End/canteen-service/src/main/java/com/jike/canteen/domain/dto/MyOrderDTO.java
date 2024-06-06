package com.jike.canteen.domain.dto;


import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@ApiModel(description = "餐厅订单联系集")
public class MyOrderDTO {

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
}
