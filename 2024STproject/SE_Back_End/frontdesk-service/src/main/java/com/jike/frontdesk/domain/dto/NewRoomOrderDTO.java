package com.jike.frontdesk.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.time.LocalDateTime;


@Data
@ApiModel(description = "房间订单实体")
public class NewRoomOrderDTO {
    @ApiModelProperty("顾客id")
    private int customerId;
    @ApiModelProperty("员工id")
    private int employeeId;
    @ApiModelProperty("房间类型名称")
    private String typeName;
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
