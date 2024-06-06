package com.jike.frontdesk.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.time.LocalDateTime;
import java.util.Date;

@Data
@ApiModel(description = "房间订单实体")
public class RoomOrderDTO {
    @ApiModelProperty("id")
    private int orderId;
    @ApiModelProperty("顾客id")
    private int customerId;
    @ApiModelProperty("房间类型id")
    private int typeId;
    @ApiModelProperty("员工id")
    private int employeeId;
    @ApiModelProperty("订单状态")
    private String orderStatus;
    @ApiModelProperty("创建时间")
    private LocalDateTime createTime;
    @ApiModelProperty("期待入住时间")
    private LocalDateTime expectInTime;
    @ApiModelProperty("期待退房时间")
    private LocalDateTime expectOutTime;
    @ApiModelProperty("价格")
    private double price;
    @ApiModelProperty("备注")
    private String note;
    @ApiModelProperty("顾客人数")
    private int customerNum;
}
