package com.jike.frontdesk.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.time.LocalDateTime;
import java.util.Date;

@Data
@ApiModel(description = "房间订单实体")
public class RoomOrderUpdateDTO {
    @ApiModelProperty("id")
    private int orderId;
    @ApiModelProperty("订单状态")
    private String orderStatus;
}
