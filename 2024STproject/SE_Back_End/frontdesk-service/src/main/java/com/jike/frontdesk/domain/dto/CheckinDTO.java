package com.jike.frontdesk.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@ApiModel(description = "新建入住信息实体")
public class CheckinDTO {
    @ApiModelProperty("顾客id")
    private int customerId;
    @ApiModelProperty("房间id")
    private int roomId;
    @ApiModelProperty("入住时间")
    private LocalDateTime checkinTime;
    @ApiModelProperty("退房时间")
    private LocalDateTime checkoutTime;
}
